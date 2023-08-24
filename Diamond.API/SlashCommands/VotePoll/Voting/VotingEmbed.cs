using System.Collections.Generic;

using Diamond.Data;
using Diamond.Data.Models.Polls;

using Discord;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.VotePoll.Voting
{
	public class VotingEmbed : BasePollEmbed
	{
		public VotingEmbed(IInteractionContext context, DbPoll poll, ulong pollMessageId, long? optionId) : base(context, poll)
		{
			using DiamondContext db = new DiamondContext();

			// Get user vote
			if (optionId == null)
			{
				DbPollVote vote = VoteUtils.GetPollVoteByUserId(db, poll, context.User.Id);
				if (vote != null)
					optionId = vote.PollOption.Id;
			}
			// Create the selection menu
			SelectMenuBuilder selectMenu = new SelectMenuBuilder()
			{
				CustomId = $"{VotePollComponentIds.SELECT_VOTEPOLL_VOTE}:{pollMessageId}",
				Placeholder = "Vote for an option...",
			};
			// Add "No vote" option
			if (optionId != null)
				_ = selectMenu.AddOption("No vote", 0.ToString(), "Don't vote for this poll.", Emoji.Parse("❌"), isDefault: optionId == 0);
			// Add options
			List<DbPollOption> pollOptions = VoteUtils.GetPollOptions(db, poll);
			foreach (DbPollOption pollOption in pollOptions)
			{
				SelectMenuOptionBuilder selectMenuOption = new SelectMenuOptionBuilder(pollOption.Name, pollOption.Id.ToString(), pollOption.Description);
				if (optionId != null && optionId == pollOption.Id)
				{
					selectMenuOption.IsDefault = true;
					// Add selected option as a field
					_ = this.AddField($"{Emoji.Parse("🗳️")} Your vote", $"**{pollOption.Name}**{(!pollOption.Description.IsEmpty() ? $"\n{pollOption.Description}" : "")}", true);
				}
				_ = selectMenu.AddOption(selectMenuOption);
			}
			// Add select menu to the response
			_ = this.AddSelectMenu(selectMenu)
				.AddButton(new ButtonBuilder("Submit vote", $"{VotePollComponentIds.BUTTON_VOTEPOLL_SUBMIT_VOTE}:{pollMessageId},{optionId}", ButtonStyle.Success, isDisabled: optionId == null))
				.AddButton(new ButtonBuilder("Cancel", VotePollComponentIds.BUTTON_VOTEPOLL_CANCEL_VOTE, ButtonStyle.Secondary));
		}
	}
}