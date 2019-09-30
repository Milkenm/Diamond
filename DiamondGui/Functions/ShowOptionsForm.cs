#region Usings
using System;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Controls
	{
		internal static void ShowOptionsForm()
		{
			try
			{
				OptionsForm.Show();
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}
