﻿
using System.Data;
using System.Data.SQLite;

namespace Diamond.Brainz.Structures
{
	public class SQLiteDB
	{
		private readonly string DatabasePath;

		public SQLiteDB(string databasePath)
		{
			DatabasePath = databasePath;

			GenerateDatabase();
		}

		public DataTable ExecuteSQL(string sql)
		{
			DataTable dt = new DataTable();

			using (SQLiteConnection sqliteConnection = new SQLiteConnection($"Data Source={DatabasePath}; Version=3;"))
			{
				using (SQLiteCommand cmd = sqliteConnection.CreateCommand())
				{
					cmd.CommandText = sql;
					SQLiteDataAdapter da = new SQLiteDataAdapter(cmd.CommandText, sqliteConnection);

					da.Fill(dt);
				}
			}

			return dt;
		}

		public void GenerateDatabase()
		{
			ExecuteSQL("CREATE TABLE IF NOT EXISTS Configs (Config TEXT, Value TEXT)");
		}
	}
}