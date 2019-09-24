#region Usings
using System;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
    internal static partial class Controls
    {
        internal static void ButtonSetGame()
        {
            try
            {
                Client.SetGameAsync(MainForm.textBox_game.Text);
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "Controls.ButtonSetGame()");
            }
        }
    }
}
