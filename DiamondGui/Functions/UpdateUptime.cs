#region Usings
using System;

using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static int CurrentUptime;

		internal static void UpdateUptime()
		{
			try
			{
				Settings.Uptime += 1; Settings.Save();
				CurrentUptime += 1;
			}
			catch (Exception _Exception) { ShowException(_Exception, "DiamondGui.Functions.UpdateUptime()"); }
		}
	}
}
