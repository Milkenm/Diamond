using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Polls
{
	[Table("PollVote")]
	public class DbPollVote
	{
		public long Id { get; set; }
		public required ulong UserId { get; set; }
		public required DbPoll Poll { get; set; }
		public required DbPollOption PollOption { get; set; }
		public required long VotedAt { get; set; }
	}
}