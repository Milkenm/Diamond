using System.Threading.Tasks;

using Diamond.API.SlashCommands.VotePoll.Voting;
using Diamond.API.Util;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.VotePoll
{
	public partial class VotePoll
	{
		/// <summary>
		/// Simply deletes the <see cref="VotingEmbed"/>.
		/// <para>(Called when a user clicks the "Cancel" button on the <see cref="VotingEmbed"/>)</para>
		/// </summary>
		/// <returns></returns>
		[ComponentInteraction(VotePollComponentIds.BUTTON_VOTEPOLL_CANCEL_VOTE, true)]
		public async Task ButtonCancelVoteHandlerAsync()
		{
			await this.DeferAsync();
			await Utils.DeleteResponseAsync(this.Context);
		}
	}
}