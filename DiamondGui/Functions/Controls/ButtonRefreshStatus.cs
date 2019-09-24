#region Usings
using System;
using System.Diagnostics;
using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
    internal static partial class Controls
    {
        internal static void ButtonRefreshStatus()
        {
            try
            {
                SetDiscordStatus();
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "DiamondGui.Controls.ButtonRefreshStatus()");
            }
        }
    }
}
