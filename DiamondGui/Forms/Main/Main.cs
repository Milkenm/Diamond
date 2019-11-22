#region Usings
using System;
using System.Windows.Forms;

using static DiamondGui.DiscordCore;
using static DiamondGui.Functions;
using static DiamondGui.Main;
using static DiamondGui.Static;
#endregion Usings

// # = #
// 1* Move form without title bar: https://stackoverflow.com/questions/23966253/moving-form-without-title-bar
// # = #

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
			CloseForm(this, e);
		}
		#endregion Main


		protected override void WndProc(ref Message msg) // 1*
		{
			switch (msg.Msg)
			{
				case 0x84:
					{
						base.WndProc(ref msg);
						if ((int)msg.Result == 0x1)
						{
							msg.Result = (IntPtr)0x2;
						}

						return;
					}
			}
			base.WndProc(ref msg);
		}



		private void button_start_Click(object sender, EventArgs e) // Button 'Start'
		{
			MainDiscordStart();
		}

		private void button_options_Click(object sender, EventArgs e) // Button 'Options...'
		{
			OptionsForm.Show();
		}

		private void comboBox_status_SelectedIndexChanged(object sender, EventArgs e) // ComboBox 'Status'
		{
			SetDiscordStatus();
		}

		private void timer_uptime_Tick(object sender, EventArgs e) // Timer 'Uptime'
		{
			MainUpdateUptime();
		}

		private void button_statistics_Click(object sender, EventArgs e) // Button 'Statistics'
		{
			StatisticsForm.Show();
		}

		private void button_close_Click(object sender, EventArgs e) // Button 'X'
		{
			Application.Exit();
		}

		private void button_minimize_Click(object sender, EventArgs e) // Button '_'
		{
			WindowState = FormWindowState.Minimized;
		}

		private void button_littlePrograms_Click(object sender, EventArgs e) // Button 'Little Programs'
		{
			LpLauncherForm.Show();
		}

		private void button_reload_Click(object sender, EventArgs e) // Button 'Reload'
		{
			DiscordCoreLoader(true).GetAwaiter();
		}

		private void Main_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			MessageBox.Show("yup");
		}
	}
}