#region Usings
using System;
using System.Windows.Forms;

using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static void ShowException(Exception _Exception)
		{
			Settings.Exceptions += 1; Settings.Save();

			MessageBox.Show("Message:\n" + _Exception.Message + "\n\n\nTarget Site:\n" + _Exception.TargetSite + "\n\n\nStack Trace:\n" + _Exception.StackTrace, "Error - " + _Exception.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
