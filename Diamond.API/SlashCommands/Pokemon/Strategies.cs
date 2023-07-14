using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.APIs.Pokemon;
using Diamond.API.Helpers;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.Pokemons;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Pokemon
{
	public partial class Pokemon
	{
		/*[DSlashCommand("strats", "View strategies for a pokémon.")]
		public async void StratsCommandHandlerAsync(
			[Summary("name", "The name of the pokémon."), Autocomplete(typeof(PokemonNameAutocompleter))] string pokemonName,
			[Summary("generation", "The generation of the pokémon."), Autocomplete(typeof(PokemonGenerationAutocompleter))] string generationAbbreviation,
			[Summary("replace-emojis", "Replaces the type emojis with a text in case you a have trouble reading.")] bool replaceEmojis = false,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			await this.SendStratsEmbed(pokemonName, generationAbbreviation, replaceEmojis, showEveryone);
		}*/

		[ComponentInteraction($"{BUTTON_POKEMON_VIEW_STRATS}:*,*,*", true)]
		public async Task ButtonViewStrategiesHandlerAsync(string pokemonName, string generationAbbreviation, bool replaceEmojis)
		{
			await this.DeferAsync();

			await this.SendStratsEmbedAsync(pokemonName, generationAbbreviation, replaceEmojis, false);
		}

		[ComponentInteraction($"{SELECT_POKEMON_STRATEGIES_GENERATION}:*:*", true)]
		public async Task SelectMenuStrategiesGenerationHandlerAsync(string pokemonName, bool replaceEmojis, string generationNumber)
		{
			await this.DeferAsync();

			await this.SendStratsEmbedAsync(pokemonName, generationNumber, replaceEmojis, false);
		}

		private async Task SendStratsEmbedAsync(string pokemonName, string? generationAbbreviation, bool replaceEmojis, bool showEveryone)
		{
			using DiamondContext db = new DiamondContext();

			this.CheckItemsLoading();

			Pokeporco poke = new Pokeporco(pokemonName, db);
			generationAbbreviation ??= poke.Generations.Last().Abbreviation;

			DefaultEmbed embed = new DefaultEmbed("Pokédex - Strategies", "🧠", this.Context)
			{
				Title = pokemonName,
			};

			List<DbPokemonStrategy> strategiesList = await PokemonUtils.GetPokemonStrategies(pokemonName, generationAbbreviation, db);

			if (strategiesList.Count == 0)
			{
				embed.Description = "No strategies found for " + pokemonName;
				_ = await embed.SendAsync(this.GetEmbedButtons(pokemonName, generationAbbreviation, PokemonEmbed.Strategies, replaceEmojis, showEveryone, db));
				return;
			}

			embed.Description = Utils.RemoveHtmlTags(strategiesList[0].Overview);
			_ = embed.AddField(strategiesList[0].MovesetName, $"Move 1: {strategiesList[0].Movesets[0].Move}\nMove 2: {strategiesList[0].Movesets[1].Move}");
			foreach (DbPokemonStrategyMoveset a in strategiesList[0].Movesets)
			{

			}
			_ = await embed.SendAsync(this.GetEmbedButtons(pokemonName, generationAbbreviation, PokemonEmbed.Strategies, replaceEmojis, showEveryone, db));
		}
	}
}
