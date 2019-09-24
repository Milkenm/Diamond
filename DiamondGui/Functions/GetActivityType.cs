#region Usings
using Discord;
using System;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
    internal static partial class Functions
    {
        internal static ActivityType GetActivityType()
        {
            ActivityType _ActivityType = ActivityType.Playing;

            try
            {
                MainForm.Invoke(new Action(() =>
                {
                    if (MainForm.comboBox_logType.Text == "Listening") _ActivityType = ActivityType.Listening;
                    else if (MainForm.comboBox_logType.Text == "Streaming") _ActivityType = ActivityType.Streaming;
                    else if (MainForm.comboBox_logType.Text == "Watching") _ActivityType = ActivityType.Watching;
                }));
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "DiamondGui.Functions.GetActivityType()");
            }

            return _ActivityType;
        }
    }
}
