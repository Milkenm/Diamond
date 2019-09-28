namespace DiamondGui
{
    partial class Main
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.button_start = new System.Windows.Forms.Button();
			this.comboBox_status = new System.Windows.Forms.ComboBox();
			this.listView_log = new System.Windows.Forms.ListView();
			this.listView_log_emptyBug = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.listView_log_type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.listView_log_source = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.listView_log_message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.button_options = new System.Windows.Forms.Button();
			this.timer_uptime = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// button_start
			// 
			this.button_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button_start.Location = new System.Drawing.Point(0, 291);
			this.button_start.Name = "button_start";
			this.button_start.Size = new System.Drawing.Size(126, 23);
			this.button_start.TabIndex = 0;
			this.button_start.Text = "Start";
			this.button_start.UseVisualStyleBackColor = true;
			this.button_start.Click += new System.EventHandler(this.button_start_Click);
			// 
			// comboBox_status
			// 
			this.comboBox_status.FormattingEnabled = true;
			this.comboBox_status.Items.AddRange(new object[] {
            "Online",
            "Idle",
            "Do Not Disturb",
            "Invisible"});
			this.comboBox_status.Location = new System.Drawing.Point(126, 292);
			this.comboBox_status.Name = "comboBox_status";
			this.comboBox_status.Size = new System.Drawing.Size(161, 21);
			this.comboBox_status.TabIndex = 1;
			this.comboBox_status.SelectedIndexChanged += new System.EventHandler(this.comboBox_status_SelectedIndexChanged);
			// 
			// listView_log
			// 
			this.listView_log.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listView_log_emptyBug,
            this.listView_log_type,
            this.listView_log_source,
            this.listView_log_message});
			this.listView_log.FullRowSelect = true;
			this.listView_log.HideSelection = false;
			this.listView_log.Location = new System.Drawing.Point(1, 1);
			this.listView_log.Name = "listView_log";
			this.listView_log.Size = new System.Drawing.Size(774, 290);
			this.listView_log.TabIndex = 3;
			this.listView_log.UseCompatibleStateImageBehavior = false;
			this.listView_log.View = System.Windows.Forms.View.Details;
			// 
			// listView_log_emptyBug
			// 
			this.listView_log_emptyBug.DisplayIndex = 3;
			this.listView_log_emptyBug.Text = "";
			this.listView_log_emptyBug.Width = 0;
			// 
			// listView_log_type
			// 
			this.listView_log_type.DisplayIndex = 0;
			this.listView_log_type.Text = "Type";
			this.listView_log_type.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// listView_log_source
			// 
			this.listView_log_source.DisplayIndex = 1;
			this.listView_log_source.Text = "Source";
			this.listView_log_source.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.listView_log_source.Width = 70;
			// 
			// listView_log_message
			// 
			this.listView_log_message.DisplayIndex = 2;
			this.listView_log_message.Text = "Message";
			this.listView_log_message.Width = 640;
			// 
			// button_options
			// 
			this.button_options.Location = new System.Drawing.Point(674, 291);
			this.button_options.Name = "button_options";
			this.button_options.Size = new System.Drawing.Size(102, 22);
			this.button_options.TabIndex = 2;
			this.button_options.Text = "Options...";
			this.button_options.UseVisualStyleBackColor = true;
			this.button_options.Click += new System.EventHandler(this.button_options_Click);
			// 
			// timer_uptime
			// 
			this.timer_uptime.Enabled = true;
			this.timer_uptime.Interval = 1000;
			this.timer_uptime.Tick += new System.EventHandler(this.timer_uptime_Tick);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(776, 314);
			this.Controls.Add(this.comboBox_status);
			this.Controls.Add(this.button_start);
			this.Controls.Add(this.button_options);
			this.Controls.Add(this.listView_log);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Diamond 💎";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
			this.Load += new System.EventHandler(this.Main_Load);
			this.ResumeLayout(false);

        }

		#endregion
		internal System.Windows.Forms.Button button_start;
        internal System.Windows.Forms.ComboBox comboBox_status;
        internal System.Windows.Forms.ListView listView_log;
        internal System.Windows.Forms.ColumnHeader listView_log_type;
        internal System.Windows.Forms.ColumnHeader listView_log_source;
        internal System.Windows.Forms.ColumnHeader listView_log_message;
		private System.Windows.Forms.ColumnHeader listView_log_emptyBug;
		private System.Windows.Forms.Button button_options;
		private System.Windows.Forms.Timer timer_uptime;
	}
}

