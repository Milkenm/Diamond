#region Using

using Discord;
using Discord.Commands;

using System.Threading.Tasks;

using static DiamondGui.Static;
using static ScriptsLib.Device;

#endregion Using

namespace DiamondGui
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		[Command("information"), Alias("info", "i", "invite", "ram", "uptime"), Summary("Displays some information about the bot.")]
		public async Task Information()
		{
			settings.CommandsUsed++;

			#region RAM

			EmbedFieldBuilder _EmbedField_RAM = new EmbedFieldBuilder();
			_EmbedField_RAM.IsInline = true;
			_EmbedField_RAM.Name = "RAM (Free | Max)";
			_EmbedField_RAM.Value = $"{GetRAM(RAMType.Free)} | {GetRAM(RAMType.Max)} GB";

			#endregion RAM

			#region Uptime

			EmbedFieldBuilder _EmbedField_Uptime = new EmbedFieldBuilder();
			_EmbedField_Uptime.IsInline = true;
			_EmbedField_Uptime.Name = "Uptime (Session | Total)";

			#endregion Uptime

			#region Invite Link

			EmbedFieldBuilder _EmbedField_InviteLink = new EmbedFieldBuilder();
			_EmbedField_InviteLink.Name = $"➖➖➖➖➖➖➖➖➖➖➖➖➖➖➖➖💠 **__Invite Link:__** {settings.BotUrl} 💠";
			_EmbedField_InviteLink.Value = "\n➖➖➖➖➖➖➖➖➖➖➖➖➖➖➖➖";

			#endregion Invite Link

			EmbedBuilder embed = new EmbedBuilder();
			embed.WithAuthor("Miłkenm 💦#6376", $"{settings.Domain}/Static/Img/DevIcon.png", settings.DiscordUrl);
			embed.WithFields(_EmbedField_RAM, _EmbedField_Uptime, _EmbedField_InviteLink);
			embed.WithThumbnailUrl($"{settings.Domain}/Static/Img/DiamondIcon.png");
			embed.WithColor(new Color(255, 255, 255));

			await ReplyAsync(embed: embed.Build()).ConfigureAwait(false);
		}
	}
}