#region Usings
using System;
using System.Drawing.Imaging;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using static DiamondGui.Functions;
#endregion Usings



namespace DiamondGui
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		[Command("ora"), Summary("ORA ORA ORA ORA!")]
		public async Task CMD_Ora()
		{
			try
			{
				EmbedBuilder embed = new EmbedBuilder();
				embed.WithImageUrl("https://media1.tenor.com/images/4795d34aa49ada5299453dfa9960ee40/tenor.gif");
				embed.WithTitle("ORAORAORAORAORAORAORAORAORAORAORAORAORAORAORAORAORAORAORAORA");

				await ReplyAsync(embed: embed.Build());
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}

		public System.Drawing.Image[] splitGif(string pathToGifFile)
		{
			System.Drawing.Image gif = System.Drawing.Image.FromFile(pathToGifFile);
			return ScriptsLib.Tools.GetGifFrames(gif);
		}
	}
}