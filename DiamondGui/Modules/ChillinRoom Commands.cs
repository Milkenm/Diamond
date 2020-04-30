using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace DiamondGui
{
	public class ChillinRoomModule : ModuleBase<SocketCommandContext>
	{
		internal static Task DiscordLinkPosted(SocketMessage msg)
		{
			bool hasRole = false;
			foreach (SocketRole role in ((SocketGuildUser)msg.Author).Roles)
			{
				if (role.Id == 684151586083045411)
				{
					hasRole = true;
				}
			}

			if (msg.Content.Contains("discord.gg") && !hasRole)
			{

				msg.Channel.DeleteMessageAsync(msg.Id);
				msg.Channel.SendMessageAsync($"{msg.Author.Mention}, to send Discord Server Links you need the 📮 Postman role.");
			}

			return Task.CompletedTask;
		}
	}
}