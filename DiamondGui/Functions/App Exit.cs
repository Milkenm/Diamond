#region Usings

using System.Windows.Forms;

using static DiamondGui.Static;

#endregion Usings

namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static bool Closing;

		internal static void AppExit()
		{
			if (!Closing)
			{
				Closing = true;

				settings.Save();

				optionsForm.Close();
				mainForm.Close();

				Application.Exit();
			}
		}
	}
}