using System;
using System.Threading.Tasks;

using Diamond.API.Attributes;

using Discord.Interactions;

namespace Diamond.API.SlashCommands;
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

		DefaultEmbed embed = new DefaultEmbed("Info", "ℹ️", this.Context.Interaction);
		embed.AddField("💻 Developer", "<@222114807887691777>");
		embed.AddField("🏷️ Version", $"v1.0", true);
		embed.AddField("⏰ Delay", $"{msDelay}ms", true);
		embed.WithThumbnailUrl(avatar);

		await embed.SendAsync();
	}
}
