#region Usings
using System;
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
                MainForm.textBox_token.Enabled = MainForm.button_start.Text != "Start";
                MainForm.comboBox_logType.Enabled = MainForm.button_start.Text != "Start";

                if (MainForm.button_start.Text == "Start") MainForm.button_start.Text = "Stop";
                else MainForm.button_start.Text = "Start";
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "Functions.TogglemainControlEnabled()");
            }
        }
    }
}