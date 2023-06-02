using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace Diamond.API.Data
{
	public partial class DiamondContext
	{
		public DbSet<PublishChannel> AutoPublisherChannels { get; set; }
	}

	[Table("AutoPublisher")]
	public class PublishChannel
	{
		public long Id { get; set; }
		public ulong GuildId { get; set; }
		public ulong ChannelId { get; set; }
		[Column("TrackingSince")] public long TrackingSinceUnix { get; set; }
		public ulong AddedByUserId { get; set; }
	}
}
