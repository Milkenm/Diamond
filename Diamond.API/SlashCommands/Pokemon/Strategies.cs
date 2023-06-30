using System.Collections.Generic;
using System.Threading.Tasks;

using Diamond.API.APIs.Pokemon;
using Diamond.API.Attributes;
using Diamond.API.Helpers;
using Diamond.Data.Models.Pokemons;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Pokemon
{
	public partial class Pokemon
	{
		[DSlashCommand("strats", "View strategies for a pokémon.")]
		public async void StratsCommandHandler(
			[Summary("name", "The name of the pokémon.")] string pokemonName,
			[Summary("replace-emojis", "Replaces the type emojis with a text in case you a have trouble reading.")] bool replaceEmojis = false,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			await this.SendStratsEmbed(pokemonName, replaceEmojis);
		}

		[ComponentInteraction($"{BUTTON_POKEMON_VIEW_STRATS}:*,*", true)]
		public async Task ButtonViewStrategiesHandler(string pokemonName, bool replaceEmojis)
		{
			await this.DeferAsync();

			await this.SendStratsEmbed(pokemonName, replaceEmojis);
		}

		private async Task SendStratsEmbed(string pokemonName, bool replaceEmojis)
		{
			_ = await PokemonAPIHelpers.GetStrategiesForPokemonAsync(pokemonName);

			DefaultEmbed embed = new DefaultEmbed("Pokédex - Strategies", "🧠", this.Context)
			{
				Title = pokemonName,
			};

			List<DbPokemonStrategy> strategiesList = await PokemonAPIHelpers.GetPokemonStrategies(pokemonName);

			embed.Description = strategiesList[0].Comments;

			_ = await embed.SendAsync(await this.GetEmbedButtonsAsync(pokemonName, PokemonEmbed.Strategies, replaceEmojis));
		}
	}
}
