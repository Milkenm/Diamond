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
			: base(ConfigSetting.PokemonsListLoadUnix, PokemonAPIHelpers.KEEP_CACHE_FOR_SECONDS, new string[] { "PokemonAbilities", "PokemonAttackEffectives", "PokemonFormats", "PokemonItems", "PokemonNatures", "PokemonPassives", "PokemonTypes", "Pokemons", "PokemonGenerations" }, PokemonAPIHelpers.KEEP_CACHE_FOR_SECONDS)
		{ }

		public override async Task<bool> LoadItemsLogicAsync(bool forceUpdate)
		{
			using DiamondContext db = new DiamondContext();

			string json = await PokemonAPIHelpers.GetSmogonResponseJsonAsync(null);
			if (json == null) return false;

			// Idk how to make this so yah
			SmogonRootObject resp = JsonConvert.DeserializeObject<SmogonRootObject>(json);
			List<SmogonGeneration> generationsList = JsonConvert.DeserializeObject<List<SmogonGeneration>>(resp.InjectRpcs[0][1].ToString());
			SmogonData data = JsonConvert.DeserializeObject<SmogonData>(resp.InjectRpcs[1][1].ToString());

			// Store generations
			foreach (SmogonGeneration generation in generationsList)
			{
				_ = db.PokemonGenerations.Add(new DbPokemonGenerations()
				{
					Name = generation.Name,
					Abbreviation = generation.Shorthand,
				});
			}

			// Store moves
			foreach (SmogonMove move in data.MovesList)
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

			// Store abilities
			foreach (SmogonAbility ability in data.AbilitiesList)
			{
				_ = db.PokemonPassives.Add(new DbPokemonPassive()
				{
					Name = ability.Name,
					Description = ability.Description,
					IsNonstandard = ability.IsNonstandard != "Standard",
					GenerationsList = string.Join(",", ability.GenerationsList),
				});
			}

			// Store formats
			foreach (SmogonFormat format in data.FormatsList)
			{
				_ = db.PokemonFormats.Add(new DbPokemonFormat()
				{
					Name = format.Name,
					Abbreviation = format.Abbreviation,
					GenerationsList = string.Join(",", format.GenerationsList),
				});
			}

			// Store items
			foreach (SmogonItem item in data.ItemsList)
			{
				_ = db.PokemonItems.Add(new DbPokemonItem()
				{
					Name = item.Name,
					Description = item.Description,
					IsNonstandard = item.IsNonstandard != "Standard",
					GenerationsList = string.Join(",", item.GenerationsList),
				});
			}

			// Store natures
			foreach (SmogonNature nature in data.NaturesList)
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

			// Store types
			foreach (SmogonPokemonType type in data.TypesList)
			{
				_ = db.PokemonTypes.Add(new DbPokemonType()
				{
					Name = type.Name,
					Description = type.Description,
					GenerationsList = string.Join(",", type.GenerationsList),
				});
			}

			// We need to save here because counters gets data from the database
			await db.SaveAsync();

			// Store counters (this needs to be after (Store types) because we need all types loaded before running this)
			foreach (SmogonPokemonType type in data.TypesList)
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
					await db.SaveAsync();
				}
			}

			// Store pokemons
			foreach (SmogonPokemonInfo pokemon in data.PokemonsList)
			{
				DbPokemon dbPokemon = new DbPokemon()
				{
					Name = pokemon.Name,
					TypesList = string.Join(",", pokemon.TypesList),
					AbilitiesList = string.Join(",", pokemon.AbilitiesList),
					FormatsList = string.Join(",", pokemon.FormatsList),
					IsNonstandard = pokemon.IsNonstandard != "Standard",
					HealthPoints = pokemon.HealthPoints,
					Attack = pokemon.Attack,
					Defense = pokemon.Defense,
					SpecialAttack = pokemon.SpecialAttack,
					SpecialDefense = pokemon.SpecialDefense,
					Speed = pokemon.Speed,
					Weight = pokemon.Weight,
					Height = pokemon.Height,
					DexNumber = pokemon.DexNumber,
					EvolutionsList = pokemon.Oob?.EvolutionsList != null ? string.Join(",", pokemon.Oob.EvolutionsList) : string.Empty,
					GenerationsList = pokemon.Oob?.GenerationsList != null ? string.Join(",", pokemon.Oob.GenerationsList) : string.Empty,
				};
				_ = db.Pokemons.Add(dbPokemon);
			}

			// Save to database
			await db.SaveAsync();

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

		public async Task<string[]> GetPokemonMovesAsync(string pokemonName)
		{
			string json = await PokemonAPIHelpers.GetSmogonResponseJsonAsync(pokemonName);

			// Idk how to make this so yah
			SmogonRootObject resp = JsonConvert.DeserializeObject<SmogonRootObject>(json);
			SmogonPokemonStrats strats = JsonConvert.DeserializeObject<SmogonPokemonStrats>(resp.InjectRpcs[2][1].ToString());

			return strats.Learnset;
		}
	}
}
