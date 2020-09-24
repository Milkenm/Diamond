using Diamond.Brainz.Utils;

using Discord;
using Discord.Commands;

using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;

namespace Diamond.Brainz.Commands
{
    public partial class DiscordModule : ModuleBase<SocketCommandContext>
    {
        [Name("Ping"), Command("ping"), Alias("p"), Summary("View the ping of the bot.")]
        public async Task Ping()
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor("Ping", Twemoji.GetEmojiUrlFromEmoji("🕒"));
            embed.AddField("**Ping (ms)**", Context.Client.Latency + "ms");

            await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);
        }
    }
}
