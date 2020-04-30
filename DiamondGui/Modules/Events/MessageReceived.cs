#region Usings
using System.Threading.Tasks;

using Discord.WebSocket;
#endregion Usings



namespace DiamondGui
{
	internal static partial class EventsModule
	{
		internal static Task MessageReceived(SocketMessage msg)
		{
			if (!msg.Author.IsBot)
			{
				if (msg.Content == "(╯°□°）╯︵ ┻━┻")
				{
					msg.Channel.SendMessageAsync("┬─┬ ノ( ゜-゜ノ)");
				}
				if (msg.Content.Contains("discord.gg"))
				{
					ChillinRoomModule.DiscordLinkPosted(msg);
				}
			}

			return Task.CompletedTask;
		}
	}
}
