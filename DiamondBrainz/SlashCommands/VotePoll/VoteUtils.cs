using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Data;
using Diamond.API.SlashCommands.VotePoll.Embeds;

using Discord;
using Discord.WebSocket;

using Microsoft.EntityFrameworkCore;

namespace Diamond.API.SlashCommands.VotePoll;
public static class VoteUtils
{
	public static List<PollOption> GetPollOptions(Poll poll)
	{
		using (DiamondDatabase db = new DiamondDatabase())
		{
			return db.PollOptions.Where(po => po.TargetPoll == poll).ToList();
		}
	}

	public static List<PollVote> GetPollVotes(Poll poll)
	{
		using (DiamondDatabase db = new DiamondDatabase())
		{
			return db.PollVotes.Where(pv => pv.Poll == poll).ToList();
		}
	}

	public static Poll GetPollByMessageId(ulong messageId)
	{
		using (DiamondDatabase db = new DiamondDatabase())
		{
			return db.Polls.Where(poll => poll.DiscordMessageId == messageId).FirstOrDefault();
		}
	}

	public static PollVote GetPollVoteByUserId(Poll poll, ulong userId)
	{
		using (DiamondDatabase db = new DiamondDatabase())
		{
			return db.PollVotes.Include(pv => pv.PollOption).Where(pv => pv.Poll == poll && pv.UserId == userId).FirstOrDefault();
		}
	}

	public static async Task UpdateEditorEmbed(SocketMessageComponent messageComponent, Poll poll, ulong messageId)
	{
		await messageComponent.UpdateAsync((msg) =>
		{
			UpdateEditorEmbed(messageComponent, poll, messageId, msg);
		});
	}

	public static async Task UpdateEditorEmbed(SocketModal modal, Poll poll, ulong messageId)
	{
		await modal.UpdateAsync((msg) =>
		{
			UpdateEditorEmbed(modal, poll, messageId, msg);
		});
	}

	private static void UpdateEditorEmbed(IDiscordInteraction interaction, Poll poll, ulong messageId, MessageProperties msg)
	{
		EditorVoteEmbed editorEmbed = new EditorVoteEmbed(interaction, poll, messageId);
		msg.Embed = editorEmbed.Build();
		msg.Components = editorEmbed.Component;
	}

	public static async Task UpdatePublishEmbed(SocketMessageComponent menu, DiscordSocketClient client, ulong messageId, Poll poll)
	{
		IMessage pollMsg = await client.GetGuild((ulong)menu.GuildId).GetTextChannel((ulong)menu.ChannelId).GetMessageAsync(poll.DiscordMessageId);
		await client.GetGuild((ulong)menu.GuildId).GetTextChannel((ulong)menu.ChannelId).ModifyMessageAsync(poll.DiscordMessageId, (msg) =>
		{
			PublishedVoteEmbed publishEmbed = new PublishedVoteEmbed(menu, client, poll, messageId);
			publishEmbed.Footer = new EmbedFooterBuilder()
			{
				Text = pollMsg.Embeds.ElementAt(0).Footer.Value.Text,
				IconUrl = pollMsg.Embeds.ElementAt(0).Footer.Value.IconUrl,
			};
			msg.Embed = publishEmbed.Build();
			msg.Components = publishEmbed.Component;
		});
	}

	public static async Task<Poll> CreatePollAsync(string pollTitle, string pollDescription, string pollImageUrl, string pollThumbnailUrl, ulong responseMessageId, ulong userId)
	{
		Poll newPoll = new Poll()
		{
			DiscordMessageId = responseMessageId,
			DiscordUserId = userId,
			Title = pollTitle,
			Description = pollDescription,
			ImageUrl = pollImageUrl,
			ThumbnailUrl = pollThumbnailUrl,
			CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
			UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
		};

		using (DiamondDatabase db = new DiamondDatabase())
		{
			db.Add(newPoll);
			await db.SaveAsync();
		}

		return newPoll;
	}
}
