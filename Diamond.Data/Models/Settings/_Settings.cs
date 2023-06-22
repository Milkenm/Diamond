using Diamond.Data.Models.Settings;

using Microsoft.EntityFrameworkCore;

namespace Diamond.Data
{
	public partial class DiamondContext
	{
		public DbSet<DbSetting> Settings { get; set; }
		public DbSet<DbCacheRecord> CacheRecords { get; set; }
	}
}