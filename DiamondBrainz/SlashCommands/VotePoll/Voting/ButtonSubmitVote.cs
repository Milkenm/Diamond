using System;
using System.Threading.Tasks;

using Diamond.API.Data;
using Diamond.API.Util;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.VotePoll
{
	public partial class VotePoll
	{
		/// <summary>
		/// Saves the user's <see cref="PollVote"/>, deletes the <see cref="VotingEmbed"/> used to vote and refreshes the <see cref="PublishedEmbed"/> with the updated votes.
		/// <para>(Called when the user submits it's vote by clicking the "Submit vote" button on the <see cref="VotingEmbed"/>)</para>
		/// </summary>
		[ComponentInteraction("button_poll_submit_vote:*:*", true)]
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
	}
}
