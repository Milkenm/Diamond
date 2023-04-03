using System.Collections.Generic;
using System.Linq;

using Diamond.API.Data;

using static Diamond.API.Data.PollsContext;

namespace Diamond.API.SlashCommands.Vote;
public static class VoteUtils
{
	public static List<PollOption> GetPollOptions(PollsContext pollsDb, Poll poll) => pollsDb.PollOptions.Where(po => po.TargetPoll == poll).ToList();

	public static List<PollVote> GetPollVotes(PollsContext pollsDb, Poll poll) => pollsDb.PollVotes.Where(pv => pv.Poll == poll).ToList();

	public static Poll GetPollByMessageId(PollsContext pollsDb, ulong messageId) => pollsDb.Polls.Where(poll => poll.DiscordMessageId == messageId).FirstOrDefault();
}
