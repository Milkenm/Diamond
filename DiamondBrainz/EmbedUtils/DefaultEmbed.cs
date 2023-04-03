using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using ScriptsLibV2.Util;

namespace Diamond.API
{
	public class DefaultEmbed : EmbedBuilder
	{
		private readonly IDiscordInteraction _interaction = null;

		public DefaultEmbed(string title, string emoji, IDiscordInteraction interaction)
		{
			_interaction = interaction;

			SocketGuildUser guildUser = (SocketGuildUser)_interaction.User;

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
		public DefaultEmbed WithFancyDescription(string content, string title = "📜 𝐂𝐨𝐦𝐦𝐚𝐧𝐝 𝐎𝐮𝐭𝐩𝐮𝐭 📜", string footer = "⚙️ 𝐂𝐨𝐦𝐦𝐚𝐧𝐝 𝐒𝐞𝐭𝐭𝐢𝐧𝐠𝐬 ⚙️") =>
			/*WithDescription($"```{title}```\n{content}\n\n```{footer}```\n‍"); // There is a "zero-width-joiner" at the end of this string*/
			this;

		/// <summary>
		/// Sends the embed.
		/// </summary>
		/// <param name="ephemeral">If the message should only be visible to the user. This is ignored if DeferAsync() was used.</param>
		public async Task SendAsync(bool ephemeral = false)
		{
			/*AddField("‍", "```👤 𝐔𝐬𝐞𝐫 👤```"); // There are some "zero-width-joiner"s in there*/

			if (_interaction.HasResponded)
			{
				await _interaction.ModifyOriginalResponseAsync((messageProperties) =>
				{
					messageProperties.Embed = Build();
				});
			}
			else
			{
				await _interaction.RespondAsync(embed: Build(), ephemeral: ephemeral);
			}
		}

		public async Task SendAsync(MessageComponent component, bool ephemeral = false)
		{
			if (_interaction.HasResponded)
			{
				await _interaction.ModifyOriginalResponseAsync((messageProperties) =>
				{
					messageProperties.Embed = Build();
					messageProperties.Components = component;
				});
			}
			else
			{
				await _interaction.RespondAsync(embed: Build(), ephemeral: ephemeral, components: component);
			}
		}
	}
}
