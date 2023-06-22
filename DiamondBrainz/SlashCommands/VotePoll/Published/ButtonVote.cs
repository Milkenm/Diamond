using System.Threading.Tasks;

using Diamond.API.SlashCommands.VotePoll.Published;
using Diamond.API.SlashCommands.VotePoll.Voting;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.Polls;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.VotePoll
{
	public partial class VotePoll
	{
		/// <summary>
		/// Sends a ephemeral message to with a <see cref="VotingEmbed"/> to allow the user to vote.
		/// <para>(Called when a user clicks the "Vote" button on the "<see cref="PublishedEmbed">Published</see>" poll embed)</para>
		/// </summary>
		[ComponentInteraction("button_poll_vote", true)]
		public async Task ButtonVoteHandlerAsync()
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();
			ulong messageId = Utils.GetButtonMessageId(this.Context);

			DbPoll poll = VoteUtils.GetPollByMessageId(db, messageId);
			if (poll == null) return;

			VotingEmbed voteEmbed = new VotingEmbed(this.Context, poll, messageId, null);
			_ = await voteEmbed.SendAsync(true, true);
		}
	}
}
