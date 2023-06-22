using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.AutoPublisher
{
	[Table("AutoPublisher")]
	public class DbAutoPublisherChannel
	{
		public long Id { get; set; }
		public required ulong GuildId { get; set; }
		public required ulong ChannelId { get; set; }
		[Column("TrackingSince")] public required long TrackingSinceUnix { get; set; }
		public required ulong AddedByUserId { get; set; }
	}
}
