using Diamond.Data.Models.Polls;

using Microsoft.EntityFrameworkCore;

namespace Diamond.Data
{
	public partial class DiamondContext
	{
		public DbSet<DbPoll> Polls { get; set; }
		public DbSet<DbPollOption> PollOptions { get; set; }
		public DbSet<DbPollVote> PollVotes { get; set; }
	}
}
