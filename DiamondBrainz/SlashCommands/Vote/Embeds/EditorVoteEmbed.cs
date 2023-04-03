using System.Collections.Generic;

using Diamond.API.Data;

using Discord;

using static Diamond.API.Data.PollsContext;

namespace Diamond.API.SlashCommands.Vote.Embeds;
public class EditorVoteEmbed : BaseVoteEmbed
{
	public EditorVoteEmbed(IDiscordInteraction interaction, PollsContext pollsDb, Poll poll, ulong messageId) : base(interaction, poll)
	{
		List<PollOption> pollOptions = VoteUtils.GetPollOptions(pollsDb, poll);

		ComponentBuilder builder = new ComponentBuilder()
			.WithButton(new ButtonBuilder("Publish", "button_publish", ButtonStyle.Success, isDisabled: pollOptions.Count < 2))
			.WithButton(new ButtonBuilder("Add option", "button_add"))
			.WithButton(new ButtonBuilder("Change title or description", "button_edit", ButtonStyle.Secondary));

		if (pollOptions.Count > 0)
		{
			// Create the selection menu
			SelectMenuBuilder selectMenu = new SelectMenuBuilder()
			{
				CustomId = "select_remove-" + messageId,
				Placeholder = "Remove an option...",
			};
			foreach (PollOption pollOption in pollOptions)
			{
				selectMenu.AddOption(pollOption.Name, $"option-{pollOption.Id}-{messageId}", pollOption.Description);
			}
			// Add to the response
			builder.WithSelectMenu(selectMenu);
		}

		base.Component = builder.Build();
	}
}
