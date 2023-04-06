using Diamond.API.Data;
using Diamond.API.EmbedUtils;

using Discord;
using Discord.WebSocket;

namespace Diamond.API.SlashCommands.Vote.Modals;
public class EditPollModal : DefaultModal
{
	public EditPollModal(ulong messageId, DiscordSocketClient client, Poll poll) : base("Edit poll", messageId, client)
	{
		AddTextInput("Name", "field_title", TextInputStyle.Short, "New option name...", value: poll.Title, maxLength: 250);
		AddTextInput("Description", "field_description", TextInputStyle.Paragraph, "New option description...", value: poll.Description);
		AddTextInput("Image URL", "field_imageurl", TextInputStyle.Short, "http://www.example.com/sussy.png", value: poll.ImageUrl, required: false);
		AddTextInput("Thumbnail URL", "field_thumbnailurl", TextInputStyle.Short, "http://www.example.com/small_sussy.png", value: poll.ThumbnailUrl, required: false);
	}
}
