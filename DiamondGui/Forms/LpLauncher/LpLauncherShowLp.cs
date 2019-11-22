#region Usings
using System;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class LpLauncher
	{
		internal static void LpLauncherShowLp(LittleProgram _LP)
		{
			try
			{
				LP_GradientGeneratorForm.Show();
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}

		internal enum LittleProgram
		{
			GradientGenerator,
		}
	}
}
