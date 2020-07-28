#region Usings

using System.Windows.Forms;

using static DiamondGui.Static;

#endregion Usings

namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static void CloseForm(Form form, FormClosingEventArgs e)
		{
			if (form.Name == "Main")
			{
				if (mainForm.button_start.Text == "Stop")
				{
					mainForm.button_start.PerformClick();
				}

				settings.StatusIndex = mainForm.comboBox_status.SelectedIndex;
				settings.Save();
			}
			else
			{
				e.Cancel = true;
				form.Hide();
			}
		}
	}
}