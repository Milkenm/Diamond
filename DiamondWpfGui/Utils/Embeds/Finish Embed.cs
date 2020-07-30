using Discord;
using Discord.Commands;

namespace Diamond.WPF.Utils
{
    public static partial class Embeds
    {
        public static Embed FinishEmbed(EmbedBuilder embed, SocketCommandContext context)
        {
            embed.WithFooter(context.User.Username);
            embed.WithCurrentTimestamp();
            embed.WithColor(Color.DarkMagenta);

            return embed.Build();
        }
    }
}