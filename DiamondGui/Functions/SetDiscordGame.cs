#region Usings
using System;

using Discord;

using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static void SetDiscordGame()
		{
			try
			{
				MainForm.Invoke(new Action(() =>
				{
					if (Client != null)
					{
						if (Client.LoginState == LoginState.LoggedIn)
						{
							if (OptionsForm.comboBox_activity.Text == "Listening") Client.SetGameAsync(OptionsForm.textBox_activityName.Text, null, ActivityType.Listening);
							else if (OptionsForm.comboBox_activity.Text == "Playing") Client.SetGameAsync(OptionsForm.textBox_activityName.Text, null, ActivityType.Playing);
							else if (OptionsForm.comboBox_activity.Text == "Streaming") Client.SetGameAsync(OptionsForm.textBox_activityName.Text, OptionsForm.textBox_streamUrl.Text, ActivityType.Streaming);
							else if (OptionsForm.comboBox_activity.Text == "Watching") Client.SetGameAsync(OptionsForm.textBox_activityName.Text, null, ActivityType.Watching);
						}
					}
				}));
			}
			catch (Exception _Exception) { ShowException(_Exception, "DiamondGui.Functions.SetDiscordGame()"); }
		}
	}
}