using System.Collections.Generic;

using Diamond.API.Data;

using Discord;

namespace Diamond.API.SlashCommands.VotePoll.Embeds;
public class EditorVoteEmbed : BaseVoteEmbed
{
	public EditorVoteEmbed(IDiscordInteraction interaction, Poll poll, ulong messageId) : base(interaction, poll)
	{
		using DiamondContext db = new DiamondContext();

		List<PollOption> pollOptions = VoteUtils.GetPollOptions(db, poll);

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

		Component = builder.Build();
	}

	/*
	public static async Task SelectMenuHandlerAsync(SocketMessageComponent messageComponent, DiscordSocketClient client)
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

					Poll poll = VoteUtils.GetPollByMessageId(messageId);
					PollOption selectedOption = this._database.PollOptions.Where(po => po.Id == optionId).FirstOrDefault();

						this._database.PollOptions.Remove(selectedOption);
					}

					await VoteUtils.UpdateEditorEmbed(messageComponent, poll, messageId);
					break;
				}
		}
	}
	*/

	/*
	public static async Task ButtonHandlerAsync(SocketMessageComponent messageComponent, DiscordSocketClient client)
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

		Poll poll;
		using (DiamondContext db = new DiamondContext())
		{
			poll = VoteUtils.GetPollByMessageId(db, messageId);
		}
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

							await VoteUtils.UpdateEditorEmbed(db, modal, poll, messageId);
					});

					await messageComponent.RespondWithModalAsync(editModal.Build());
				}
				break;
			case "button_add":
				{
				}
				break;
			case "button_publish":
				{
					await messageComponent.DeferAsync();
					await messageComponent.DeleteOriginalResponseAsync();

					PublishedVoteEmbed publishedEmbed = new PublishedVoteEmbed( messageComponent, client, poll, null);

					ulong responseId = await publishedEmbed.SendAsync();

					poll.IsPublished = true;
					poll.DiscordMessageId = responseId;
				}
				break;
		}
	}
	*/
}