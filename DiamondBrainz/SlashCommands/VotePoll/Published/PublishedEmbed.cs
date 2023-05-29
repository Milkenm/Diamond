using System.Collections.Generic;
using System.Linq;

using Diamond.API.Data;

using Discord;

namespace Diamond.API.SlashCommands.VotePoll
{
	public class PublishedEmbed : BasePollEmbed
	{
		public PublishedEmbed(IInteractionContext context, Poll poll) : base(context, poll)
		{
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
						_ = this.AddField(pollOption.Name, $"{votes} votes ({percentage}%)", true);
						continue;
					}
				}
				_ = this.AddField(pollOption.Name, "0 votes", true);
			}

			ComponentBuilder builder = new ComponentBuilder();
			_ = builder.WithButton(new ButtonBuilder("Vote", $"button_poll_vote", ButtonStyle.Primary));

			this.Component = builder.Build();
		}
	}
}