#region Usings
using System;
using System.Windows.Forms;

using static DiamondGui.DiscordCore;
using static DiamondGui.Functions;
using static DiamondGui.Main;
using static DiamondGui.Static;
#endregion Usings

namespace DiamondGui.Forms
{
	public partial class Main : Form
	{
		#region Main
		public Main()
		{
			try
			{
				InitializeComponent();
				MainForm = this;
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}

		private void Main_Load(object sender, EventArgs e)
		{
			MainLoad();
		}

		private void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			AppExit();
		}
		#endregion Main






		// Button 'Start'
		private void button_start_Click(object sender, EventArgs e)
		{
			MainDiscordStart();
		}

		// Button 'Options...'
		private void button_options_Click(object sender, EventArgs e)
		{
			OptionsForm.Show();
		}

		// ComboBox 'Status'
		private void comboBox_status_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetDiscordStatus();
		}

		// Timer 'Uptime'
		private void timer_uptime_Tick(object sender, EventArgs e)
		{
			MainUpdateUptime();
		}

		// Button 'Statistics'
		private void button_statistics_Click(object sender, EventArgs e)
		{
			StatisticsForm.Show();
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

		// Button 'Little Programs'
		private void button_littlePrograms_Click(object sender, EventArgs e)
		{
			LpLauncherForm.Show();
		}

		// Button 'Reload'
		private void button_reload_Click(object sender, EventArgs e)
		{
			DiscordCoreLoader(true).GetAwaiter();
		}

		// Button 'Private Chat'
		private void button_privateChat_Click(object sender, EventArgs e)
		{
			if (Client != null)
			{
				PrivateChatForm.Show();
			}
			else
			{
				MessageBox.Show("Client not initialized.");
			}
		}
	}
}