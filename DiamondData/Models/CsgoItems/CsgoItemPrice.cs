using System.ComponentModel.DataAnnotations.Schema;

using Diamond.Data.Enums;

namespace Diamond.Data.Models.CsgoItems
{
	[Table("CsgoItemPrices")]
	public class DbCsgoItemPrice
	{
		public long Id { get; set; }
		public required DbCsgoItem Item { get; set; }
		public required Currency Currency { get; set; }
		public required string Epoch { get; set; }
		public required double Average { get; set; }
		public required double Median { get; set; }
		public required long Sold { get; set; }
		public required float StandardDeviation { get; set; }
		public required double LowestPrice { get; set; }
		public required double HighestPrice { get; set; }
	}
}