#region Usings
using System;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Options
	{
		internal static void RevealToken()
		{
			try
			{
				OptionsForm.textBox_token.PasswordChar = OptionsForm.checkBox_revealToken.Checked == true ? '•' : '\0';
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}