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

namespace Diamond.API.APIs.Pokemon
{
	public class PokemonAPI : AutoUpdatingSearchableAPIManager<DbPokemon>
	{
		public PokemonAPI()
			: base(ConfigSetting.PokemonsListLoadUnix, PokemonAPIHelpers.KEEP_CACHE_FOR_SECONDS, new string[] {
				// Generated
				"DbPokemonDbPokemon", "DbPokemonDbPokemonFormat", "DbPokemonDbPokemonGeneration", "DbPokemonDbPokemonPassive", "DbPokemonDbPokemonType", "DbPokemonFormatDbPokemonGeneration", "DbPokemonGenerationDbPokemonItem", "DbPokemonGenerationDbPokemonMove", "DbPokemonGenerationDbPokemonNature", "DbPokemonGenerationDbPokemonType",
				// Tables
				"PokemonAbilities", "PokemonAttackEffectives", "PokemonFormats", "PokemonItems", "PokemonNatures", "PokemonPassives", "PokemonTypes", "Pokemons", "PokemonGenerations", "PokemonLearnsets"
			}, PokemonAPIHelpers.KEEP_CACHE_FOR_SECONDS)
		{ }

		public override Task<List<SearchMatchInfo<DbPokemon>>> SearchItemAsync(string search, Func<SearchMatchInfo<DbPokemon>, bool> filter = null)
		{
			// "Disable" this method
			throw new InvalidOperationException();
		}

		public async Task<List<SearchMatchInfo<DbPokemon>>> SearchItemAsync(string search, string generationAbbreviation)
		{
			return await base.SearchItemAsync(search, smi => smi.Item.GenerationAbbreviation == generationAbbreviation);
		}

		public override async Task<bool> LoadItemsLogicAsync(bool forceUpdate)
		{
			using DiamondContext db = new DiamondContext();

			bool isFirstDownloadIsValid = await this.LoadPokemons("SM", db);
			if (!isFirstDownloadIsValid)
			{
				return false;
			}

			foreach (DbPokemonGeneration dbGeneration in db.PokemonGenerations.ToList())
			{
				if (dbGeneration.Abbreviation == "SM") continue;

				bool isValid = await this.LoadPokemons(dbGeneration.Abbreviation, db);

				if (!isValid) return false;
			}

			return true;
		}

		private async Task<bool> LoadPokemons(string generationAbbreviation, DiamondContext db, int retries = 5)
		{
			Debug.WriteLine($"Loading pokémons for generation '{generationAbbreviation}'.");

			string json = await PokemonAPIHelpers.GetSmogonResponseJsonAsync(null, generationAbbreviation);
			if (json == null) return false;

			try
			{
				// TODO: Idk how to make this so yah

				SmogonRootObject resp = JsonConvert.DeserializeObject<SmogonRootObject>(json);
				List<SmogonGeneration> generationsList = JsonConvert.DeserializeObject<List<SmogonGeneration>>(resp.InjectRpcs[0][1].ToString());
				SmogonData data = JsonConvert.DeserializeObject<SmogonData>(resp.InjectRpcs[1][1].ToString());

				await SaveGenerations(generationsList, db);
				await SaveMoves(data.MovesList, db);
				await SaveAbilities(data.AbilitiesList, db);
				await SaveFormats(data.FormatsList, db);
				await SaveItems(data.ItemsList, db);
				SaveNatures(data.NaturesList, db);
				await SaveTypes(data.TypesList, db);
				await SaveCounters(data.TypesList, db);

				DbPokemonGeneration smGen = db.PokemonGenerations.Where(g => g.Abbreviation == generationAbbreviation).First();
				await SavePokemons(data.PokemonsList, smGen, db);

				// There is no need to save the database here
				return true;
			}
			catch
			{
				if (retries == 0) throw;

				// Retry
				return await this.LoadPokemons(generationAbbreviation, db, --retries);
			}
		}

		public override void LoadItemsMap(Dictionary<string, DbPokemon> itemsMap)
		{
			using DiamondContext db = new DiamondContext();

			foreach (DbPokemon pokemon in db.Pokemons)
			{
				itemsMap.Add(pokemon.Name, pokemon);
			}
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
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveMoves(List<SmogonMove> movesList, DiamondContext db)
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
					GenerationsList = PokemonAPIHelpers.GetGenerationsFromList(move.GenerationsList, db),
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveAbilities(List<SmogonAbility> abilitiesList, DiamondContext db)
		{
			// Store abilities
			foreach (SmogonAbility ability in abilitiesList)
			{
				_ = db.PokemonPassives.Add(new DbPokemonPassive()
				{
					Name = ability.Name,
					Description = ability.Description,
					IsNonstandard = ability.IsNonstandard != "Standard",
					GenerationsList = string.Join(",", ability.GenerationsList),
					PokemonsWithPassiveList = new List<DbPokemon>()
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveFormats(List<SmogonFormat> formatsList, DiamondContext db)
		{
			// Store formats
			foreach (SmogonFormat format in formatsList)
			{
				_ = db.PokemonFormats.Add(new DbPokemonFormat()
				{
					Name = format.Name,
					Abbreviation = format.Abbreviation,
					GenerationsList = PokemonAPIHelpers.GetGenerationsFromList(format.GenerationsList, db),
					PokemonsWithFormatList = new List<DbPokemon>(),
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveItems(List<SmogonItem> itemsList, DiamondContext db)
		{
			// Store items
			foreach (SmogonItem item in itemsList)
			{
				_ = db.PokemonItems.Add(new DbPokemonItem()
				{
					Name = item.Name,
					Description = item.Description,
					IsNonstandard = item.IsNonstandard != "Standard",
					GenerationsList = PokemonAPIHelpers.GetGenerationsFromList(item.GenerationsList, db),
				});
			}

			await db.SaveAsync();
		}

		private static void SaveNatures(List<SmogonNature> naturesList, DiamondContext db)
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
					GenerationsList = PokemonAPIHelpers.GetGenerationsFromList(nature.GenerationsList, db),
				});
			}
		}

		private static async Task SaveTypes(List<SmogonPokemonType> typesList, DiamondContext db)
		{
			// Store types
			foreach (SmogonPokemonType type in typesList)
			{
				_ = db.PokemonTypes.Add(new DbPokemonType()
				{
					Name = type.Name,
					Description = type.Description,
					GenerationsList = PokemonAPIHelpers.GetGenerationsFromList(type.GenerationsList, db),
					PokemonsWithTypeList = new List<DbPokemon>(),
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveCounters(List<SmogonPokemonType> typesList, DiamondContext db)
		{
			// Store counters (this needs to be after (Store types) because we need all types loaded before running this)
			foreach (SmogonPokemonType type in typesList)
			{
				DbPokemonType attackerType = db.PokemonTypes.Where(t => t.Name == type.Name).FirstOrDefault();
				foreach (object attackEffectiveness in type.AttackEffectivesList)
				{
					JToken token = JToken.FromObject(attackEffectiveness);

					string targetTypeString = token.SelectToken("[0]").ToString();
					DbPokemonType targetType = db.PokemonTypes.Where(t => t.Name == targetTypeString).FirstOrDefault();

					float effectiveness = Convert.ToSingle(token.SelectToken("[1]").ToString());

					_ = db.PokemonAttackEffectivenesses.Add(new DbPokemonAttackEffectiveness()
					{
						AttackerType = attackerType,
						TargetType = targetType,
						Value = effectiveness,
					});
				}
			}

			await db.SaveAsync();
		}

		private static async Task SavePokemons(List<SmogonPokemonInfo> pokemonsList, DbPokemonGeneration generation, DiamondContext db)
		{
			// Store pokemons
			foreach (SmogonPokemonInfo pokemon in pokemonsList)
			{
				DbPokemon newPokemon = await db.CreatePokemonAsync(pokemon.Name, generation.Abbreviation, pokemon.IsNonstandard,
					pokemon.HealthPoints, pokemon.Attack, pokemon.Defense, pokemon.SpecialAttack, pokemon.SpecialDefense, pokemon.Speed,
					pokemon.Weight, pokemon.Height, pokemon.Oob?.DexNumber,
					db, false);

				foreach (string type in pokemon.TypesList)
				{
					DbPokemonType pokemonType = db.PokemonTypes.Where(t => t.Name == type).First();
					newPokemon.TypesList.Add(pokemonType);
				}

				foreach (string ability in pokemon.AbilitiesList)
				{
					DbPokemonPassive pokemonAbility = db.PokemonPassives.Where(p => p.Name == ability).First();
					newPokemon.AbilitiesList.Add(pokemonAbility);
				}

				foreach (string format in pokemon.FormatsList)
				{
					DbPokemonFormat pokemonFormat = db.PokemonFormats.Where(f => f.Abbreviation == format).First();
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
					DbPokemon dbPokemon = db.SelectPokemon(smogonPokemon.Name, generation, db);
					foreach (string evolution in smogonPokemon.Oob.EvolutionsList)
					{
						DbPokemon evolutionPokemon = db.SelectPokemon(evolution, generation, db);
						dbPokemon.EvolutionsList.Add(evolutionPokemon);
					}
				}

				// Save alts
				if (smogonPokemon.Oob.AltsList != null)
				{
					DbPokemon dbPokemon = db.SelectPokemon(smogonPokemon.Name, generation, db);
					foreach (string alt in smogonPokemon.Oob.AltsList)
					{
						DbPokemon altPokemon = db.SelectPokemon(alt, generation, db);
						dbPokemon.AltsList.Add(altPokemon);
					}
				}
			}
			await db.SaveAsync();
		}
		#endregion
	}
}
