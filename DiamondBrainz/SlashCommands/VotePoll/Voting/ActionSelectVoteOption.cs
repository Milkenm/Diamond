using System.Threading.Tasks;

using Diamond.API.Data;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.VotePoll
{
	public partial class VotePoll
	{
		/// <summary>
		/// Refreshes the <see cref="VotingEmbed"/> with the <see cref="PollOption"/> the user selected.
		/// <para>(Called when a user selects a <see cref="PollOption"/> on the <see cref="VotingEmbed"/>)</para>
		/// </summary>
		/// <param name="messageId">The ID of the message containing the select menu.</param>
		/// <param name="selectedOptionId">The ID of the PollOption selected by the user.</param>
		[ComponentInteraction("sm_poll_vote:*", true)]
		public async Task SelectMenuPollVoteHandlerAsync(ulong messageId, long selectedOptionId)
		{
			await this.DeferAsync();
			using DiamondContext db = new DiamondContext();

			Poll poll = VoteUtils.GetPollByMessageId(db, messageId);
			await VoteUtils.UpdateVotingEmbed(this.Context, poll, messageId, selectedOptionId);
		}
	}
}