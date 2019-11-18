#region Usings
using System;
using System.Windows.Forms;

using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static void CloseForm(Form form, FormClosingEventArgs e)
		{
			try
			{
				if (form.Name == "Main")
				{
					if (MainForm.button_start.Text == "Stop")
					{
						MainForm.button_start.PerformClick();
					}

					Settings.StatusIndex = MainForm.comboBox_status.SelectedIndex;
					Settings.Save();
				}
				else
				{
					e.Cancel = true;
					form.Hide();
				}
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}