#region Usings
using System;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Controls
	{
		internal static void ButtonRevealToken()
		{
			try
			{
				if (MainForm.textBox_token.PasswordChar == '•') MainForm.textBox_token.PasswordChar = '\0';
				else MainForm.textBox_token.PasswordChar = '•';
			}
			catch (Exception _Exception)
			{
				ShowException(_Exception, "Controls.ButtonRevealToken()");
			}
		}
	}
}