using System.Threading.Tasks;

using Diamond.API.Data;

using Discord.Interactions;
namespace Diamond.API.SlashCommands.VotePoll
{
	public partial class VotePoll
	{
		/// <summary>
		/// Removes the selected <see cref="PollOption"/> from the <see cref="Poll"/>'s vote options list and refreshes the <see cref="EditorEmbed"/>.
		/// <para>(Called when the user selects an option to be removed on the <see cref="EditorEmbed"/>)</para>
		/// </summary>
		/// <param name="messageId"></param>
		/// <param name="selectedOptionId"></param>
		/// <returns></returns>
		[ComponentInteraction("sm_poll_remove_option:*", true)]
		public async Task SelectMenuRemoveOptionHandlerAsync(ulong messageId, long selectedOptionId)
		{
			await this.DeferAsync();
			using DiamondContext db = new DiamondContext();

			// Remove option from database
			PollOption? pollOption = db.PollOptions.Find(selectedOptionId);
			if (pollOption == null) return;
			_ = db.PollOptions.Remove(pollOption);
			await db.SaveAsync();

			// Refresh embed
			Poll poll = VoteUtils.GetPollByMessageId(db, messageId);
			await VoteUtils.UpdateEditorEmbed(this.Context, poll, messageId);
		}
	}
}