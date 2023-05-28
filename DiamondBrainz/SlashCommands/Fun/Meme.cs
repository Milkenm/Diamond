using System.Linq;
using System.Threading.Tasks;

using Diamond.API.APIs;
using Diamond.API.Attributes;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;
using Discord.Rest;

using static Diamond.API.APIs.MemeAPI;

namespace Diamond.API.SlashCommands.Fun
{
	public partial class Fun
	{
		[DSlashCommand("meme", "Get a random meme from Reddit.")]
		public async Task MemeCommandAsync(
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			(DefaultEmbed embed, MessageComponent components) = this.GetMemeEmbed(showRerollButton: !showEveryone, showShareButton: !showEveryone);

			await embed.SendAsync(component: components);
		}

		private (DefaultEmbed embed, MessageComponent components) GetMemeEmbed(MemeResponse meme = null, bool showRerollButton = false, bool showShareButton = false)
		{
			meme ??= MemeAPI.GetRandomMeme();

			// Base embed
			DefaultEmbed embed = new DefaultEmbed("Meme", "😂", this.Context)
			{
				Title = meme.Title,
				ImageUrl = meme.Url,
			};
			embed.AddField("**__OP__**", meme.Author, true);
			embed.AddField("**__Subreddit__**", meme.Subreddit, true);
			embed.AddField("**__Upvotes__**", meme.Upvotes, true);
			// Buttons
			ComponentBuilder components = new ComponentBuilder();
			// "Reroll" button
			if (showRerollButton)
			{
				components.WithButton("Reroll", "button_reroll_meme", style: ButtonStyle.Success, emote: Emoji.Parse("🔁"));
			}
			// "View post" button
			components.WithButton("View post", style: ButtonStyle.Link, emote: Emoji.Parse("📄"), url: meme.PostLink);
			// "Share" button
			if (showShareButton)
			{
				components.WithButton("Share", $"button_share_meme:{meme.PostLink}", style: ButtonStyle.Secondary, emote: Emoji.Parse("📲"));
			}

			return (embed, components.Build());
		}

		[ComponentInteraction("button_reroll_meme", true)]
		public async Task ButtonRerollMemeAsync()
		{
			await this.DeferAsync(true);

			(DefaultEmbed embed, MessageComponent components) = this.GetMemeEmbed(showRerollButton: true, showShareButton: true);

			await this.ModifyOriginalResponseAsync((msg) =>
			{
				msg.Components = components;
				msg.Embed = embed.Build();
			});
		}

		[ComponentInteraction("button_share_meme:*", true)]
		public async Task ButtonShareMemeAsync(string postLink)
		{
			await this.DeferAsync(true);

			RestInteractionMessage response = await this.Context.Interaction.GetOriginalResponseAsync();
			Embed embed = response.Embeds.ElementAt(0);
			ComponentBuilder components = new ComponentBuilder()
				.WithButton("View post", style: ButtonStyle.Link, emote: Emoji.Parse("📄"), url: postLink);

			await this.ReplyAsync(embed: embed, components: components.Build());
		}
	}
}