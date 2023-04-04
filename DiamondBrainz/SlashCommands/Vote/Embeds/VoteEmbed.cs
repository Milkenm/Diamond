using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Diamond.API.Data;

using Discord;
using Discord.WebSocket;

using ScriptsLibV2.Extensions;

using static Diamond.API.Data.PollsContext;

namespace Diamond.API.SlashCommands.Vote.Embeds;
public class VoteEmbed : BaseVoteEmbed
{
	public VoteEmbed(IDiscordInteraction interaction, PollsContext pollsDb, Poll poll, ulong messageId, long? optionId) : base(interaction, poll)
	{
		// Get user vote
		if (optionId == null)
		{
			PollVote vote = VoteUtils.GetPollVoteByUserId(pollsDb, poll, interaction.User.Id);
			if (vote != null)
			{
				optionId = vote.PollOption.Id;
			}
		}
		// Create the selection menu
		SelectMenuBuilder selectMenu = new SelectMenuBuilder()
		{
			CustomId = "select_vote-" + messageId,
			Placeholder = "Vote for an option...",
		};
		// Add "No vote" option
		if (optionId != null)
		{
			selectMenu.AddOption("No vote", $"option_remove-0-{messageId}", "Don't vote for this poll.", Emoji.Parse("❌"), isDefault: optionId == 0);
		}
		// Add options
		List<PollOption> pollOptions = VoteUtils.GetPollOptions(pollsDb, poll);
		foreach (PollOption pollOption in pollOptions)
		{
			SelectMenuOptionBuilder selectMenuOption = new SelectMenuOptionBuilder(pollOption.Name, $"option-{pollOption.Id}-{messageId}", pollOption.Description);
			if (optionId != null && optionId == pollOption.Id)
			{
				selectMenuOption.IsDefault = true;
				// Add selected option as a field
				AddField($"{Emoji.Parse("🗳️")} Your vote", $"**{pollOption.Name}**{(!pollOption.Description.IsEmpty() ? $"\n{pollOption.Description}" : "")}", true);
			}
			selectMenu.AddOption(selectMenuOption);
		}
		// Add select menu to the response
		ComponentBuilder builder = new ComponentBuilder()
			.WithSelectMenu(selectMenu)
			.WithButton(new ButtonBuilder("Submit Vote", $"button_submitVote-{optionId}-{messageId}", ButtonStyle.Success, isDisabled: optionId == null))
			.WithButton(new ButtonBuilder("Cancel", $"button_cancelVote-{optionId}-{messageId}", ButtonStyle.Secondary));
		base.Component = builder.Build();
	}

	public static async Task SelectMenuHandlerAsync(SocketMessageComponent menu, PollsContext pollsDb, DiscordSocketClient client)
	{
		string[] menuData = menu.Data.CustomId.Split("-");
		string menuName = menuData[0];

		string[] menuOptionData = string.Join("", menu.Data.Values).Split("-");

		switch (menuName)
		{
			case "select_vote":
				{
					string optionName = menuOptionData[0];
					long optionId = Convert.ToInt64(menuOptionData[1]);
					if (optionName == "option_remove")
					{
						optionId = 0;
					}

					ulong messageId = Convert.ToUInt64(menuData[1]);
					Poll poll = VoteUtils.GetPollByMessageId(pollsDb, messageId);
					VoteEmbed voteEmbed = new VoteEmbed(menu, pollsDb, poll, messageId, optionId);

					await menu.UpdateAsync((msg) =>
					{
						msg.Embed = voteEmbed.Build();
						msg.Components = voteEmbed.Component;
					});
					break;
				}
		}
		pollsDb.SaveChanges();
	}

	public static async Task ButtonHandlerAsync(SocketMessageComponent messageComponent, PollsContext pollsDb, DiscordSocketClient client)
	{
		string[] buttonData = messageComponent.Data.CustomId.Split("-");
		string buttonName = buttonData[0];

		switch (buttonName)
		{
			case "button_submitVote":
				{
					await messageComponent.DeferAsync();
					await messageComponent.DeleteOriginalResponseAsync();

					ulong messageId = Convert.ToUInt64(buttonData[2]);
					long selectedOptionId = Convert.ToInt64(buttonData[1]);

					Poll poll = VoteUtils.GetPollByMessageId(pollsDb, messageId);
					if (poll == null)
					{
						return;
					}

					PollVote existingVote = VoteUtils.GetPollVoteByUserId(pollsDb, poll, messageComponent.User.Id);
					if (selectedOptionId == 0)
					{
						if (existingVote != null)
						{
							pollsDb.PollVotes.Remove(existingVote);
						}
					}
					else
					{
						PollOption selectedOption = pollsDb.PollOptions.Find(selectedOptionId);
						// New vote
						if (existingVote == null)
						{
							pollsDb.Add(new PollVote()
							{
								UserId = messageComponent.User.Id,
								Poll = poll,
								PollOption = selectedOption,
								VotedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
							});
						}
						// Update vote
						else if (selectedOption != null)
						{
							existingVote.PollOption = selectedOption;
							existingVote.VotedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
						}
						// Remove vote
						else
						{
							pollsDb.PollVotes.Remove(existingVote);
						}
					}
					pollsDb.SaveChanges();

					await VoteUtils.UpdatePublishEmbed(messageComponent, pollsDb, client, messageId, poll);

					break;
				}
			case "button_cancelVote":
				{
					await messageComponent.DeferAsync();
					await messageComponent.DeleteOriginalResponseAsync();

					break;
				}
		}
	}
}
