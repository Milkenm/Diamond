using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace TestingProject
{
	public partial class SQLite : Form
	{
		private static SQLiteConnection sqliteConnection;

		public SQLite()
		{
			InitializeComponent();

			sqliteConnection = new SQLiteConnection(@"Data Source=C:\Milkenm\Data\DiamondDB.db; Version=3;");
			sqliteConnection.Open();
		}

		private void button_run_Click(object sender, EventArgs e)
		{
			SQLiteDataAdapter da = null;
			DataTable dt = new DataTable();

			try
			{
				using (SQLiteCommand cmd = sqliteConnection.CreateCommand())
				{
					cmd.CommandText = textBox_sql.Text;
					da = new SQLiteDataAdapter(cmd.CommandText, sqliteConnection);
					da.Fill(dt);

					dataGrid.DataSource = dt;
				}

				foreach (DataRow row in dt.Rows)
				{
					foreach (DataColumn column in dt.Columns)
					{
						object item = row[column];

						MessageBox.Show(column + " | " + item.ToString());
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
