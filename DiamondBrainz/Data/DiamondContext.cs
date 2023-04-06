using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace Diamond.API.Data;
public partial class DiamondDatabase
{
	public DbSet<Setting> Settings { get; set; }
	public DbSet<CacheRecord> Cache { get; set; }
}

public class Setting
{
	public long Id { get; set; }
	[Column("Setting")] public string Name { get; set; }
	[NotMapped] public string Value { get; set; }
}

public class CacheRecord
{
	public long Id { get; set; }
	public string Key { get; set; }
	public byte[] Value { get; set; }
	public long UpdatedAt { get; set; }
}
