using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace Diamond.API.Data;
public partial class DiamondDatabase
{
	public DbSet<Poll> Polls { get; set; }
	public DbSet<PollOption> PollOptions { get; set; }
	public DbSet<PollVote> PollVotes { get; set; }
}

public class Poll
{
	public long Id { get; set; }
	public ulong DiscordMessageId { get; set; }
	public ulong DiscordUserId { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string ImageUrl { get; set; }
	public string ThumbnailUrl { get; set; }
	[DefaultValue(false)] public bool IsPublished { get; set; }
	public long CreatedAt { get; set; }
	public long UpdatedAt { get; set; }
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
	public long VotedAt { get; set; }
}
