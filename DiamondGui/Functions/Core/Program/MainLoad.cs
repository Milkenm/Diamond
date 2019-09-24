#region Usings
using System;
using System.Diagnostics;
using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Core
	{
		internal static void MainLoad()
		{
			try
			{
				MainForm.textBox_token.Text = Settings.BotToken;
				MainForm.comboBox_logType.SelectedIndex = Settings.LogTypeIndex;
			}
			catch (Exception _Exception)
			{
                ShowException(_Exception, new StackFrame().GetMethod().DeclaringType.ReflectedType.ToString());
            }
		}
	}
}
