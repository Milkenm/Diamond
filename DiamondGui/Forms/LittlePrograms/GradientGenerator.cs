#region Usings
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
#endregion Usings

// # = #
// https://stackoverflow.com/questions/1493944/c-sharp-create-gradient-image
// # = #

namespace TestingProject
{
	public partial class GradientGenerator : Form
	{
		static Button _ColorButton;

		public GradientGenerator()
		{
			InitializeComponent();
		}

		private void GradientGenerator_Load(object sender, EventArgs e)
		{
			foreach (string _Enum in Enum.GetNames(typeof(LinearGradientMode)))
			{
				comboBox_mode.Items.Add(_Enum);
			}
			comboBox_mode.SelectedIndex = 0;
		}

		private void button_generate_Click(object sender, EventArgs e)
		{
			if (pictureBox_result.Image != null) pictureBox_result.Image.Dispose();

			Bitmap _Bitmap = new Bitmap((int)numeric_width.Value, (int)numeric_height.Value);
			Graphics graphics = Graphics.FromImage(_Bitmap);

			Rectangle _Rectangle = new Rectangle(0, 0, (int)numeric_width.Value, (int)numeric_height.Value);

			LinearGradientBrush _GradientBrush = new LinearGradientBrush(_Rectangle, button_color1.BackColor, button_color2.BackColor, (LinearGradientMode)Enum.Parse(typeof(LinearGradientMode), comboBox_mode.Text));

			graphics.FillRectangle(_GradientBrush, _Rectangle);

			pictureBox_result.Image = _Bitmap;
		}

		private void button_color1_Click(object sender, EventArgs e)
		{
			colorDialog.Color = button_color1.BackColor;
			_ColorButton = button_color1;
			colorDialog.ShowDialog();
		}

		private void button_color2_Click(object sender, EventArgs e)
		{
			colorDialog.Color = button_color2.BackColor;
			_ColorButton = button_color2;
			colorDialog.ShowDialog();
		}

		private void timer_updateColor_Tick(object sender, EventArgs e)
		{
			if (_ColorButton != null) _ColorButton.BackColor = colorDialog.Color;
		}

		private void button_save_Click(object sender, EventArgs e)
		{
			DialogResult _Result = folderDialog.ShowDialog();

			if (_Result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
			{
				try
				{
					pictureBox_result.Image.Save($@"{folderDialog.SelectedPath}\Gradient.png", ImageFormat.Png);
					MessageBox.Show($@"Gradient saved to '{folderDialog.SelectedPath}\Gradient.png'!");
				}
				catch
				{
					MessageBox.Show("There was an error while saving the gradient!");
				}
			}
		}

		private void folderDialog_HelpRequest(object sender, EventArgs e)
		{

		}
	}
}
