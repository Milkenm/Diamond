#region Usings
using System;
using System.Windows.Forms;

using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static void CloseForm(Form Form, FormClosingEventArgs Event)
		{
			try
			{
				if (Form.Name == "Main")
				{
					if (MainForm.button_start.Text == "Stop") MainForm.button_start.PerformClick();

					Settings.StatusIndex = MainForm.comboBox_status.SelectedIndex;
					Settings.Save();
				}
				else
				{
					Event.Cancel = true;
					Form.Hide();
				}
			}
			catch (Exception _Exception) { ShowException(_Exception, "DiamondGui.Functions.CloseForm()"); }
		}
	}
}