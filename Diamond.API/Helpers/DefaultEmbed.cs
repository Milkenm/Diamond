using System.Collections.Generic;
using System.Threading.Tasks;

using Discord;

using ScriptsLibV2.Util;

namespace Diamond.API.Helpers
{
	public class DefaultEmbed : EmbedBuilder
	{
		private IInteractionContext _context;
		private IDiscordInteraction _interaction;
		private MessageComponent _msgComponent;
		private readonly ComponentBuilder _components = new ComponentBuilder();

		public MessageComponent GetComponents()
		{
			this._msgComponent = this._components.Build();
			return this._msgComponent;
		}

		public DefaultEmbed(IInteractionContext context)
		{
			this.SetContext(context);
		}

		public DefaultEmbed(string title, string emoji, IInteractionContext context, string url = null)
		{
			this.SetAuthorInfoWithEmoji(title, emoji, url);
			this.SetContext(context);
		}

		public DefaultEmbed(Embed embed, IInteractionContext context)
		{
			this.SetContext(context);

			this.Title = embed.Title;
			this.Description = embed.Description;
			this.Color = embed.Color;
			this.Url = embed.Url;

			if (embed.Author.HasValue)
			{
				_ = this.WithAuthor(embed.Author.Value.Name, embed.Author.Value.IconUrl, embed.Author.Value.Url);
			}
			foreach (EmbedField field in embed.Fields)
			{
				_ = this.AddField(field.Name, field.Value, field.Inline);
			}
			if (embed.Thumbnail.HasValue)
			{
				_ = this.WithThumbnailUrl(embed.Thumbnail.Value.Url);
			}
			if (embed.Image.HasValue)
			{
				_ = this.WithImageUrl(embed.Image.Value.Url);
			}
			if (embed.Timestamp.HasValue)
			{
				this.Timestamp = embed.Timestamp.Value;
			}
			if (embed.Footer.HasValue)
			{
				_ = this.WithFooter(embed.Footer.Value.Text, embed.Footer.Value.IconUrl);
			}
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

		public DefaultEmbed AddButton(ButtonBuilder buttonBuilder, int row = 0)
		{
			_ = this._components.WithButton(buttonBuilder, row);
			return this;
		}

		public DefaultEmbed AddButton(string label = null, string customId = null, ButtonStyle style = ButtonStyle.Primary, IEmote emote = null, string url = null, bool isDisabled = false, int row = 0)
		{
			return this.AddButton(new ButtonBuilder(label, customId, style, url, emote, isDisabled), row);
		}

		public DefaultEmbed AddSelectMenu(SelectMenuBuilder selectMenuBuilder, int row = 0)
		{
			_ = this._components.WithSelectMenu(selectMenuBuilder, row);
			return this;
		}

		public DefaultEmbed AddSelectMenu(string customId, List<SelectMenuOptionBuilder> options = null, string placeholder = null, int maxValues = 1, int minValues = 1, bool isDisabled = false, ComponentType type = ComponentType.SelectMenu, List<ChannelType> channelTypes = null, int row = 0)
		{
			return this.AddSelectMenu(new SelectMenuBuilder(customId, options, placeholder, maxValues, minValues, isDisabled, type, channelTypes), row);
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
				return (await this._context.Channel.SendMessageAsync(embed: this.Build(), components: this.GetComponents())).Id;
			}

			// Follow up
			if (isFollowUp)
			{
				IUserMessage followUp = await this._interaction.FollowupAsync(embed: this.Build(), ephemeral: ephemeral, components: this.GetComponents());
				return followUp.Id;
			}

			// Edit or Respond
			if (this._interaction.HasResponded)
			{
				_ = await this._interaction.ModifyOriginalResponseAsync((messageProperties) =>
				{
					messageProperties.Embed = this.Build();
					messageProperties.Components = this.GetComponents();
				});
			}
			else
			{
				await this._interaction.RespondAsync(embed: this.Build(), components: this.GetComponents(), ephemeral: ephemeral);
			}
			return (await this._interaction.GetOriginalResponseAsync()).Id;
		}

		public virtual async Task<ulong> SendAsync(MessageComponent component, bool ephemeral = false)
		{
			this.SetDefaultEmbedSettings();

			if (this._interaction.HasResponded)
			{
				_ = await this._interaction.ModifyOriginalResponseAsync((messageProperties) =>
				{
					messageProperties.Embed = this.Build();
					messageProperties.Components = this.GetComponents();
				});
			}
			else
			{
				await this._interaction.RespondAsync(embed: this.Build(), ephemeral: ephemeral, components: this.GetComponents());
			}
			return (await this._interaction.GetOriginalResponseAsync()).Id;
		}
	}
}
