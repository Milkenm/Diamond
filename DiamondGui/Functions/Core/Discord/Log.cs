#region Usings
using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Discord;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Core
	{
		internal static Task DiscordLog(LogMessage LogMsg)
		{
			try
			{
				MainForm.Invoke(new Action(() =>
				{
					MainForm.listBox_output.Items.Add(LogMsg.ToString());
				}));
			}
			catch (Exception _Exception)
			{
                ShowException(_Exception, new StackFrame().GetMethod().DeclaringType.ReflectedType.ToString());
            }

			return Task.CompletedTask;
		}
	}
}
