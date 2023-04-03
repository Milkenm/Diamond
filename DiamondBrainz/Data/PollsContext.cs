using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

using Microsoft.EntityFrameworkCore;

using ScriptsLibV2.Util;

namespace Diamond.API.Data;
public class PollsContext : IDatabaseContext
{
	public DbSet<Poll> Polls { get; set; }
	public DbSet<PollOption> PollOptions { get; set; }
	public DbSet<PollVote> PollVotes { get; set; }

	public PollsContext() : base(Path.Join(Utils.GetInstallationFolder(), @"\Data\PollsDB.db")) { }

	public class Poll
	{
		public long Id { get; set; }
		public ulong DiscordMessageId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public bool IsPublished { get; set; } = false;
	}

	public class PollOption
	{
		public long Id { get; set; }
		public Poll TargetPoll { get; set; }
		[Column("OptionName")] public string Name { get; set; }
		[Column("OptionDescription")] public string Description { get; set; }
	}

	public class PollVote
	{
		public long Id { get; set; }
		public ulong UserId { get; set; }
		public Poll Poll { get; set; }
		public PollOption PollOption { get; set; }
	}
}
