using System.Threading.Tasks;

using Diamond.API.APIs.Pokemon;
using Diamond.Data.Models.Pokemons;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Pokemon
{
	[Group("pokemon", "Commands related to the 'Pokémon' game.")]
	public partial class Pokemon : InteractionModuleBase<SocketInteractionContext>
	{
		private const string BUTTON_POKEMON_BASE = "button_pokemon";
		private const string BUTTON_POKEMON_VIEW_MOVES = $"{BUTTON_POKEMON_BASE}_moves";
		private const string BUTTON_POKEMON_VIEW_STRATS = $"{BUTTON_POKEMON_BASE}_builds";
		private const string BUTTON_POKEMON_VIEW_INFO = $"{BUTTON_POKEMON_BASE}_stats";
		private const string BUTTON_POKEMON_VIEW_MOVES_FIRST = $"{BUTTON_POKEMON_BASE}_FIRST";
		private const string BUTTON_POKEMON_VIEW_MOVES_BACK = $"{BUTTON_POKEMON_BASE}_BACK";
		private const string BUTTON_POKEMON_VIEW_MOVES_NEXT = $"{BUTTON_POKEMON_BASE}_NEXT";
		private const string BUTTON_POKEMON_VIEW_MOVES_LAST = $"{BUTTON_POKEMON_BASE}_LAST";

		private readonly PokemonAPI _pokemonApi;

		public Pokemon(PokemonAPI pokemonApi)
		{
			this._pokemonApi = pokemonApi;
		}

		private async Task<MessageComponent> GetEmbedButtonsAsync(string pokemonName, PokemonEmbed embed, bool replaceEmojis, int movesStartingIndex = 0, int movesMaxIndex = 0)
		{
			DbPokemon pokemon = (await this._pokemonApi.SearchItemAsync(pokemonName))[0].Item;

			ComponentBuilder components = new ComponentBuilder();

			// First row
			if (embed != PokemonEmbed.Info)
			{
				_ = components.WithButton("Info", $"{BUTTON_POKEMON_VIEW_INFO}:{pokemon.Name},{replaceEmojis}", ButtonStyle.Primary, Emoji.Parse("❤️"));
			}
			if (embed != PokemonEmbed.Moves)
			{
				_ = components.WithButton("Moves", $"{BUTTON_POKEMON_VIEW_MOVES}:{pokemon.Name},{replaceEmojis}", ButtonStyle.Primary, Emoji.Parse("👊"));
			}
			if (embed != PokemonEmbed.Strategies)
			{
				_ = components.WithButton("Strategies", $"{BUTTON_POKEMON_VIEW_STRATS}:{pokemon.Name},{replaceEmojis}", ButtonStyle.Primary, Emoji.Parse("🧠"));
			}

			if (!pokemon.IsNonstandard)
			{
				_ = components.WithButton("View on Pokédex", style: ButtonStyle.Link, url: PokemonAPIHelpers.GetPokedexUrl((int)pokemon.DexNumber));
			}
			_ = components.WithButton("View on Smogon", style: ButtonStyle.Link, url: PokemonAPIHelpers.GetSmogonUrl(pokemon.Name));

			// Second row
			if (embed == PokemonEmbed.Moves)
			{
				_ = components.WithButton("First page", $"{BUTTON_POKEMON_VIEW_MOVES_FIRST}:{pokemon.Name},{replaceEmojis}", ButtonStyle.Secondary, Emoji.Parse("⏮️"), disabled: movesStartingIndex == 0, row: 1);
				_ = components.WithButton("Back", $"{BUTTON_POKEMON_VIEW_MOVES_BACK}:{pokemon.Name},{movesStartingIndex},{replaceEmojis}", ButtonStyle.Secondary, Emoji.Parse("◀️"), disabled: movesStartingIndex == 0, row: 1);
				_ = components.WithButton("Next", $"{BUTTON_POKEMON_VIEW_MOVES_NEXT}:{pokemon.Name},{movesStartingIndex},{replaceEmojis}", ButtonStyle.Secondary, Emoji.Parse("▶️"), disabled: movesStartingIndex + MOVES_PER_PAGE >= movesMaxIndex, row: 1);
				_ = components.WithButton("Last page", $"{BUTTON_POKEMON_VIEW_MOVES_LAST}:{pokemon.Name},{replaceEmojis}", ButtonStyle.Secondary, Emoji.Parse("⏭️"), disabled: movesStartingIndex + MOVES_PER_PAGE >= movesMaxIndex, row: 1);
			}

			return components.Build();
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
