using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Settings
{
	[Table("Setting")]
	public class DbSetting
	{
		public long Id { get; set; }
		[Column("Setting")] public required string Name { get; set; }
		public required string Value { get; set; }
	}
}