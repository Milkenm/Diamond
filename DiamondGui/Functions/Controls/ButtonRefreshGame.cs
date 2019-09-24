#region Usings
using System;
using System.Diagnostics;
using static DiamondGui.Functions;
#endregion Usings



namespace DiamondGui
{
    internal static partial class Controls
    {
        internal static void ButtonRefreshGame()
        {
            try
            {
                SetDiscordGame();
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "DiamondGui.Controls.ButtonRefreshGame()");
            }
        }
    }
}
