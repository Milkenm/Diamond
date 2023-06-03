using Diamond.API.APIs;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Pokemon
{
	[Group("pokemon", "Commands related to the 'Pokémon' game.")]
	public partial class Pokemon : InteractionModuleBase<SocketInteractionContext>
	{
		private readonly PokemonAPI _pokeApi;

		public Pokemon(PokemonAPI pokeApi)
		{
			this._pokeApi = pokeApi;
		}
	}
}
