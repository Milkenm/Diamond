#region Usings
using Discord;
using System;
using System.Diagnostics;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static LogSeverity GetLogLevel()
		{
			LogSeverity _LogType = LogSeverity.Info;

			try
			{
				MainForm.Invoke(new Action(() =>
				{
					if (OptionsForm.comboBox_logType.Text == "Critical") _LogType = LogSeverity.Critical;
					else if (OptionsForm.comboBox_logType.Text == "Debug") _LogType = LogSeverity.Debug;
					else if (OptionsForm.comboBox_logType.Text == "Error") _LogType = LogSeverity.Error;
					else if (OptionsForm.comboBox_logType.Text == "Verbose") _LogType = LogSeverity.Verbose;
					else if (OptionsForm.comboBox_logType.Text == "Warning") _LogType = LogSeverity.Warning;
				}));
			}
			catch (Exception _Exception) { ShowException(_Exception, "DiamondGui.Functions.GetLogLevel()"); }

			return _LogType;
		}
	}
}
