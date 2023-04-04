using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Data;
using Diamond.API.SlashCommands.Vote.Embeds;

using Discord;
using Discord.WebSocket;

using Microsoft.EntityFrameworkCore;

using static Diamond.API.Data.PollsContext;

namespace Diamond.API.SlashCommands.Vote;
public static class VoteUtils
{
	public static List<PollOption> GetPollOptions(PollsContext pollsDb, Poll poll) => pollsDb.PollOptions.Where(po => po.TargetPoll == poll).ToList();

	public static List<PollVote> GetPollVotes(PollsContext pollsDb, Poll poll) => pollsDb.PollVotes.Where(pv => pv.Poll == poll).ToList();

	public static Poll GetPollByMessageId(PollsContext pollsDb, ulong messageId) => pollsDb.Polls.Where(poll => poll.DiscordMessageId == messageId).FirstOrDefault();

	public static PollVote GetPollVoteByUserId(PollsContext pollsDb, Poll poll, ulong userId) => pollsDb.PollVotes.Include(pv => pv.PollOption).Where(pv => pv.Poll == poll && pv.UserId == userId).FirstOrDefault();

	public static async Task UpdateEditorEmbed(SocketMessageComponent messageComponent, PollsContext pollsDb, Poll poll, ulong messageId)
	{
		await messageComponent.UpdateAsync((msg) =>
		{
			UpdateEditorEmbed(messageComponent, pollsDb, poll, messageId, msg);
		});
	}

	public static async Task UpdateEditorEmbed(SocketModal modal, PollsContext pollsDb, Poll poll, ulong messageId)
	{
		await modal.UpdateAsync((msg) =>
		{
			UpdateEditorEmbed(modal, pollsDb, poll, messageId, msg);
		});
	}

	private static void UpdateEditorEmbed(IDiscordInteraction interaction, PollsContext pollsDb, Poll poll, ulong messageId, MessageProperties msg)
	{
		EditorVoteEmbed editorEmbed = new EditorVoteEmbed(interaction, pollsDb, poll, messageId);
		msg.Embed = editorEmbed.Build();
		msg.Components = editorEmbed.Component;
	}

	public static async Task UpdatePublishEmbed(SocketMessageComponent menu, PollsContext pollsDb, DiscordSocketClient client, ulong messageId, Poll poll)
	{
		IMessage pollMsg = await client.GetGuild((ulong)menu.GuildId).GetTextChannel((ulong)menu.ChannelId).GetMessageAsync(poll.DiscordMessageId);
		await client.GetGuild((ulong)menu.GuildId).GetTextChannel((ulong)menu.ChannelId).ModifyMessageAsync(poll.DiscordMessageId, (msg) =>
		{
			PublishedVoteEmbed publishEmbed = new PublishedVoteEmbed(menu, client, pollsDb, poll, messageId);
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
