using System;
using System.Threading.Tasks;

using Diamond.API.Data;
using Diamond.API.SlashCommands.VotePoll.Embeds;
using Diamond.API.Util;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.VotePoll
{
	public partial class VotePoll
	{
		/// <summary>
		/// Sends a ephemeral message to with a <see cref="VotingEmbed"/> to allow the user to vote.
		/// <para>(Called when a user clicks the "Vote" button on the "<see cref="PublishedVoteEmbed">Published</see>" poll embed)</para>
		/// </summary>
		[ComponentInteraction("button_poll_vote")]
		public async Task ButtonVoteHandlerAsync()
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();
			ulong messageId = VoteUtils.GetButtonMessageId(this.Context);

			Poll poll = VoteUtils.GetPollByMessageId(db, messageId);
			if (poll == null) return;

			VotingEmbed voteEmbed = new VotingEmbed(this.Context, poll, messageId, null);
			_ = await voteEmbed.SendAsync(true, true);
		}

		/// <summary>
		/// Refreshes the <see cref="VotingEmbed"/> with the <see cref="PollOption"/> the user selected.
		/// <para>(Called when a user selects a <see cref="PollOption"/> on the <see cref="VotingEmbed"/>)</para>
		/// </summary>
		/// <param name="messageId">The ID of the message containing the select menu.</param>
		/// <param name="selectedOptionId">The ID of the PollOption selected by the user.</param>
		[ComponentInteraction("sm_poll_vote:*")]
		public async Task SelectMenuPollVoteHandlerAsync(ulong messageId, long selectedOptionId)
		{
			await this.DeferAsync();
			using DiamondContext db = new DiamondContext();

			Poll poll = VoteUtils.GetPollByMessageId(db, messageId);
			await VoteUtils.UpdateVotingEmbed(this.Context, poll, messageId, selectedOptionId);
		}

		/// <summary>
		/// Saves the user's <see cref="PollVote"/>, deletes the <see cref="VotingEmbed"/> used to vote and refreshes the <see cref="PublishedVoteEmbed"/> with the updated votes.
		/// <para>(Called when the user submits it's vote by clicking the "Submit vote" button on the <see cref="VotingEmbed"/>)</para>
		/// </summary>
		[ComponentInteraction("button_poll_submit_vote:*:*")]
		public async Task ButtonSubmitVoteHandlerAsync(ulong pollMessageId, long selectedOptionId)
		{
			await this.DeferAsync(true);
			using DiamondContext db = new DiamondContext();

			Poll poll = VoteUtils.GetPollByMessageId(db, pollMessageId);
			if (poll == null) return;
			PollVote existingVote = VoteUtils.GetPollVoteByUserId(db, poll, this.Context.User.Id);

			PollOption selectedOption = db.PollOptions.Find(selectedOptionId);
			// New vote
			if (existingVote == null)
			{
				_ = db.PollVotes.Add(new PollVote()
				{
					UserId = this.Context.User.Id,
					Poll = poll,
					PollOption = selectedOption,
					VotedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
				});
			}
			// Update vote
			else if (selectedOption != null)
			{
				existingVote.PollOption = selectedOption;
				existingVote.VotedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			}
			// Remove vote
			else
			{
				_ = db.PollVotes.Remove(existingVote);
			}

			await db.SaveAsync();
			await Utils.DeleteResponseAsync(this.Context);
			await VoteUtils.UpdatePublishedEmbed(this.Context, this._client, poll);
		}

		/// <summary>
		/// Simply deletes the <see cref="VotingEmbed"/>.
		/// <para>(Called when a user clicks the "Cancel" button on the <see cref="VotingEmbed"/>)</para>
		/// </summary>
		/// <returns></returns>
		[ComponentInteraction("button_poll_cancel_vote")]
		public async Task ButtonCancelVoteHandlerAsync()
		{
			await this.DeferAsync();
			await Utils.DeleteResponseAsync(this.Context);
		}
	}
}
