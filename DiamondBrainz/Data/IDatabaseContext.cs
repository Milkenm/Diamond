using Microsoft.EntityFrameworkCore;

namespace Diamond.API.Data;
public abstract class IDatabaseContext : DbContext
{
	public string DbPath { get; }

	public IDatabaseContext(string databasePath)
	{
		DbPath = databasePath;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
}
