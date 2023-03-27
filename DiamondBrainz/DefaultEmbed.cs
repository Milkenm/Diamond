using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using ScriptsLibV2.Util;

namespace Diamond.API
{
	public class DefaultEmbed : EmbedBuilder
	{
		private readonly SocketSlashCommand _command;

		public DefaultEmbed(string title, string emoji, SocketSlashCommand command)
		{
			_command = command;

			SocketGuildUser guildUser = (SocketGuildUser)_command.User;

			Author = new EmbedAuthorBuilder()
			{
				Name = title,
				IconUrl = TwemojiUtils.GetUrlFromEmoji(emoji),
			};
			Footer = new EmbedFooterBuilder()
			{
				Text = guildUser.Nickname,
				IconUrl = guildUser.GetAvatarUrl(),
			};
			WithCurrentTimestamp();
			Color = Discord.Color.DarkMagenta;
		}

		public async Task SendAsync(bool ephemeral = false)
		{
			await _command.RespondAsync(embed: this.Build(), ephemeral: ephemeral);
		}
	}
}
