using Discord;
using Discord.WebSocket;

namespace Diamond.API
{
    public static partial class EmbedUtils
    {
        public static Embed GenerateErrorEmbed(string message, SocketSlashCommand command)
        {
            DefaultEmbed embed = new DefaultEmbed("Error!", "❌", command);
            embed.WithDescription(message);

            return embed.Build();
        }
    }
}
