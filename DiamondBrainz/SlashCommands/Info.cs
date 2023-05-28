using System;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Util;

using Discord.Interactions;

namespace Diamond.API.SlashCommands
{
	public class Info : InteractionModuleBase<SocketInteractionContext>
	{
		[DSlashCommand("info", "Shows info about the bot.")]
		public async Task InfoCommandAsync(
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			string avatar = this.Context.Client.CurrentUser.GetAvatarUrl();
			avatar = avatar.Replace("?size=128", "?size=" + 512);
			long msDelay = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - this.Context.Interaction.CreatedAt.ToUnixTimeMilliseconds();

			DefaultEmbed embed = new DefaultEmbed("Info", "ℹ️", this.Context);
			_ = embed.AddField("💻 Developer", "<@222114807887691777>");
			_ = embed.AddField("🏷 Version", $"v" + , true);
			_ = embed.AddField("⏰ Ping", $"{msDelay}ms", true);
			_ = embed.WithThumbnailUrl(avatar);

			_ = await embed.SendAsync();
		}
	}
}