using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Data;
using Diamond.API.SlashCommands.Vote.Embeds;

using Discord;
using Discord.WebSocket;

using Microsoft.EntityFrameworkCore;

namespace Diamond.API.SlashCommands.Vote;
public static class VoteUtils
{
	public static List<PollOption> GetPollOptions(DiamondDatabase database, Poll poll) => database.PollOptions.Where(po => po.TargetPoll == poll).ToList();

	public static List<PollVote> GetPollVotes(DiamondDatabase database, Poll poll) => database.PollVotes.Where(pv => pv.Poll == poll).ToList();

	public static Poll GetPollByMessageId(DiamondDatabase database, ulong messageId) => database.Polls.Where(poll => poll.DiscordMessageId == messageId).FirstOrDefault();

	public static PollVote GetPollVoteByUserId(DiamondDatabase database, Poll poll, ulong userId) => database.PollVotes.Include(pv => pv.PollOption).Where(pv => pv.Poll == poll && pv.UserId == userId).FirstOrDefault();

	public static async Task UpdateEditorEmbed(SocketMessageComponent messageComponent, DiamondDatabase database, Poll poll, ulong messageId)
	{
		await messageComponent.UpdateAsync((msg) =>
		{
			UpdateEditorEmbed(messageComponent, database, poll, messageId, msg);
		});
	}

	public static async Task UpdateEditorEmbed(SocketModal modal, DiamondDatabase database, Poll poll, ulong messageId)
	{
		await modal.UpdateAsync((msg) =>
		{
			UpdateEditorEmbed(modal, database, poll, messageId, msg);
		});
	}

	private static void UpdateEditorEmbed(IDiscordInteraction interaction, DiamondDatabase database, Poll poll, ulong messageId, MessageProperties msg)
	{
		EditorVoteEmbed editorEmbed = new EditorVoteEmbed(interaction, database, poll, messageId);
		msg.Embed = editorEmbed.Build();
		msg.Components = editorEmbed.Component;
	}

	public static async Task UpdatePublishEmbed(SocketMessageComponent menu, DiamondDatabase database, DiscordSocketClient client, ulong messageId, Poll poll)
	{
		IMessage pollMsg = await client.GetGuild((ulong)menu.GuildId).GetTextChannel((ulong)menu.ChannelId).GetMessageAsync(poll.DiscordMessageId);
		await client.GetGuild((ulong)menu.GuildId).GetTextChannel((ulong)menu.ChannelId).ModifyMessageAsync(poll.DiscordMessageId, (msg) =>
		{
			PublishedVoteEmbed publishEmbed = new PublishedVoteEmbed(menu, client, database, poll, messageId);
			publishEmbed.Footer = new EmbedFooterBuilder()
			{
				Text = pollMsg.Embeds.ElementAt(0).Footer.Value.Text,
				IconUrl = pollMsg.Embeds.ElementAt(0).Footer.Value.IconUrl,
			};
			msg.Embed = publishEmbed.Build();
			msg.Components = publishEmbed.Component;
		});
	}
}
