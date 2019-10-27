﻿#region Usings
using System;

using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static void ShowLp(LittleProgram _LP)
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
