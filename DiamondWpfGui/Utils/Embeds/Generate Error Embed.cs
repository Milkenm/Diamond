using Discord;
using Discord.Commands;

namespace Diamond.WPF.Utils
{
    public static partial class Embeds
    {
        public static Embed GenerateErrorEmbed(string message, SocketCommandContext context)
        {
            EmbedBuilder embed = new EmbedBuilder()
                .WithTitle("❌ Error!")
                .WithDescription(message)
                .WithFooter(context.User.Username)
                .WithCurrentTimestamp()
                .WithColor(Color.DarkPurple);

            return embed.Build();
        }
    }
}