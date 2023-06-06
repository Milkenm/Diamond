using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Diamond.API.APIs;
using Diamond.API.Attributes;
using Diamond.API.Data;
using Diamond.API.Schemes.Smogon;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;

using Microsoft.EntityFrameworkCore;

namespace Diamond.API.SlashCommands.Pokemon
{
	public partial class Pokemon
	{
		private const string BUTTON_POKEMON_VIEW_MOVES = $"{BUTTON_POKEMON_BASE}_moves";
		private const string BUTTON_POKEMON_VIEW_BUILDS = $"{BUTTON_POKEMON_BASE}_builds";

		[DSlashCommand("search", "Search for a pokémon.")]
		public async Task PokemonSearchCommandAsync(
			[Summary("name", "The name of the pokémon to search for.")] string name,
			[Summary("replace-emojis", "Replaces the type emojis with a text in case you a have trouble reading.")] bool replaceEmojis = false,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			using DiamondContext db = new DiamondContext();

			DbPokemon pokemon = this._pokeApi.SearchItem(name)[0].Item;

			Dictionary<PokemonType, double> effectivenessMap = new Dictionary<PokemonType, double>();
			StringBuilder typesSb = new StringBuilder();
			foreach (string type in pokemon.TypesList.Split(",").ToList())
			{
				DbPokemonType dbType = db.PokemonTypes.Where(t => t.Name == type).FirstOrDefault();
				PokemonType pokeType = PokemonAPI.GetPokemonTypeByTypeName(type);

				// Get all counters
				foreach (DbPokemonAttackEffectives atkef in db.PokemonAttackEffectives.Include(af => af.AttackerType).Where(af => af.TargetType == dbType))
				{
					if (atkef.Value == 1) continue;

					PokemonType attackerType = PokemonAPI.GetPokemonTypeByTypeName(atkef.AttackerType.Name);
					if (effectivenessMap.ContainsKey(attackerType))
					{
						if (atkef.Value == 0.5)
						{
							effectivenessMap[attackerType] -= 1;
						}
						else
						{
							effectivenessMap[attackerType] += atkef.Value;
						}
					}
					else
					{
						if (atkef.Value == 0.5)
						{
							effectivenessMap.Add(attackerType, -1);
						}
						else
						{
							effectivenessMap.Add(attackerType, atkef.Value);
						}
					}
				}

				_ = typesSb.Append($"{GetTypeDisplay(type, replaceEmojis)}", " ");
			}
			StringBuilder weakToSb = new StringBuilder();
			StringBuilder immuneToSb = new StringBuilder();
			StringBuilder resistsToSb = new StringBuilder();
			foreach (KeyValuePair<PokemonType, double> effectiveness in effectivenessMap.OrderBy(kv => kv.Value))
			{
				if (effectiveness.Value > 1)
				{
					if (effectiveness.Value == 2)
					{
						_ = weakToSb.Append($"{GetTypeDisplay(effectiveness.Key, replaceEmojis)} (x2)", "\n");
					}
					else if (effectiveness.Value == 3)
					{
						_ = weakToSb.Append($"**{GetTypeDisplay(effectiveness.Key, replaceEmojis)} (x4)**", "\n");
					}
				}
				else if (effectiveness.Value == 0)
				{
					_ = immuneToSb.Append(GetTypeDisplay(effectiveness.Key, replaceEmojis), "\n");
				}
				else if (effectiveness.Value < 0)
				{
					if (effectiveness.Value == -1)
					{
						_ = resistsToSb.Append($"{GetTypeDisplay(effectiveness.Key, replaceEmojis)} (x0.5)", "\n");
					}
					else if (effectiveness.Value == -2)
					{
						_ = resistsToSb.Append($"**{GetTypeDisplay(effectiveness.Key, replaceEmojis)} (x0.25)**", "\n");
					}
				}
			}

			// Abilities
			List<string> abilitiesList = pokemon.AbilitiesList.Split(",").ToList();
			StringBuilder abilitiesSb = new StringBuilder();
			foreach (string ability in abilitiesList)
			{
				DbPokemonPassive passive = db.PokemonPassives.Where(p => p.Name == ability).FirstOrDefault();
				_ = abilitiesSb.Append($"**{passive.Name}**: {passive.Description}", "\n");
			}

			// Evolutions
			List<string> evolutionsList = pokemon.EvolutionsList.Split(",").ToList();
			StringBuilder evolutionsSb = new StringBuilder();
			foreach (string evolution in evolutionsList)
			{
				_ = evolutionsSb.Append($"[{evolution}]({PokemonAPI.GetSmogonUrl(evolution)})", "\n");
			}

			DefaultEmbed embed = new DefaultEmbed("Pokédex", "🖥️", this.Context)
			{
				Title = $"{pokemon.Name} #{pokemon.DexNumber}",
				Description = typesSb.ToString(),
				/*ThumbnailUrl = PokemonAPI.GetPokemonGif(pokemon.Name),
				ImageUrl = PokemonAPI.GetPokemonImage((int)pokemon.DexNumber),*/
			};

			// Generations
			List<string> generationsList = pokemon.GenerationsList.Split(",").ToList();
			StringBuilder generationsSb = new StringBuilder();
			foreach (string generation in generationsList)
			{
				string fullGeneration = PokemonAPI.GetGenerationName(generation);
				_ = generationsSb.Append($"[{fullGeneration}]({PokemonAPI.GetGenerationUrl(generation)})", "\n");
			}

			// Formats
			List<string> formatsList = pokemon.FormatsList.Split(",").ToList();
			StringBuilder formatsSb = new StringBuilder();
			foreach (string format in formatsList)
			{
				string fullFormat = PokemonAPI.GetFormatName(format);
				_ = formatsSb.Append($"[{fullFormat}]({PokemonAPI.GetFormatUrl(format)})", "\n");
			}

			// First row
			_ = embed.AddField("⚔️ **__Stats__**", $"HP: {pokemon.HealthPoints}\nAttack: {pokemon.Attack}\nDefense: {pokemon.Defense}\nSp. Atk: {pokemon.SpecialAttack}\nSp. Def: {pokemon.SpecialDefense}\nSpeed: {pokemon.SpecialDefense}", true);
			_ = embed.AddField($"⬆️ **__{Utils.Plural("Evolution", "", "s", evolutionsList)}__**", evolutionsSb.ToStringOrDefault("None"), true);
			_ = embed.AddEmptyField(true);
			// Second row
			_ = embed.AddField("😨 **__Weak to__**", weakToSb.ToStringOrDefault("None"), true);
			_ = embed.AddField("💪 **__Resists to__**", resistsToSb.ToStringOrDefault("None"), true);
			_ = embed.AddField("🛡️ **__Immune to__**", immuneToSb.ToStringOrDefault("None"), true);
			// Third row
			_ = embed.AddField($"📆 **__{Utils.Plural("Generation", "", "s", generationsList)}__**", generationsSb.ToStringOrDefault("None"), true);
			_ = embed.AddField($"🏆 **__{Utils.Plural("Format", "", "s", formatsList)}__**", formatsSb.ToStringOrDefault("None"), true);
			_ = embed.AddEmptyField(true);
			// Fourth row
			_ = embed.AddField($"✨ **__{Utils.Plural("Abilit", "y", "ies", abilitiesList)}__**", abilitiesSb.ToString());
			MessageComponent components = new ComponentBuilder()
				.WithButton("Moves", $"{BUTTON_POKEMON_VIEW_MOVES}:{pokemon.Name}", style: ButtonStyle.Primary, Emoji.Parse("👊"))
				.WithButton("Strategies", $"{BUTTON_POKEMON_VIEW_BUILDS}:{pokemon.Name}", style: ButtonStyle.Primary, Emoji.Parse("🧱"))
				.WithButton("View on Pokédex", style: ButtonStyle.Link, url: PokemonAPI.GetPokedexUrl((int)pokemon.DexNumber))
				.WithButton("View on Smogon", style: ButtonStyle.Link, url: PokemonAPI.GetSmogonUrl(pokemon.Name))
				.Build();
			embed.Component = components;

			_ = await embed.SendAsync();
		}

		[ComponentInteraction($"{BUTTON_POKEMON_VIEW_MOVES}:*", true)]
		public async Task ButtonViewMovesHandler(string pokemonName)
		{
			await this.DeferAsync();
		}

		[ComponentInteraction($"{BUTTON_POKEMON_VIEW_BUILDS}:*", true)]
		public async Task ButtonViewStrategiesHandler(string pokemonName)
		{
			await this.DeferAsync();

			SmogonStrategies strats = await PokemonAPI.GetStrategiesForPokemonAsync(pokemonName);

			_ = await this.Context.Channel.SendMessageAsync(strats.StrategiesList[0].Overview);
		}

		private static string GetTypeDisplay(PokemonType type, bool replaceEmojis)
		{
			return !replaceEmojis ? PokemonAPI.GetTypeEmoji(type) : type.ToString();
		}

		private static string GetTypeDisplay(string typeString, bool replaceEmojis)
		{
			if (!replaceEmojis)
			{
				PokemonType type = PokemonAPI.GetPokemonTypeByTypeName(typeString);
				return PokemonAPI.GetTypeEmoji(type);
			}
			else
			{
				return typeString;
			}
		}
	}
}
