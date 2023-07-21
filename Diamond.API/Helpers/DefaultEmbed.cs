using System.Threading.Tasks;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using ScriptsLibV2.Util;

namespace Diamond.API.Helpers
{
	public class DefaultEmbed : EmbedBuilder
	{
		private readonly IInteractionContext _context;
		private readonly IDiscordInteraction _interaction;

		public MessageComponent Component { get; set; }

		public DefaultEmbed(string title, string emoji, IInteractionContext context, string link = null)
		{
			this._context = context;
			this._interaction = context.Interaction;

			SocketGuildUser guildUser = (SocketGuildUser)this._interaction.User;

			this.Author = new EmbedAuthorBuilder()
			{
				Name = title,
				IconUrl = TwemojiUtils.GetUrlFromEmoji(Emoji.Parse(emoji).ToString()),
				Url = link,
			};
			this.Footer = new EmbedFooterBuilder()
			{
				Text = guildUser.DisplayName,
				IconUrl = guildUser.GetAvatarUrl(),
			};
			_ = this.WithCurrentTimestamp();
			this.Color = Discord.Color.DarkMagenta;
		}

		/// <summary>
		/// Sends the embed.
		/// </summary>
		/// <param name="ephemeral">If the message should only be visible to the user. This is ignored if DeferAsync() was used.</param>
		public virtual async Task<ulong> SendAsync(bool ephemeral = false, bool isFollowUp = false, bool sendAsNew = false)
		{
			// Send a new message
			if (sendAsNew)
			{
				return (await this._context.Channel.SendMessageAsync(embed: this.Build(), components: this.Component)).Id;
			}

			// Follow up
			if (isFollowUp)
			{
				IUserMessage followUp = await this._interaction.FollowupAsync(embed: this.Build(), ephemeral: ephemeral, components: this.Component);
				return followUp.Id;
			}

			// Edit or Respond
			if (this._interaction.HasResponded)
			{
				_ = await this._interaction.ModifyOriginalResponseAsync((messageProperties) =>
				{
					messageProperties.Embed = this.Build();
					messageProperties.Components = this.Component;
				});
			}
			else
			{
				await this._interaction.RespondAsync(embed: this.Build(), components: this.Component, ephemeral: ephemeral);
			}
			return (await this._interaction.GetOriginalResponseAsync()).Id;
		}

		public virtual async Task<ulong> SendAsync(MessageComponent component, bool ephemeral = false)
		{
			this.Component = component;
			if (this._interaction.HasResponded)
			{
				_ = await this._interaction.ModifyOriginalResponseAsync((messageProperties) =>
				{
					messageProperties.Embed = this.Build();
					messageProperties.Components = this.Component;
				});
			}
			else
			{
				await this._interaction.RespondAsync(embed: this.Build(), ephemeral: ephemeral, components: this.Component);
			}
			return (await this._interaction.GetOriginalResponseAsync()).Id;
		}
	}
}
