#region Usings
using System;
using System.Windows.Forms;
using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
    internal static partial class Core
    {
        internal static void MainLoad()
        {
            try
            {
                // TextBox
                MainForm.textBox_token.Text = Settings.BotToken;
                MainForm.textBox_activityName.Text = Settings.Game;
                MainForm.textBox_streamUrl.Text = Settings.StreamUrl;

                // ComboBox
                MainForm.comboBox_logType.SelectedIndex = Settings.LogTypeIndex;
                MainForm.comboBox_activity.SelectedIndex = Settings.ActivityIndex;
                MainForm.comboBox_status.SelectedIndex = Settings.StatusIndex;
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "DiamondGui.Core.Mainload()");
            }
        }
    }
}
