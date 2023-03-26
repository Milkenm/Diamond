using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Diamond.API
{
	public static partial class Extensions
	{
		public static Embed FinishEmbed(this EmbedBuilder embed, IUser user)
		{
			SocketGuildUser guildUser = (SocketGuildUser)user;

			embed.WithFooter(guildUser.Nickname);
			embed.WithCurrentTimestamp();
			embed.WithColor(Color.DarkMagenta);

			return embed.Build();
		}

		public static Embed FinishEmbed(this EmbedBuilder embed, SocketCommandContext context) => embed.FinishEmbed(context.User);

		public static Embed FinishEmbed(this EmbedBuilder embed, SocketSlashCommand comand) => embed.FinishEmbed(comand.User);
	}
}
