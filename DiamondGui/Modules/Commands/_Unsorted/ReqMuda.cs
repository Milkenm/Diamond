#region Usings
using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using static DiamondGui.Functions;
#endregion Usings



namespace DiamondGui
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		[Command("reqmuda"), Summary("MUDA MUDA MUDA MUDA!")]
		public async Task CMD_ReqMuda()
		{
			try
			{
				EmbedBuilder embed = new EmbedBuilder();
				embed.WithImageUrl("https://media.tenor.com/images/1bafe0ee0ea1649b5b342ce894d98675/tenor.gif");
				embed.WithTitle("MUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDA");

				await ReplyAsync(embed: embed.Build());
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}