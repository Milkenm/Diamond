namespace TestingProject
{
    partial class GradientGenerator
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GradientGenerator));
			this.numeric_width = new System.Windows.Forms.NumericUpDown();
			this.numeric_height = new System.Windows.Forms.NumericUpDown();
			this.pictureBox_result = new System.Windows.Forms.PictureBox();
			this.label_width = new System.Windows.Forms.Label();
			this.label_height = new System.Windows.Forms.Label();
			this.comboBox_mode = new System.Windows.Forms.ComboBox();
			this.button_generate = new System.Windows.Forms.Button();
			this.label_mode = new System.Windows.Forms.Label();
			this.label_px1 = new System.Windows.Forms.Label();
			this.label_px2 = new System.Windows.Forms.Label();
			this.button_color1 = new System.Windows.Forms.Button();
			this.button_color2 = new System.Windows.Forms.Button();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.timer_updateColor = new System.Windows.Forms.Timer(this.components);
			this.button_save = new System.Windows.Forms.Button();
			this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
			((System.ComponentModel.ISupportInitialize)(this.numeric_width)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_height)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_result)).BeginInit();
			this.SuspendLayout();
			// 
			// numeric_width
			// 
			this.numeric_width.Location = new System.Drawing.Point(185, 7);
			this.numeric_width.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.numeric_width.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numeric_width.Name = "numeric_width";
			this.numeric_width.Size = new System.Drawing.Size(132, 20);
			this.numeric_width.TabIndex = 1;
			this.numeric_width.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			// 
			// numeric_height
			// 
			this.numeric_height.Location = new System.Drawing.Point(185, 33);
			this.numeric_height.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.numeric_height.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numeric_height.Name = "numeric_height";
			this.numeric_height.Size = new System.Drawing.Size(132, 20);
			this.numeric_height.TabIndex = 2;
			this.numeric_height.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			// 
			// pictureBox_result
			// 
			this.pictureBox_result.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox_result.Location = new System.Drawing.Point(1, 1);
			this.pictureBox_result.Name = "pictureBox_result";
			this.pictureBox_result.Size = new System.Drawing.Size(128, 128);
			this.pictureBox_result.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox_result.TabIndex = 3;
			this.pictureBox_result.TabStop = false;
			// 
			// label_width
			// 
			this.label_width.AutoSize = true;
			this.label_width.Location = new System.Drawing.Point(144, 9);
			this.label_width.Name = "label_width";
			this.label_width.Size = new System.Drawing.Size(38, 13);
			this.label_width.TabIndex = 4;
			this.label_width.Text = "Width:";
			// 
			// label_height
			// 
			this.label_height.AutoSize = true;
			this.label_height.Location = new System.Drawing.Point(138, 35);
			this.label_height.Name = "label_height";
			this.label_height.Size = new System.Drawing.Size(41, 13);
			this.label_height.TabIndex = 5;
			this.label_height.Text = "Height:";
			// 
			// comboBox_mode
			// 
			this.comboBox_mode.FormattingEnabled = true;
			this.comboBox_mode.Location = new System.Drawing.Point(185, 59);
			this.comboBox_mode.Name = "comboBox_mode";
			this.comboBox_mode.Size = new System.Drawing.Size(153, 21);
			this.comboBox_mode.TabIndex = 6;
			// 
			// button_generate
			// 
			this.button_generate.Location = new System.Drawing.Point(269, 106);
			this.button_generate.Name = "button_generate";
			this.button_generate.Size = new System.Drawing.Size(75, 23);
			this.button_generate.TabIndex = 7;
			this.button_generate.Text = "Generate";
			this.button_generate.UseVisualStyleBackColor = true;
			this.button_generate.Click += new System.EventHandler(this.button_generate_Click);
			// 
			// label_mode
			// 
			this.label_mode.AutoSize = true;
			this.label_mode.Location = new System.Drawing.Point(142, 62);
			this.label_mode.Name = "label_mode";
			this.label_mode.Size = new System.Drawing.Size(37, 13);
			this.label_mode.TabIndex = 8;
			this.label_mode.Text = "Mode:";
			// 
			// label_px1
			// 
			this.label_px1.AutoSize = true;
			this.label_px1.Location = new System.Drawing.Point(323, 9);
			this.label_px1.Name = "label_px1";
			this.label_px1.Size = new System.Drawing.Size(21, 13);
			this.label_px1.TabIndex = 9;
			this.label_px1.Text = "px.";
			// 
			// label_px2
			// 
			this.label_px2.AutoSize = true;
			this.label_px2.Location = new System.Drawing.Point(323, 35);
			this.label_px2.Name = "label_px2";
			this.label_px2.Size = new System.Drawing.Size(21, 13);
			this.label_px2.TabIndex = 10;
			this.label_px2.Text = "px.";
			// 
			// button_color1
			// 
			this.button_color1.Location = new System.Drawing.Point(131, 106);
			this.button_color1.Name = "button_color1";
			this.button_color1.Size = new System.Drawing.Size(48, 23);
			this.button_color1.TabIndex = 11;
			this.button_color1.Text = "Color 1";
			this.button_color1.UseVisualStyleBackColor = true;
			this.button_color1.Click += new System.EventHandler(this.button_color1_Click);
			// 
			// button_color2
			// 
			this.button_color2.Location = new System.Drawing.Point(181, 106);
			this.button_color2.Name = "button_color2";
			this.button_color2.Size = new System.Drawing.Size(48, 23);
			this.button_color2.TabIndex = 13;
			this.button_color2.Text = "Color 2";
			this.button_color2.UseVisualStyleBackColor = true;
			this.button_color2.Click += new System.EventHandler(this.button_color2_Click);
			// 
			// timer_updateColor
			// 
			this.timer_updateColor.Enabled = true;
			this.timer_updateColor.Interval = 10;
			this.timer_updateColor.Tick += new System.EventHandler(this.timer_updateColor_Tick);
			// 
			// button_save
			// 
			this.button_save.Location = new System.Drawing.Point(269, 84);
			this.button_save.Name = "button_save";
			this.button_save.Size = new System.Drawing.Size(75, 23);
			this.button_save.TabIndex = 14;
			this.button_save.Text = "Save";
			this.button_save.UseVisualStyleBackColor = true;
			this.button_save.Click += new System.EventHandler(this.button_save_Click);
			// 
			// folderDialog
			// 
			this.folderDialog.HelpRequest += new System.EventHandler(this.folderDialog_HelpRequest);
			// 
			// GradientGenerator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(345, 130);
			this.Controls.Add(this.button_save);
			this.Controls.Add(this.button_color2);
			this.Controls.Add(this.button_color1);
			this.Controls.Add(this.label_px2);
			this.Controls.Add(this.label_px1);
			this.Controls.Add(this.label_mode);
			this.Controls.Add(this.button_generate);
			this.Controls.Add(this.comboBox_mode);
			this.Controls.Add(this.label_height);
			this.Controls.Add(this.label_width);
			this.Controls.Add(this.pictureBox_result);
			this.Controls.Add(this.numeric_height);
			this.Controls.Add(this.numeric_width);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "GradientGenerator";
			this.Text = "Gradient Generator";
			this.Load += new System.EventHandler(this.GradientGenerator_Load);
			((System.ComponentModel.ISupportInitialize)(this.numeric_width)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numeric_height)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_result)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		private System.Windows.Forms.NumericUpDown numeric_width;
		private System.Windows.Forms.NumericUpDown numeric_height;
		private System.Windows.Forms.PictureBox pictureBox_result;
		private System.Windows.Forms.Label label_width;
		private System.Windows.Forms.Label label_height;
		private System.Windows.Forms.ComboBox comboBox_mode;
		private System.Windows.Forms.Button button_generate;
		private System.Windows.Forms.Label label_mode;
		private System.Windows.Forms.Label label_px1;
		private System.Windows.Forms.Label label_px2;
		private System.Windows.Forms.Button button_color1;
		private System.Windows.Forms.Button button_color2;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.Timer timer_updateColor;
		private System.Windows.Forms.Button button_save;
		private System.Windows.Forms.FolderBrowserDialog folderDialog;
	}
}

