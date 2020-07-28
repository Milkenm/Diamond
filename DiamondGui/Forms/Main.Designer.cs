namespace DiamondGui.Forms
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.comboBox_status = new System.Windows.Forms.ComboBox();
			this.listView_log = new System.Windows.Forms.ListView();
			this.listView_log_emptyBug = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.listView_log_type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.listView_log_source = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.listView_log_message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.button_close = new System.Windows.Forms.Button();
			this.button_minimize = new System.Windows.Forms.Button();
			this.label_title = new System.Windows.Forms.Label();
			this.pictureBox_titleIcon = new System.Windows.Forms.PictureBox();
			this.button_start = new System.Windows.Forms.Button();
			this.button_options = new System.Windows.Forms.Button();
			this.button_reload = new System.Windows.Forms.Button();
			this.button_privateChat = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_titleIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// comboBox_status
			// 
			this.comboBox_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_status.FormattingEnabled = true;
			this.comboBox_status.Items.AddRange(new object[] {
            "Online",
            "Idle",
            "Do Not Disturb",
            "Invisible"});
			this.comboBox_status.Location = new System.Drawing.Point(126, 315);
			this.comboBox_status.Name = "comboBox_status";
			this.comboBox_status.Size = new System.Drawing.Size(161, 21);
			this.comboBox_status.TabIndex = 1;
			this.comboBox_status.TabStop = false;
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
			this.listView_log.Location = new System.Drawing.Point(1, 24);
			this.listView_log.Name = "listView_log";
			this.listView_log.ShowItemToolTips = true;
			this.listView_log.Size = new System.Drawing.Size(774, 290);
			this.listView_log.TabIndex = 4;
			this.listView_log.TabStop = false;
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
			this.listView_log_message.Width = 623;
			// 
			// button_close
			// 
			this.button_close.FlatAppearance.BorderSize = 0;
			this.button_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_close.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button_close.ForeColor = System.Drawing.Color.DarkTurquoise;
			this.button_close.Location = new System.Drawing.Point(754, 0);
			this.button_close.Name = "button_close";
			this.button_close.Size = new System.Drawing.Size(23, 23);
			this.button_close.TabIndex = 5;
			this.button_close.TabStop = false;
			this.button_close.Text = "X";
			this.button_close.UseVisualStyleBackColor = true;
			this.button_close.Click += new System.EventHandler(this.button_close_Click);
			// 
			// button_minimize
			// 
			this.button_minimize.FlatAppearance.BorderSize = 0;
			this.button_minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_minimize.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button_minimize.ForeColor = System.Drawing.Color.DarkTurquoise;
			this.button_minimize.Location = new System.Drawing.Point(731, 0);
			this.button_minimize.Name = "button_minimize";
			this.button_minimize.Size = new System.Drawing.Size(23, 23);
			this.button_minimize.TabIndex = 6;
			this.button_minimize.TabStop = false;
			this.button_minimize.Text = "_";
			this.button_minimize.UseVisualStyleBackColor = true;
			this.button_minimize.Click += new System.EventHandler(this.button_minimize_Click);
			// 
			// label_title
			// 
			this.label_title.AutoSize = true;
			this.label_title.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_title.ForeColor = System.Drawing.Color.DarkTurquoise;
			this.label_title.Location = new System.Drawing.Point(24, 1);
			this.label_title.Name = "label_title";
			this.label_title.Size = new System.Drawing.Size(107, 22);
			this.label_title.TabIndex = 8;
			this.label_title.Text = "Diamond GUI";
			// 
			// pictureBox_titleIcon
			// 
			this.pictureBox_titleIcon.Image = global::DiamondGui.Properties.Resources.Icon_Diamond;
			this.pictureBox_titleIcon.Location = new System.Drawing.Point(2, 2);
			this.pictureBox_titleIcon.Name = "pictureBox_titleIcon";
			this.pictureBox_titleIcon.Size = new System.Drawing.Size(20, 20);
			this.pictureBox_titleIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox_titleIcon.TabIndex = 9;
			this.pictureBox_titleIcon.TabStop = false;
			// 
			// button_start
			// 
			this.button_start.BackgroundImage = global::DiamondGui.Properties.Resources.Control_ButtonGradient;
			this.button_start.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button_start.Location = new System.Drawing.Point(0, 314);
			this.button_start.Name = "button_start";
			this.button_start.Size = new System.Drawing.Size(126, 23);
			this.button_start.TabIndex = 0;
			this.button_start.TabStop = false;
			this.button_start.Text = "Start";
			this.button_start.UseVisualStyleBackColor = true;
			this.button_start.Click += new System.EventHandler(this.button_start_Click);
			// 
			// button_options
			// 
			this.button_options.BackgroundImage = global::DiamondGui.Properties.Resources.Control_ButtonGradient;
			this.button_options.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button_options.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_options.Location = new System.Drawing.Point(673, 314);
			this.button_options.Name = "button_options";
			this.button_options.Size = new System.Drawing.Size(102, 23);
			this.button_options.TabIndex = 2;
			this.button_options.TabStop = false;
			this.button_options.Text = "Options";
			this.button_options.UseVisualStyleBackColor = true;
			this.button_options.Click += new System.EventHandler(this.button_options_Click);
			// 
			// button_reload
			// 
			this.button_reload.BackgroundImage = global::DiamondGui.Properties.Resources.Control_ButtonGradient;
			this.button_reload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button_reload.Enabled = false;
			this.button_reload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_reload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.button_reload.Location = new System.Drawing.Point(287, 314);
			this.button_reload.Name = "button_reload";
			this.button_reload.Size = new System.Drawing.Size(68, 23);
			this.button_reload.TabIndex = 10;
			this.button_reload.TabStop = false;
			this.button_reload.Text = "Reload";
			this.button_reload.UseVisualStyleBackColor = true;
			this.button_reload.Click += new System.EventHandler(this.button_reload_Click);
			// 
			// button_privateChat
			// 
			this.button_privateChat.BackgroundImage = global::DiamondGui.Properties.Resources.Control_ButtonGradient;
			this.button_privateChat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button_privateChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button_privateChat.Location = new System.Drawing.Point(572, 314);
			this.button_privateChat.Name = "button_privateChat";
			this.button_privateChat.Size = new System.Drawing.Size(102, 23);
			this.button_privateChat.TabIndex = 11;
			this.button_privateChat.TabStop = false;
			this.button_privateChat.Text = "Private Chat";
			this.button_privateChat.UseVisualStyleBackColor = true;
			this.button_privateChat.Click += new System.EventHandler(this.button_privateChat_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.ClientSize = new System.Drawing.Size(776, 337);
			this.Controls.Add(this.button_privateChat);
			this.Controls.Add(this.button_reload);
			this.Controls.Add(this.pictureBox_titleIcon);
			this.Controls.Add(this.label_title);
			this.Controls.Add(this.button_minimize);
			this.Controls.Add(this.button_close);
			this.Controls.Add(this.comboBox_status);
			this.Controls.Add(this.button_start);
			this.Controls.Add(this.button_options);
			this.Controls.Add(this.listView_log);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(776, 337);
			this.MinimumSize = new System.Drawing.Size(776, 337);
			this.Name = "Main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Diamond 💎 Control Panel";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
			this.Load += new System.EventHandler(this.Main_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_titleIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion
		internal System.Windows.Forms.Button button_start;
        internal System.Windows.Forms.ComboBox comboBox_status;
        internal System.Windows.Forms.ListView listView_log;
        internal System.Windows.Forms.ColumnHeader listView_log_type;
        internal System.Windows.Forms.ColumnHeader listView_log_source;
        internal System.Windows.Forms.ColumnHeader listView_log_message;
		internal System.Windows.Forms.ColumnHeader listView_log_emptyBug;
		internal System.Windows.Forms.Button button_options;
		private System.Windows.Forms.Button button_close;
		private System.Windows.Forms.Button button_minimize;
		private System.Windows.Forms.Label label_title;
		private System.Windows.Forms.PictureBox pictureBox_titleIcon;
		internal System.Windows.Forms.Button button_reload;
		private System.Windows.Forms.Button button_privateChat;
	}
}

