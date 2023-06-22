using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Schemes.Smogon;
using Diamond.Data;
using Diamond.Data.Models.Pokemons;

using Newtonsoft.Json;

using ScriptsLibV2.Util;

namespace Diamond.API.APIs.Pokemon
{
	public class PokemonAPIHelpers
	{
		public const string SMOGON_ENDPOINT = "https://www.smogon.com/dex/sm/pokemon/";
		/// <summary>
		/// {0}: Pokémon name
		/// </summary>
		public const string SMOGON_POKEMON_GIFS_URL = "https://www.smogon.com/dex/media/sprites/xy/{0}.gif";
		/// <summary>
		/// {0}: Dex number
		/// </summary>
		public const string POKEMON_IMAGES_URL = "https://assets.pokemon.com/assets/cms2/img/pokedex/full/{0}.png";
		/// <summary>
		/// {0}: Pokémon name
		/// </summary>
		public const string SMOGON_POKEMON_URL = "https://www.smogon.com/dex/ss/pokemon/{0}/";
		/// <summary>
		/// {0}: Dex number
		/// </summary>
		public const string POKEMON_POKEDEX_URL = "https://www.pokemon.com/us/pokedex/{0}";
		/// <summary>
		/// {0}: Generation abbreviation
		/// </summary>
		public const string SMOGON_GENERATION_URL = "https://www.smogon.com/dex/{0}/pokemon/";
		/// <summary>
		/// {0}: Format abbreviation
		/// </summary>
		public const string SMOGON_FORMAT_URL = "https://www.smogon.com/dex/ss/formats/{0}/";

		public readonly Dictionary<string, DbPokemon> _pokemonsMap = new Dictionary<string, DbPokemon>();

		public static readonly Dictionary<PokemonType, string> _pokemonTypesEmojiMap = new Dictionary<PokemonType, string>()
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

		public static readonly Dictionary<PokemonAttackType, string> _pokemonAttackTypesEmojiMap = new Dictionary<PokemonAttackType, string>
		{
			{ PokemonAttackType.Status, "<:pokemon_attack_type_status1:1114536566740766720><:pokemon_attack_type_status2:1114536555961393303>" },
			{ PokemonAttackType.Physical, "<:pokemon_attack_type_physical1:1114536599657644122><:pokemon_attack_type_physical2:1114536581399859281>" },
			{ PokemonAttackType.Special, "<:pokemon_attack_type_special1:1114536620817911847><:pokemon_attack_type_special2:1114536611389116546>" },
		};

		// Keep for 1 day
		public const long KEEP_CACHE_FOR_SECONDS = 60L * 60L * 24L;

		#region Utils
		public static async Task<SmogonStrategies> GetStrategiesForPokemonAsync(string pokemonName)
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
			PokemonType type = GetPokemonTypeByName(typeString);
			return GetTypeEmoji(type);
		}

		public static string GetAttackTypeEmoji(PokemonAttackType attackType)
		{
			return _pokemonAttackTypesEmojiMap[attackType];
		}

		public static string GetAttackTypeEmoji(string attackTypeString)
		{
			PokemonAttackType attackType = GetPokemonAttackTypeByName(attackTypeString);
			return GetAttackTypeEmoji(attackType);
		}

		public static PokemonType GetPokemonTypeByName(string typeString)
		{
			return (PokemonType)Enum.Parse(typeof(PokemonType), typeString);
		}

		public static PokemonAttackType GetPokemonAttackTypeByName(string attackTypeString)
		{
			return attackTypeString == "Non-Damaging"
				? PokemonAttackType.Status
				: (PokemonAttackType)Enum.Parse(typeof(PokemonAttackType), attackTypeString);
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

		public static string GetGenerationName(string generationAbbreviation)
		{
			using DiamondContext db = new DiamondContext();

			DbPokemonGenerations generation = db.PokemonGenerations.Where(g => g.Abbreviation == generationAbbreviation).FirstOrDefault();
			return generation?.Name;
		}

		public static string GetFormatName(string formatAbbreviation)
		{
			using DiamondContext db = new DiamondContext();

			DbPokemonFormat format = db.PokemonFormats.Where(pf => pf.Abbreviation == formatAbbreviation).FirstOrDefault();
			return format?.Name;
		}

		public static async Task<string> GetSmogonResponseJsonAsync(string pokemonName)
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

		public static string GetTypeDisplay(PokemonType type, bool replaceEmojis)
		{
			return !replaceEmojis ? GetTypeEmoji(type) : type.ToString();
		}

		public static string GetTypeDisplay(string typeString, bool replaceEmojis)
		{
			if (!replaceEmojis)
			{
				PokemonType type = GetPokemonTypeByName(typeString);
				return GetTypeEmoji(type);
			}
			else
			{
				return typeString;
			}
		}

		public static string GetAttackTypeDisplay(PokemonAttackType attackType, bool replaceEmojis)
		{
			return !replaceEmojis ? GetAttackTypeEmoji(attackType) : attackType.ToString();
		}

		public static string GetAttackTypeDisplay(string attackTypeString, bool replaceEmojis)
		{
			if (!replaceEmojis)
			{
				PokemonAttackType attackType = GetPokemonAttackTypeByName(attackTypeString);
				return GetAttackTypeEmoji(attackType);
			}
			else
			{
				return attackTypeString;
			}
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

	public enum PokemonAttackType
	{
		Status,
		Physical,
		Special,
	}
}
