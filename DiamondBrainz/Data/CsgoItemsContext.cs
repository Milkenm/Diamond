using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.IO;

using ScriptsLibV2.Util;

namespace Diamond.API.Data;
public class CsgoItemsContext : IDatabaseContext
{
	public DbSet<CsgoItem> CsgoItems { get; set; }
	public DbSet<CsgoPrice> CsgoPrices { get; set; }
	public DbSet<CsgoRarity> CsgoRarities { get; set; }

	public CsgoItemsContext() : base(Path.Join(Utils.GetInstallationFolder() + @"\Data\CsgoItemsDB.db")) { }
}

[Table("Items")]
public class CsgoItem
{
	[Column("ID")] public long Id { get; set; }
	public string Name { get; set; }
	public string IconUrl { get; set; }
	public string Type { get; set; }
	public CsgoRarity Rarity { get; set; }
}

[Table("Prices")]
public class CsgoPrice
{
	public long Id { get; set; }
	public CsgoItem TargetItem { get; set; } = new CsgoItem();
	public string Currency { get; set; }
	public double Average { get; set; }
	public double Median { get; set; }
	public long Sold { get; set; }
	public double StandardDeviation { get; set; }
	public double LowestPrice { get; set; }
	public double HighestPrice { get; set; }
}

[Table("Rarities")]
public class CsgoRarity
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string Color { get; set; }
}