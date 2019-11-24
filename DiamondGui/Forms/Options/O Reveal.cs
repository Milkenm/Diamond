#region Usings
using System;
using System.Windows.Forms;
using static DiamondGui.Functions;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Options
	{
		internal static void OptionsReveal(CheckBox cb, TextBox tb)
		{
			try
			{
				tb.PasswordChar = cb.Checked ? '•' : '\0';
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}