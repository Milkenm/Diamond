using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Data;

using Discord;
using Discord.WebSocket;

namespace Diamond.API.SlashCommands.Vote.Embeds;
public class PublishedVoteEmbed : BaseVoteEmbed
{
	private readonly IDiscordInteraction _interaction;
	private readonly DiscordSocketClient _client;
	private readonly ulong? _messageId;

	public PublishedVoteEmbed(IDiscordInteraction interaction, DiscordSocketClient client, DiamondDatabase diamondDatabase, Poll poll, ulong? messageId) : base(interaction, poll)
	{
		_interaction = interaction;
		_client = client;
		_messageId = messageId;

		List<PollVote> pollVotes = VoteUtils.GetPollVotes(diamondDatabase, poll);
		List<PollOption> pollOptions = VoteUtils.GetPollOptions(diamondDatabase, poll);

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

	public static async Task ButtonHandlerAsync(SocketMessageComponent messageComponent, DiamondDatabase database)
	{
		string[] buttonData = messageComponent.Data.CustomId.Split("-");
		string buttonName = buttonData[0];

		ulong messageId = messageComponent.Message.Id;

		Poll poll = VoteUtils.GetPollByMessageId(database, messageId);
		if (poll == null)
		{
			return;
		}

		switch (buttonName)
		{
			case "button_vote":
				{
					await messageComponent.DeferLoadingAsync(true);

					VoteEmbed voteEmbed = new VoteEmbed(messageComponent, database, poll, messageId, null);

					await voteEmbed.SendAsync(true, true);
					break;
				}
		}
	}
}