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

using ScriptsLibV2.Util;

using SConvert = System.Convert;

namespace Diamond.API.APIs
{
	public class PokemonAPI : AutoUpdatingSearchableAPIManager<DbPokemon>
	{
		private const string SMOGON_ENDPOINT = "https://www.smogon.com/dex/sm/pokemon/";
		/// <summary>
		/// {0}: Pokémon name
		/// </summary>
		private const string SMOGON_POKEMON_GIFS_URL = "https://www.smogon.com/dex/media/sprites/xy/{0}.gif";
		/// <summary>
		/// {0}: Dex number
		/// </summary>
		private const string POKEMON_IMAGES_URL = "https://assets.pokemon.com/assets/cms2/img/pokedex/full/{0}.png";
		/// <summary>
		/// {0}: Pokémon name
		/// </summary>
		private const string SMOGON_POKEMON_URL = "https://www.smogon.com/dex/ss/pokemon/{0}/";
		/// <summary>
		/// {0}: Dex number
		/// </summary>
		private const string POKEMON_POKEDEX_URL = "https://www.pokemon.com/us/pokedex/{0}";
		/// <summary>
		/// {0}: Generation abbreviation
		/// </summary>
		private const string SMOGON_GENERATION_URL = "https://www.smogon.com/dex/{0}/pokemon/";
		/// <summary>
		/// {0}: Format abbreviation
		/// </summary>
		private const string SMOGON_FORMAT_URL = "https://www.smogon.com/dex/ss/formats/{0}/";

		private readonly Dictionary<string, DbPokemon> _pokemonsMap = new Dictionary<string, DbPokemon>();

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

		// Keep for 1 day
		private const long KEEP_CACHE_FOR_SECONDS = 60L * 60L * 24L;

		public PokemonAPI()
			: base(ConfigSetting.PokemonsListLoadUnix, KEEP_CACHE_FOR_SECONDS, new string[] { "PokemonAbilities", "PokemonAttackEffectives", "PokemonFormats", "PokemonItems", "PokemonNatures", "PokemonPassives", "PokemonTypes", "Pokemons", "PokemonGenerations" }, KEEP_CACHE_FOR_SECONDS)
		{ }

		public override async Task<bool> LoadItemsLogicAsync(bool forceUpdate)
		{
			using DiamondContext db = new DiamondContext();

			string json = await GetSmogonResponseJsonAsync(null);
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

					float effectiveness = SConvert.ToSingle(token.SelectToken("[1]").ToString());

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

				// Create pokemons map
				this._pokemonsMap.Add(pokemon.Name, dbPokemon);
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

		#region Utils
		public static async Task<SmogonStrategies?> GetStrategiesForPokemonAsync(string pokemonName)
		{
			string json = await GetSmogonResponseJsonAsync(pokemonName);
			if (json == null) return null;

			SmogonRootObject resp = JsonConvert.DeserializeObject<SmogonRootObject>(json);
			SmogonStrategies strats = JsonConvert.DeserializeObject<SmogonStrategies>(resp.InjectRpcs[2][1].ToString());

			return strats;
		}

		public static string GetTypeEmoji(PokemonType type)
		{
			return _pokemonTypesEmojiMap[type];
		}

		public static string GetTypeEmoji(string typeString)
		{
			PokemonType type = GetPokemonTypeByTypeName(typeString);
			return GetTypeEmoji(type);
		}

		public static string GetPokemonGif(string name)
		{
			return string.Format(SMOGON_POKEMON_GIFS_URL, name.ToLower().Replace(" ", "-"));
		}

		public static string GetPokemonImage(int dexNumber)
		{
			return string.Format(POKEMON_IMAGES_URL, dexNumber);
		}

		public static string GetSmogonUrl(string name)
		{
			return string.Format(SMOGON_POKEMON_URL, name.ToLower().Replace(" ", "-"));
		}

		public static string GetPokedexUrl(int dexNumber)
		{
			return string.Format(POKEMON_POKEDEX_URL, dexNumber);
		}

		public static string GetGenerationUrl(string generationAbbreviation)
		{
			return string.Format(SMOGON_GENERATION_URL, generationAbbreviation);
		}

		public static string GetFormatUrl(string formatAbbreviation)
		{
			return string.Format(SMOGON_FORMAT_URL, formatAbbreviation);
		}

		public static PokemonType GetPokemonTypeByTypeName(string type)
		{
			return (PokemonType)Enum.Parse(typeof(PokemonType), type);
		}

		public static string? GetGenerationName(string generationAbbreviation)
		{
			using DiamondContext db = new DiamondContext();

			DbPokemonGenerations generation = db.PokemonGenerations.Where(g => g.Abbreviation == generationAbbreviation).FirstOrDefault();
			return generation?.Name;
		}

		public static string? GetFormatName(string formatAbbreviation)
		{
			using DiamondContext db = new DiamondContext();

			DbPokemonFormat format = db.PokemonFormats.Where(pf => pf.Abbreviation == formatAbbreviation).FirstOrDefault();
			return format?.Name;
		}

		private static async Task<string?> GetSmogonResponseJsonAsync(string pokemonName)
		{
			string url = pokemonName != null ? string.Format(SMOGON_POKEMON_URL, pokemonName) : SMOGON_ENDPOINT;
			string response = await RequestUtils.GetAsync(url);

			string json = null;
			foreach (string line in response.Split('\n'))
			{
				if (line.Trim().StartsWith("dexSettings"))
				{
					json = line.Split(" = ")[1];
				}
			}
			return json;
		}
		#endregion
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
