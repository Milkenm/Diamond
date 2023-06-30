using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Helpers.APIManager;
using Diamond.API.Schemes.Smogon;
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
			: base(ConfigSetting.PokemonsListLoadUnix, PokemonAPIHelpers.KEEP_CACHE_FOR_SECONDS, new string[] { "DbPokemonDbPokemon", "PokemonAbilities", "PokemonAttackEffectives", "PokemonFormats", "PokemonItems", "PokemonNatures", "PokemonPassives", "PokemonTypes", "Pokemons", "PokemonGenerations", "PokemonLearnsets" }, PokemonAPIHelpers.KEEP_CACHE_FOR_SECONDS)
		{ }

		public override async Task<bool> LoadItemsLogicAsync(bool forceUpdate)
		{
			using DiamondContext db = new DiamondContext();

			string json = await PokemonAPIHelpers.GetSmogonResponseJsonAsync(null);
			if (json == null) return false;

			// TODO: Idk how to make this so yah
			SmogonRootObject resp = JsonConvert.DeserializeObject<SmogonRootObject>(json);
			List<SmogonGeneration> generationsList = JsonConvert.DeserializeObject<List<SmogonGeneration>>(resp.InjectRpcs[0][1].ToString());
			SmogonData data = JsonConvert.DeserializeObject<SmogonData>(resp.InjectRpcs[1][1].ToString());

			await SaveGenerations(generationsList);
			await SaveMoves(data.MovesList);
			await SaveAbilities(data.AbilitiesList);
			await SaveFormats(data.FormatsList);
			await SaveItems(data.ItemsList);
			await SaveNatures(data.NaturesList);
			await SaveTypes(data.TypesList);
			await SaveCounters(data.TypesList);
			await SavePokemons(data.PokemonsList);

			return true;
		}

		public override void LoadItemsMap(Dictionary<string, DbPokemon> itemsMap)
		{
			using DiamondContext db = new DiamondContext();

			foreach (DbPokemon pokemon in db.Pokemons)
			{
				itemsMap.Add(pokemon.Name, pokemon);
			}
		}

		private static async Task SaveGenerations(List<SmogonGeneration> generationsList)
		{
			using DiamondContext db = new DiamondContext();

			// Store generations
			foreach (SmogonGeneration generation in generationsList)
			{
				_ = db.PokemonGenerations.Add(new DbPokemonGeneration()
				{
					Name = generation.Name,
					Abbreviation = generation.Shorthand,
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveMoves(List<SmogonMove> movesList)
		{
			using DiamondContext db = new DiamondContext();

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
					GenerationsList = string.Join(",", move.GenerationsList)
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveAbilities(List<SmogonAbility> abilitiesList)
		{
			using DiamondContext db = new DiamondContext();

			// Store abilities
			foreach (SmogonAbility ability in abilitiesList)
			{
				_ = db.PokemonPassives.Add(new DbPokemonPassive()
				{
					Name = ability.Name,
					Description = ability.Description,
					IsNonstandard = ability.IsNonstandard != "Standard",
					GenerationsList = string.Join(",", ability.GenerationsList),
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveFormats(List<SmogonFormat> formatsList)
		{
			using DiamondContext db = new DiamondContext();

			// Store formats
			foreach (SmogonFormat format in formatsList)
			{
				_ = db.PokemonFormats.Add(new DbPokemonFormat()
				{
					Name = format.Name,
					Abbreviation = format.Abbreviation,
					GenerationsList = string.Join(",", format.GenerationsList),
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveItems(List<SmogonItem> itemsList)
		{
			using DiamondContext db = new DiamondContext();

			// Store items
			foreach (SmogonItem item in itemsList)
			{
				_ = db.PokemonItems.Add(new DbPokemonItem()
				{
					Name = item.Name,
					Description = item.Description,
					IsNonstandard = item.IsNonstandard != "Standard",
					GenerationsList = string.Join(",", item.GenerationsList),
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveNatures(List<SmogonNature> naturesList)
		{
			using DiamondContext db = new DiamondContext();

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
					GenerationsList = string.Join(",", nature.GenerationsList),
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveTypes(List<SmogonPokemonType> typesList)
		{
			using DiamondContext db = new DiamondContext();

			// Store types
			foreach (SmogonPokemonType type in typesList)
			{
				_ = db.PokemonTypes.Add(new DbPokemonType()
				{
					Name = type.Name,
					Description = type.Description,
					GenerationsList = string.Join(",", type.GenerationsList),
				});
			}

			await db.SaveAsync();
		}

		private static async Task SaveCounters(List<SmogonPokemonType> typesList)
		{
			using DiamondContext db = new DiamondContext();

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

		private static async Task SavePokemons(List<SmogonPokemonInfo> pokemonsList)
		{
			using DiamondContext db = new DiamondContext();

			// Store pokemons
			foreach (SmogonPokemonInfo pokemon in pokemonsList)
			{
				DbPokemon newPokemon = new DbPokemon()
				{
					Name = pokemon.Name,
					TypesList = new List<DbPokemonType>(),
					AbilitiesList = new List<DbPokemonPassive>(),
					FormatsList = new List<DbPokemonFormat>(),
					IsNonstandard = pokemon.IsNonstandard != "Standard",
					HealthPoints = pokemon.HealthPoints,
					Attack = pokemon.Attack,
					Defense = pokemon.Defense,
					SpecialAttack = pokemon.SpecialAttack,
					SpecialDefense = pokemon.SpecialDefense,
					Speed = pokemon.Speed,
					Weight = pokemon.Weight,
					Height = pokemon.Height,
					DexNumber = pokemon.Oob?.DexNumber,
					AltsList = new List<DbPokemon>(), // AltsList is filled later
					EvolutionsList = new List<DbPokemon>(), //EvolutionsList is filled later
					GenerationsList = new List<DbPokemonGeneration>(),
				};

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
					foreach (string generation in pokemon.Oob.GenerationsList)
					{
						DbPokemonGeneration pokemonGeneration = db.PokemonGenerations.Where(g => g.Abbreviation == generation).First();
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
					DbPokemon dbPokemon = db.Pokemons.Where(p => p.Name == smogonPokemon.Name).First();
					foreach (string evolution in smogonPokemon.Oob.EvolutionsList)
					{
						DbPokemon evolutionPokemon = db.Pokemons.Where(p => p.Name == evolution).First();
						dbPokemon.EvolutionsList.Add(evolutionPokemon);
					}
				}
				// Save alts
				if (smogonPokemon.Oob.AltsList != null)
				{
					DbPokemon dbPokemon = db.Pokemons.Where(p => p.Name == smogonPokemon.Name).First();
					foreach (string alt in smogonPokemon.Oob.AltsList)
					{
						DbPokemon altPokemon = db.Pokemons.Where(p => p.Name == alt).First();
						dbPokemon.AltsList.Add(altPokemon);
					}
				}
			}
			await db.SaveAsync();
		}
	}
}
