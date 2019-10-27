#region Usings
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using static DiamondGui.Controls;
using static DiamondGui.Core;
using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings

// # = #
// 1* Drag form without borders: https://stackoverflow.com/questions/23966253/moving-form-without-title-bar
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

        private void Main_Load(object sender, EventArgs e) => MainLoad();

        private void Main_FormClosing(object sender, FormClosingEventArgs e) => CloseForm(this, e);
		#endregion Main


		protected override void WndProc(ref Message _Message) // 1*
		{
			switch (_Message.Msg)
			{
				case 0x84:
					{
						base.WndProc(ref _Message);
						if ((int)_Message.Result == 0x1) _Message.Result = (IntPtr)0x2;
						return;
					}
			}
			base.WndProc(ref _Message);
		}
		



		private void button_start_Click(object sender, EventArgs e) => DiscordStart(); // Button 'Start'

		private void button_options_Click(object sender, EventArgs e) => ShowOptionsForm(); // Button 'Options'

		private void comboBox_status_SelectedIndexChanged(object sender, EventArgs e) => SetDiscordStatus(); // ComboBox 'Status'

		private void timer_uptime_Tick(object sender, EventArgs e) => UpdateUptime(); // Timer 'Uptime'

		private void button_statistics_Click(object sender, EventArgs e) => ShowStatisticsForm(); // Button 'Statistics'

		private void button_close_Click(object sender, EventArgs e) => Application.Exit(); // Button 'X'

		private void button_minimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized; // Button '_'

		private void button_littlePrograms_Click(object sender, EventArgs e) => ShowLittleProgramsForm(); // Button 'Little Programs'
	}
}