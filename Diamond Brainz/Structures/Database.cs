using MySql.Data.MySqlClient;

using System.Data.Common;
using System.Threading.Tasks;

using static Diamond.Brainz.Classes;

// https://dev.mysql.com/doc/connector-net/en/connector-net-tutorials-sql-command.html

namespace Diamond.Brainz
{
	public class Database
	{
		private MySqlConnection MySqlConnection { get; set; }

		public Database(DatabaseConfig dbConfig)
		{
			this.MySqlConnection = new MySqlConnection($"Server={dbConfig.Server};User Id={dbConfig.User};Password={dbConfig.Password};Database={dbConfig.Database}");
			this.MySqlConnection.Open();
		}

		~Database()
		{
			this.MySqlConnection.Close();
		}

		/// <summary>
		/// Returns multiple records.
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public async Task<DbDataReader> ExecuteReaderAsync(string sql)
		{
			using (MySqlCommand cmd = new MySqlCommand(sql, this.MySqlConnection))
			{
				return await cmd.ExecuteReaderAsync();
			}
		}

		/// <summary>
		/// Returns a single record.
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public async Task<object> ExecuteScalarAsync(string sql)
		{
			using (MySqlCommand cmd = new MySqlCommand(sql, this.MySqlConnection))
			{
				return await cmd.ExecuteScalarAsync();
			}
		}

		/// <summary>
		/// Does not return any record, but the count of rows updated.
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public async Task<int> ExecuteNonQueryAsync(string sql)
		{
			using (MySqlCommand cmd = new MySqlCommand(sql, this.MySqlConnection))
			{
				return await cmd.ExecuteNonQueryAsync();
			}
		}
	}
}
