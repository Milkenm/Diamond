﻿using System.Collections.Generic;

using Diamond.API.Data;

using Discord;

namespace Diamond.API.SlashCommands.VotePoll
{
	public class EditorEmbed : BasePollEmbed
	{
		public EditorEmbed(IInteractionContext context, Poll poll, ulong messageId) : base(context, poll)
		{
			using DiamondContext db = new DiamondContext();

			List<PollOption> pollOptions = VoteUtils.GetPollOptions(db, poll);

			ComponentBuilder builder = new ComponentBuilder()
				.WithButton(new ButtonBuilder("Publish", $"button_publish:{messageId}", ButtonStyle.Success, isDisabled: pollOptions.Count < 2))
				.WithButton(new ButtonBuilder("Add option", $"button_add:{messageId}"))
				.WithButton(new ButtonBuilder("Change title or description", $"button_edit:{messageId}", ButtonStyle.Secondary));

			if (pollOptions.Count > 0)
			{
				// Create the selection menu
				SelectMenuBuilder selectMenu = new SelectMenuBuilder()
				{
					CustomId = "sm_poll_remove_option:" + messageId,
					Placeholder = "Remove an option...",
				};
				foreach (PollOption pollOption in pollOptions)
				{
					_ = selectMenu.AddOption(pollOption.Name, pollOption.Id.ToString(), pollOption.Description);
				}
				// Add to the response
				_ = builder.WithSelectMenu(selectMenu);
			}

			this.Component = builder.Build();
		}
	}
}