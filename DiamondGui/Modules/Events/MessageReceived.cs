#region Usings
using System.Threading.Tasks;

using Discord.WebSocket;
#endregion Usings



namespace DiamondGui
{
	internal static partial class EventsModule
	{
		internal static async Task MessageReceived(SocketMessage msg)
		{
			await msg.Channel.SendMessageAsync(msg.Author.ToString());

			if (!msg.Author.IsBot)
			{
				if (msg.Content == "(╯°□°）╯︵ ┻━┻")
				{
					await msg.Channel.SendMessageAsync("┬─┬ ノ( ゜-゜ノ)");
				}
				if (msg.Content.Contains("discord.gg"))
				{
					await ChillinRoomModule.DiscordLinkPosted(msg);
				}
			}
		}
	}
}
