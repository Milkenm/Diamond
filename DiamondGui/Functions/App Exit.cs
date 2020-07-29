using System.Windows.Forms;

using static DiamondGui.Static;

namespace DiamondGui
{
	internal static partial class Functions
	{
		private static bool closing;

		internal static void AppExit()
		{
			if (!closing)
			{
				closing = true;

				settings.Save();

				optionsForm.Close();
				mainForm.Close();

				Application.Exit();
			}
		}
	}
}