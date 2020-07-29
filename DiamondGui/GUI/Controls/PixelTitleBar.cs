using System;
using System.Windows.Forms;
using static ChillinRoomGMG.WinAPI;

namespace ChillinRoomGMG.Controls
{
	public partial class PixelTitleBar : UserControl
	{
		// SHOW MINIMIZE BUTTON PROPERTY
		public bool ShowMinimizeButton
		{
			get { return pictureBox_minimize.Visible; }
			set
			{
				pictureBox_minimize.Visible = value;
			}
		}

		// TITLE PROPERTY
		private static string titleExtension;

		private const string baseTitle = "Diamond GUI";

		public string TitleExtension
		{
			get
			{
				return titleExtension;
			}
			set
			{
				titleExtension = value;

				label_title.Text = !string.IsNullOrEmpty(titleExtension) ? baseTitle + " - " + titleExtension : baseTitle;
			}
		}

		// INITIALIZE
		public PixelTitleBar()
		{
			InitializeComponent();

			foreach (Control control in Controls)
			{
				if (!control.Name.Contains("pictureBox_"))
				{
					control.MouseDown += (s, e) => AllowDragEvent(e);
				}
			}

			Dock = DockStyle.Top;
		}

		// PREVENT WINDOW HEIGHT CHANGE ## https://stackoverflow.com/a/47005249/10601212 ##
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			base.SetBoundsCore(x, y, width, 22, specified);
		}

		// CLOSE BUTTON
		private void pictureBox_close_Click(object sender, EventArgs e)
		{
			Form parent = GetFinalParent();

			parent.Close();
		}

		// MINIMIZE BUTTON
		private void pictureBox_minimize_Click(object sender, EventArgs e)
		{
			Form parent = GetFinalParent();

			parent.WindowState = FormWindowState.Minimized;
		}

		// ALLOW DRAG
		private void AllowDragEvent(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ReleaseCapture();

				SendMessage(GetFinalParent().Handle, 0xA1, 0x2, 0);
			}
		}

		// GET THE FORM WHERE THE TITLE BAR IS PLACED AT
		private Form GetFinalParent()
		{
			Control finalParent = Parent;

			while (finalParent.Parent != null)
			{
				finalParent = finalParent.Parent;
			}

			return finalParent is Form form ? form : null;
		}
	}
}