using Discord;

using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

using static DiamondGui.Functions;
using static DiamondGui.Static;
using static ScriptsLib.Controls.Tweaks.SlForm;

namespace DiamondGui.Forms
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();
			mainForm = this;
		}

		private void Main_Load(object sender, EventArgs e)
		{
			string token = null;
			try
			{
				using (SQLiteConnection sqliteConnection = new SQLiteConnection($@"Data Source={Directory.GetCurrentDirectory()}\DiamondDB.db; Version=3;"))
				{
					using (SQLiteCommand cmd = sqliteConnection.CreateCommand())
					{
						cmd.CommandText = "SELECT Value FROM Configs WHERE Config = 'BotToken'";
						SQLiteDataAdapter da = new SQLiteDataAdapter(cmd.CommandText, sqliteConnection);

						DataTable dt = new DataTable();
						da.Fill(dt);

						token = dt.Rows[0][0].ToString();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			diamondCore = new DiamondCore(token, LogSeverity.Info, "!", Assembly.GetEntryAssembly(), Client_Log);

			// ComboBox
			comboBox_status.SelectedIndex = settings.StatusIndex;

			// Load other forms
			optionsForm = new Options();
			privateChatForm = new PrivateChat();

			// Load APIs
			LoadAPIKeys();

			// Make the Main form dragable
			AllowDrag(this);
		}

		private Task Client_Log(LogMessage msg)
		{
			string[] _Log = new string[4];

			_Log[0] = string.Empty;
			_Log[1] = msg.Severity.ToString();
			_Log[2] = msg.Source;
			_Log[3] = msg.Message;

			Invoke(new Action(() =>
			{
				listView_log.Items.Add(new ListViewItem(_Log));
				listView_log.Items[listView_log.Items.Count - 1].EnsureVisible();
			}));

			return Task.CompletedTask;
		}

		private void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			AppExit();
		}

		// Button 'Start'
		private void button_start_Click(object sender, EventArgs e)
		{
			if (button_start.Text == "Start")
			{
				diamondCore.Start();
			}
			else
			{
				diamondCore.Stop();
			}

			button_start.Text = button_start.Text == "Start" ? "Stop" : "Start";
			button_reload.Enabled = button_start.Text == "Stop";

			optionsForm.textBox_token.Enabled = mainForm.button_start.Text == "Start";
			optionsForm.comboBox_logType.Enabled = mainForm.button_start.Text == "Start";
		}

		// Button 'Options...'
		private void button_options_Click(object sender, EventArgs e)
		{
			optionsForm.Show();
		}

		// ComboBox 'Status'
		private void comboBox_status_SelectedIndexChanged(object sender, EventArgs e)
		{
			diamondCore.SetStatus(GetUserStatus(mainForm.comboBox_status.Text));
		}

		// Button <close> 'X'
		private void button_close_Click(object sender, EventArgs e)
		{
			AppExit();
		}

		// Button <minimize> '_'
		private void button_minimize_Click(object sender, EventArgs e)
		{
			WindowState = FormWindowState.Minimized;
		}

		// Button 'Reload'
		private void button_reload_Click(object sender, EventArgs e)
		{
			diamondCore.Reload();
		}

		// Button 'Private Chat'
		private void button_privateChat_Click(object sender, EventArgs e)
		{
			if (diamondCore.Client != null)
			{
				privateChatForm.Show();
			}
			else
			{
				MessageBox.Show("Client not initialized.");
			}
		}
	}
}