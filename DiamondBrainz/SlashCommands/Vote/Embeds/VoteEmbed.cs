using System.Collections.Generic;

using Diamond.API.Data;

using Discord;

using static Diamond.API.Data.PollsContext;

namespace Diamond.API.SlashCommands.Vote.Embeds;
public class VoteEmbed : BaseVoteEmbed
{
	public VoteEmbed(IDiscordInteraction interaction, PollsContext pollsDb, Poll poll, ulong messageId, long? optionId) : base(interaction, poll)
	{
		List<PollOption> pollOptions = VoteUtils.GetPollOptions(pollsDb, poll);

		ComponentBuilder builder = new ComponentBuilder();

		// Create the selection menu
		SelectMenuBuilder selectMenu = new SelectMenuBuilder()
		{
			CustomId = "select_vote-" + messageId,
			Placeholder = "Vote for an option...",
		};
		// Add "No vote" option
		if (optionId != null)
		{
			selectMenu.AddOption("No vote", $"option-remove-{messageId}", "Don't vote for this poll.", Emoji.Parse("❌"));
		}
		// Add options
		foreach (PollOption pollOption in pollOptions)
		{
			SelectMenuOptionBuilder selectMenuOption = new SelectMenuOptionBuilder(pollOption.Name, $"option-{pollOption.Id}-{messageId}", pollOption.Description);
			if (optionId != null && optionId == pollOption.Id)
			{
				selectMenuOption.IsDefault = true;
			}
			selectMenu.AddOption(selectMenuOption);
		}
		// Add to the response
		builder.WithSelectMenu(selectMenu);
		builder.WithButton(new ButtonBuilder("Submit Vote", $"button_submitVote-{optionId}-{messageId}", ButtonStyle.Success, isDisabled: optionId == null));
		builder.WithButton(new ButtonBuilder("Cancel", $"button_cancelVote-{optionId}-{messageId}", ButtonStyle.Secondary));

		base.Component = builder.Build();
	}
}
