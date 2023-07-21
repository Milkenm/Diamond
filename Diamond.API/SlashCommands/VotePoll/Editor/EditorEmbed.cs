using System.Collections.Generic;
using System.ComponentModel.Design;

using Diamond.Data;
using Diamond.Data.Models.Polls;

using Discord;

namespace Diamond.API.SlashCommands.VotePoll.Editor
{
	public class EditorEmbed : BasePollEmbed
	{
		public EditorEmbed(IInteractionContext context, DbPoll poll, ulong messageId) : base(context, poll)
		{
			using DiamondContext db = new DiamondContext();

			List<DbPollOption> pollOptions = VoteUtils.GetPollOptions(db, poll);

			ComponentBuilder builder = new ComponentBuilder()
				.WithButton(new ButtonBuilder("Publish", $"{VotePollComponentIds.BUTTON_VOTEPOLL_PUBLISH}:{messageId}", ButtonStyle.Success, isDisabled: pollOptions.Count < 2))
				.WithButton(new ButtonBuilder("Add option", $"{VotePollComponentIds.BUTTON_VOTEPOLL_ADD_OPTION}:{messageId}"))
				.WithButton(new ButtonBuilder("Change title or description", $"{VotePollComponentIds.BUTTON_VOTEPOLL_EDIT_OPTION}:{messageId}", ButtonStyle.Secondary));

			if (pollOptions.Count > 0)
			{
				// Create the selection menu
				SelectMenuBuilder selectMenu = new SelectMenuBuilder()
				{
					CustomId = $"{VotePollComponentIds.SELECT_VOTEPOLL_REMOVE_OPTION}:{messageId}",
					Placeholder = "Remove an option...",
				};
				foreach (DbPollOption pollOption in pollOptions)
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