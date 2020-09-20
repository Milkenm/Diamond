using Discord;
using Discord.Commands;

namespace Diamond.Brainz.Utils
{
    public static partial class Embeds
    {
        public static Embed FinishEmbed(EmbedBuilder embed, IUser user)
        {
            embed.WithFooter(user.Username);
            embed.WithCurrentTimestamp();
            embed.WithColor(Color.DarkMagenta);

            return embed.Build();
        }

        public static Embed FinishEmbed(EmbedBuilder embed, SocketCommandContext context)
        {
            return FinishEmbed(embed, context.User);
        }
    }
}