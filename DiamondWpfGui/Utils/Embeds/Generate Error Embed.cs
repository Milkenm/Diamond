using Discord;
using Discord.Commands;

namespace Diamond.WPF.Utils
{
    public static partial class Embeds
    {
        public static Embed GenerateErrorEmbed(string message, SocketCommandContext context)
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle("❌ Error!");
            embed.WithDescription(message);
            embed.WithFooter(context.User.Username);
            embed.WithCurrentTimestamp();
            embed.WithColor(Color.DarkPurple);

            return embed.Build();
        }
    }
}