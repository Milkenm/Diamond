#region Usings
using System;
using System.Windows.Forms;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static void CloseForm(Form Form, FormClosingEventArgs Event)
		{
			try
			{
				Event.Cancel = true;
				Form.Hide();
			}
			catch (Exception _Exception) { ShowException(_Exception, "DiamondGui.Functions.CloseForm()"); }
		}
	}
}