#region Usings
using Discord;
using System;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
    internal static partial class Functions
    {
        internal static UserStatus GetUserStatus()
        {
            UserStatus _UserStatus = UserStatus.Online;

            try
            {
                MainForm.Invoke(new Action(() =>
                {
                    if (MainForm.comboBox_status.Text == "Do Not Disturb") _UserStatus = UserStatus.DoNotDisturb;
                    else if (MainForm.comboBox_status.Text == "Idle") _UserStatus = UserStatus.Idle;
                    else if (MainForm.comboBox_status.Text == "Invisible") _UserStatus = UserStatus.Invisible;
                }));
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "DiamondGui.Functions.GetUserStatus()");
            }

            return _UserStatus;
        }
    }
}
