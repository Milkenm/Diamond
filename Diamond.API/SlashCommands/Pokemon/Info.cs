using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Diamond.API.APIs.Pokemon;
using Diamond.API.Attributes;
using Diamond.API.Helpers;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.Pokemons;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Pokemon
{
	public partial class Pokemon
	{
		[DSlashCommand("info", "View info about a pokémon.")]
		public async Task PokemonSearchCommandAsync(
			[Summary("name", "The name of the pokémon."), Autocomplete(typeof(PokemonNameAutocompleter))] string pokemonName,
			[Summary("replace-emojis", "Replaces the type emojis with a text in case you a have trouble reading.")] bool replaceEmojis = false,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			await this.SendInfoEmbedAsync(pokemonName, null, replaceEmojis);
		}

		[ComponentInteraction($"{BUTTON_POKEMON_VIEW_INFO}:*,*,*", true)]
		public async Task ButtonViewStatsHandler(string pokemonName, string generationAbbreviation, bool replaceEmojis)
		{
			await this.DeferAsync();

			await this.SendInfoEmbedAsync(pokemonName, generationAbbreviation, replaceEmojis);
		}

		[ComponentInteraction($"{SELECT_POKEMON_INFO_GENERATION}:*:*", true)]
		public async Task SelectMenuInfoGenerationHandlerAsync(string pokemonName, bool replaceEmojis, string generationAbbreviation)
		{
			await this.DeferAsync();

			await this.SendInfoEmbedAsync(pokemonName, generationAbbreviation, replaceEmojis);
		}

		private async Task SendInfoEmbedAsync(string pokemonName, string? generationAbbreviation, bool replaceEmojis)
		{
			generationAbbreviation ??= PokemonAPI.POKEMON_DEFAULT_GENERATION;

			using DiamondContext db = new DiamondContext();

			Pokeporco poke = new Pokeporco(pokemonName,db);
			DbPokemon dbPokemon = poke.GetFullFromGeneration(generationAbbreviation);

			// Effectiveness
			Dictionary<Effectiveness, string> attackEffectiveness = new PokemonTypeEffectiveness(dbPokemon.TypesList).ToString(replaceEmojis);

			// Types
			StringBuilder typesSb = new StringBuilder();
			foreach (DbPokemonType dbType in dbPokemon.TypesList)
			{
				_ = typesSb.Append($"{PokemonUtils.GetTypeDisplay(dbType.Name, replaceEmojis)}", " ");
			}

			// Abilities
			StringBuilder abilitiesSb = new StringBuilder();
			foreach (DbPokemonPassive dbPassive in dbPokemon.AbilitiesList)
			{
				_ = abilitiesSb.Append($"**{dbPassive.Name}**: {dbPassive.Description}", "\n");
			}

			// Evolutions
			int evolutions = 0;
			StringBuilder evolutionsSb = new StringBuilder();
			foreach (DbPokemon evolution in dbPokemon.EvolutionsList)
			{
				evolutions++;
				_ = evolutionsSb.Append($"[{evolution.Name}]({new Pokeporco(evolution, db).GetSmogonPokemonUrl(evolution.GenerationAbbreviation)})", "\n");
			}

			DefaultEmbed embed = new DefaultEmbed("Pokédex - Info", "🖥️", this.Context)
			{
				Title = $"{dbPokemon.Name} #{dbPokemon.DexNumber}",
				Description = typesSb.ToString(),
				/*ThumbnailUrl = PokemonAPIHelpers.GetPokemonGif(dbPokemon.Name),
				ImageUrl = PokemonAPIHelpers.GetPokemonImage((int)dbPokemon.DexNumber),*/
			};

			// Generations
			StringBuilder generationsSb = new StringBuilder();
			foreach (DbPokemonGeneration generation in dbPokemon.GenerationsList)
			{
				_ = generationsSb.Append($"[{generation.Name}]({PokemonUtils.GetGenerationUrl(generation.Abbreviation)})", "\n");
			}

			// Formats
			StringBuilder formatsSb = new StringBuilder();
			foreach (DbPokemonFormat format in dbPokemon.FormatsList)
			{
				_ = formatsSb.Append($"[{format.Name}]({PokemonUtils.GetFormatUrl(format.Abbreviation, dbPokemon.GenerationAbbreviation)})", "\n");
			}

			// First row
			_ = embed.AddField("⚔️ **__Stats__**", $"HP: {dbPokemon.HealthPoints}\nAttack: {dbPokemon.Attack}\nDefense: {dbPokemon.Defense}\nSp. Atk: {dbPokemon.SpecialAttack}\nSp. Def: {dbPokemon.SpecialDefense}\nSpeed: {dbPokemon.SpecialDefense}", true);
			_ = embed.AddField($"⬆️ **__{Utils.Plural("Evolution", "", "s", evolutions)}__**", evolutionsSb.ToStringOrDefault("None"), true);
			_ = embed.AddEmptyField(true);
			// Second row
			_ = embed.AddField("😨 **__Weak to__**", attackEffectiveness[Effectiveness.Weak].OrDefault("None"), true);
			_ = embed.AddField("💪 **__Resists to__**", attackEffectiveness[Effectiveness.Resists].OrDefault("None"), true);
			_ = embed.AddField("🛡️ **__Immune to__**", attackEffectiveness[Effectiveness.Immune].OrDefault("None"), true);
			// Third row
			_ = embed.AddField($"📆 **__{Utils.Plural("Generation", "", "s", dbPokemon.GenerationsList)}__**", generationsSb.ToStringOrDefault("None"), true);
			_ = embed.AddField($"🏆 **__{Utils.Plural("Format", "", "s", dbPokemon.FormatsList)}__**", formatsSb.ToStringOrDefault("None"), true);
			_ = embed.AddEmptyField(true);
			// Fourth row
			_ = embed.AddField($"✨ **__{Utils.Plural("Abilit", "y", "ies", dbPokemon.AbilitiesList)}__**", abilitiesSb.ToString());

			_ = await embed.SendAsync(await this.GetEmbedButtonsAsync(dbPokemon.Name, dbPokemon.GenerationAbbreviation, PokemonEmbed.Info, replaceEmojis, db));
		}

		private static string GetAttackStatString(int value, Stat stat)
		{
			return value == 0
				? "—"
				: stat switch
				{
					Stat.Accuracy => $"{value}%",
					Stat.PowerPoints => $"{value} PP",
					_ => value.ToString()
				};
		}

		public class PokemonTypeEffectiveness
		{
			private readonly Dictionary<PokemonType, double> EffectivenessMap = new Dictionary<PokemonType, double>();

			public PokemonTypeEffectiveness(List<DbPokemonType> attackerTypes)
			{
				using DiamondContext db = new DiamondContext();

				foreach (DbPokemonType type in attackerTypes)
				{
					this.EffectivenessMap.Merge(PokemonUtils.GetEffectivenessMapForType(type, db));
				}
			}

			public Dictionary<Effectiveness, string> ToString(bool replaceEmojis)
			{
				StringBuilder weakToSb = new StringBuilder();
				StringBuilder immuneToSb = new StringBuilder();
				StringBuilder resistsToSb = new StringBuilder();

				foreach (KeyValuePair<PokemonType, double> kv in this.EffectivenessMap)
				{
					if (kv.Value > 1)
					{
						if (kv.Value == 2)
						{
							_ = weakToSb.Append($"{PokemonUtils.GetTypeDisplay(kv.Key, replaceEmojis)} (x2)", "\n");
						}
						else if (kv.Value == 3)
						{
							_ = weakToSb.Append($"**{PokemonUtils.GetTypeDisplay(kv.Key, replaceEmojis)} (x4)**", "\n");
						}
					}
					else if (kv.Value == 0)
					{
						_ = immuneToSb.Append(PokemonUtils.GetTypeDisplay(kv.Key, replaceEmojis), "\n");
					}
					else if (kv.Value < 0)
					{
						if (kv.Value == -1)
						{
							_ = resistsToSb.Append($"{PokemonUtils.GetTypeDisplay(kv.Key, replaceEmojis)} (x0.5)", "\n");
						}
						else if (kv.Value == -2)
						{
							_ = resistsToSb.Append($"**{PokemonUtils.GetTypeDisplay(kv.Key, replaceEmojis)} (x0.25)**", "\n");
						}
					}
				}

				return new Dictionary<Effectiveness, string>()
				{
					{ Effectiveness.Weak, weakToSb.ToString() },
					{ Effectiveness.Resists, resistsToSb.ToString() },
					{ Effectiveness.Immune, immuneToSb.ToString() },
				};
			}
		}

		private enum Stat
		{
			Power,
			Accuracy,
			PowerPoints,
		}

		public enum Effectiveness
		{
			Weak,
			Immune,
			Resists,
			Normal,
		}
	}
}
