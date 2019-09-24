#region Usings
using System;
using System.Threading.Tasks;

using static DiamondGui.Core;
using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Controls
	{
		internal static void ButtonStart()
		{
			try
			{
				if (MainForm.button_start.Text == "Start")
				{
					new Task(new Action(() =>
					{
						DiscordMain().GetAwaiter().GetResult();
					})).Start();

					MainForm.textBox_token.Enabled = false;
					MainForm.button_start.Text = "Stop";
				}
				else
				{
					new Task(new Action(() =>
					{
						Client.LogoutAsync();
					})).Start();

					MainForm.textBox_token.Enabled = true;
					MainForm.button_start.Text = "Start";
				}
			}
			catch (Exception _Exception)
			{
				ShowException(_Exception, "Controls.ButtonStart()");
			}
		}
	}
}