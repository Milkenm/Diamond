#region Usings
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using DiamondGui.Forms;
using DiamondGui.LittlePrograms;

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
				LpLauncherForm = new LpLauncher();

				// Load LittlePrograms
				LP_GradientGeneratorForm = new GradientGenerator();
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}
