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

		/// <summary>
		/// The default values for <paramref name="title"/> and <paramref name="footer"/> is a "zero-width-joiner".
		/// </summary>
		/// <param name="content"></param>
		/// <param name="title"></param>
		/// <param name="footer"></param>
		/// <returns></returns>
		public DefaultEmbed WithFancyDescription(string content, string title = "📜 𝐂𝐨𝐦𝐦𝐚𝐧𝐝 𝐎𝐮𝐭𝐩𝐮𝐭 📜", string footer = "⚙️ 𝐂𝐨𝐦𝐦𝐚𝐧𝐝 𝐒𝐞𝐭𝐭𝐢𝐧𝐠𝐬 ⚙️")
		{
			/*WithDescription($"```{title}```\n{content}\n\n```{footer}```\n‍"); // There is a "zero-width-joiner" at the end of this string*/
			return this;
		}

		public async Task SendAsync(bool ephemeral = false)
		{
			/*AddField("‍", "```👤 𝐔𝐬𝐞𝐫 👤```"); // There are some "zero-width-joiner"s in there*/
			await _command.RespondAsync(embed: this.Build(), ephemeral: ephemeral);
		}
	}
}
