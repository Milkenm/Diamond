using ScriptsLibV2.Databases;

namespace Diamond.API
{
	public class Database : SQLiteDB
	{
		public Database(string databasePath) : base(string.Format(DEFAULT_CONNECTION_STRING, databasePath)) { }
	}
}
