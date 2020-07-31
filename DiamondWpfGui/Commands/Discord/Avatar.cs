using Diamond.WPF.Utils;

using Discord;
using Discord.Commands;

using System.Linq;
using System.Threading.Tasks;

namespace Diamond.WPF.Commands
{
    public partial class Discord_Module : ModuleBase<SocketCommandContext>
    {
        [Name("Avatar"), Command("avatar"), Summary("Gets the avatar image of the selected user.")]
        public async Task Avatar(IUser user, int size = 256)
        {
            int[] allowedSizes = { 16, 32, 64, 128, 256, 512 };

            if (!allowedSizes.Contains(size))
            {
                string allowedSizesString = string.Join(",", allowedSizes.Select(num => num.ToString()).ToArray());
                await ReplyAsync(embed: Embeds.GenerateErrorEmbed("**Invalid size!**\nValid sizes: " + allowedSizesString, Context)).ConfigureAwait(false);
            }
            else
            {
                string avatar = user.GetAvatarUrl();
                avatar = avatar.Replace("?size=128", "?size=" + size);

                EmbedBuilder embed = new EmbedBuilder()
                {
                    Title = "📸 Avatar",
                    Description = "**User:** " + user.Mention + "\n**Size:** x" + size,
                    ImageUrl = avatar,
                };

                await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);
            }
        }
    }
}