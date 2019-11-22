#region Usings
using System;
using System.Windows.Forms;

using static DiamondGui.Functions;
using static DiamondGui.Statistics;
#endregion Usings



namespace DiamondGui.Forms
{
	public partial class Statistics : Form
	{
		#region Statistics
		public Statistics()
		{
			InitializeComponent();
		}

		private void Statistics_FormClosing(object sender, FormClosingEventArgs e)
		{
			CloseForm(this, e);
		}
		#endregion Statistics







		private void timer_updater_Tick(object sender, EventArgs e)
		{
			S_UpdateStatistics();
		}
	}
}
