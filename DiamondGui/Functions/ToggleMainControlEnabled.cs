#region Usings
using System;
using System.Diagnostics;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
    internal static partial class Functions
    {
        internal static void ToggleMainControlEnabled()
        {
            try
            {
                if (MainForm.button_start.Text == "Start") MainForm.button_start.Text = "Stop";
                else MainForm.button_start.Text = "Start";



                MainForm.textBox_token.Enabled = MainForm.button_start.Text == "Start";
                MainForm.comboBox_logType.Enabled = MainForm.button_start.Text == "Start";

                MainForm.button_refreshGame.Enabled = MainForm.button_start.Text == "Stop";
                MainForm.button_refreshStatus.Enabled = MainForm.button_start.Text == "Stop";
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "DiamondGui.Functions.ToggleMainControlEnabled()");
            }
        }
    }
}