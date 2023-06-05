using System;
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

using DbPokemonType = Diamond.API.Data.DbPokemonType;
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
		private const string SMOGON_POKEMON_GIFS_URL = "https://www.smogon.com/dex/media/sprites/xy/{0}.gif";
		/// <summary>
		/// {0}: Dex number
		/// </summary>
		private const string POKEMON_IMAGES_URL = "https://assets.pokemon.com/assets/cms2/img/pokedex/full/{0}.png";

		private static readonly Dictionary<string, DbPokemon> _pokemonsMap = new Dictionary<string, DbPokemon>();

		private static readonly Dictionary<PokemonType, string> _pokemonTypesEmojiMap = new Dictionary<PokemonType, string>()
		{
			{ PokemonType.Normal, "<:pokemon_type_normal1:1114518386383269920><:pokemon_type_normal2:1114518377654927452>" },
			{ PokemonType.Fire, "<:pokemon_type_fire1:1114518572857819166><:pokemon_type_fire2:1114518561503846451>" },
			{ PokemonType.Water, "<:pokemon_type_water1:1114518601731428352><:pokemon_type_water2:1114518590935285820>" },
			{ PokemonType.Grass, "<:pokemon_type_grass1:1114522123269050428><:pokemon_type_grass2:1114522114674921513>" },
			{ PokemonType.Electric, "<:pokemon_type_electric1:1114522151333150781><:pokemon_type_electric2:1114522141103230997>" },
			{ PokemonType.Ice, "<:pokemon_type_ice1:1114522188452741130><:pokemon_type_ice2:1114522178059259945>" },
			{ PokemonType.Fighting, "<:pokemon_type_fighting1:1114522209654952030><:pokemon_type_fighting2:1114522198640697396>" },
			{ PokemonType.Poison, "<:pokemon_type_poison1:1114522249194635316><:pokemon_type_poison2:1114522239145082971>" },
			{ PokemonType.Ground, "<:pokemon_type_ground1:1114522278584127518><:pokemon_type_ground2:1114522268668796979>" },
			{ PokemonType.Flying, "<:pokemon_type_flying1:1114522298884558929><:pokemon_type_flying2:1114522289862619246>" },
			{ PokemonType.Psychic, "<:pokemon_type_psychic1:1114522324415287347><:pokemon_type_psychic2:1114522312398602240>" },
			{ PokemonType.Bug, "<:pokemon_type_bug1:1114522346724798576><:pokemon_type_pokemon_type_bug2:1114522337484738653> " },
			{ PokemonType.Rock, "<:pokemon_type_rock1:1114522375724204123><:pokemon_type_rock2:1114522364827402272>" },
			{ PokemonType.Ghost, "<:pokemon_type_ghost1:1114522395571667024><:pokemon_type_ghost2:1114522386587463700>" },
			{ PokemonType.Dragon, "<:pokemon_type_dragon1:1114522415779815476><:pokemon_type_dragon2:1114522406682361896>" },
			{ PokemonType.Dark, "<:pokemon_type_dark1:1114522435061030953><:pokemon_type_dark2:1114522426622103562>" },
			{ PokemonType.Steel, "<:pokemon_type_steel1:1114522461002801343><:pokemon_type_steel2:1114522451322339449>" },
			{ PokemonType.Fairy, "<:pokemon_type_fairy1:1114522480619565067><:pokemon_type_fairy2:1114522471417258066>" },
		};

		private static readonly Dictionary<AttackType, string> _pokemonAttackTypesEmojiMap = new Dictionary<AttackType, string>
		{
			{ AttackType.Status, "<:pokemon_attack_type_status1:1114536566740766720><:pokemon_attack_type_status2:1114536555961393303>" },
			{ AttackType.Physical, "<:pokemon_attack_type_physical1:1114536599657644122><:pokemon_attack_type_physical2:1114536581399859281>" },
			{ AttackType.Special, "<:pokemon_attack_type_special1:1114536620817911847><:pokemon_attack_type_special2:1114536611389116546>" },
		};

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

			// Store types
			foreach (SmogonPokemonType type in data.types)
			{
				_ = db.PokemonTypes.Add(new DbPokemonType()
				{
					Name = type.name,
					Description = type.description,
					GenerationsList = string.Join(",", type.genfamily),
				});
			}
			await db.SaveAsync();

			// Store counters
			foreach (SmogonPokemonType type in data.types)
			{
				DbPokemonType attackerType = db.PokemonTypes.Where(t => t.Name == type.name).FirstOrDefault();
				foreach (object attackEffectiveness in type.atk_effectives)
				{
					JToken token = JToken.FromObject(attackEffectiveness);

					string targetTypeString = token.SelectToken("[0]").ToString();
					DbPokemonType targetType = db.PokemonTypes.Where(t => t.Name == targetTypeString).FirstOrDefault();

					float effectiveness = SConvert.ToSingle(token.SelectToken("[1]").ToString());

					_ = db.PokemonAttackEffectives.Add(new DbPokemonAttackEffectives()
					{
						AttackerType = attackerType,
						TargetType = targetType,
						Value = effectiveness,
					});
					await db.SaveAsync();
				}
			}

			// Store pokemons
			foreach (PokemonInfo pokemon in data.pokemon)
			{
				DbPokemon dbPokemon = new DbPokemon()
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
				};
				_ = db.Pokemons.Add(dbPokemon);

				// Create pokemons map
				_pokemonsMap.Add(pokemon.name, dbPokemon);
			}

			// Save to database
			await db.SaveAsync();
		}

		public DbPokemon SearchPokemon(string name)
		{
			List<SearchMatchInfo<DbPokemon>> result = DUtils.Search(_pokemonsMap, name);
			return result[0].Item;
		}

		public string GetTypeEmoji(string typeString)
		{
			PokemonType type = GetPokemonTypeByName(typeString);
			return _pokemonTypesEmojiMap[type];
		}

		public string GetPokemonGif(string name)
		{
			return string.Format(SMOGON_POKEMON_GIFS_URL, name.ToLower());
		}

		public string GetPokemonImage(int dexNumber)
		{
			return string.Format(POKEMON_IMAGES_URL, dexNumber);
		}

		public static PokemonType GetPokemonTypeByName(string type)
		{
			return (PokemonType)Enum.Parse(typeof(PokemonType), type);
		}
	}

	public enum PokemonType
	{
		Normal,
		Fire,
		Water,
		Grass,
		Electric,
		Ice,
		Fighting,
		Poison,
		Ground,
		Flying,
		Psychic,
		Bug,
		Rock,
		Ghost,
		Dragon,
		Dark,
		Steel,
		Fairy,
	}

	public enum AttackType
	{
		Status,
		Physical,
		Special,
	}
}
