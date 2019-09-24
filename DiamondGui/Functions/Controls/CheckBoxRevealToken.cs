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
		internal static void CheckBoxRevealToken()
		{
			try
			{
                if (MainForm.checkBox_revealToken.Checked == true)  MainForm.textBox_token.PasswordChar = '•';
                else MainForm.textBox_token.PasswordChar = '\0';
			}
			catch (Exception _Exception)
			{
                ShowException(_Exception, "DiamondGui.Controls.CheckBoxRevealToken()");
            }
		}
	}
}