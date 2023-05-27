using System;
using System.Collections.Generic;

using Diamond.API.Data;

using Discord;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.VotePoll.Embeds;
public class VotingEmbed : BaseVoteEmbed
{
	public VotingEmbed(IInteractionContext context, Poll poll, ulong pollMessageId, long? optionId) : base(context, poll)
	{
		using DiamondContext db = new DiamondContext();

		// Get user vote
		if (optionId == null)
		{
			PollVote vote = VoteUtils.GetPollVoteByUserId(db, poll, context.User.Id);
			if (vote != null)
			{
				optionId = vote.PollOption.Id;
			}
		}
		// Create the selection menu
		SelectMenuBuilder selectMenu = new SelectMenuBuilder()
		{
			CustomId = "sm_poll_vote:" + pollMessageId,
			Placeholder = "Vote for an option...",
		};
		// Add "No vote" option
		if (optionId != null)
		{
			_ = selectMenu.AddOption("No vote", 0.ToString(), "Don't vote for this poll.", Emoji.Parse("❌"), isDefault: optionId == 0);
		}
		// Add options
		List<PollOption> pollOptions = VoteUtils.GetPollOptions(db, poll);
		foreach (PollOption pollOption in pollOptions)
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
		ComponentBuilder builder = new ComponentBuilder()
			.WithSelectMenu(selectMenu)
			.WithButton(new ButtonBuilder("Submit vote", $"button_poll_submit_vote:{pollMessageId}:{optionId}", ButtonStyle.Success, isDisabled: optionId == null))
			.WithButton(new ButtonBuilder("Cancel", $"button_poll_cancel_vote:{pollMessageId}:{optionId}", ButtonStyle.Secondary));
		this.Component = builder.Build();
	}
}
