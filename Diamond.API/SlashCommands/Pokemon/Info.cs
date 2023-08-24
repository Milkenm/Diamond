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
			[Summary("generation", "The generation of the pokémon."), Autocomplete(typeof(PokemonGenerationAutocompleter))] Generation? generation = null,
			[Summary("replace-emojis", "Replaces the type emojis with a text in case you a have trouble reading.")] bool replaceEmojis = false,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			// Get generation abbreviation
			using DiamondContext db = new DiamondContext();
			string? generationAbbreviation = generation?.ToString();

			await this.SendInfoEmbedAsync(pokemonName, generationAbbreviation, replaceEmojis, showEveryone, false);
		}

		[ComponentInteraction($"{PokemonComponentIds.BUTTON_POKEMON_VIEW_INFO}:*,*,*", true)]
		public async Task ButtonViewInfoHandlerAsync(string pokemonName, string generationAbbreviation, bool replaceEmojis)
		{
			await this.DeferAsync();

			await this.SendInfoEmbedAsync(pokemonName, generationAbbreviation, replaceEmojis, false, false);
		}

		[ComponentInteraction($"{PokemonComponentIds.BUTTON_POKEMON_SHARE_INFO}:*,*,*", true)]
		public async Task ButtonSharePokemonHandlerAsync(string pokemonName, string generationAbbreviation, bool replaceEmojis)
		{
			await this.DeferAsync();

			await this.SendInfoEmbedAsync(pokemonName, generationAbbreviation, replaceEmojis, true, true);
		}

		[ComponentInteraction($"{PokemonComponentIds.SELECT_POKEMON_INFO_GENERATION}:*,*", true)]
		public async Task SelectMenuInfoGenerationHandlerAsync(string pokemonName, bool replaceEmojis, string generationAbbreviation)
		{
			await this.DeferAsync();

			await this.SendInfoEmbedAsync(pokemonName, generationAbbreviation, replaceEmojis, false, false);
		}

		private async Task SendInfoEmbedAsync(string pokemonName, string? generationAbbreviation, bool replaceEmojis, bool showEveryone, bool sendAsNew)
		{
			using DiamondContext db = new DiamondContext();

			this.CheckItemsLoading();

			Pokeporco poke = new Pokeporco(pokemonName, db);
			generationAbbreviation = poke.GetValidGeneration(generationAbbreviation);
			DbPokemon dbPokemon = poke.GetFullFromGeneration(generationAbbreviation);

			// Effectiveness
			Dictionary<Effectiveness, string> attackEffectiveness = new PokemonTypeEffectiveness(dbPokemon.TypesList, db).ToString(replaceEmojis);

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
				Title = $"{dbPokemon.Name} #{dbPokemon.DexNumber} ({generationAbbreviation})",
				Description = typesSb.ToString(),
				ThumbnailUrl = poke.GetPokemonGif(generationAbbreviation),
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
				_ = formatsSb.Append($"[{format.Name}]({PokemonUtils.GetFormatUrl(format.Abbreviation.Replace(" ", "-"), dbPokemon.GenerationAbbreviation)})", "\n");
			}

			// First row
			_ = embed.AddField("⚔️ **__Stats__**", $"{GetPokemonStatString(dbPokemon.HealthPoints, PokemonStat.HP)}\n{GetPokemonStatString(dbPokemon.Attack, PokemonStat.Attack)}\n{GetPokemonStatString(dbPokemon.Defense, PokemonStat.Defense)}\n{GetPokemonStatString(dbPokemon.SpecialAttack, PokemonStat.SpecialAttack)}\n{GetPokemonStatString(dbPokemon.SpecialDefense, PokemonStat.SpecialDefense)}\n{GetPokemonStatString(dbPokemon.Speed, PokemonStat.Speed)}", true);
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
			_ = embed.AddField($"✨ **__{Utils.Plural("Abilit", "y", "ies", dbPokemon.AbilitiesList)}__**", abilitiesSb.ToStringOrDefault("None"));

			this.AddEmbedButtons(embed, pokemonName, generationAbbreviation, PokemonEmbedType.Info, replaceEmojis, showEveryone, db);

			_ = await embed.SendAsync(sendAsNew: sendAsNew);
		}

		private static string GetPokemonStatString(int value, PokemonStat stat)
		{
			string statString = "`" + stat switch
			{
				PokemonStat.HP => "HP",
				PokemonStat.Attack => "Attack",
				PokemonStat.Defense => "Defense",
				PokemonStat.SpecialAttack => "Sp. Atk",
				PokemonStat.SpecialDefense => "Sp. Def",
				PokemonStat.Speed => "Speed",
			} + "`:";

			return value switch
			{
				<= 50 => $"{statString} *{value}*",
				>= 100 => $"{statString} **{value}**",
				_ => $"{statString} {value}",
			};
		}

		private enum PokemonStat
		{
			HP,
			Attack,
			Defense,
			SpecialAttack,
			SpecialDefense,
			Speed,
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

		private enum Stat
		{
			Power,
			Accuracy,
			PowerPoints,
		}

		public enum Generation
		{
			[ChoiceDisplay("Red/Blue (RB)")] RB,
			[ChoiceDisplay("Gold/Silver (GS)")] GS,
			[ChoiceDisplay("Ruby/Sapphire (RS)")] RS,
			[ChoiceDisplay("Diamond/Pearl (DP)")] DP,
			[ChoiceDisplay("Black/White (BW)")] BW,
			[ChoiceDisplay("X/Y (XY)")] XY,
			[ChoiceDisplay("Sun/Moon (SM")] SM,
			[ChoiceDisplay("Sword/Shield (SS)")] SS,
			[ChoiceDisplay("Scarlet/Violet")] SV
		}
	}
}
