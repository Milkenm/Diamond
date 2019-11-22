#region Usings
using System;
using System.Threading.Tasks;

using static DiamondGui.DiscordCore;
using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Main
	{
		internal static void MainDiscordStart()
		{
			try
			{
				if (MainForm.button_start.Text == "Start")
				{
					new Task(new Action(() =>
					{
						DiscordCoreLoader().GetAwaiter().GetResult();
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



				MainForm.button_start.Text = MainForm.button_start.Text == "Start" ? "Stop" : "Start";

				OptionsForm.textBox_token.Enabled = MainForm.button_start.Text == "Start";
				OptionsForm.comboBox_logType.Enabled = MainForm.button_start.Text == "Start";
				MainForm.timer_uptime.Enabled = MainForm.button_start.Text == "Stop";
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}