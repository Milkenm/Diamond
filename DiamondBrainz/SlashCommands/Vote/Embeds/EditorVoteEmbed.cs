using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Data;
using Diamond.API.EmbedUtils;
using Diamond.API.SlashCommands.Vote.Modals;

using Discord;
using Discord.WebSocket;

using ScriptsLibV2.Extensions;

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

	public static async Task SelectMenuHandlerAsync(SocketMessageComponent messageComponent, PollsContext pollsDb, DiscordSocketClient client)
	{
		string[] menuData = messageComponent.Data.CustomId.Split("-");
		string menuName = menuData[0];

		switch (menuName)
		{
			case "select_remove":
				{
					ulong messageId = Convert.ToUInt64(menuData[1]);

					string[] menuOptionData = string.Join("", messageComponent.Data.Values).Split("-");
					long optionId = Convert.ToInt64(menuOptionData[1]);

					PollOption selectedOption = pollsDb.PollOptions.Where(po => po.Id == optionId).FirstOrDefault();
					Poll poll = VoteUtils.GetPollByMessageId(pollsDb, messageId);

					pollsDb.PollOptions.Remove(selectedOption);
					pollsDb.SaveChanges();

					await VoteUtils.UpdateEditorEmbed(messageComponent, pollsDb, poll, messageId);
					break;
				}
		}
	}

	public static async Task ButtonHandlerAsync(SocketMessageComponent messageComponent, DiscordSocketClient client, PollsContext pollsDb)
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

		Poll poll = VoteUtils.GetPollByMessageId(pollsDb, messageId);
		if (poll == null)
		{
			return;
		}

		switch (buttonName)
		{
			case "button_edit":
				{
					EditPollModal editModal = new EditPollModal(messageId, client, poll);
					editModal.OnModalSubmit += new DefaultModal.ModalSubmitEvent(async (modal, fieldsMap, messageId) =>
					{
						// Get the values of components.
						poll.Title = fieldsMap["field_title"];
						poll.Description = fieldsMap["field_description"];
						poll.ImageUrl = fieldsMap["field_imageurl"];
						poll.ThumbnailUrl = fieldsMap["field_thumbnailurl"];
						poll.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
						pollsDb.SaveChanges();

						await VoteUtils.UpdateEditorEmbed(modal, pollsDb, poll, messageId);
					});

					await messageComponent.RespondWithModalAsync(editModal.Build());
					break;
				}
			case "button_add":
				{
					NewOptionModal newModal = new NewOptionModal(messageId, client);
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

						pollsDb.Add(newOption);
						pollsDb.SaveChanges();

						await VoteUtils.UpdateEditorEmbed(modal, pollsDb, poll, messageId);
					});

					await messageComponent.RespondWithModalAsync(newModal.Build());
					break;
				}
			case "button_publish":
				{
					await messageComponent.DeferAsync();
					await messageComponent.DeleteOriginalResponseAsync();

					PublishedVoteEmbed publishedEmbed = new PublishedVoteEmbed(messageComponent, client, pollsDb, poll, null);

					ulong responseId = await publishedEmbed.SendAsync();

					poll.IsPublished = true;
					poll.DiscordMessageId = responseId;
					pollsDb.SaveChanges();

					break;
				}
		}
	}
}