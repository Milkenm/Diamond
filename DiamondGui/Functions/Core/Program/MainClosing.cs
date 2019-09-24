#region Usings
using System;
using System.Diagnostics;
using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
    internal static partial class Core
    {
        internal static void MainClosing()
        {
            try
            {
                if (Settings.BotToken != MainForm.textBox_token.Text) Settings.BotToken = MainForm.textBox_token.Text;
                if (Settings.LogTypeIndex != MainForm.comboBox_logType.SelectedIndex) Settings.LogTypeIndex = MainForm.comboBox_logType.SelectedIndex;
                if (Settings.Game != MainForm.textBox_activityName.Text) Settings.Game = MainForm.textBox_activityName.Text;
                if (Settings.ActivityIndex != MainForm.comboBox_activity.SelectedIndex) Settings.ActivityIndex = MainForm.comboBox_activity.SelectedIndex;
                if (Settings.StreamUrl != MainForm.textBox_streamUrl.Text) Settings.StreamUrl = MainForm.textBox_streamUrl.Text;
                if (Settings.StatusIndex != MainForm.comboBox_status.SelectedIndex) Settings.StatusIndex = MainForm.comboBox_status.SelectedIndex;

                Settings.Save();
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "DiamondGui.Core.MainClosing()");
            }
        }
    }
}