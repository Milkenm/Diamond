#region Usings
using System.Windows.Forms;

using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static bool Closing = false;

		internal static void AppExit()
		{
			if (!Closing)
			{
				Closing = true;


				Settings.Save();

				OptionsForm.Close();
				StatisticsForm.Close();
				LpLauncherForm.Close();
				MainForm.Close();

				Application.Exit();
			}
		}
	}
}
