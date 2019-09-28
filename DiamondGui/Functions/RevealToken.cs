#region Usings
using System;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Controls
	{
		internal static void RevealToken()
		{
			try
			{
                if (OptionsForm.checkBox_revealToken.Checked == true)  OptionsForm.textBox_token.PasswordChar = '•';
                else OptionsForm.textBox_token.PasswordChar = '\0';
			}
			catch (Exception _Exception) { ShowException(_Exception, "DiamondGui.Functions.RevealToken()"); }
		}
	}
}