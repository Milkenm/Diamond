#region Using
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

using Discord;
using Discord.Commands;
using ScriptsLib.Database;
#endregion



namespace Diamond.Commands
{
	public class Information : ModuleBase<SocketCommandContext>
	{
		[Command("information"), Alias("info", "i"), Summary("Displays some information about the bot.")]
		public async Task CMD_Information()
		{
			try
			{
				SlDatabase _Database = new SlDatabase();
				Program program = new Program();

				string _Host = _Database.Select("DConfig", "Value", "Config = 'Host'")[0];


				#region RAM
				var _EmbedField_RAM = new EmbedFieldBuilder();
				_EmbedField_RAM.IsInline = true;
				_EmbedField_RAM.Name = "RAM (Free | Max)";
				_EmbedField_RAM.Value = $"{Scripts.device.RAM(Scripts.Device.RAMType.Free)} | {Scripts.device.RAM(Scripts.Device.RAMType.Max)} GB";
				#endregion RAM
				
				#region Uptime
				var _EmbedField_Uptime = new EmbedFieldBuilder();
				_EmbedField_Uptime.IsInline = true;
				_EmbedField_Uptime.Name = "Uptime (Session | Total)";
				_EmbedField_Uptime.Value = $"{Program._Uptime} | {Convert.ToInt32(_Database.Select("DData", "Value", "Data = 'Uptime'")[0]) + Program._Uptime} seconds";
				#endregion Uptime

				#region Invite Link
				var _EmbedField_InviteLink = new EmbedFieldBuilder();
				_EmbedField_InviteLink.Name = "➖➖➖➖➖➖➖➖➖➖➖➖➖➖➖➖Invite Link";
				_EmbedField_InviteLink.Value = $"{_Database.Select("DConfig", "Value", "Config = 'InviteLink'")[0]} \n➖➖➖➖➖➖➖➖➖➖➖➖➖➖➖➖";
				#endregion Invite Link



				var embed = new EmbedBuilder();
				embed.WithAuthor("Miłkenm 💦#6376", $"{_Host}_diamond/Developer_Icon.png", "https://discord.gg/uJcXEq6");
				embed.WithFields(_EmbedField_RAM, _EmbedField_Uptime, _EmbedField_InviteLink);
				embed.WithThumbnailUrl($"{_Host}_diamond/Diamond_Icon.png");
				embed.WithColor(Scripts.discord.Color("white"));

				await ReplyAsync(embed: embed.Build());
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message.ToString());
			}
		}
	}
}