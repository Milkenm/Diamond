﻿using System.Collections.Generic;
using System.Threading.Tasks;

using Diamond.API.APIs.Pokemon;
using Diamond.API.Attributes;
using Diamond.API.Helpers;
using Diamond.Data;
using Diamond.Data.Models.Pokemons;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Pokemon
{
	public partial class Pokemon
	{
		private const int MOVES_PER_PAGE = 10;

		[DSlashCommand("moves", "View a pokémon's moves.")]
		public async Task PokemonMovesCommandAsync(
			[Summary("name", "The name of the pokémon.")] string pokemonName,
			[Summary("generation", "The generation of the pokémon."), Autocomplete(typeof(PokemonGenerationAutocompleter))] string generationAbbreviation,
			[Summary("replace-emojis", "Replaces the type emojis with a text in case you a have trouble reading.")] bool replaceEmojis = false,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			await this.SendMovesEmbedAsync(pokemonName, generationAbbreviation, 0, replaceEmojis);
		}

		[ComponentInteraction($"{BUTTON_POKEMON_VIEW_MOVES}:*,*,*", true)]
		public async Task ButtonViewMovesHandler(string pokemonName, string generationAbbreviation, bool replaceEmojis)
		{
			await this.DeferAsync();

			await this.SendMovesEmbedAsync(pokemonName, generationAbbreviation, 0, replaceEmojis);
		}

		private async Task SendMovesEmbedAsync(string pokemonName, string generationAbbreviation, int startingIndex, bool replaceEmojis)
		{
			using DiamondContext db = new DiamondContext();

			DefaultEmbed embed = new DefaultEmbed("Pokédex - Moves", "👊", this.Context)
			{
				Title = pokemonName,
			};

			List<DbPokemonMove> movesList = await PokemonAPIHelpers.GetPokemonMovesAsync(pokemonName, generationAbbreviation, db);

			// Go to last page (startingIndex = -1)
			if (startingIndex == -1)
			{
				startingIndex = movesList.Count - (movesList.Count % MOVES_PER_PAGE);
				if (startingIndex == movesList.Count)
				{
					// This is in case a pokémon has a multipe of 10 moves (10, 20, 30... like Eevee)
					startingIndex -= MOVES_PER_PAGE;
				}
			}

			for (int i = startingIndex; i < startingIndex + MOVES_PER_PAGE && i < movesList.Count; i++)
			{
				DbPokemonMove dbMove = movesList[i];
				_ = embed.AddField($"{PokemonAPIHelpers.GetTypeEmoji(dbMove.Type)} {PokemonAPIHelpers.GetAttackTypeEmoji(dbMove.Category)} {dbMove.Name} (💥 {GetAttackStatString(dbMove.Power, Stat.Power)}   🎯 {GetAttackStatString(dbMove.Accuracy, Stat.Accuracy)}   ⚡ {GetAttackStatString(dbMove.PowerPoints, Stat.PowerPoints)})", dbMove.Description);
			}

			_ = await embed.SendAsync(await this.GetEmbedButtonsAsync(pokemonName, generationAbbreviation, PokemonEmbed.Moves, replaceEmojis, startingIndex, movesList.Count));
		}

		[ComponentInteraction($"{BUTTON_POKEMON_VIEW_MOVES_FIRST}:*,*,*", true)]
		public async Task ButtonMovesFirstPageHandlerAsync(string pokemonName, string generationAbbreviation, bool replaceEmojis)
		{
			await this.DeferAsync();

			await this.SendMovesEmbedAsync(pokemonName, generationAbbreviation, 0, replaceEmojis);
		}

		[ComponentInteraction($"{BUTTON_POKEMON_VIEW_MOVES_BACK}:*,*,*,*", true)]
		public async Task ButtonMovesBackHandlerAsync(string pokemonName, string generationAbbreviation, int startingIndex, bool replaceEmojis)
		{
			await this.DeferAsync();

			await this.SendMovesEmbedAsync(pokemonName, generationAbbreviation, startingIndex - MOVES_PER_PAGE, replaceEmojis);
		}

		[ComponentInteraction($"{BUTTON_POKEMON_VIEW_MOVES_NEXT}:*,*,*,*", true)]
		public async Task ButtonMovesNextHandlerAsync(string pokemonName, string generationAbbreviation, int startingIndex, bool replaceEmojis)
		{
			await this.DeferAsync();

			await this.SendMovesEmbedAsync(pokemonName, generationAbbreviation, startingIndex + MOVES_PER_PAGE, replaceEmojis);
		}

		[ComponentInteraction($"{BUTTON_POKEMON_VIEW_MOVES_LAST}:*,*,*", true)]
		public async Task ButtonMovesLastPageHandlerAsync(string pokemonName, string generationAbbreviation, bool replaceEmojis)
		{
			await this.DeferAsync();

			await this.SendMovesEmbedAsync(pokemonName, generationAbbreviation, -1, replaceEmojis);
		}
	}
}
