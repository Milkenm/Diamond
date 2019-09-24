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

                    ToggleMainControlEnabled();
				}
				else
				{
					new Task(new Action(() =>
					{
						Client.LogoutAsync();
                        Client.StopAsync();
					})).Start();

                    ToggleMainControlEnabled();
                }
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "Controls.ButtonStart()");
            }
        }
    }
}