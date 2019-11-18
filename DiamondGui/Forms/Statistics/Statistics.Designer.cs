namespace DiamondGui.Forms
{
	partial class Statistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Statistics));
            this.label_uptime = new System.Windows.Forms.Label();
            this.label_commandsUsed = new System.Windows.Forms.Label();
            this.label_exceptions = new System.Windows.Forms.Label();
            this.timer_updater = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label_uptime
            // 
            this.label_uptime.AutoSize = true;
            this.label_uptime.Location = new System.Drawing.Point(12, 9);
            this.label_uptime.Name = "label_uptime";
            this.label_uptime.Size = new System.Drawing.Size(50, 13);
            this.label_uptime.TabIndex = 0;
            this.label_uptime.Text = "<uptime>";
            // 
            // label_commandsUsed
            // 
            this.label_commandsUsed.AutoSize = true;
            this.label_commandsUsed.Location = new System.Drawing.Point(12, 33);
            this.label_commandsUsed.Name = "label_commandsUsed";
            this.label_commandsUsed.Size = new System.Drawing.Size(95, 13);
            this.label_commandsUsed.TabIndex = 1;
            this.label_commandsUsed.Text = "<commandsUsed>";
            // 
            // label_exceptions
            // 
            this.label_exceptions.AutoSize = true;
            this.label_exceptions.Location = new System.Drawing.Point(12, 57);
            this.label_exceptions.Name = "label_exceptions";
            this.label_exceptions.Size = new System.Drawing.Size(70, 13);
            this.label_exceptions.TabIndex = 2;
            this.label_exceptions.Text = "<exceptions>";
            // 
            // timer_updater
            // 
            this.timer_updater.Enabled = true;
            this.timer_updater.Interval = 1000;
            this.timer_updater.Tick += new System.EventHandler(this.timer_updater_Tick);
            // 
            // Statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 82);
            this.Controls.Add(this.label_exceptions);
            this.Controls.Add(this.label_commandsUsed);
            this.Controls.Add(this.label_uptime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Statistics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Diamond 💎 Statistics";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Statistics_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		internal System.Windows.Forms.Label label_uptime;
		internal System.Windows.Forms.Label label_commandsUsed;
		internal System.Windows.Forms.Label label_exceptions;
		internal System.Windows.Forms.Timer timer_updater;
	}
}