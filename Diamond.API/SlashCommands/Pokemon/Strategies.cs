using System.Collections.Generic;
using System.Threading.Tasks;

using Diamond.API.APIs.Pokemon;
using Diamond.API.Attributes;
using Diamond.API.Helpers;
using Diamond.Data;
using Diamond.Data.Models.Pokemons;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Pokemon
{
	public partial class Pokemon
	{
		[DSlashCommand("strats", "View strategies for a pokémon.")]
		public async void StratsCommandHandlerAsync(
			[Summary("name", "The name of the pokémon."), Autocomplete(typeof(PokemonNameAutocompleter))] string pokemonName,
			[Summary("generation", "The generation of the pokémon."), Autocomplete(typeof(PokemonGenerationAutocompleter))] string generationAbbreviation,
			[Summary("replace-emojis", "Replaces the type emojis with a text in case you a have trouble reading.")] bool replaceEmojis = false,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			await this.SendStratsGenerationSelectorEmbed(pokemonName, generationAbbreviation, replaceEmojis);
		}

		[ComponentInteraction($"{BUTTON_POKEMON_VIEW_STRATS}:*,*,*", true)]
		public async Task ButtonViewStrategiesHandlerAsync(string pokemonName, string generationAbbreviation, bool replaceEmojis)
		{
			await this.DeferAsync();

			await this.SendStratsGenerationSelectorEmbed(pokemonName, generationAbbreviation, replaceEmojis);
		}

		private async Task SendStratsGenerationSelectorEmbed(string pokemonName, string generationAbbreviation, bool replaceEmojis)
		{
			using DiamondContext db = new DiamondContext();

			DefaultEmbed embed = new DefaultEmbed("Pokédex - Strategies", "🧠", this.Context)
			{
				Title = pokemonName,
				Description = "Select a generation to get the strategies for.",
			};

			List<SelectMenuOptionBuilder> selectOptions = new List<SelectMenuOptionBuilder>();
			foreach (DbPokemonGeneration gen in db.PokemonGenerations)
			{
				selectOptions.Add(new SelectMenuOptionBuilder(gen.Name, gen.Abbreviation));
			}

			ComponentBuilder components = new ComponentBuilder()
				.WithSelectMenu($"{SELECT_POKEMON_STRATEGIES_GENERATION}:{pokemonName},{generationAbbreviation},{replaceEmojis}", selectOptions, row: 1);

			_ = await embed.SendAsync(components.Build());
		}

		[ComponentInteraction($"{SELECT_POKEMON_STRATEGIES_GENERATION}:*,*,*", true)]
		private async Task SelectStrategyGenerationHandlerAsync(string pokemonName, string generationAbbreviation, bool replaceEmojis)
		{
			await this.DeferAsync();

			await this.SendStratsEmbed(pokemonName, generationAbbreviation, replaceEmojis);
		}

		private async Task SendStratsEmbed(string pokemonName, string generationAbbreviation, bool replaceEmojis)
		{
			using DiamondContext db = new DiamondContext();

			DefaultEmbed embed = new DefaultEmbed("Pokédex - Strategies", "🧠", this.Context)
			{
				Title = pokemonName,
			};

			List<DbPokemonStrategy> strategiesList = await PokemonAPIHelpers.GetPokemonStrategies(pokemonName, generationAbbreviation, db);

			if (strategiesList.Count == 0)
			{
				embed.Description = "No strategies found for " + pokemonName;
				_ = await embed.SendAsync(await this.GetEmbedButtonsAsync(pokemonName, generationAbbreviation, PokemonEmbed.Strategies, replaceEmojis, db));
				return;
			}

			embed.Description = strategiesList[0].Comments;
			_ = await embed.SendAsync(await this.GetEmbedButtonsAsync(pokemonName, generationAbbreviation, PokemonEmbed.Strategies, replaceEmojis, db));
		}
	}
}
