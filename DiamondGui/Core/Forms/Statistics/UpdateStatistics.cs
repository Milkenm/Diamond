#region Usings
using System;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Core
	{
		internal static void UpdateStatistics()
		{
			try
			{
				StatisticsForm.label_uptime.Text = "Uptime: " + Settings.Uptime + " seconds";
				StatisticsForm.label_commandsUsed.Text = "Commands Used: " + Settings.CommandsUsed;
				StatisticsForm.label_exceptions.Text = "Exceptions: " + Settings.Exceptions;
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}
