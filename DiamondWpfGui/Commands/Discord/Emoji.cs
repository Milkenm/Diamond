using Diamond.WPF.Utils;

using Discord;
using Discord.Commands;

using System.Threading.Tasks;

namespace Diamond.WPF.Commands
{
    public partial class Discord_Module : ModuleBase<SocketCommandContext>
    {
        [Name("Emoji"), Command("emoji"), Alias("twemoji"), Summary("Gets the provided emoji.")]
        public async Task Emoji(string emoji)
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor("Twemoji", Twemoji.GetEmojiUrlFromEmoji("😃"));
            embed.WithImageUrl(Twemoji.GetEmojiUrlFromEmoji(emoji));

            await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);
        }
    }
}