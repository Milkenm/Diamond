#region Usings
using Discord;
using System;
using static DiamondGui.Static;
using static DiamondGui.Functions;
#endregion Usings



namespace DiamondGui
{
    internal static partial class Functions
    {
        internal static void SetDiscordGame()
        {
            try
            {
                MainForm.Invoke(new Action(() =>
                {
                    if (Client.LoginState == LoginState.LoggedIn)
                    {
                        if (MainForm.comboBox_activity.Text == "Listening") Client.SetGameAsync(MainForm.textBox_activityName.Text, null, ActivityType.Listening);
                        else if (MainForm.comboBox_activity.Text == "Playing") Client.SetGameAsync(MainForm.textBox_activityName.Text, null, ActivityType.Playing);
                        else if (MainForm.comboBox_activity.Text == "Streaming") Client.SetGameAsync(MainForm.textBox_activityName.Text, MainForm.textBox_streamUrl.Text, ActivityType.Streaming);
                        else if (MainForm.comboBox_activity.Text == "Watching") Client.SetGameAsync(MainForm.textBox_activityName.Text, null, ActivityType.Watching);
                    }
                }));
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "DiamondGui.Functions.SetDiscordGame()");
            }
        }
    }
}