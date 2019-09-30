#region Usings
using System;

using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static void ShowStatisticsForm()
		{
			try
			{
				StatisticsForm.Show();
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}