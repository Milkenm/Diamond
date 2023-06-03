using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Schemes.Smogon;
using Diamond.API.Util;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Pokemon
{
	public partial class Pokemon
	{
		[DSlashCommand("search", "Search for a pokémon.")]
		public async Task PokemonSearchCommandAsync(
			[Summary("name", "The name of the pokémon to search for.")] string name,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			PokemonInfo pokemon = this._pokeApi.SearchPokemon(name);

			DefaultEmbed embed = new DefaultEmbed("Pokémon Search", "🥎", this.Context)
			{
				Title = pokemon.name,
				ThumbnailUrl = this._pokeApi.GetPokemonImage(pokemon.name),
			};
			_ = await embed.SendAsync();
		}
	}
}
