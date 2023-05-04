using Discord.Interactions;

namespace Diamond.API.SlashCommands.Music;
[Group("music", "Music related commands.")]
public partial class Music : InteractionModuleBase<SocketInteractionContext>
{
	private readonly Lavanode _lavanode;

	public Music(Lavanode lavanode)
	{
		this._lavanode = lavanode;
	}
}
