using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.CsgoItems
{
	[Table("CsgoItems")]
	public class DbCsgoItem
	{
		public long Id { get; set; }
		public required string Name { get; set; }
		public required long ClassId { get; set; }
		public required string IconUrl { get; set; }
		public required string RarityHexColor { get; set; }
		public required long FirstSaleDateUnix { get; set; }
	}
}