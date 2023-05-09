using Discord.Interactions;

namespace Diamond.API.SlashCommands.Music;

[Group("music", "Music related commands.")]
public partial class Music : InteractionModuleBase<SocketInteractionContext>
{
	private readonly Lava _lava;

	public Music(Lava lava)
	{
		this._lava = lava;
	}
}