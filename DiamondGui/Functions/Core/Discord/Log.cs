#region Usings
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                string[] _Log = new string[2];

                _Log[0] = LogMsg.Severity.ToString();
                _Log[1] = LogMsg.Source;
                _Log[2] = LogMsg.Message;

                MainForm.Invoke(new Action(() =>
                {
                    MainForm.listView_log.Items.Add(new ListViewItem(_Log));
                }));
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "DiamondGui.Core.DiscordLog()");
            }

            return Task.CompletedTask;
        }
    }
}
