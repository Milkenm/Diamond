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
				hasRole = role.Id == 684151586083045411;
			}

			if (msg.Content.Contains("discord.gg") && !hasRole)
			{
				msg.Channel.DeleteMessageAsync(msg.Id);
				msg.Channel.SendMessageAsync($"{msg.Author.Mention}, to send Discord Server Links you need the <@&684151586083045411> role so you can access <#684155144451260488>.");
			}

			return Task.CompletedTask;
		}
	}
}