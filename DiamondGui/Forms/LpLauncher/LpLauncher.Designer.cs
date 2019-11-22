namespace DiamondGui.Forms
{
	partial class LpLauncher
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LpLauncher));
			this.button_gradientGenerator = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button_gradientGenerator
			// 
			this.button_gradientGenerator.Location = new System.Drawing.Point(12, 12);
			this.button_gradientGenerator.Name = "button_gradientGenerator";
			this.button_gradientGenerator.Size = new System.Drawing.Size(171, 23);
			this.button_gradientGenerator.TabIndex = 0;
			this.button_gradientGenerator.Text = "Gradient Generator";
			this.button_gradientGenerator.UseVisualStyleBackColor = true;
			this.button_gradientGenerator.Click += new System.EventHandler(this.button_gradientGenerator_Click);
			// 
			// LpLauncher
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(196, 154);
			this.Controls.Add(this.button_gradientGenerator);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LpLauncher";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "LP Launcher";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LpLauncher_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button_gradientGenerator;
	}
}