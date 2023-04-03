using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using ScriptsLibV2.Util;

namespace Diamond.API
{
	public class DefaultEmbed : EmbedBuilder
	{
		public MessageComponent Component { get; set; }

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
		public async Task<ulong> SendAsync(bool ephemeral = false, bool isFollowUp = false)
		{
			/*AddField("‍", "```👤 𝐔𝐬𝐞𝐫 👤```"); // There are some "zero-width-joiner"s in there*/

			if (isFollowUp)
			{
				IUserMessage followUp = await _interaction.FollowupAsync(embed: Build(), ephemeral: ephemeral, components: Component);
				return followUp.Id;
			}
			else if (_interaction.HasResponded)
			{
				await _interaction.ModifyOriginalResponseAsync((messageProperties) =>
				{
					messageProperties.Embed = Build();
					if (this.Component != null)
					{
						messageProperties.Components = Component;
					}
				});
			}
			else
			{
				if (this.Component != null)
				{
					await _interaction.RespondAsync(embed: Build(), ephemeral: ephemeral);
				}
				else
				{
					await _interaction.RespondAsync(embed: Build(), components: Component, ephemeral: ephemeral);
				}
			}
			return (await _interaction.GetOriginalResponseAsync()).Id;
		}

		public async Task<ulong> SendAsync(MessageComponent component, bool ephemeral = false)
		{
			this.Component = component;
			if (_interaction.HasResponded)
			{
				await _interaction.ModifyOriginalResponseAsync((messageProperties) =>
				{
					messageProperties.Embed = Build();
					messageProperties.Components = this.Component;
				});
			}
			else
			{
				await _interaction.RespondAsync(embed: Build(), ephemeral: ephemeral, components: this.Component);
			}
			return (await _interaction.GetOriginalResponseAsync()).Id;
		}
	}
}
