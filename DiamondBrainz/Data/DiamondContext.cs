using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

using Microsoft.EntityFrameworkCore;

using ScriptsLibV2.Util;

namespace Diamond.API.Data;
public class DiamondContext : IDatabaseContext
{
	public DbSet<Setting> Settings { get; set; }

	public DiamondContext() : base(Path.Join(Utils.GetInstallationFolder(), @"\Data\DiamondDB.db")) { }

	public class Setting
	{
		public long Id { get; set; }
		[Column("Setting")] public string Name { get; set; }
		public object Value { get; set; }
	}
}
