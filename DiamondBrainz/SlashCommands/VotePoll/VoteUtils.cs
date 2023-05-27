using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Data;
using Diamond.API.SlashCommands.VotePoll.Embeds;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using Microsoft.EntityFrameworkCore;

namespace Diamond.API.SlashCommands.VotePoll;
public static class VoteUtils
{
	public static List<PollOption> GetPollOptions(DiamondContext db, Poll poll)
	{
		return db.PollOptions.Where(po => po.TargetPoll == poll).ToList();
	}

	public static List<PollVote> GetPollVotes(DiamondContext db, Poll poll)
	{
		return db.PollVotes.Where(pv => pv.Poll == poll).ToList();
	}

	public static Poll GetPollByMessageId(DiamondContext db, ulong messageId)
	{
		return db.Polls.Where(poll => poll.DiscordMessageId == messageId).FirstOrDefault();
	}

	public static PollVote GetPollVoteByUserId(DiamondContext db, Poll poll, ulong userId)
	{
		return db.PollVotes.Include(pv => pv.PollOption).Where(pv => pv.Poll == poll && pv.UserId == userId).FirstOrDefault();
	}

	public static async Task UpdateEditorEmbed(IInteractionContext context, Poll poll, ulong messageId)
	{
		EditorEmbed editorEmbed = new EditorEmbed(context, poll, messageId);
		_ = await context.Interaction.ModifyOriginalResponseAsync((msg) =>
		{
			msg.Embed = editorEmbed.Build();
			msg.Components = editorEmbed.Component;
		});
	}

	public static async Task UpdatePublishedEmbed(IInteractionContext context, DiscordSocketClient client, Poll poll)
	{
		// All this stuff is needed to get the original poll embed
		IMessage pollMsg = await client.GetGuild(context.Guild.Id).GetTextChannel(context.Channel.Id).GetMessageAsync(poll.DiscordMessageId);
		_ = await client.GetGuild(context.Guild.Id).GetTextChannel(context.Channel.Id).ModifyMessageAsync(poll.DiscordMessageId, (msg) =>
		{
			PublishedVoteEmbed publishEmbed = new PublishedVoteEmbed(context, poll)
			{
				Footer = new EmbedFooterBuilder()
				{
					Text = pollMsg.Embeds.ElementAt(0).Footer.Value.Text,
					IconUrl = pollMsg.Embeds.ElementAt(0).Footer.Value.IconUrl,
				}
			};
			msg.Embed = publishEmbed.Build();
		});
	}

	public static async Task UpdateVotingEmbed(IInteractionContext context, Poll poll, ulong messageId, long? selectedOptionId)
	{
		VotingEmbed voteEmbed = new VotingEmbed(context, poll, messageId, selectedOptionId);
		_ = await context.Interaction.ModifyOriginalResponseAsync((msg) =>
		{
			msg.Embed = voteEmbed.Build();
			msg.Components = voteEmbed.Component;
		});
	}

	public static async Task<Poll> CreatePollAsync(DiamondContext db, string pollTitle, string pollDescription, string pollImageUrl, string pollThumbnailUrl, ulong responseMessageId, ulong userId)
	{
		Poll newPoll = new Poll()
		{
			DiscordMessageId = responseMessageId,
			DiscordUserId = userId,
			Title = pollTitle,
			Description = pollDescription,
			ImageUrl = pollImageUrl,
			ThumbnailUrl = pollThumbnailUrl,
			CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
			UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
		};
		_ = db.Polls.Add(newPoll);
		await db.SaveAsync();

		return newPoll;
	}

	public static ulong GetButtonMessageId(SocketInteractionContext context)
	{
		return (context.Interaction as SocketMessageComponent).Message.Id;
	}
}
