#region Usings
using System;
using System.Windows.Forms;
using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
    internal static partial class Core
    {
        internal static void MainLoad()
        {
			try
			{
				// ComboBox
				MainForm.comboBox_status.SelectedIndex = Settings.StatusIndex;

				// Load other forms
				OptionsForm = new Options(); OptionsLoad();
				StatisticsForm = new Statistics();
			}
			catch (Exception _Exception) { ShowException(_Exception, "DiamondGui.Core.Mainload()"); }
		}
	}
}
