#region Usings
using System;
using System.Windows.Forms;

using static DiamondGui.Controls;
using static DiamondGui.Core;
using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
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
				OptionsForm = new Options();
			}
			catch (Exception _Exception) { ShowException(_Exception, "DiamondGui.Main.Main()"); }
		}

		private void Main_Load(object sender, EventArgs e) => MainLoad();

		private void Main_FormClosing(object sender, FormClosingEventArgs e) => MainClosing();
		#endregion Main







		private void button_start_Click(object sender, EventArgs e) => DiscordStart(); // Button 'Start'

		private void button_options_Click(object sender, EventArgs e) => ShowOptionsForm(); // Button 'Options'

		private void comboBox_status_SelectedIndexChanged(object sender, EventArgs e) => SetDiscordStatus(); // ComboBox 'Status'

		private void timer_uptime_Tick(object sender, EventArgs e) => UpdateUptime(); // Timer 'Uptime'
	}
}