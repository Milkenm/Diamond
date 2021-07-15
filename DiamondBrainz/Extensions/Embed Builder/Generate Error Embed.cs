using Discord;
using Discord.Commands;

namespace Diamond.Brainz
{
	public static partial class Extensions
	{
		public static Embed GenerateErrorEmbed(this EmbedBuilder embed, string message, SocketCommandContext context)
		{
			embed.WithTitle("❌ Error!");
			embed.WithDescription(message);
			embed.WithFooter(context.User.Username);
			embed.WithCurrentTimestamp();
			embed.WithColor(Color.DarkPurple);

			return embed.Build();
		}
	}
}
