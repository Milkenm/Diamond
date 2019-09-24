﻿#region Usings
using System;
using Discord;

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
					if (MainForm.comboBox_logType.Text == "Critical") _LogType = LogSeverity.Critical;
					else if (MainForm.comboBox_logType.Text == "Debug") _LogType = LogSeverity.Debug;
					else if (MainForm.comboBox_logType.Text == "Error") _LogType = LogSeverity.Error;
					else if (MainForm.comboBox_logType.Text == "Info") _LogType = LogSeverity.Info;
					else if (MainForm.comboBox_logType.Text == "Verbose") _LogType = LogSeverity.Verbose;
					else _LogType = LogSeverity.Warning;
				}));
			}
			catch (Exception _Exception)
			{
				ShowException(_Exception, "Functions.GetLogLevel()");
			}

			return _LogType;
		}
	}
}
