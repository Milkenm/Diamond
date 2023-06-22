using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Settings
{
	[Table("CacheRecord")]
	public class DbCacheRecord
	{
		public long Id { get; set; }
		public required string Key { get; set; }
		public required byte[] Value { get; set; }
		public required long UpdatedAt { get; set; }
	}
}