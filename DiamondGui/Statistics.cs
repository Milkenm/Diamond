#region Usings
using System;
using System.Windows.Forms;

using static DiamondGui.Core;
#endregion Usings



namespace DiamondGui
{
	public partial class Statistics : Form
	{
		#region Statistics
		public Statistics() => InitializeComponent();
		#endregion Statistics







		private void timer_updater_Tick(object sender, EventArgs e) => UpdateStatistics();
	}
}
