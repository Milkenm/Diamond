using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Diamond.API.APIs.Pokemon;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.Pokemons;

using Discord;
using Discord.Interactions;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.Pokemon
{
	[Group("pokemon", "Commands related to the 'Pokémon' game.")]
	public partial class Pokemon : InteractionModuleBase<SocketInteractionContext>
	{
		private readonly PokemonAPI _pokemonApi;

		public Pokemon(PokemonAPI pokemonApi)
		{
			this._pokemonApi = pokemonApi;
		}

		private MessageComponent GetEmbedButtons(string pokemonName, string generationAbbreviation, PokemonEmbed embed, bool replaceEmojis, bool showEveryone, DiamondContext db, int movesStartingIndex = 0, int movesMaxIndex = 0)
		{
			Pokeporco poke = new Pokeporco(pokemonName, db);
			DbPokemon dbPokemon = poke.GetFullFromGeneration(generationAbbreviation);

			ComponentBuilder components = new ComponentBuilder();

			// We have to use this because the rows thing has to be in order else it breaks
			int rowIndex = 0;

			if (!showEveryone)
			{
				_ = components.WithButton("Info", $"{PokemonComponentIds.BUTTON_POKEMON_VIEW_INFO}:{dbPokemon.Name},{generationAbbreviation},{replaceEmojis}", ButtonStyle.Primary, Emoji.Parse("❤️"), disabled: embed == PokemonEmbed.Info, row: rowIndex);
				_ = components.WithButton("Moves", $"{PokemonComponentIds.BUTTON_POKEMON_VIEW_MOVES}:{dbPokemon.Name},{generationAbbreviation},{replaceEmojis}", ButtonStyle.Primary, Emoji.Parse("👊"), disabled: embed == PokemonEmbed.Moves, row: rowIndex);
				_ = components.WithButton("Strategies", $"{PokemonComponentIds.BUTTON_POKEMON_VIEW_STRATS}:{dbPokemon.Name},{generationAbbreviation},{replaceEmojis}", ButtonStyle.Secondary, Emoji.Parse("🧠"), disabled: embed == PokemonEmbed.Strategies || true, row: rowIndex);
				rowIndex++;
			}

			if (!showEveryone)
			{
				if (embed == PokemonEmbed.Moves)
				{
					_ = components.WithButton("First page", $"{PokemonComponentIds.BUTTON_POKEMON_VIEW_MOVES_FIRST}:{dbPokemon.Name},{generationAbbreviation},{replaceEmojis}", ButtonStyle.Secondary, Emoji.Parse("⏮️"), disabled: movesStartingIndex == 0, row: rowIndex);
					_ = components.WithButton("Back", $"{PokemonComponentIds.BUTTON_POKEMON_VIEW_MOVES_BACK}:{dbPokemon.Name},{generationAbbreviation},{movesStartingIndex},{replaceEmojis}", ButtonStyle.Secondary, Emoji.Parse("◀️"), disabled: movesStartingIndex == 0, row: rowIndex);
					_ = components.WithButton("Next", $"{PokemonComponentIds.BUTTON_POKEMON_VIEW_MOVES_NEXT}:{dbPokemon.Name},{generationAbbreviation},{movesStartingIndex},{replaceEmojis}", ButtonStyle.Secondary, Emoji.Parse("▶️"), disabled: movesStartingIndex + MOVES_PER_PAGE >= movesMaxIndex, row: rowIndex);
					_ = components.WithButton("Last page", $"{PokemonComponentIds.BUTTON_POKEMON_VIEW_MOVES_LAST}:{dbPokemon.Name},{generationAbbreviation},{replaceEmojis}", ButtonStyle.Secondary, Emoji.Parse("⏭️"), disabled: movesStartingIndex + MOVES_PER_PAGE >= movesMaxIndex, row: rowIndex);
					rowIndex++;
				}

				List<SelectMenuOptionBuilder> infoGenerationsList = new List<SelectMenuOptionBuilder>();
				foreach (DbPokemonGeneration dbGeneration in dbPokemon.GenerationsList)
				{
					this.AddGeneration(infoGenerationsList, dbGeneration, dbGeneration.Abbreviation == generationAbbreviation);
				}
				string selectMenuId = embed switch
				{
					PokemonEmbed.Info => PokemonComponentIds.SELECT_POKEMON_INFO_GENERATION,
					PokemonEmbed.Moves => PokemonComponentIds.SELECT_POKEMON_MOVES_GENERATION,
					PokemonEmbed.Strategies => PokemonComponentIds.SELECT_POKEMON_STRATEGIES_GENERATION,
					_ => throw new ArgumentOutOfRangeException(),
				};
				_ = components.WithSelectMenu($"{selectMenuId}:{pokemonName},{replaceEmojis}", infoGenerationsList, row: rowIndex);
				rowIndex++;
			}

			if (!showEveryone)
			{
				string buttonId = embed switch
				{
					PokemonEmbed.Info => $"{PokemonComponentIds.BUTTON_POKEMON_SHARE_INFO}:{dbPokemon.Name},{dbPokemon.GenerationAbbreviation},{replaceEmojis}",
					PokemonEmbed.Moves => $"{PokemonComponentIds.BUTTON_POKEMON_SHARE_MOVES}:{dbPokemon.Name},{dbPokemon.GenerationAbbreviation},{replaceEmojis},{movesStartingIndex}",
					PokemonEmbed.Strategies => $"{PokemonComponentIds.BUTTON_POKEMON_SHARE_STRATEGIES}:{dbPokemon.Name},{dbPokemon.GenerationAbbreviation},{replaceEmojis}",
				};
				_ = components.WithButton("Share", buttonId, style: ButtonStyle.Secondary, emote: Emoji.Parse("📲"), disabled: true, row: rowIndex);
			}
			if (!dbPokemon.IsNonstandard || dbPokemon.DexNumber > 0)
			{
				_ = components.WithButton("View on Pokédex", style: ButtonStyle.Link, url: poke.GetPokedexUrl(), row: rowIndex);
			}
			_ = components.WithButton("View on Smogon", style: ButtonStyle.Link, url: poke.GetSmogonPokemonUrl(dbPokemon.GenerationAbbreviation), row: rowIndex);

			return components.Build();
		}

		private void CheckItemsLoading()
		{
			while (this._pokemonApi.IsLoadingItems)
			{
				Thread.Sleep(250);
			}
		}

		private void AddGeneration(List<SelectMenuOptionBuilder> optionsList, DbPokemonGeneration dbGeneration, bool isSelected)
		{
			optionsList.Add(new SelectMenuOptionBuilder($"{dbGeneration.Name} ({dbGeneration.Abbreviation})", dbGeneration.Abbreviation, isDefault: isSelected));
		}

		public class PokemonNameAutocompleter : AutocompleteHandler
		{
			private readonly PokemonAPI _pokemonApi;

			public PokemonNameAutocompleter(PokemonAPI pokemonApi)
			{
				this._pokemonApi = pokemonApi;
			}

			public override async Task<AutocompletionResult> GenerateSuggestionsAsync(IInteractionContext context, IAutocompleteInteraction autocompleteInteraction, IParameterInfo parameter, IServiceProvider services)
			{
				List<AutocompleteResult> autocompletions = new List<AutocompleteResult>();

				if (!autocompleteInteraction.Data.Current.Value.ToString().IsEmpty())
				{
					List<SearchMatchInfo<DbPokemon>> searchResult = await this._pokemonApi.SearchItemAsync(autocompleteInteraction.Data.Current.Value.ToString());

					foreach (SearchMatchInfo<DbPokemon> pokemonSmi in searchResult.AsEnumerable().DistinctBy(smi => smi.Item.Name))
					{
						autocompletions.Add(new AutocompleteResult(pokemonSmi.Item.Name, pokemonSmi.Item.Name));
					}
				}
				else
				{
					using DiamondContext db = new DiamondContext();

					foreach (DbPokemon dbPokemon in db.Pokemons.AsEnumerable().DistinctBy(p => p.Name).OrderBy(p => p.Name))
					{
						autocompletions.Add(new AutocompleteResult(dbPokemon.Name, dbPokemon.Name));
					}
				}

				return AutocompletionResult.FromSuccess(autocompletions.Take(25));
			}
		}

		public class PokemonGenerationAutocompleter : AutocompleteHandler
		{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
			public override async Task<AutocompletionResult> GenerateSuggestionsAsync(IInteractionContext context, IAutocompleteInteraction autocompleteInteraction, IParameterInfo parameter, IServiceProvider services)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
			{
				using DiamondContext db = new DiamondContext();

				List<AutocompleteResult> autocompletions = new List<AutocompleteResult>();

				if (!autocompleteInteraction.Data.Current.Value.ToString().IsEmpty())
				{
					List<SearchMatchInfo<DbPokemonGeneration>> searchResult = Utils.Search(PokemonUtils.GetGenerationsMap(db), autocompleteInteraction.Data.Current.Value.ToString());

					foreach (SearchMatchInfo<DbPokemonGeneration> smiGeneration in searchResult)
					{
						autocompletions.Add(new AutocompleteResult(smiGeneration.Item.Name, smiGeneration.Item.Abbreviation));
					}
				}
				else
				{
					foreach (DbPokemonGeneration dbGeneration in db.PokemonGenerations.AsEnumerable().DistinctBy(g => g.Name).OrderBy(g => g.Name))
					{
						autocompletions.Add(new AutocompleteResult(dbGeneration.Name, dbGeneration.Abbreviation));
					}
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
