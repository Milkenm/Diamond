#region Using
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

using Discord;
using Discord.Commands;
#endregion



namespace DiamondGui.Commands
{
	internal class Information : ModuleBase<SocketCommandContext>
	{
		[Command("information"), Alias("info", "i"), Summary("Displays some information about the bot.")]
		internal async Task CMD_Information()
		{
			try
			{
				#region RAM
				var _EmbedField_RAM = new EmbedFieldBuilder();
				_EmbedField_RAM.IsInline = true;
				_EmbedField_RAM.Name = "RAM (Free | Max)";
				_EmbedField_RAM.Value = $"<free ram> | <max ram> GB";
				#endregion RAM
				
				#region Uptime
				var _EmbedField_Uptime = new EmbedFieldBuilder();
				_EmbedField_Uptime.IsInline = true;
				_EmbedField_Uptime.Name = "Uptime (Session | Total)";
				_EmbedField_Uptime.Value = $"<uptime> | <total uptime> seconds";
				#endregion Uptime

				#region Invite Link
				var _EmbedField_InviteLink = new EmbedFieldBuilder();
				_EmbedField_InviteLink.Name = "➖➖➖➖➖➖➖➖➖➖➖➖➖➖➖➖Invite Link";
				_EmbedField_InviteLink.Value = $"<invite link> \n➖➖➖➖➖➖➖➖➖➖➖➖➖➖➖➖";
				#endregion Invite Link



				var embed = new EmbedBuilder();
				embed.WithAuthor("Miłkenm 💦#6376", $"<host>_diamond/Developer_Icon.png", "https://discord.gg/uJcXEq6");
				embed.WithFields(_EmbedField_RAM, _EmbedField_Uptime, _EmbedField_InviteLink);
				embed.WithThumbnailUrl($"<host>_diamond/Diamond_Icon.png");
				embed.WithColor(new Color(255,255,255));

				await ReplyAsync(embed: embed.Build());
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message.ToString());
			}
		}
	}
}