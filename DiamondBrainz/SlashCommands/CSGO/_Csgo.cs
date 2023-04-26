using Diamond.API.Stuff;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.CSGO;

[Group("csgo", "CS:GO related commands.")]
public partial class Csgo : InteractionModuleBase<SocketInteractionContext>
{
	private readonly CsgoBackpack _csgoBackpack;

	public Csgo(CsgoBackpack csgoBackpack)
	{
		this._csgoBackpack = csgoBackpack;
	}
}
