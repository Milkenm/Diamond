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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.textBox_token = new System.Windows.Forms.TextBox();
            this.label_token = new System.Windows.Forms.Label();
            this.button_start = new System.Windows.Forms.Button();
            this.comboBox_logType = new System.Windows.Forms.ComboBox();
            this.label_logType = new System.Windows.Forms.Label();
            this.textBox_activityName = new System.Windows.Forms.TextBox();
            this.label_activityName = new System.Windows.Forms.Label();
            this.button_refreshGame = new System.Windows.Forms.Button();
            this.textBox_streamUrl = new System.Windows.Forms.TextBox();
            this.comboBox_activity = new System.Windows.Forms.ComboBox();
            this.label_streamUrl = new System.Windows.Forms.Label();
            this.label_activity = new System.Windows.Forms.Label();
            this.groupBox_buttons = new System.Windows.Forms.GroupBox();
            this.button_refreshStatus = new System.Windows.Forms.Button();
            this.comboBox_status = new System.Windows.Forms.ComboBox();
            this.label_status = new System.Windows.Forms.Label();
            this.checkBox_revealToken = new System.Windows.Forms.CheckBox();
            this.listView_log = new System.Windows.Forms.ListView();
            this.listView_log_type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_log_source = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_log_message = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_token
            // 
            this.textBox_token.Location = new System.Drawing.Point(91, 319);
            this.textBox_token.MaxLength = 200;
            this.textBox_token.Name = "textBox_token";
            this.textBox_token.PasswordChar = '•';
            this.textBox_token.Size = new System.Drawing.Size(409, 20);
            this.textBox_token.TabIndex = 1;
            // 
            // label_token
            // 
            this.label_token.AutoSize = true;
            this.label_token.Location = new System.Drawing.Point(44, 323);
            this.label_token.Name = "label_token";
            this.label_token.Size = new System.Drawing.Size(41, 13);
            this.label_token.TabIndex = 2;
            this.label_token.Text = "Token:";
            // 
            // button_start
            // 
            this.button_start.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_start.Location = new System.Drawing.Point(6, 103);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(209, 23);
            this.button_start.TabIndex = 3;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // comboBox_logType
            // 
            this.comboBox_logType.FormattingEnabled = true;
            this.comboBox_logType.Items.AddRange(new object[] {
            "Critical",
            "Debug",
            "Error",
            "Info",
            "Verbose",
            "Warning"});
            this.comboBox_logType.Location = new System.Drawing.Point(91, 344);
            this.comboBox_logType.Name = "comboBox_logType";
            this.comboBox_logType.Size = new System.Drawing.Size(170, 21);
            this.comboBox_logType.TabIndex = 4;
            // 
            // label_logType
            // 
            this.label_logType.AutoSize = true;
            this.label_logType.Location = new System.Drawing.Point(31, 346);
            this.label_logType.Name = "label_logType";
            this.label_logType.Size = new System.Drawing.Size(55, 13);
            this.label_logType.TabIndex = 5;
            this.label_logType.Text = "Log Type:";
            // 
            // textBox_activityName
            // 
            this.textBox_activityName.Location = new System.Drawing.Point(316, 394);
            this.textBox_activityName.MaxLength = 200;
            this.textBox_activityName.Name = "textBox_activityName";
            this.textBox_activityName.Size = new System.Drawing.Size(233, 20);
            this.textBox_activityName.TabIndex = 7;
            // 
            // label_activityName
            // 
            this.label_activityName.AutoSize = true;
            this.label_activityName.Location = new System.Drawing.Point(272, 397);
            this.label_activityName.Name = "label_activityName";
            this.label_activityName.Size = new System.Drawing.Size(38, 13);
            this.label_activityName.TabIndex = 8;
            this.label_activityName.Text = "Name:";
            // 
            // button_refreshGame
            // 
            this.button_refreshGame.Enabled = false;
            this.button_refreshGame.Location = new System.Drawing.Point(113, 19);
            this.button_refreshGame.Name = "button_refreshGame";
            this.button_refreshGame.Size = new System.Drawing.Size(102, 23);
            this.button_refreshGame.TabIndex = 9;
            this.button_refreshGame.Text = "Refresh Game";
            this.button_refreshGame.UseVisualStyleBackColor = true;
            this.button_refreshGame.Click += new System.EventHandler(this.button_setGame_Click);
            // 
            // textBox_streamUrl
            // 
            this.textBox_streamUrl.Location = new System.Drawing.Point(316, 420);
            this.textBox_streamUrl.Name = "textBox_streamUrl";
            this.textBox_streamUrl.Size = new System.Drawing.Size(233, 20);
            this.textBox_streamUrl.TabIndex = 10;
            // 
            // comboBox_activity
            // 
            this.comboBox_activity.FormattingEnabled = true;
            this.comboBox_activity.Items.AddRange(new object[] {
            "Playing",
            "Streaming",
            "Listening",
            "Watching"});
            this.comboBox_activity.Location = new System.Drawing.Point(91, 394);
            this.comboBox_activity.Name = "comboBox_activity";
            this.comboBox_activity.Size = new System.Drawing.Size(170, 21);
            this.comboBox_activity.TabIndex = 11;
            // 
            // label_streamUrl
            // 
            this.label_streamUrl.AutoSize = true;
            this.label_streamUrl.Location = new System.Drawing.Point(242, 423);
            this.label_streamUrl.Name = "label_streamUrl";
            this.label_streamUrl.Size = new System.Drawing.Size(68, 13);
            this.label_streamUrl.TabIndex = 12;
            this.label_streamUrl.Text = "Stream URL:";
            // 
            // label_activity
            // 
            this.label_activity.AutoSize = true;
            this.label_activity.Location = new System.Drawing.Point(41, 397);
            this.label_activity.Name = "label_activity";
            this.label_activity.Size = new System.Drawing.Size(44, 13);
            this.label_activity.TabIndex = 13;
            this.label_activity.Text = "Activity:";
            // 
            // groupBox_buttons
            // 
            this.groupBox_buttons.Controls.Add(this.button_refreshStatus);
            this.groupBox_buttons.Controls.Add(this.button_start);
            this.groupBox_buttons.Controls.Add(this.button_refreshGame);
            this.groupBox_buttons.Location = new System.Drawing.Point(563, 308);
            this.groupBox_buttons.Name = "groupBox_buttons";
            this.groupBox_buttons.Size = new System.Drawing.Size(221, 132);
            this.groupBox_buttons.TabIndex = 14;
            this.groupBox_buttons.TabStop = false;
            this.groupBox_buttons.Text = "Magic Buttons";
            // 
            // button_refreshStatus
            // 
            this.button_refreshStatus.Enabled = false;
            this.button_refreshStatus.Location = new System.Drawing.Point(6, 19);
            this.button_refreshStatus.Name = "button_refreshStatus";
            this.button_refreshStatus.Size = new System.Drawing.Size(102, 23);
            this.button_refreshStatus.TabIndex = 10;
            this.button_refreshStatus.Text = "Refresh Status";
            this.button_refreshStatus.UseVisualStyleBackColor = true;
            this.button_refreshStatus.Click += new System.EventHandler(this.button_refreshStatus_Click);
            // 
            // comboBox_status
            // 
            this.comboBox_status.FormattingEnabled = true;
            this.comboBox_status.Items.AddRange(new object[] {
            "Online",
            "Idle",
            "Do Not Disturb",
            "Invisible"});
            this.comboBox_status.Location = new System.Drawing.Point(330, 344);
            this.comboBox_status.Name = "comboBox_status";
            this.comboBox_status.Size = new System.Drawing.Size(170, 21);
            this.comboBox_status.TabIndex = 15;
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Location = new System.Drawing.Point(284, 347);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(40, 13);
            this.label_status.TabIndex = 16;
            this.label_status.Text = "Status:";
            // 
            // checkBox_revealToken
            // 
            this.checkBox_revealToken.AutoSize = true;
            this.checkBox_revealToken.Checked = true;
            this.checkBox_revealToken.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_revealToken.Location = new System.Drawing.Point(506, 321);
            this.checkBox_revealToken.Name = "checkBox_revealToken";
            this.checkBox_revealToken.Size = new System.Drawing.Size(48, 17);
            this.checkBox_revealToken.TabIndex = 17;
            this.checkBox_revealToken.Text = "Hide";
            this.checkBox_revealToken.UseVisualStyleBackColor = true;
            this.checkBox_revealToken.CheckedChanged += new System.EventHandler(this.checkBox_revealToken_CheckedChanged);
            // 
            // listView_log
            // 
            this.listView_log.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listView_log_type,
            this.listView_log_source,
            this.listView_log_message});
            this.listView_log.FullRowSelect = true;
            this.listView_log.HideSelection = false;
            this.listView_log.Location = new System.Drawing.Point(12, 12);
            this.listView_log.Name = "listView_log";
            this.listView_log.Size = new System.Drawing.Size(772, 290);
            this.listView_log.TabIndex = 18;
            this.listView_log.UseCompatibleStateImageBehavior = false;
            this.listView_log.View = System.Windows.Forms.View.Details;
            // 
            // listView_log_type
            // 
            this.listView_log_type.Text = "Type";
            this.listView_log_type.Width = 86;
            // 
            // listView_log_source
            // 
            this.listView_log_source.Text = "Source";
            this.listView_log_source.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.listView_log_source.Width = 112;
            // 
            // listView_log_message
            // 
            this.listView_log_message.Text = "Message";
            this.listView_log_message.Width = 570;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 451);
            this.Controls.Add(this.listView_log);
            this.Controls.Add(this.checkBox_revealToken);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.comboBox_status);
            this.Controls.Add(this.groupBox_buttons);
            this.Controls.Add(this.label_activity);
            this.Controls.Add(this.label_streamUrl);
            this.Controls.Add(this.comboBox_activity);
            this.Controls.Add(this.textBox_streamUrl);
            this.Controls.Add(this.label_activityName);
            this.Controls.Add(this.textBox_activityName);
            this.Controls.Add(this.label_logType);
            this.Controls.Add(this.comboBox_logType);
            this.Controls.Add(this.label_token);
            this.Controls.Add(this.textBox_token);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Diamond 💎";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox_buttons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

		#endregion
		internal System.Windows.Forms.TextBox textBox_token;
		internal System.Windows.Forms.Label label_token;
		internal System.Windows.Forms.Button button_start;
		internal System.Windows.Forms.ComboBox comboBox_logType;
		internal System.Windows.Forms.Label label_logType;
        internal System.Windows.Forms.TextBox textBox_activityName;
        internal System.Windows.Forms.Label label_activityName;
        internal System.Windows.Forms.Label label_streamUrl;
        internal System.Windows.Forms.Label label_activity;
        internal System.Windows.Forms.TextBox textBox_streamUrl;
        internal System.Windows.Forms.ComboBox comboBox_activity;
        internal System.Windows.Forms.GroupBox groupBox_buttons;
        internal System.Windows.Forms.Button button_refreshGame;
        internal System.Windows.Forms.ComboBox comboBox_status;
        internal System.Windows.Forms.Label label_status;
        internal System.Windows.Forms.Button button_refreshStatus;
        internal System.Windows.Forms.CheckBox checkBox_revealToken;
        internal System.Windows.Forms.ListView listView_log;
        internal System.Windows.Forms.ColumnHeader listView_log_type;
        internal System.Windows.Forms.ColumnHeader listView_log_source;
        internal System.Windows.Forms.ColumnHeader listView_log_message;
    }
}

