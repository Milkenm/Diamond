#region Usings
using System;
using System.Windows.Forms;

using static DiamondGui.Core;
using static DiamondGui.Functions;
#endregion Usings



namespace DiamondGui
{
	public partial class Statistics : Form
	{
		#region Statistics
		public Statistics() => InitializeComponent();

		private void Statistics_FormClosing(object sender, FormClosingEventArgs e) => CloseForm(this, e);
		#endregion Statistics







		private void timer_updater_Tick(object sender, EventArgs e) => UpdateStatistics();
	}
}
