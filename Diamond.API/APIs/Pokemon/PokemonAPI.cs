using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Helpers.APIManager;
using Diamond.API.Schemes.Smogon;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Enums;
using Diamond.Data.Models.Pokemons;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ScriptsLibV2.Extensions;

namespace Diamond.API.APIs.Pokemon
{
	public class PokemonAPI : AutoUpdatingSearchableAPIManager<DbPokemon>
	{
		/// <summary>
		/// This is the first generation to be downloaded and the default one if none is provided.
		/// </summary>
		public const string POKEMON_DEFAULT_GENERATION = "SM";
		/// <summary>
		/// {0}: Generation Abbreviation
		/// {1}: Pokémon name
		/// </summary>
		public const string SMOGON_POKEMON_GIFS_URL = "https://www.smogon.com/dex/media/sprites/{0}/{1}.gif";
		/// <summary>
		/// {0}: Dex number
		/// </summary>
		public const string POKEMON_IMAGES_URL = "https://assets.pokemon.com/assets/cms2/img/pokedex/full/{0}.png";
		/// <summary>
		/// {0}: Generation abbreviation
		/// {1}: Pokémon name
		/// </summary>
		public const string SMOGON_POKEMON_URL = "https://www.smogon.com/dex/{0}/pokemon/{1}/";
		/// <summary>
		/// {0}: Dex number
		/// </summary>
		public const string POKEMON_POKEDEX_URL = "https://www.pokemon.com/us/pokedex/{0}";
		/// <summary>
		/// {0}: Generation abbreviation
		/// </summary>
		public const string SMOGON_ENDPOINT = "https://www.smogon.com/dex/{0}/pokemon/";
		/// <summary>
		/// {0}: Generation abbreviation
		/// </summary>
		public const string SMOGON_GENERATION_URL = "https://www.smogon.com/dex/{0}/pokemon/";
		/// <summary>
		/// {0}: Generation abbreviation
		/// {1}: Format abbreviation
		/// </summary>
		public const string SMOGON_FORMAT_URL = "https://www.smogon.com/dex/{0}/formats/{1}/";
		/// <summary>
		/// {0}: User ID
		/// </summary>
		public const string SMOGON_USER_PROFILE_URL = "https://www.smogon.com/forums/members/{0}/";

		public PokemonAPI()
			: base(ConfigSetting.PokemonsListLoadUnix, PokemonUtils.KEEP_CACHE_FOR_SECONDS, new string[] {
				// Generated
				"DbPokemonDbPokemon", "DbPokemonDbPokemonFormat", "DbPokemonDbPokemonGeneration", "DbPokemonDbPokemonPassive", "DbPokemonDbPokemonType", "DbPokemonFormatDbPokemonGeneration", "DbPokemonGenerationDbPokemonItem", "DbPokemonGenerationDbPokemonMove", "DbPokemonGenerationDbPokemonNature", "DbPokemonGenerationDbPokemonPassive", "DbPokemonGenerationDbPokemonType",
				// Tables
				"PokemonAbilities", "PokemonAttackEffectives", "PokemonFormats", "PokemonItems", "PokemonNatures", "PokemonPassives", "PokemonTypes", "Pokemons", "PokemonGenerations", "PokemonLearnsets"
			}, PokemonUtils.KEEP_CACHE_FOR_SECONDS)
		{ }

		public override async Task<bool> LoadItemsLogicAsync(bool forceUpdate)
		{
			using DiamondContext db = new DiamondContext();

			bool isFirstDownloadValid = await this.LoadPokemons(POKEMON_DEFAULT_GENERATION, db);
			if (!isFirstDownloadValid)
			{
				return false;
			}

			foreach (DbPokemonGeneration dbGeneration in db.PokemonGenerations.ToList())
			{
				if (dbGeneration.Abbreviation == POKEMON_DEFAULT_GENERATION) continue;

				bool isValid = await this.LoadPokemons(dbGeneration.Abbreviation, db);

				if (!isValid) return false;
			}

			return true;
		}

		private async Task<bool> LoadPokemons(string generationAbbreviation, DiamondContext db)
		{
			Debug.WriteLine($"Loading pokémons for generation '{generationAbbreviation}'");

			string json = await PokemonUtils.GetSmogonResponseJsonAsync(null, generationAbbreviation);
			if (json == null) return false;

			// TODO: Idk how to make this so yah
			SmogonRootObject resp = JsonConvert.DeserializeObject<SmogonRootObject>(json);
			List<SmogonGeneration> generationsList = JsonConvert.DeserializeObject<List<SmogonGeneration>>(resp.InjectRpcs[0][1].ToString());
			SmogonData data = JsonConvert.DeserializeObject<SmogonData>(resp.InjectRpcs[1][1].ToString());

			if (generationAbbreviation == POKEMON_DEFAULT_GENERATION) await SaveGenerations(generationsList, db);

			await SaveMovesAsync(data.MovesList, generationAbbreviation, db);
			await SaveAbilitiesAsync(data.AbilitiesList, generationAbbreviation, db);
			await SaveFormatsAsync(data.FormatsList, generationAbbreviation, db);
			await SaveItemsAsync(data.ItemsList, generationAbbreviation, db);
			await SaveNaturesAsync(data.NaturesList, generationAbbreviation, db);
			await SaveTypesAsync(data.TypesList, generationAbbreviation, db);
			await SaveCountersAsync(data.TypesList, generationAbbreviation, db);

			DbPokemonGeneration smGen = db.PokemonGenerations.Where(g => g.Abbreviation == generationAbbreviation).First();
			await SavePokemonsAsync(data.PokemonsList, smGen, db);

			// There is no need to save to the database here
			return true;
		}

		public override void LoadItemsMap(Dictionary<string, DbPokemon> itemsMap)
		{
			using DiamondContext db = new DiamondContext();

			foreach (DbPokemon pokemon in db.Pokemons.AsEnumerable().DistinctBy(p => p.Name))
			{
				itemsMap.Add(pokemon.Name, pokemon);
			}
		}

		public async Task<List<SearchMatchInfo<DbPokemon>>> SearchPokemonAsync(string pokemonName, string? generationAbbreviation)
		{
			List<SearchMatchInfo<DbPokemon>> results = await this.SearchItemAsync(pokemonName);

			if (!generationAbbreviation.IsEmpty())
			{
				_ = results.RemoveAll(smi => smi.Item.GenerationAbbreviation != generationAbbreviation);
			}

			return results;
		}

		#region Saving
		private static async Task SaveGenerations(List<SmogonGeneration> generationsList, DiamondContext db)
		{
			// Store generations
			foreach (SmogonGeneration generation in generationsList)
			{
				_ = db.PokemonGenerations.Add(new DbPokemonGeneration()
				{
					Name = generation.Name,
					Abbreviation = generation.Shorthand,
					PokemonsWithGenerationList = new List<DbPokemon>(),
					FormatsWithGenerationList = new List<DbPokemonFormat>(),
					ItemsWithGenerationList = new List<DbPokemonItem>(),
					MovesWithGenerationList = new List<DbPokemonMove>(),
					NaturesWithGenerationList = new List<DbPokemonNature>(),
					TypesWithGenerationList = new List<DbPokemonType>(),
					PassivesWithGenerationList = new List<DbPokemonPassive>(),
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveMovesAsync(List<SmogonMove> movesList, string generationAbbreviation, DiamondContext db)
		{
			// Store moves
			foreach (SmogonMove move in movesList)
			{
				_ = db.PokemonMoves.Add(new DbPokemonMove()
				{
					Name = move.Name,
					Description = move.Description,
					Type = move.Type,
					Category = move.Category,
					Power = move.Power,
					Accuracy = move.Accuracy,
					Priority = move.Priority,
					PowerPoints = move.PowerPoints,
					GenerationsList = PokemonUtils.GetGenerationsFromList(move.GenerationsList, db),
					GenerationAbbreviation = generationAbbreviation
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveAbilitiesAsync(List<SmogonAbility> abilitiesList, string generationAbbreviation, DiamondContext db)
		{
			// Store abilities
			foreach (SmogonAbility ability in abilitiesList)
			{
				_ = db.PokemonPassives.Add(new DbPokemonPassive()
				{
					Name = ability.Name,
					Description = ability.Description,
					IsNonstandard = ability.IsNonstandard != "Standard",
					GenerationsList = PokemonUtils.GetGenerationsFromList(ability.GenerationsList, db),
					PokemonsWithPassiveList = new List<DbPokemon>(),
					GenerationAbbreviation = generationAbbreviation,
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveFormatsAsync(List<SmogonFormat> formatsList, string generationAbbreviation, DiamondContext db)
		{
			// Store formats
			foreach (SmogonFormat format in formatsList)
			{
				_ = db.PokemonFormats.Add(new DbPokemonFormat()
				{
					Name = format.Name,
					Abbreviation = format.Abbreviation,
					GenerationsList = PokemonUtils.GetGenerationsFromList(format.GenerationsList, db),
					PokemonsWithFormatList = new List<DbPokemon>(),
					GenerationAbbreviation = generationAbbreviation,
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveItemsAsync(List<SmogonItem> itemsList, string generationAbbreviation, DiamondContext db)
		{
			// Store items
			foreach (SmogonItem item in itemsList)
			{
				_ = db.PokemonItems.Add(new DbPokemonItem()
				{
					Name = item.Name,
					Description = item.Description,
					IsNonstandard = item.IsNonstandard != "Standard",
					GenerationsList = PokemonUtils.GetGenerationsFromList(item.GenerationsList, db),
					GenerationAbbreviation = generationAbbreviation,
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveNaturesAsync(List<SmogonNature> naturesList, string generationAbbreviation, DiamondContext db)
		{
			// Store natures
			foreach (SmogonNature nature in naturesList)
			{
				_ = db.PokemonNatures.Add(new DbPokemonNature()
				{
					Name = nature.Name,
					Summary = nature.Summary,
					HealthPoints = nature.HealthPoints,
					Attack = nature.Attack,
					Defense = nature.Defense,
					SpecialAttack = nature.SpecialAttack,
					SpecialDefense = nature.SpecialDefense,
					Speed = nature.Speed,
					GenerationsList = PokemonUtils.GetGenerationsFromList(nature.GenerationsList, db),
					GenerationAbbreviation = generationAbbreviation,
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveTypesAsync(List<SmogonPokemonType> typesList, string generationAbbreviation, DiamondContext db)
		{
			// Store types
			foreach (SmogonPokemonType type in typesList)
			{
				_ = db.PokemonTypes.Add(new DbPokemonType()
				{
					Name = type.Name,
					Description = type.Description,
					GenerationsList = PokemonUtils.GetGenerationsFromList(type.GenerationsList, db),
					PokemonsWithTypeList = new List<DbPokemon>(),
					GenerationAbbreviation = generationAbbreviation,
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveCountersAsync(List<SmogonPokemonType> typesList, string generationAbbreviation, DiamondContext db)
		{
			// Store counters (this needs to be after (Store types) because we need all types loaded before running this)
			foreach (SmogonPokemonType type in typesList)
			{
				DbPokemonType attackerType = db.PokemonTypes.Where(t => t.Name == type.Name && t.GenerationAbbreviation == generationAbbreviation).FirstOrDefault();
				foreach (object attackEffectiveness in type.AttackEffectivesList)
				{
					JToken token = JToken.FromObject(attackEffectiveness);

					string targetTypeString = token.SelectToken("[0]").ToString();
					DbPokemonType targetType = db.PokemonTypes.Where(t => t.Name == targetTypeString && t.GenerationAbbreviation == generationAbbreviation).FirstOrDefault();

					float effectiveness = Convert.ToSingle(token.SelectToken("[1]").ToString());

					_ = db.PokemonAttackEffectivenesses.Add(new DbPokemonAttackEffectiveness()
					{
						AttackerType = attackerType,
						TargetType = targetType,
						Value = effectiveness,
						GenerationAbbreviation = generationAbbreviation,
					});
				}
			}

			await db.SaveAsync();
		}

		private static async Task SavePokemonsAsync(List<SmogonPokemonInfo> pokemonsList, DbPokemonGeneration generation, DiamondContext db)
		{
			// Store pokemons
			foreach (SmogonPokemonInfo pokemon in pokemonsList)
			{
				DbPokemon newPokemon = await db.CreatePokemonAsync(pokemon.Name, generation.Abbreviation, pokemon.IsNonstandard,
					pokemon.HealthPoints, pokemon.Attack, pokemon.Defense, pokemon.SpecialAttack, pokemon.SpecialDefense, pokemon.Speed,
					pokemon.Weight, pokemon.Height, pokemon.Oob?.DexNumber,
					 false);

				foreach (string type in pokemon.TypesList)
				{
					DbPokemonType pokemonType = db.PokemonTypes.Where(t => t.Name == type && t.GenerationAbbreviation == generation.Abbreviation).First();
					newPokemon.TypesList.Add(pokemonType);
				}

				foreach (string ability in pokemon.AbilitiesList)
				{
					DbPokemonPassive pokemonAbility = db.PokemonPassives.Where(p => p.Name == ability && p.GenerationAbbreviation == generation.Abbreviation).First();
					newPokemon.AbilitiesList.Add(pokemonAbility);
				}

				foreach (string format in pokemon.FormatsList)
				{
					// Pokemon format can be full name of abbreviation
					DbPokemonFormat pokemonFormat = db.PokemonFormats.Where(f => f.Abbreviation == format || (f.Name == format && f.GenerationAbbreviation == generation.Abbreviation)).First();
					newPokemon.FormatsList.Add(pokemonFormat);
				}

				if (pokemon.Oob != null)
				{
					foreach (string genString in pokemon.Oob.GenerationsList)
					{
						DbPokemonGeneration pokemonGeneration = db.PokemonGenerations.Where(g => g.Abbreviation == genString).First();
						newPokemon.GenerationsList.Add(pokemonGeneration);
					}
				}

				_ = db.Pokemons.Add(newPokemon);
			}
			await db.SaveAsync();

			// Fill EvolutionsList & AltsList (TODO: can be improved (create pokémons based on evolutions))
			foreach (SmogonPokemonInfo smogonPokemon in pokemonsList)
			{
				// Idk if they have evolutions, but I'll just ignore all "alts"
				if (smogonPokemon.Oob == null) continue;

				// Save evolutions
				if (smogonPokemon.Oob.EvolutionsList != null)
				{
					DbPokemon dbPokemon = db.SelectPokemon(smogonPokemon.Name, generation);
					foreach (string evolution in smogonPokemon.Oob.EvolutionsList)
					{
						DbPokemon evolutionPokemon = db.SelectPokemon(evolution, generation);
						dbPokemon.EvolutionsList.Add(evolutionPokemon);
					}
				}

				// Save alts
				if (smogonPokemon.Oob.AltsList != null)
				{
					DbPokemon dbPokemon = db.SelectPokemon(smogonPokemon.Name, generation);
					foreach (string alt in smogonPokemon.Oob.AltsList)
					{
						DbPokemon altPokemon = db.SelectPokemon(alt, generation);
						dbPokemon.AltsList.Add(altPokemon);
					}
				}
			}
			await db.SaveAsync();
		}
		#endregion
	}
}
