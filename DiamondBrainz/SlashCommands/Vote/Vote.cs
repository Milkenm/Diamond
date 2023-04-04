using System;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Bot;
using Diamond.API.Data;
using Diamond.API.EmbedUtils;
using Diamond.API.SlashCommands.Vote.Embeds;
using Diamond.API.SlashCommands.Vote.Modals;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using ScriptsLibV2.Extensions;

using static Diamond.API.Data.PollsContext;

namespace Diamond.API.SlashCommands.Vote;

public class Vote : InteractionModuleBase<SocketInteractionContext>
{
	private static bool _initializedEvents = false;

	private readonly DiamondBot _bot;
	private readonly PollsContext _pollsDb;

	public Vote(DiamondBot bot, PollsContext pollsDb)
	{
		_bot = bot;

		if (!_initializedEvents)
		{
			_bot.Client.ButtonExecuted += ButtonHandlerAsync;
			_bot.Client.SelectMenuExecuted += SelectMenuHandlerAsync;
			_initializedEvents = true;
		}

		_pollsDb = pollsDb;
	}

	[SlashCommand("poll", "Create a vote poll.")]
	public async Task VoteCommandAsync(
	[Summary("title", "The title of the poll.")][MinLength(1)][MaxLength(250)] string title,
	[Summary("description", "The description of the poll.")][MaxLength(4000)] string description
)
	{
		await DeferAsync(false);

		Poll poll = await CreatePollAsync(title, description);

		ulong deferId = (await GetOriginalResponseAsync()).Id;
		await UpdateInteractionAsync(poll, Context.Interaction, deferId);
	}

	private async Task ButtonHandlerAsync(SocketMessageComponent messageComponent)
	{
		string[] buttonData = messageComponent.Data.CustomId.Split("-");

		ulong messageId = messageComponent.Message.Id;
		long? selectedOptionId = null;

		string buttonName = buttonData[0];
		if (buttonData.Length == 2)
		{
			messageId = Convert.ToUInt64(buttonData[1]);
		}
		else if (buttonData.Length == 3)
		{
			selectedOptionId = Convert.ToInt64(buttonData[1]);
			messageId = Convert.ToUInt64(buttonData[2]);
		}

		Poll poll = VoteUtils.GetPollByMessageId(_pollsDb, messageId);
		if (poll == null)
		{
			return;
		}

		switch (buttonName)
		{
			case "button_edit":
				{
					EditPollModal editModal = new EditPollModal(messageId, _bot.Client, poll);
					editModal.OnModalSubmit += new DefaultModal.ModalSubmitEvent(async (modal, fieldsMap, messageId) =>
					{
						// Get the values of components.
						poll.Title = fieldsMap["field_title"];
						poll.Description = fieldsMap["field_description"];
						_pollsDb.SaveChanges();

						await UpdateInteractionAsync(poll, modal, messageId);
					});

					await messageComponent.RespondWithModalAsync(editModal.Build());
					break;
				}
			case "button_add":
				{
					NewOptionModal newModal = new NewOptionModal(messageId, _bot.Client);
					newModal.OnModalSubmit += new DefaultModal.ModalSubmitEvent(async (modal, fieldsMap, messageId) =>
					{
						PollOption newOption = new PollOption()
						{
							TargetPoll = poll,
							Name = fieldsMap["field_name"],
						};
						if (!fieldsMap["field_description"].IsEmpty())
						{
							newOption.Description = fieldsMap["field_description"];
						}

						_pollsDb.Add(newOption);
						_pollsDb.SaveChanges();

						await UpdateInteractionAsync(poll, modal, messageId);
					});

					await messageComponent.RespondWithModalAsync(newModal.Build());
					break;
				}
			case "button_publish":
				{
					await messageComponent.DeferAsync();
					await messageComponent.DeleteOriginalResponseAsync();

					PublishedVoteEmbed publishedEmbed = new PublishedVoteEmbed(messageComponent, _bot.Client, _pollsDb, poll, null);

					ulong responseId = await publishedEmbed.SendAsync();

					poll.IsPublished = true;
					poll.DiscordMessageId = responseId;
					_pollsDb.SaveChanges();

					break;
				}
			case "button_vote":
				{
					await messageComponent.DeferLoadingAsync(true);

					VoteEmbed voteEmbed = new VoteEmbed(messageComponent, _pollsDb, poll, messageId, null);

					await voteEmbed.SendAsync(true, true);
					break;
				}
			case "button_submitVote":
				{
					await messageComponent.DeferAsync();
					await messageComponent.DeleteOriginalResponseAsync();

					PollOption selectedOption = _pollsDb.PollOptions.Where(po => po.Id == selectedOptionId).FirstOrDefault();

					PollVote existingVote = _pollsDb.PollVotes.Where(pv => pv.UserId == messageComponent.User.Id && pv.Poll == poll).FirstOrDefault();
					if (existingVote == null)
					{
						_pollsDb.Add(new PollVote()
						{
							UserId = messageComponent.User.Id,
							Poll = poll,
							PollOption = selectedOption,
						});
					}
					else
					{
						// New vote
						if (selectedOption != null)
						{
							existingVote.PollOption = selectedOption;
						}
						// Remove vote
						else
						{
							_pollsDb.PollVotes.Remove(existingVote);
						}
					}

					PublishedVoteEmbed publishEmbed = new PublishedVoteEmbed(messageComponent, _bot.Client, _pollsDb, poll, messageId);
					await messageComponent.Channel.ModifyMessageAsync(messageId, (msg) =>
					{
						msg.Embed = publishEmbed.Build();
						msg.Components = publishEmbed.Component;
					});

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

	public async Task SelectMenuHandlerAsync(SocketMessageComponent menu)
	{
		await menu.DeferAsync();

		string[] menuData = menu.Data.CustomId.Split("-");
		string menuName = menuData[0];
		ulong messageId = Convert.ToUInt64(menuData[1]);

		string[] menuOptionData = string.Join("", menu.Data.Values).Split("-");
		string optionIdString = menuOptionData[1];

		PollOption selectedOption = null;
		Poll poll = null;
		if (optionIdString != "remove")
		{
			selectedOption = _pollsDb.PollOptions.Find(Convert.ToInt64(optionIdString));
			poll = selectedOption.TargetPoll;
		}
		else
		{
			poll = _pollsDb.Polls.Where(p => p.DiscordMessageId == messageId).FirstOrDefault();
		}


		switch (menuName)
		{
			case "select_remove":
				{
					_pollsDb.PollOptions.Remove(selectedOption);
					break;
				}
			case "select_vote":
				{
					VoteEmbed voteEmbed = new VoteEmbed(menu, _pollsDb, poll, messageId, Convert.ToInt64(optionIdString));
					voteEmbed.AddField("Your vote", $"**{selectedOption.Name}**{(!selectedOption.Description.IsEmpty() ? $"\n{selectedOption.Description}" : "")}", true);
					await menu.ModifyOriginalResponseAsync((msg) =>
					{
						msg.Embed = voteEmbed.Build();
						msg.Components = voteEmbed.Component;
					});
					break;
				}
		}
		_pollsDb.SaveChanges();
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

	private async Task<ulong> UpdateInteractionAsync(Poll poll, IDiscordInteraction interaction, ulong messageId)
	{

		if (!poll.IsPublished)
		{
			return await new EditorVoteEmbed(interaction, _pollsDb, poll, messageId).SendAsync();
		}
		else
		{
			return await new PublishedVoteEmbed(interaction, _bot.Client, _pollsDb, poll, messageId).SendAsync();
		}
	}

}