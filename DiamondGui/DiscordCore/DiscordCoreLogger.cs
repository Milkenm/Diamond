#region Usings
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

using Discord;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class DiscordCore
	{
		internal static Task DiscordCoreLogger(LogMessage LogMsg)
		{
			try
			{
				string[] _Log = new string[4];

				_Log[0] = string.Empty;
				_Log[1] = LogMsg.Severity.ToString();
				_Log[2] = LogMsg.Source;
				_Log[3] = LogMsg.Message;

				MainForm.Invoke(new Action(() =>
				{
					MainForm.listView_log.Items.Add(new ListViewItem(_Log));
					MainForm.listView_log.Items[MainForm.listView_log.Items.Count - 1].EnsureVisible();
				}));
			}
			catch (Exception _Exception) { ShowException(_Exception); }

			return Task.CompletedTask;
		}
	}
}
