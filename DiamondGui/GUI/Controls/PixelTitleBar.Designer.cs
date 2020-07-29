namespace ChillinRoomGMG.Controls
{
	partial class PixelTitleBar
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label_title = new System.Windows.Forms.Label();
			this.pictureBox_minimize = new System.Windows.Forms.PictureBox();
			this.pictureBox_close = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_minimize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).BeginInit();
			this.SuspendLayout();
			// 
			// label_title
			// 
			this.label_title.AutoEllipsis = true;
			this.label_title.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label_title.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_title.ForeColor = System.Drawing.SystemColors.ControlLight;
			this.label_title.Location = new System.Drawing.Point(0, 0);
			this.label_title.Name = "label_title";
			this.label_title.Size = new System.Drawing.Size(465, 22);
			this.label_title.TabIndex = 9;
			this.label_title.Text = "Diamond GUI";
			this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pictureBox_minimize
			// 
			this.pictureBox_minimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
			this.pictureBox_minimize.Dock = System.Windows.Forms.DockStyle.Right;
			this.pictureBox_minimize.Image = global::DiamondGui.Properties.Resources.PixelMinimize_PNG;
			this.pictureBox_minimize.Location = new System.Drawing.Point(465, 0);
			this.pictureBox_minimize.Name = "pictureBox_minimize";
			this.pictureBox_minimize.Size = new System.Drawing.Size(22, 22);
			this.pictureBox_minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox_minimize.TabIndex = 9;
			this.pictureBox_minimize.TabStop = false;
			this.pictureBox_minimize.Click += new System.EventHandler(this.pictureBox_minimize_Click);
			// 
			// pictureBox_close
			// 
			this.pictureBox_close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
			this.pictureBox_close.Dock = System.Windows.Forms.DockStyle.Right;
			this.pictureBox_close.Image = global::DiamondGui.Properties.Resources.PixelClose_PNG;
			this.pictureBox_close.Location = new System.Drawing.Point(487, 0);
			this.pictureBox_close.Name = "pictureBox_close";
			this.pictureBox_close.Size = new System.Drawing.Size(22, 22);
			this.pictureBox_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox_close.TabIndex = 8;
			this.pictureBox_close.TabStop = false;
			this.pictureBox_close.Click += new System.EventHandler(this.pictureBox_close_Click);
			// 
			// PixelTitleBar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
			this.Controls.Add(this.label_title);
			this.Controls.Add(this.pictureBox_minimize);
			this.Controls.Add(this.pictureBox_close);
			this.Name = "PixelTitleBar";
			this.Size = new System.Drawing.Size(509, 22);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_minimize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.PictureBox pictureBox_minimize;
		private System.Windows.Forms.PictureBox pictureBox_close;
		private System.Windows.Forms.Label label_title;
	}
}
