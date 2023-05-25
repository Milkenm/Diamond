﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Data;

using Discord;
using Discord.WebSocket;

namespace Diamond.API.SlashCommands.VotePoll.Embeds;
public class PublishedVoteEmbed : BaseVoteEmbed
{
	private readonly IDiscordInteraction _interaction;
	private readonly DiscordSocketClient _client;
	private readonly ulong? _messageId;

	public PublishedVoteEmbed(IDiscordInteraction interaction, DiscordSocketClient client, Poll poll, ulong? messageId) : base(interaction, poll)
	{
		_interaction = interaction;
		_client = client;
		_messageId = messageId;

		using DiamondContext db = new DiamondContext();

		List<PollVote> pollVotes = VoteUtils.GetPollVotes(db, poll);
		List<PollOption> pollOptions = VoteUtils.GetPollOptions(db, poll);

		foreach (PollOption pollOption in pollOptions)
		{
			if (pollVotes.Count > 0)
			{
				int votes = pollVotes.Where(pv => pv.PollOption == pollOption).Count();
				if (votes > 0)
				{
					double percentage = votes * 100 / pollVotes.Count;
					AddField(pollOption.Name, $"{votes} votes ({percentage}%)", true);
					continue;
				}
			}
			AddField(pollOption.Name, "0 votes", true);
		}

		ComponentBuilder builder = new ComponentBuilder();
		builder.WithButton(new ButtonBuilder("Vote", "button_vote", ButtonStyle.Primary));

		Component = builder.Build();
	}

	public async Task<ulong> SendAsync()
	{
		Embed embed = this.Build();
		SocketTextChannel channel = _client.GetGuild((ulong)_interaction.GuildId).GetTextChannel((ulong)_interaction.ChannelId);

		// Publish
		if (_messageId == null)
		{
			return (await channel.SendMessageAsync(embed: embed, components: Component)).Id;
		}
		// Vote update
		else
		{
			await channel.ModifyMessageAsync((ulong)_messageId, (msg) =>
			{
				msg.Embed = embed;
				msg.Components = Component;
			});
			return (ulong)_messageId;
		}
	}

	/*
	public static async Task ButtonHandlerAsync(DiamondContext db, SocketMessageComponent messageComponent)
	{
		string[] buttonData = messageComponent.Data.CustomId.Split("-");
		string buttonName = buttonData[0];

		ulong messageId = messageComponent.Message.Id;

		Poll poll = VoteUtils.GetPollByMessageId(db, messageId);
		if (poll == null)
		{
			return;
		}

		switch (buttonName)
		{
			case "button_vote":
				{
					await messageComponent.DeferLoadingAsync(true);

					VoteEmbed voteEmbed = new VoteEmbed(db, messageComponent, poll, messageId, null);

					await voteEmbed.SendAsync(true, true);
					break;
				}
		}
	}
	*/
}