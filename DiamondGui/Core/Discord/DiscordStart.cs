#region Usings
using System;
using System.Threading.Tasks;

using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Core
	{
		internal static void DiscordStart()
		{
			try
			{
				if (MainForm.button_start.Text == "Start")
				{
					new Task(new Action(() =>
					{
						DiscordMain().GetAwaiter().GetResult();
					})).Start();
				}
				else
				{
					new Task(new Action(() =>
					{
						Client.LogoutAsync();
                        Client.StopAsync();
					})).Start();
                }



				if (MainForm.button_start.Text == "Start") MainForm.button_start.Text = "Stop";
				else MainForm.button_start.Text = "Start";

				OptionsForm.textBox_token.Enabled = MainForm.button_start.Text == "Start";
				OptionsForm.comboBox_logType.Enabled = MainForm.button_start.Text == "Start";
				MainForm.timer_uptime.Enabled = MainForm.button_start.Text == "Stop";
			}
			catch (Exception _Exception) { ShowException(_Exception, "DiamondGui.Core.DiscordStart()"); }
		}
	}
}