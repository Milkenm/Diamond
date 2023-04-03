using Diamond.API.EmbedUtils;

using Discord;
using Discord.WebSocket;

using static Diamond.API.Data.PollsContext;

namespace Diamond.API.SlashCommands.Vote.Modals;
public class EditPollModal : DefaultModal
{
	public EditPollModal(ulong messageId, DiscordSocketClient client, Poll poll) : base("Edit poll", messageId, client)
	{
		AddTextInput("Name", "field_title", placeholder: "New option name...", value: poll.Title, maxLength: 250);
		AddTextInput("Description", "field_description", TextInputStyle.Paragraph, "New option description...", value: poll.Description);
	}
}
