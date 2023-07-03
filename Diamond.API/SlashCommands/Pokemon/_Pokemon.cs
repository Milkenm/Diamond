using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.APIs.Pokemon;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.CsgoItems;
using Diamond.Data.Models.Pokemons;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Diamond.API.SlashCommands.Pokemon
{
	[Group("pokemon", "Commands related to the 'Pokémon' game.")]
	public partial class Pokemon : InteractionModuleBase<SocketInteractionContext>
	{
		private const string BUTTON_POKEMON_BASE = "button_pokemon";
		private const string BUTTON_POKEMON_VIEW_MOVES = $"{BUTTON_POKEMON_BASE}_moves";
		private const string BUTTON_POKEMON_VIEW_STRATS = $"{BUTTON_POKEMON_BASE}_builds";
		private const string BUTTON_POKEMON_VIEW_INFO = $"{BUTTON_POKEMON_BASE}_strats";
		private const string BUTTON_POKEMON_VIEW_MOVES_FIRST = $"{BUTTON_POKEMON_BASE}_first";
		private const string BUTTON_POKEMON_VIEW_MOVES_BACK = $"{BUTTON_POKEMON_BASE}_back";
		private const string BUTTON_POKEMON_VIEW_MOVES_NEXT = $"{BUTTON_POKEMON_BASE}_next";
		private const string BUTTON_POKEMON_VIEW_MOVES_LAST = $"{BUTTON_POKEMON_BASE}_last";
		private const string BUTTON_POKEMON_LOAD_STRATEGIES = $"{BUTTON_POKEMON_BASE}_strats_load";

		private const string SELECT_POKEMON_BASE = "select_pokemon";
		private const string SELECT_POKEMON_STRATEGIES_GENERATION = $"{SELECT_POKEMON_BASE}_strats_generation";

		private readonly PokemonAPI _pokemonApi;

		public Pokemon(PokemonAPI pokemonApi)
		{
			this._pokemonApi = pokemonApi;
		}

		private async Task<MessageComponent> GetEmbedButtonsAsync(string pokemonName, string generationAbbreviation, PokemonEmbed embed, bool replaceEmojis, int movesStartingIndex = 0, int movesMaxIndex = 0)
		{
			DbPokemon pokemon = (await this._pokemonApi.SearchItemAsync(pokemonName, generationAbbreviation))[0].Item;

			ComponentBuilder components = new ComponentBuilder();

			// First row
			_ = components.WithButton("Info", $"{BUTTON_POKEMON_VIEW_INFO}:{pokemon.Name},{generationAbbreviation},{replaceEmojis}", ButtonStyle.Primary, Emoji.Parse("❤️"), disabled: embed == PokemonEmbed.Info);
			_ = components.WithButton("Moves", $"{BUTTON_POKEMON_VIEW_MOVES}:{pokemon.Name},{generationAbbreviation},{replaceEmojis}", ButtonStyle.Primary, Emoji.Parse("👊"), disabled: embed == PokemonEmbed.Moves);
			_ = components.WithButton("Strategies", $"{BUTTON_POKEMON_VIEW_STRATS}:{pokemon.Name},{generationAbbreviation},{replaceEmojis}", ButtonStyle.Primary, Emoji.Parse("🧠"), disabled: embed == PokemonEmbed.Strategies);

			if (!pokemon.IsNonstandard)
			{
				_ = components.WithButton("View on Pokédex", style: ButtonStyle.Link, url: PokemonAPIHelpers.GetPokedexUrl((int)pokemon.DexNumber));
			}
			_ = components.WithButton("View on Smogon", style: ButtonStyle.Link, url: PokemonAPIHelpers.GetSmogonPokemonUrl( pokemon.Name, generationAbbreviation));

			// Second row
			if (embed == PokemonEmbed.Moves)
			{
				_ = components.WithButton("First page", $"{BUTTON_POKEMON_VIEW_MOVES_FIRST}:{pokemon.Name},{generationAbbreviation},{replaceEmojis}", ButtonStyle.Secondary, Emoji.Parse("⏮️"), disabled: movesStartingIndex == 0, row: 1);
				_ = components.WithButton("Back", $"{BUTTON_POKEMON_VIEW_MOVES_BACK}:{pokemon.Name},{generationAbbreviation},{movesStartingIndex},{replaceEmojis}", ButtonStyle.Secondary, Emoji.Parse("◀️"), disabled: movesStartingIndex == 0, row: 1);
				_ = components.WithButton("Next", $"{BUTTON_POKEMON_VIEW_MOVES_NEXT}:{pokemon.Name},{generationAbbreviation},{movesStartingIndex},{replaceEmojis}", ButtonStyle.Secondary, Emoji.Parse("▶️"), disabled: movesStartingIndex + MOVES_PER_PAGE >= movesMaxIndex, row: 1);
				_ = components.WithButton("Last page", $"{BUTTON_POKEMON_VIEW_MOVES_LAST}:{pokemon.Name},{generationAbbreviation},{replaceEmojis}", ButtonStyle.Secondary, Emoji.Parse("⏭️"), disabled: movesStartingIndex + MOVES_PER_PAGE >= movesMaxIndex, row: 1);
			}

			return components.Build();
		}

		public class PokemonGenerationAutocompleter : AutocompleteHandler
		{
			public override async Task<AutocompletionResult> GenerateSuggestionsAsync(IInteractionContext context, IAutocompleteInteraction autocompleteInteraction, IParameterInfo parameter, IServiceProvider services)
			{
				using DiamondContext db = new DiamondContext();

				List<AutocompleteResult> autocompletions = new List<AutocompleteResult>();
				foreach (var dbGeneration in db.PokemonGenerations)
				{
					autocompletions.Add(new AutocompleteResult(dbGeneration.Name, dbGeneration.Abbreviation));
				}

				return AutocompletionResult.FromSuccess(autocompletions.Take(25));
			}
		}

		private enum PokemonEmbed
		{
			Info,
			Moves,
			Strategies,
		}

		private enum MovesEmbedPage
		{
			HasPrevious,
			HasNext,
			HasPreviousAndNext,
		}
	}
}
