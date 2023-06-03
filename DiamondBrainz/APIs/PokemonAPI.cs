using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Data;
using Diamond.API.Schemes.Smogon;
using Diamond.API.Util;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ScriptsLibV2.Util;

using DUtils = Diamond.API.Util.Utils;
using SConvert = System.Convert;

namespace Diamond.API.APIs
{
	public class PokemonAPI
	{
		private const string SMOGON_ENDPOINT = "https://www.smogon.com/dex/sm/pokemon/";
		/// <summary>
		/// {0}: Pokemon name
		/// </summary>
		private const string SMOGON_POKEMON_IMAGES_URL = "https://www.smogon.com/dex/media/sprites/xy/{0}.gif";

		private static readonly Dictionary<string, PokemonInfo> _pokemonsMap = new Dictionary<string, PokemonInfo>();

		public async Task LoadPokemonsAsync()
		{
			using DiamondContext db = new DiamondContext();

			// Clear database
			db.ClearTable("PokemonAbilities");
			db.ClearTable("PokemonAttackEffectives");
			db.ClearTable("PokemonFormats");
			db.ClearTable("PokemonItems");
			db.ClearTable("PokemonNatures");
			db.ClearTable("PokemonPassives");
			db.ClearTable("PokemonTypes");
			db.ClearTable("Pokemons");

			string response = await RequestUtils.GetAsync(SMOGON_ENDPOINT);

			string json = null;
			foreach (string line in response.Split('\n'))
			{
				if (line.Trim().StartsWith("dexSettings"))
				{
					json = line.Split(" = ")[1]
						.Replace(@"""[\""dump-gens\""]"",", "")
						.Replace(@"""[\""dump-basics\"",{\""gen\"":\""sm\""}]"",", "");
				}
			}

			if (json == null) return;

			// Idk how to make this so yah
			SmogonRootObject resp = JsonConvert.DeserializeObject<SmogonRootObject>(json);
			SmogonData data = JsonConvert.DeserializeObject<SmogonData>(resp.InjectRpcs[1][0].ToString());

			// Store moves
			foreach (Move move in data.moves)
			{
				Debug.WriteLine("saving move: " + move.name);
				_ = db.PokemonAbilities.Add(new PokemonAbility()
				{
					Name = move.name,
					Description = move.description,
					Type = move.type,
					Category = move.category,
					Power = move.power,
					Accuracy = move.accuracy,
					Priority = move.priority,
					PowerPoints = move.pp,
					GenerationsList = string.Join(",", move.genfamily)
				});
			}

			// Store abilities
			foreach (Ability ability in data.abilities)
			{
				_ = db.PokemonPassives.Add(new PokemonPassive()
				{
					Name = ability.name,
					Description = ability.description,
					IsNonstandard = ability.isNonstandard != "Standard",
					GenerationsList = string.Join(",", ability.genfamily),
				});
			}

			// Store formats
			foreach (Format format in data.formats)
			{
				_ = db.PokemonFormats.Add(new PokemonFormat()
				{
					Name = format.name,
					Abbreviation = format.shorthand,
					GenerationsList = string.Join(",", format.genfamily),
				});
			}

			// Store items
			foreach (Item item in data.items)
			{
				_ = db.PokemonItems.Add(new PokemonItem()
				{
					Name = item.name,
					Description = item.description,
					IsNonstandard = item.isNonstandard != "Standard",
					GenerationsList = string.Join(",", item.genfamily),
				});
			}

			// Store natures
			foreach (Nature nature in data.natures)
			{
				_ = db.PokemonNatures.Add(new PokemonNature()
				{
					Name = nature.name,
					Summary = nature.summary,
					HealthPoints = nature.hp,
					Attack = nature.atk,
					Defense = nature.def,
					SpecialAttack = nature.spa,
					SpecialDefense = nature.spd,
					Speed = nature.spe,
					GenerationsList = string.Join(",", nature.genfamily),
				});
			}

			// Store types and counters
			foreach (Type type in data.types)
			{
				_ = db.PokemonTypes.Add(new PokemonType()
				{
					Name = type.name,
					Description = type.description,
					GenerationsList = string.Join(",", type.genfamily),
				});
			}
			await db.SaveAsync();
			foreach (Type type in data.types)
			{
				PokemonType attackerType = db.PokemonTypes.Where(t => t.Name == type.name).FirstOrDefault();
				foreach (object attackEffectiveness in type.atk_effectives)
				{
					JToken token = JToken.FromObject(attackEffectiveness);

					string targetTypeString = token.SelectToken("[0]").ToString();
					PokemonType targetType = db.PokemonTypes.Where(t => t.Name == targetTypeString).FirstOrDefault();

					float effectiveness = SConvert.ToSingle(token.SelectToken("[1]").ToString());

					_ = db.PokemonAttackEffectives.Add(new PokemonAttackEffectives()
					{
						AttackerType = attackerType,
						TargetType = targetType,
						Value = effectiveness,
					});
				}
			}

			// Store pokemons
			foreach (PokemonInfo pokemon in data.pokemon)
			{
				_ = db.Pokemons.Add(new Pokemon()
				{
					Name = pokemon.name,
					TypesList = string.Join(",", pokemon.types),
					AbilitiesList = string.Join(",", pokemon.abilities),
					FormatsList = string.Join(",", pokemon.formats),
					IsNonstandard = pokemon.isNonstandard != "Standard",
					HealthPoints = pokemon.hp,
					Attack = pokemon.atk,
					Defense = pokemon.def,
					SpecialAttack = pokemon.spa,
					SpecialDefense = pokemon.spd,
					Speed = pokemon.spe,
					Weight = pokemon.weight,
					Height = pokemon.height,
					DexNumber = pokemon.oob?.dex_number,
					EvolutionsList = pokemon.oob != null ? string.Join(",", pokemon.oob.evos) : null,
					GenerationsList = pokemon.oob != null ? string.Join(",", pokemon.oob.genfamily) : null,
				});

				// Create pokemons map
				_pokemonsMap.Add(pokemon.name, pokemon);
			}

			// Save to database
			await db.SaveAsync();
		}

		public PokemonInfo SearchPokemon(string name)
		{
			List<SearchMatchInfo<PokemonInfo>> result = DUtils.Search(_pokemonsMap, name);
			return result[0].Item;
		}

		public string GetPokemonImage(string name)
		{
			return string.Format(SMOGON_POKEMON_IMAGES_URL, name);
		}
	}
}
