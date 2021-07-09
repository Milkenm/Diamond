using Discord;
using Discord.Commands;

namespace Diamond.Brainz
{
	public static partial class Extensions
	{
		public static Embed FinishEmbed(this EmbedBuilder embed, IUser user)
		{
			embed.WithFooter(user.Username);
			embed.WithCurrentTimestamp();
			embed.WithColor(Color.DarkMagenta);

			return embed.Build();
		}

		public static Embed FinishEmbed(this EmbedBuilder embed, SocketCommandContext context)
		{
			return embed.FinishEmbed(context.User);
		}
	}
}
