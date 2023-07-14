using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.SlashCommands.VotePoll.Editor;
using Diamond.API.SlashCommands.VotePoll.Published;
using Diamond.API.SlashCommands.VotePoll.Voting;
using Diamond.Data;
using Diamond.Data.Models.Polls;

using Discord;

using Microsoft.EntityFrameworkCore;

namespace Diamond.API.SlashCommands.VotePoll
{
	public static class VoteUtils
	{
		public static List<DbPollOption> GetPollOptions(DiamondContext db, DbPoll poll)
		{
			return db.PollOptions.Where(po => po.TargetPoll == poll).ToList();
		}

		public static List<DbPollVote> GetPollVotes(DiamondContext db, DbPoll poll)
		{
			return db.PollVotes.Where(pv => pv.Poll == poll).ToList();
		}

		public static DbPoll GetPollByMessageId(DiamondContext db, ulong messageId)
		{
			return db.Polls.Where(poll => poll.DiscordMessageId == messageId).FirstOrDefault();
		}

		public static DbPollVote GetPollVoteByUserId(DiamondContext db, DbPoll poll, ulong userId)
		{
			return db.PollVotes.Include(pv => pv.PollOption).Where(pv => pv.Poll == poll && pv.UserId == userId).FirstOrDefault();
		}

		public static async Task UpdateEditorEmbed(IInteractionContext context, DbPoll poll, ulong messageId)
		{
			EditorEmbed editorEmbed = new EditorEmbed(context, poll, messageId);
			_ = await context.Interaction.ModifyOriginalResponseAsync((msg) =>
			{
				msg.Embed = editorEmbed.Build();
				msg.Components = editorEmbed.Component;
			});
		}

		public static async Task UpdatePublishedEmbed(IInteractionContext context, DiamondClient client, DbPoll poll)
		{
			// All this stuff is needed to get the original poll embed
			IMessage pollMsg = await client.GetGuild(context.Guild.Id).GetTextChannel(context.Channel.Id).GetMessageAsync(poll.DiscordMessageId);
			_ = await client.GetGuild(context.Guild.Id).GetTextChannel(context.Channel.Id).ModifyMessageAsync(poll.DiscordMessageId, (msg) =>
			{
				PublishedEmbed publishEmbed = new PublishedEmbed(context, poll)
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

		public static async Task UpdateVotingEmbed(IInteractionContext context, DbPoll poll, ulong messageId, long? selectedOptionId)
		{
			VotingEmbed voteEmbed = new VotingEmbed(context, poll, messageId, selectedOptionId);
			_ = await context.Interaction.ModifyOriginalResponseAsync((msg) =>
			{
				msg.Embed = voteEmbed.Build();
				msg.Components = voteEmbed.Component;
			});
		}

		public static async Task<DbPoll> CreatePollAsync(DiamondContext db, string pollTitle, string pollDescription, string pollImageUrl, string pollThumbnailUrl, ulong responseMessageId, ulong userId)
		{
			DbPoll newPoll = new DbPoll()
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
	}
}