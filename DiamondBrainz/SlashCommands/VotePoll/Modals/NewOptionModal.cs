using Diamond.API.EmbedUtils;

using Discord.WebSocket;

namespace Diamond.API.SlashCommands.VotePoll.Modals;
public class NewOptionModal : DefaultModal
{
	public NewOptionModal(ulong messageId, DiscordSocketClient client) : base("New option", messageId, client)
	{
		AddTextInput("Name", "field_name", placeholder: "New option name...");
		AddTextInput("Description", "field_description", placeholder: "New option description...", required: false);
	}
}
