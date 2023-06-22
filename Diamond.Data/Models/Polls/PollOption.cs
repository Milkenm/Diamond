using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Polls
{
	[Table("PollOption")]
	public class DbPollOption
	{
		public long Id { get; set; }
		public required DbPoll TargetPoll { get; set; }
		[Column("OptionName")] public required string Name { get; set; }
		[Column("OptionDescription")] public string? Description { get; set; }
	}
}