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
				if (Settings.BotToken != MainForm.textBox_token.Text) Settings.BotToken = MainForm.textBox_token.Text;
				if (Settings.LogTypeIndex != MainForm.comboBox_logType.SelectedIndex) Settings.LogTypeIndex = MainForm.comboBox_logType.SelectedIndex;

				Settings.Save();
			}
			catch (Exception _Exception)
			{
				ShowException(_Exception, "Core.MainClosing()");
			}
		}
	}
}