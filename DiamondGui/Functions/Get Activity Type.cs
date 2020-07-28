#region Usings

using Discord;

using System;

using static DiamondGui.Static;

#endregion Usings

namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static ActivityType GetActivityType()
		{
			ActivityType _ActivityType = ActivityType.Playing;

			mainForm.Invoke(new Action(() =>
			{
				if (optionsForm.comboBox_logType.Text == "Listening")
				{
					_ActivityType = ActivityType.Listening;
				}
				else if (optionsForm.comboBox_logType.Text == "Streaming")
				{
					_ActivityType = ActivityType.Streaming;
				}
				else if (optionsForm.comboBox_logType.Text == "Watching")
				{
					_ActivityType = ActivityType.Watching;
				}
			}));

			return _ActivityType;
		}
	}
}