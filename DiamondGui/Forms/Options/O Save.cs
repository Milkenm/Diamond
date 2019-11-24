#region Usings
using System;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Options
	{
		internal static void OptionsSave()
		{
			try
			{
				Settings.BotToken = OptionsForm.textBox_token.Text;
				Settings.LogTypeIndex = OptionsForm.comboBox_logType.SelectedIndex;
				Settings.Game = OptionsForm.textBox_activityName.Text;
				Settings.ActivityIndex = OptionsForm.comboBox_activity.SelectedIndex;
				Settings.StreamUrl = OptionsForm.textBox_streamUrl.Text;
				Settings.BotUrl = OptionsForm.textBox_botUrl.Text;
				Settings.DiscordUrl = OptionsForm.textBox_discordUrl.Text;
				Settings.Domain = OptionsForm.textBox_domain.Text;
				Settings.AdminId = Convert.ToUInt64(OptionsForm.textBox_adminId.Text);
				Settings.BotPrefix = OptionsForm.textBox_botPrefix.Text;
				Settings.AllowMention = OptionsForm.checkBox_allowMention.Checked;
				Settings.AllowPrefix = OptionsForm.checkBox_allowPrefix.Checked;
				Settings.DisableCommands = OptionsForm.checkBox_disableCommands.Checked;
				Settings.RiotAPI = OptionsForm.textBox_riotApi.Text;

				SetDiscordGame();
				Settings.Save();
				OptionsForm.Hide();
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}