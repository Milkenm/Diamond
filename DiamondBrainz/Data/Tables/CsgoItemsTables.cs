using System.ComponentModel.DataAnnotations.Schema;

using Diamond.API.APIs;

using Microsoft.EntityFrameworkCore;

namespace Diamond.API.Data
{
	public partial class DiamondContext
	{
		public DbSet<DbCsgoItem> CsgoItems { get; set; }
		public DbSet<DbCsgoItemPrice> CsgoItemPrices { get; set; }
	}

	[Table("CsgoItems")]
	public class DbCsgoItem
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public long ClassId { get; set; }
		public string IconUrl { get; set; }
		public string RarityHexColor { get; set; }
		public long FirstSaleDateUnix { get; set; }
	}

	[Table("CsgoItemPrices")]
	public class DbCsgoItemPrice
	{
		public long Id { get; set; }
		public DbCsgoItem Item { get; set; }
		public Currency Currency { get; set; }
		public string Epoch { get; set; }
		public double Average { get; set; }
		public double Median { get; set; }
		public long Sold { get; set; }
		public float StandardDeviation { get; set; }
		public double LowestPrice { get; set; }
		public double HighestPrice { get; set; }
	}
}