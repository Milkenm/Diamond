#region Usings
using System;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Core
    {
        internal static void MainClosing()
        {
            try
            {
                Settings.StatusIndex = MainForm.comboBox_status.SelectedIndex;

                Settings.Save();
            }
            catch (Exception _Exception) { ShowException(_Exception, "DiamondGui.Core.MainClosing()"); }
        }
    }
}