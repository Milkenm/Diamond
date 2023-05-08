using Diamond.API.Bot;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Services;
[Group("service", "Service commands, like APIs and webhooks.")]
public partial class Services : InteractionModuleBase<SocketInteractionContext>
{
	private readonly DiamondBot _bot;

	public Services(DiamondBot bot)
	{
		_bot = bot;
	}
}
