using Diamond.Brainz.Utils;

using Discord;
using Discord.Commands;

using System.Linq;
using System.Threading.Tasks;

namespace Diamond.Brainz.Commands
{
	public partial class DiscordModule : ModuleBase<SocketCommandContext>
	{
		[Name("Avatar"), Command("avatar"), Summary("Gets the avatar image of the selected user.")]
		public async Task Avatar(IUser user, int size = 256)
		{
			int[] allowedSizes = { 16, 32, 64, 128, 256, 512 };

			if (allowedSizes.Contains(size) == false)
			{
				string allowedSizesString = string.Join(",", allowedSizes.Select(num => num.ToString()).ToArray());
				await this.ReplyAsync(embed: new EmbedBuilder().GenerateErrorEmbed("**Invalid size!**\nValid sizes: " + allowedSizesString, this.Context)).ConfigureAwait(false);
			}
			else
			{
				string avatar = user.GetAvatarUrl();
				avatar = avatar.Replace("?size=128", "?size=" + size);

				EmbedBuilder embed = new EmbedBuilder();
				embed.WithAuthor("Avatar", Twemoji.GetEmojiUrlFromEmoji("📸"));
				embed.AddField("**User**", user.Mention);
				embed.AddField("**Size**", $"{size}²px");
				embed.WithImageUrl(avatar);

				await this.ReplyAsync(embed: embed.FinishEmbed(this.Context)).ConfigureAwait(false);
			}
		}
	}
}