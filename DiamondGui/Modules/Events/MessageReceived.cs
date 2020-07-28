using Discord.WebSocket;

using System.Threading.Tasks;

namespace DiamondGui
{
	internal static partial class EventsModule
	{
		internal static async Task MessageReceived(SocketMessage msg)
		{
			if (msg.Content.Contains("discord.gg"))
			{
				await ChillinRoomModule.DiscordLinkPosted(msg).ConfigureAwait(false);
			}
			if (msg.Content == "(╯°□°）╯︵ ┻━┻")
			{
				await msg.Channel.SendMessageAsync("┬─┬ ノ( ゜-゜ノ)").ConfigureAwait(false);
			}
		}
	}
}