using Diamond.API.APIs;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.World;

[Group("world", "Real world related commands.")]
public partial class World : InteractionModuleBase<SocketInteractionContext>
{
	private readonly OpenMeteoGeocoding _openMeteoGeocoding;
	private readonly OpenMeteoWeather _openMeteoWeather;

	public World(OpenMeteoGeocoding openMeteoGeocoding, OpenMeteoWeather openMeteoWeather)
	{
		this._openMeteoGeocoding = openMeteoGeocoding;
		this._openMeteoWeather = openMeteoWeather;
	}
}
