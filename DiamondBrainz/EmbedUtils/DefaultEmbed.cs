using System.Threading.Tasks;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using ScriptsLibV2.Util;

namespace Diamond.API
{
	public class DefaultEmbed : EmbedBuilder
	{
		private readonly SocketInteractionContext _context;

		public DefaultEmbed(string title, string emoji, SocketSlashCommand cmd) { }

		public DefaultEmbed(string title, string emoji, SocketInteractionContext context)
		{
			_context = context;

			SocketGuildUser guildUser = (SocketGuildUser)_context.User;

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

		/// <summary>
		/// Sends the embed.
		/// </summary>
		/// <param name="ephemeral">If the message should only be visible to the user. This is ignored if DeferAsync() was used.</param>
		public async Task SendAsync(bool ephemeral = false)
		{
			/*AddField("‍", "```👤 𝐔𝐬𝐞𝐫 👤```"); // There are some "zero-width-joiner"s in there*/

			if (_context.Interaction.HasResponded)
			{
				await _context.Interaction.ModifyOriginalResponseAsync((messageProperties) =>
				{
					messageProperties.Embed = this.Build();
				});
			}
			else
			{
				await _context.Interaction.RespondAsync(embed: Build(), ephemeral: ephemeral);
			}
		}
	}
}
