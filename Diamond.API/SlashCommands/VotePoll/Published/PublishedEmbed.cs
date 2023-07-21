using System.Collections.Generic;
using System.Linq;

using Diamond.Data;
using Diamond.Data.Models.Polls;

using Discord;

namespace Diamond.API.SlashCommands.VotePoll.Published
{
	public class PublishedEmbed : BasePollEmbed
	{
		public PublishedEmbed(IInteractionContext context, DbPoll poll) : base(context, poll)
		{
			using DiamondContext db = new DiamondContext();

			List<DbPollVote> pollVotes = VoteUtils.GetPollVotes(db, poll);
			List<DbPollOption> pollOptions = VoteUtils.GetPollOptions(db, poll);

			foreach (DbPollOption pollOption in pollOptions)
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
			_ = builder.WithButton(new ButtonBuilder("Vote", VotePollComponentIds.BUTTON_VOTEPOLL_VOTE, ButtonStyle.Primary));

			this.Component = builder.Build();
		}
	}
}