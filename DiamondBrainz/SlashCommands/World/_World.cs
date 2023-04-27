using Diamond.API.APIs;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.World;

[Group("world", "Real world related commands.")]
public partial class World : InteractionModuleBase<SocketInteractionContext>
{
	OpenMeteoGeocoding _openMeteoGeocoding;

	public World(OpenMeteoGeocoding openMeteoGeocoding)
	{
		this._openMeteoGeocoding = openMeteoGeocoding;
	}
}
