#region Usings
using System;

using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static void ShowLittleProgramsForm()
		{
			try
			{
				LpLauncherForm.Show();
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}
