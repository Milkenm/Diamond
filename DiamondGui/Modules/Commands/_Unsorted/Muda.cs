#region Usings
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Discord;
using Discord.Commands;

using static DiamondGui.Functions;
#endregion Usings



namespace DiamondGui
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		[Command("muda"), Summary("MUDA MUDA MUDA MUDA!")]
		public async Task CMD_Muda()
		{
			try
			{
				EmbedBuilder embed = new EmbedBuilder();
				embed.WithImageUrl("https://gifimage.net/wp-content/uploads/2017/08/muda-muda-muda-gif-17.gif");
				embed.WithTitle("MUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDAMUDA");

				await ReplyAsync(embed: embed.Build());

				var sizee = TextRenderer.MeasureText("Hello there", new Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Pixel));

				MessageBox.Show(Math.Round((decimal)sizee.Width, 0, MidpointRounding.AwayFromZero).ToString());
				MessageBox.Show(Math.Round((decimal)sizee.Height, 0, MidpointRounding.AwayFromZero).ToString());
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}