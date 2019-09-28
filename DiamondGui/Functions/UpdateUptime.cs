#region Usings
using System;

using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static void UpdateUptime()
		{
			try
			{
				Settings.Uptime += 1; Settings.Save();
			}
			catch (Exception _Exception) { ShowException(_Exception, "DiamondGui.Functions.UpdateUptime()"); }
		}
	}
}
