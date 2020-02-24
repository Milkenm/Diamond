#region Usings

using DiamondGui.LittlePrograms;

using System;

using static DiamondGui.Functions;
using static DiamondGui.Options;
using static DiamondGui.Static;

#endregion Usings

namespace DiamondGui
{
	internal static partial class Main
	{
		internal static void MainLoad()
		{
			try
			{
				// ComboBox
				MainForm.comboBox_status.SelectedIndex = Settings.StatusIndex;

				// Load other forms
				OptionsForm = new Forms.Options(); OptionsLoad();
				StatisticsForm = new Forms.Statistics();
				LpLauncherForm = new Forms.LPLauncher();
				PrivateChatForm = new Forms.PrivateChat();

				// Load LittlePrograms
				LP_GradientGeneratorForm = new GradientGenerator();

				// Load APIs
				LoadAPIKeys();
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}