#region Using
using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using static DiamondGui.Static;
using static DiamondGui.Functions;
#endregion



namespace DiamondGui
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		[Command("information"), Alias("info", "i", "invite", "ram", "uptime"), Summary("Displays some information about the bot.")]
		public async Task Information()
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
				_EmbedField_Uptime.Value = $"{CurrentUptime} | {Settings.Uptime} seconds";
				#endregion Uptime

				#region Invite Link
				var _EmbedField_InviteLink = new EmbedFieldBuilder();
				_EmbedField_InviteLink.Name = $"➖➖➖➖➖➖➖➖➖➖➖➖➖➖➖➖💠 **__Invite Link:__** {Settings.BotUrl} 💠";
				_EmbedField_InviteLink.Value = "\n➖➖➖➖➖➖➖➖➖➖➖➖➖➖➖➖";
				#endregion Invite Link



				var embed = new EmbedBuilder();
				embed.WithAuthor("Miłkenm 💦#6376", $"{Settings.Domain}/Static/Img/DevIcon.png", Settings.DiscordUrl);
				embed.WithFields(_EmbedField_RAM, _EmbedField_Uptime, _EmbedField_InviteLink);
				embed.WithThumbnailUrl($"{Settings.Domain}/Static/Img/DiamondIcon.png");
				embed.WithColor(new Color(255, 255, 255));

				await ReplyAsync(embed: embed.Build());
			}
			catch (Exception _Exception) { ShowException(_Exception, "DiamondGui.CommandsModule.Information()"); }
		}
	}
}