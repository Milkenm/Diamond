using Diamond.Data.Models.CsgoItems;

using Microsoft.EntityFrameworkCore;

namespace Diamond.Data
{
	public partial class DiamondContext
	{
		public DbSet<DbCsgoItem> CsgoItems { get; set; }
		public DbSet<DbCsgoItemPrice> CsgoItemPrices { get; set; }
	}
}
