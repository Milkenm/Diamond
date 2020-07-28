using Discord.Commands;
using Discord.WebSocket;

using System.Threading.Tasks;

namespace DiamondGui.Commands
{
	public class Member_Count : ModuleBase<SocketCommandContext>
	{
		[Command("membercount"), Alias("mb"), Summary("Member Count")]
		public async Task CMD_Member()
		{
			int userCount = 0, botCount = 0;
			foreach (SocketGuildUser user in Context.Guild.Users)
			{
				if (!user.IsBot)
				{
					userCount++;
				}
				else
				{
					botCount++;
				}
			}

			await Context.Channel.SendMessageAsync($"Existem **{userCount} utilizadores** e **{botCount} bots** neste servidor para um **total de {userCount + botCount} membros**.").ConfigureAwait(false);
		}
	}
}