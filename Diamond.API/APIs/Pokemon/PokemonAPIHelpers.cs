using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Diamond.API.Schemes.Smogon;
using Diamond.Data;
using Diamond.Data.Models.Pokemons;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

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
		/// <summary>
		/// {0}: User ID
		/// </summary>
		public const string SMOGON_USER_PROFILE_URL = "https://www.smogon.com/forums/members/{0}/";

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

		public static async Task DownloadPokemonAdditionalInfoAsync(string pokemonName)
		{
			using DiamondContext db = new DiamondContext();

			if (!DoesPokemonExist(pokemonName, out DbPokemon dbPokemon)) return;

			SmogonStrategies strats = await GetStratsForPokemon(pokemonName);

			List<DbPokemonMove> movesList = db.PokemonMoves.ToList();
			foreach (string move in strats.LearnsetList)
			{
				DbPokemonMove dbMove = movesList.Where(m => m.Name == move).FirstOrDefault();
				if (dbMove == null) continue;

				_ = db.PokemonLearnsets.Add(new DbPokemonLearnset()
				{
					Pokemon = dbPokemon,
					Move = dbMove,
				});
			}
			await db.SaveAsync();

			foreach (SmogonStrategy strat in strats.StrategiesList)
			{
				await SaveStrategy(dbPokemon, movesList, strat);
			}
		}

		private static async Task SaveStrategy(DbPokemon dbPokemon, List<DbPokemonMove> movesList, SmogonStrategy strat)
		{
			using DiamondContext db = new DiamondContext();

			// Get strat format
			List<DbPokemonFormat> formatsList = db.PokemonFormats.ToList();
			DbPokemonFormat format = formatsList.Where(f => f.Name == strat.Format).FirstOrDefault();

			// Get strat passives/abilities
			List<DbPokemonPassive> passivesList = db.PokemonPassives.ToList();
			List<DbPokemonPassive> passives = new List<DbPokemonPassive>();
			foreach (string passive in strat.MovesetsList[0].AbilitiesList)
			{
				passives.Add(passivesList.Where(p => p.Name == passive).FirstOrDefault());
			}

			// Get strat items
			List<DbPokemonItem> itemsList = db.PokemonItems.ToList();
			List<DbPokemonItem> items = new List<DbPokemonItem>();
			foreach (string item in strat.MovesetsList[0].ItemsList)
			{
				items.Add(itemsList.Where(i => i.Name == item).FirstOrDefault());
			}

			// Get strat movesets
			List<DbPokemonType> typesList = db.PokemonTypes.ToList();
			List<DbPokemonStrategyMoveset> movesets = new List<DbPokemonStrategyMoveset>();
			foreach (SmogonStrategyMoveSlot moveset in strat.MovesetsList[0].MoveSlotsList[0])
			{
				DbPokemonMove move = movesList.Where(m => m.Name == moveset.Move).FirstOrDefault();
				DbPokemonType type = null;
				if (moveset.Type != null)
				{
					type = typesList.Where(t => t.Name == moveset.Type).FirstOrDefault();
				}

				DbPokemonStrategyMoveset dbMoveset = new DbPokemonStrategyMoveset()
				{
					Move = move,
					Type = type,
				};
				_ = db.PokemonStrategyMovesets.Add(dbMoveset);
				movesets.Add(dbMoveset);
			}
			// Save strat movesets
			await db.SaveAsync();

			// Get strat natures
			List<DbPokemonNature> naturesList = db.PokemonNatures.ToList();
			List<DbPokemonNature> natures = new List<DbPokemonNature>();
			foreach (string nature in strat.MovesetsList[0].NaturesList)
			{
				natures.Add(naturesList.Where(n => n.Name == nature).FirstOrDefault());
			}

			// Get strat written by
			List<PokemonSmogonUser> writtenBy = new List<PokemonSmogonUser>();
			foreach (SmogonStrategyMember user in strat.Credits.WrittenByList)
			{
				// TODO: Improve code
				// Check if the user already exists in the database
				IQueryable<PokemonSmogonUser> dbUsers = db.PokemonSmogonUsers.Where(u => u.Id == user.UserId);
				PokemonSmogonUser dbUser = null;
				if (dbUsers.Any())
				{
					dbUser = dbUsers.FirstOrDefault();
				}
				else
				{
					dbUser = new PokemonSmogonUser()
					{
						Username = user.Username,
						SmogonUserId = user.UserId,
					};
					_ = db.PokemonSmogonUsers.Add(dbUser);
					await db.SaveAsync();
				}

				writtenBy.Add(dbUser);
			}

			// Get credits teams
			List<DbPokemonStrategyCreditsTeam> creditsTeams = new List<DbPokemonStrategyCreditsTeam>();
			foreach (SmogonStrategyTeam team in strat.Credits.TeamsList)
			{
				List<PokemonSmogonUser> teamUsers = new List<PokemonSmogonUser>();
				foreach (SmogonStrategyMember user in team.MembersList)
				{
					// TODO: Improve code
					IQueryable<PokemonSmogonUser> dbUsers = db.PokemonSmogonUsers.Where(u => u.Id == user.UserId);
					PokemonSmogonUser dbUser = null;
					if (dbUsers.Any())
					{
						dbUser = dbUsers.FirstOrDefault();
					}
					else
					{
						dbUser = new PokemonSmogonUser()
						{
							Username = user.Username,
							SmogonUserId = user.UserId,
						};
						_ = db.PokemonSmogonUsers.Add(dbUser);
						await db.SaveAsync();
					}
					teamUsers.Add(dbUser);
				}

				DbPokemonStrategyCreditsTeam dbTeam = new DbPokemonStrategyCreditsTeam()
				{
					TeamName = team.Name,
					TeamMembersList = teamUsers,
				};
				_ = db.PokemonStrategyCreditsTeams.Add(dbTeam);
				await db.SaveAsync();

				creditsTeams.Add(dbTeam);
			}

			// Save strategy to database
			_ = db.PokemonStrategies.Add(new DbPokemonStrategy()
			{
				Format = format,
				Outdated = strat.Outdated,
				Pokemon = dbPokemon,
				Overview = strat.Overview,
				Comments = strat.Comments,
				MovesetName = strat.MovesetsList[0].Name,
				Gender = strat.MovesetsList[0].Gender,
				PassivesList = passives,
				ItemsList = items,
				Movesets = movesets,
				EVsHealth = strat.MovesetsList[0].EvConfigsList[0].HP,
				EVsAttack = strat.MovesetsList[0].EvConfigsList[0].Attack,
				EVsDefense = strat.MovesetsList[0].EvConfigsList[0].Defense,
				EVsSpecialAttack = strat.MovesetsList[0].EvConfigsList[0].SpecialAttack,
				EVsSpecialDefense = strat.MovesetsList[0].EvConfigsList[0].SpecialDefense,
				EVsSpeed = strat.MovesetsList[0].EvConfigsList[0].Speed,
				NaturesList = natures,
				WrittenByUsersList = writtenBy,
				TeamsList = creditsTeams,
			});
			await db.SaveAsync();
		}

		public static void LoadPokemonExtraInfo(ref DbPokemon pokemon)
		{
			using DiamondContext db = new DiamondContext();

			long id = pokemon.Id;
			pokemon = db.Pokemons.Where(p => p.Id == id)
				.Include(p => p.TypesList)
				.Include(p => p.AbilitiesList)
				.Include(p => p.EvolutionsList)
				.Include(p => p.GenerationsList)
				.Include(p => p.FormatsList)
				.First();
		}

		private static bool DoesPokemonExist(string pokemonName, out DbPokemon dbPokemon)
		{
			using DiamondContext db = new DiamondContext();

			List<DbPokemon> pokemonsList = db.Pokemons.ToList();
			dbPokemon = pokemonsList.Where(p => p.Name == pokemonName).FirstOrDefault();
			return dbPokemon != null;
		}

		private static async Task<SmogonStrategies> GetStratsForPokemon(string pokemonName)
		{
			string extraJson = await GetSmogonResponseJsonAsync(pokemonName);

			// TOOD: Idk how to make this so yah
			SmogonRootObject extraResp = JsonConvert.DeserializeObject<SmogonRootObject>(extraJson);
			SmogonStrategies strats = JsonConvert.DeserializeObject<SmogonStrategies>(extraResp.InjectRpcs[2][1].ToString());
			return strats;
		}

		public static async Task<List<DbPokemonMove>> GetPokemonMovesAsync(string pokemonName)
		{
			using DiamondContext db = new DiamondContext();

			IQueryable<DbPokemonLearnset> moves = db.PokemonLearnsets.Where(ls => ls.Pokemon.Name == pokemonName);
			if (!moves.Any())
			{
				await PokemonAPIHelpers.DownloadPokemonAdditionalInfoAsync(pokemonName);
				return await GetPokemonMovesAsync(pokemonName);
			}
			return moves.Select(ls => ls.Move).ToList();
		}

		public static async Task<List<DbPokemonStrategy>> GetPokemonStrategies(string pokemonName)
		{
			using DiamondContext db = new DiamondContext();

			IQueryable<DbPokemonStrategy> strategies = db.PokemonStrategies.Where(st => st.Pokemon.Name == pokemonName);
			if (!strategies.Any())
			{
				await PokemonAPIHelpers.DownloadPokemonAdditionalInfoAsync(pokemonName);
				return await GetPokemonStrategies(pokemonName);
			}
			return strategies.ToList();
		}

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

		public static string GetSmogonPokemonUrl(string pokemonName)
		{
			return string.Format(SMOGON_POKEMON_URL, pokemonName.ToLower().Replace(" ", "-"));
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

		public static string GetUserProfileUrl(long userId)
		{
			return string.Format(SMOGON_USER_PROFILE_URL, userId);
		}

		public static async Task<string> GetSmogonResponseJsonAsync(string pokemonName)
		{
			string url = pokemonName != null ? string.Format(SMOGON_POKEMON_URL, pokemonName) : SMOGON_ENDPOINT;
			string response = await (await new HttpClient().GetAsync(url)).Content.ReadAsStringAsync();

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
