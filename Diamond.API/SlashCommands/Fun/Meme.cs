using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.APIs.Funny;
using Diamond.API.Attributes;
using Diamond.API.Helpers;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;
using Discord.Rest;

using static Diamond.API.APIs.Funny.MemeAPI;

namespace Diamond.API.SlashCommands.Fun
{
	public partial class Fun
	{
		/// Key: MessageId, Value: List of Reddit Post ID
		private static readonly Dictionary<ulong, List<string>> _shownMemes = new Dictionary<ulong, List<string>>();

		[DSlashCommand("meme", "Get a random meme from Reddit.")]
		public async Task MemeCommandAsync(
			[Summary("prevent-duplicates", "Prevents the same meme from appearing but might become slower. Only works if show-everyone is false.")] bool preventDuplicates = false,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);
			(DefaultEmbed embed, MemeResponse meme) = GetMemeEmbed(this.Context, null, 0, !showEveryone, !showEveryone && preventDuplicates, 0);

			ulong responseId = await embed.SendAsync();
			if (preventDuplicates)
			{
				_shownMemes.Add(responseId, new List<string>() { GetRedditPostId(meme) });
			}
		}

		[ComponentInteraction($"{MemeComponentIds.BUTTON_MEME_REROLL}:*,*,*", true)]
		public async Task ButtonRerollMemeAsync(int rerollCount, bool preventDuplicates, int duplicates)
		{
			await this.DeferAsync(true);

			(DefaultEmbed embed, _) = GetMemeEmbed(this.Context, Utils.GetButtonMessageId(this.Context), rerollCount + 1, true, preventDuplicates, duplicates);

			_ = await this.ModifyOriginalResponseAsync((msg) =>
			{
				msg.Components = embed.Component;
				msg.Embed = embed.Build();
			});
		}

		[ComponentInteraction($"{MemeComponentIds.BUTTON_MEME_SHARE}:*", true)]
		public async Task ButtonShareMemeAsync(string postLink)
		{
			await this.DeferAsync(true);

			RestInteractionMessage response = await this.Context.Interaction.GetOriginalResponseAsync();
			Embed embed = response.Embeds.ElementAt(0);
			ComponentBuilder components = new ComponentBuilder()
				.WithButton("View post", style: ButtonStyle.Link, emote: Emoji.Parse("📄"), url: postLink);

			_ = await this.ReplyAsync(embed: embed, components: components.Build());
		}

		private static (DefaultEmbed embed, MemeResponse meme) GetMemeEmbed(SocketInteractionContext context, ulong? messageId, int rerollCount, bool showAdditionalButtons, bool preventDuplicates, int duplicates)
		{
			(MemeResponse meme, int duplicated) = GetMeme(messageId, preventDuplicates);

			// Base embed
			DefaultEmbed embed = new DefaultEmbed($"Meme", "😂", context)
			{
				Title = meme.Title,
				ImageUrl = meme.Url,
			};

			string rerollsString = $"{rerollCount} reroll{(rerollCount > 1 ? "s" : "")}";

			// First row - Post info
			_ = embed.AddField("**__OP__**", meme.Author, true);
			_ = embed.AddField("**__Subreddit__**", meme.Subreddit, true);
			_ = embed.AddField("**__Upvotes__**", meme.Upvotes, true);
			// Second row - Command info
			_ = embed.AddField("**__Rerolls__**", rerollsString, true);
			_ = embed.AddField("**__Prevent Duplicates__**", preventDuplicates ? "Yes" : "No", true);
			if (preventDuplicates)
			{
				_ = embed.AddField("**__Duplicates__**", (duplicates + duplicated).ToString(), true);
			}
			else
			{
				// Empty field with zero-width-joiners just to align the embed fields
				_ = embed.AddField("‍", "‍", true);
			}

			// Buttons
			ComponentBuilder components = new ComponentBuilder();
			// "Reroll" button
			if (showAdditionalButtons)
			{
				_ = components.WithButton("Reroll", $"{MemeComponentIds.BUTTON_MEME_REROLL}:{rerollCount},{preventDuplicates},{duplicates + duplicated}", style: ButtonStyle.Success, emote: Emoji.Parse("🔁"));
			}
			// "View post" button
			_ = components.WithButton("View post", style: ButtonStyle.Link, emote: Emoji.Parse("📄"), url: meme.PostLink);
			// "Share" button
			if (showAdditionalButtons)
			{
				_ = components.WithButton("Share", $"{MemeComponentIds.BUTTON_MEME_SHARE}:{meme.PostLink}", style: ButtonStyle.Secondary, emote: Emoji.Parse("📲"));
			}
			embed.Component = components.Build();

			return (embed, meme);
		}

		private static (MemeResponse meme, int duplicated) GetMeme(ulong? messageId, bool preventDuplicates, int duplicated = 0)
		{
			// Get meme
			MemeResponse meme = MemeAPI.GetRandomMeme();

			// Return if preventDuplicates is off
			if (!preventDuplicates || messageId == null)
			{
				return (meme, duplicated);
			}
			string postId = GetRedditPostId(meme);

			// Store current meme to duplicates list (this will probably never run)
			if (!_shownMemes.ContainsKey((ulong)messageId))
			{
				_shownMemes.Add((ulong)messageId, new List<string>() { postId });
				return (meme, duplicated);
			}
			// In case this meme wasn't shown before
			else if (!_shownMemes[(ulong)messageId].Contains(postId))
			{
				_shownMemes[(ulong)messageId].Add(postId);
				return (meme, duplicated);
			}

			// Auto-reroll
			return GetMeme(messageId, preventDuplicates, duplicated + 1);
		}

		private static string GetRedditPostId(MemeResponse meme)
		{
			return meme.PostLink.Split("/").Last();
		}
	}
}