using Diamond.Data.Models.AutoPublisher;

using Microsoft.EntityFrameworkCore;

namespace Diamond.Data
{
	public partial class DiamondContext
	{
		public DbSet<DbAutoPublisherChannel> AutoPublisherChannels { get; set; }
	}
}
