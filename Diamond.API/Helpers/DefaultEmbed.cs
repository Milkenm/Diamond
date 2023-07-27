using System.Threading.Tasks;

using Discord;

using ScriptsLibV2.Util;

namespace Diamond.API.Helpers
{
	public class DefaultEmbed : EmbedBuilder
	{
		private IInteractionContext _context;
		private IDiscordInteraction _interaction;

		public MessageComponent Component { get; set; }

		public DefaultEmbed() { }

		public DefaultEmbed(IInteractionContext context)
		{
			this.SetContext(context);
		}

		public DefaultEmbed(string title, string emoji, IInteractionContext context, string url = null)
		{
			this.SetAuthorInfoWithEmoji(title, emoji, url);
			this.SetContext(context);
		}

		public void SetAuthorInfo(string title, string iconUrl, string url = null)
		{
			this.Author = new EmbedAuthorBuilder()
			{
				Name = title,
				IconUrl = iconUrl,
				Url = url,
			};
		}

		public void SetAuthorInfoWithEmoji(string title, string emoji, string url = null)
		{
			this.SetAuthorInfo(title, TwemojiUtils.GetUrlFromEmoji(Emoji.Parse(emoji).ToString()), url);
		}

		public void SetContext(IInteractionContext context)
		{
			this._context = context;
			this._interaction = context.Interaction;
		}

		private void SetDefaultEmbedSettings()
		{
			string username = this._context.User is IGuildUser guildUser ? guildUser.DisplayName : this._context.User.GlobalName;
			this.Footer = new EmbedFooterBuilder()
			{
				Text = username,
				IconUrl = this._context.User.GetAvatarUrl(),
			};
			_ = this.WithCurrentTimestamp();
			_ = this.WithColor(Discord.Color.DarkMagenta);
		}

		/// <summary>
		/// Sends the embed.
		/// </summary>
		/// <param name="ephemeral">If the message should only be visible to the user. This is ignored if DeferAsync() was used.</param>
		public virtual async Task<ulong> SendAsync(bool ephemeral = false, bool isFollowUp = false, bool sendAsNew = false)
		{
			this.SetDefaultEmbedSettings();

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
			this.SetDefaultEmbedSettings();

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
