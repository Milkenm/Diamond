using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

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
	}
}
