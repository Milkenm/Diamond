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

			DefaultEmbed embed = this.GetMemeEmbed(showRerollButton: !showEveryone, showShareButton: !showEveryone);

			_ = await embed.SendAsync();
		}

		private DefaultEmbed GetMemeEmbed(MemeResponse meme = null, int rerollCount = 0, bool showRerollButton = false, bool showShareButton = false)
		{
			meme ??= MemeAPI.GetRandomMeme();

			// Base embed
			string rerollsSuffix = $" {(rerollCount > 0 ? $" ({rerollCount} reroll{(rerollCount > 1 ? "s" : "")})" : "")}";
			DefaultEmbed embed = new DefaultEmbed($"Meme{rerollsSuffix}", "😂", this.Context)
			{
				Title = meme.Title,
				ImageUrl = meme.Url,
			};

			// First row
			_ = embed.AddField("**__OP__**", meme.Author, true);
			_ = embed.AddField("**__Subreddit__**", meme.Subreddit, true);
			_ = embed.AddField("**__Upvotes__**", meme.Upvotes, true);

			// Buttons
			ComponentBuilder components = new ComponentBuilder();
			// "Reroll" button
			if (showRerollButton)
			{
				_ = components.WithButton("Reroll", $"button_reroll_meme:{rerollCount}", style: ButtonStyle.Success, emote: Emoji.Parse("🔁"));
			}
			// "View post" button
			_ = components.WithButton("View post", style: ButtonStyle.Link, emote: Emoji.Parse("📄"), url: meme.PostLink);
			// "Share" button
			if (showShareButton)
			{
				_ = components.WithButton("Share", $"button_share_meme:{meme.PostLink}", style: ButtonStyle.Secondary, emote: Emoji.Parse("📲"));
			}
			embed.Component = components.Build();

			return embed;
		}

		[ComponentInteraction("button_reroll_meme:*", true)]
		public async Task ButtonRerollMemeAsync(int rerollCount)
		{
			await this.DeferAsync(true);

			DefaultEmbed embed = this.GetMemeEmbed(rerollCount: rerollCount + 1, showRerollButton: true, showShareButton: true);

			_ = await this.ModifyOriginalResponseAsync((msg) =>
			{
				msg.Components = embed.Component;
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

			_ = await this.ReplyAsync(embed: embed, components: components.Build());
		}
	}
}