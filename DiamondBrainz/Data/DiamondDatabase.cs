using System.IO;

namespace Diamond.API.Data;
public partial class DiamondDatabase : IDatabaseContext
{
	public DiamondDatabase() : base(Path.Join(ScriptsLibV2.Util.Utils.GetInstallationFolder(), @"\Data\DiamondDB.db")) { }
}
