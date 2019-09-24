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
        internal static void ButtonSetGame()
        {
            try
            {
                Client.SetGameAsync(MainForm.textBox_game.Text);
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, new StackFrame().GetMethod().DeclaringType.ReflectedType.ToString());
            }
        }
    }
}
