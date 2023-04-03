using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Bot;
using Diamond.API.Data;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using ScriptsLibV2.Extensions;

using static Diamond.API.Data.PollsContext;

namespace Diamond.API.SlashCommands;

public class Vote : InteractionModuleBase<SocketInteractionContext>
{
	private readonly DiamondBot _bot;
	private readonly PollsContext _pollsDb;

	public Vote(DiamondBot bot, PollsContext pollsDb)
	{
		_bot = bot;
		_bot.Client.ButtonExecuted += ButtonHandlerAsync;
		_bot.Client.ModalSubmitted += ModalHandlerAsync;
		_bot.Client.SelectMenuExecuted += SelectMenuHandlerAsync;

		_pollsDb = pollsDb;
	}

	[SlashCommand("poll", "Create a vote poll.")]
	public async Task VoteCommandAsync(
	[Summary("title", "The title for the poll.")][MinLength(1)][MaxLength(250)] string title,
	[Summary("description", "The description off your poll.")][MaxLength(4000)] string description
)
	{
		await DeferAsync(false);

		Poll poll = await CreatePollAsync(title, description);

		await UpdateInteractionAsync(poll, Context.Interaction, null);
	}

	private async Task ButtonHandlerAsync(SocketMessageComponent messageComponent)
	{
		ulong messageId = messageComponent.Message.Id;
		Poll poll = GetPollByMessageId(messageId);

		if (poll == null)
		{
			return;
		}

		switch (messageComponent.Data.CustomId)
		{
			case "button_publish":
				{
					poll.IsPublished = true;
					_pollsDb.SaveChanges();

					try
					{
						await UpdateInteractionAsync(poll, messageComponent, messageId, true);
					}
					catch { }
					break;
				}
			case "button_add":
				{
					ModalBuilder mb = new ModalBuilder()
					   .WithTitle("New option")
					   .WithCustomId($"modal_newOption-{messageId}")
					   .AddTextInput("Name", "field_name", placeholder: "New option name...")
					   .AddTextInput("Description", "field_description", placeholder: "New option description...", required: false);

					try
					{
						await messageComponent.RespondWithModalAsync(mb.Build());
					}
					catch { }
					break;
				}
			case "button_edit":
				{
					ModalBuilder mb = new ModalBuilder()
					   .WithTitle("Edit poll")
					   .WithCustomId($"modal_edit-{messageId}")
					   .AddTextInput("Name", "field_title", placeholder: "New option name...", value: poll.Title, maxLength: 250)
					   .AddTextInput("Description", "field_description", TextInputStyle.Paragraph, "New option description...", value: poll.Description);

					try
					{
						await messageComponent.RespondWithModalAsync(mb.Build());
					}
					catch { }
					break;
				}
			case "button_vote":
				{
					DefaultEmbed embed = new DefaultEmbed($"Poll", "🗳️", messageComponent);
					// Create the selection menu
					SelectMenuBuilder selectMenu = new SelectMenuBuilder()
					{
						CustomId = "select_vote-" + messageId,
						Placeholder = "Vote for an option...",
					};
					// Add options
					selectMenu.AddOption("No vote", $"option-remove-{messageId}", "Don't vote for this poll.", Emoji.Parse("❌"), true);
					List<PollOption> pollOptions = GetPollOptions(poll);
					foreach (PollOption pollOption in pollOptions)
					{
						selectMenu.AddOption(pollOption.Name, $"option-{pollOption.Id}-{messageId}", pollOption.Description);
					}
					// Add to the response
					ComponentBuilder builder = new ComponentBuilder();
					builder.WithSelectMenu(selectMenu);
					await embed.SendAsync(builder.Build(), true);
					break;
				}
		}
	}

	private async Task ModalHandlerAsync(SocketModal modal)
	{
		await modal.DeferAsync();

		string[] modalData = modal.Data.CustomId.Split("-");
		string modalType = modalData[0];
		ulong messageId = Convert.ToUInt64(modalData[1]);

		Poll poll = GetPollByMessageId(messageId);

		switch (modalType)
		{
			case "modal_newOption":
				{
					string optionName = GetModalFieldValue(modal, "field_name");
					string optionDescription = GetModalFieldValue(modal, "field_description");

					PollOption newOption = new PollOption()
					{
						TargetPoll = poll,
						Name = optionName,
					};
					if (!optionDescription.IsEmpty())
					{
						newOption.Description = optionDescription;
					}

					_pollsDb.Add(newOption);
					_pollsDb.SaveChanges();

					await UpdateInteractionAsync(poll, modal, messageId);
					break;
				}
			case "modal_edit":
				{
					// Get the values of components.
					string newTitle = GetModalFieldValue(modal, "field_title");
					string newDescription = GetModalFieldValue(modal, "field_description");

					poll.Title = newTitle;
					poll.Description = newDescription;
					_pollsDb.SaveChanges();

					try
					{
						await UpdateInteractionAsync(poll, modal, messageId);
					}
					catch { }
					break;
				}
		}
	}

	public async Task SelectMenuHandlerAsync(SocketMessageComponent menu)
	{
		await menu.DeferAsync();

		string[] modalData = string.Join("", menu.Data.Values).Split("-");
		string optionId = modalData[1];
		ulong messageId = Convert.ToUInt64(modalData[2]);

		PollOption? option = null;
		Poll poll = null;
		if (optionId != "remove")
		{
			option = _pollsDb.PollOptions.Find(Convert.ToInt64(optionId));
			poll = option.TargetPoll;
		}
		else
		{
			poll = _pollsDb.Polls.Where(p => p.DiscordMessageId == messageId).FirstOrDefault();
		}

		// Voting
		if (poll.IsPublished)
		{
			PollVote existingVote = _pollsDb.PollVotes.Where(pv => pv.UserId == menu.User.Id && pv.Poll == poll).FirstOrDefault();
			if (existingVote == null)
			{
				_pollsDb.Add(new PollVote()
				{
					UserId = menu.User.Id,
					Poll = poll,
					PollOption = option,
				});
			}
			else
			{
				// New vote
				if (option != null)
				{
					existingVote.PollOption = option;
				}
				// Remove vote
				else
				{
					_pollsDb.PollVotes.Remove(existingVote);
				}
			}
		}
		// Editing
		else
		{
			_pollsDb.PollOptions.Remove(option);
		}

		_pollsDb.SaveChanges();
		try
		{
			await UpdateInteractionAsync(GetPollByMessageId(messageId), menu, messageId, true);
		}
		catch { }
	}

	private async Task<Poll> CreatePollAsync(string pollTitle, string pollDescription)
	{
		Poll newPoll = new Poll()
		{
			DiscordMessageId = (await Context.Interaction.GetOriginalResponseAsync()).Id,
			Title = pollTitle,
			Description = pollDescription
		};

		_pollsDb.Add(newPoll);
		await _pollsDb.SaveChangesAsync();

		return newPoll;
	}

	private async Task UpdateInteractionAsync(Poll poll, IDiscordInteraction interaction, ulong? messageId, bool publish = false)
	{
		DefaultEmbed embed = new DefaultEmbed($"Poll", "🗳️", interaction)
		{
			Title = poll.Title,
			Description = poll.Description,
		};

		List<PollOption> pollOptions = GetPollOptions(poll);

		ComponentBuilder builder = new ComponentBuilder();
		if (!publish)
		{
			builder
			.WithButton(new ButtonBuilder("Publish", "button_publish", ButtonStyle.Success, isDisabled: pollOptions.Count < 2))
			.WithButton(new ButtonBuilder("Add option", "button_add"))
			.WithButton(new ButtonBuilder("Change title or description", "button_edit", ButtonStyle.Secondary));
		}
		else
		{
			builder.WithButton(new ButtonBuilder("Vote", "button_vote", ButtonStyle.Success));
		}

		if (pollOptions.Count > 0)
		{
			// Create the selection menu
			SelectMenuBuilder selectMenu = new SelectMenuBuilder()
			{
				CustomId = "select_remove-" + messageId,
				Placeholder = publish ? "Vote for an option..." : "Remove an option...",
			};
			// Add options
			if (publish)
			{
				selectMenu.AddOption("No vote", $"option-remove-{messageId}", "Don't vote for this poll.", Emoji.Parse("❌"), true);
			}
			foreach (PollOption pollOption in pollOptions)
			{
				selectMenu.AddOption(pollOption.Name, $"option-{pollOption.Id}-{messageId}", pollOption.Description);
			}
			// Add to the response
			builder.WithSelectMenu(selectMenu);
		}

		await embed.SendAsync(builder.Build());
	}

	private List<PollOption> GetPollOptions(Poll poll) => _pollsDb.PollOptions.Where(po => po.TargetPoll == poll).ToList();

	private Poll GetPollByMessageId(ulong messageId) => _pollsDb.Polls.Where(poll => poll.DiscordMessageId == messageId).FirstOrDefault<Poll>();

	private string GetModalFieldValue(SocketModal modal, string fieldName)
	{
		List<SocketMessageComponentData> components = modal.Data.Components.ToList();
		SocketMessageComponentData componentData = components.FirstOrDefault(data => data.CustomId == fieldName, null);
		return componentData?.Value;
	}
}