namespace DiamondGui
{
	partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.label_token = new System.Windows.Forms.Label();
            this.textBox_token = new System.Windows.Forms.TextBox();
            this.checkBox_revealToken = new System.Windows.Forms.CheckBox();
            this.comboBox_logType = new System.Windows.Forms.ComboBox();
            this.label_logType = new System.Windows.Forms.Label();
            this.groupBox_activity = new System.Windows.Forms.GroupBox();
            this.textBox_activityName = new System.Windows.Forms.TextBox();
            this.label_activityName = new System.Windows.Forms.Label();
            this.label_activity = new System.Windows.Forms.Label();
            this.textBox_streamUrl = new System.Windows.Forms.TextBox();
            this.label_streamUrl = new System.Windows.Forms.Label();
            this.comboBox_activity = new System.Windows.Forms.ComboBox();
            this.groupBox_general = new System.Windows.Forms.GroupBox();
            this.label_adminId = new System.Windows.Forms.Label();
            this.textBox_adminId = new System.Windows.Forms.TextBox();
            this.label_discordUrl = new System.Windows.Forms.Label();
            this.label_botUrl = new System.Windows.Forms.Label();
            this.textBox_discordUrl = new System.Windows.Forms.TextBox();
            this.textBox_botUrl = new System.Windows.Forms.TextBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.groupBox_webHost = new System.Windows.Forms.GroupBox();
            this.label_domain = new System.Windows.Forms.Label();
            this.textBox_domain = new System.Windows.Forms.TextBox();
            this.linkLabel_discordDev = new System.Windows.Forms.LinkLabel();
            this.linkLabel_riotDev = new System.Windows.Forms.LinkLabel();
            this.groupBox_activity.SuspendLayout();
            this.groupBox_general.SuspendLayout();
            this.groupBox_webHost.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_token
            // 
            this.label_token.AutoSize = true;
            this.label_token.Location = new System.Drawing.Point(37, 22);
            this.label_token.Name = "label_token";
            this.label_token.Size = new System.Drawing.Size(41, 13);
            this.label_token.TabIndex = 5;
            this.label_token.Text = "Token:";
            // 
            // textBox_token
            // 
            this.textBox_token.Location = new System.Drawing.Point(84, 19);
            this.textBox_token.MaxLength = 200;
            this.textBox_token.Name = "textBox_token";
            this.textBox_token.PasswordChar = '•';
            this.textBox_token.Size = new System.Drawing.Size(430, 20);
            this.textBox_token.TabIndex = 0;
            // 
            // checkBox_revealToken
            // 
            this.checkBox_revealToken.AutoSize = true;
            this.checkBox_revealToken.Checked = true;
            this.checkBox_revealToken.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_revealToken.Location = new System.Drawing.Point(520, 21);
            this.checkBox_revealToken.Name = "checkBox_revealToken";
            this.checkBox_revealToken.Size = new System.Drawing.Size(48, 17);
            this.checkBox_revealToken.TabIndex = 4;
            this.checkBox_revealToken.TabStop = false;
            this.checkBox_revealToken.Text = "Hide";
            this.checkBox_revealToken.UseVisualStyleBackColor = true;
            this.checkBox_revealToken.CheckedChanged += new System.EventHandler(this.checkBox_revealToken_CheckedChanged);
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
            this.comboBox_logType.Location = new System.Drawing.Point(84, 45);
            this.comboBox_logType.Name = "comboBox_logType";
            this.comboBox_logType.Size = new System.Drawing.Size(170, 21);
            this.comboBox_logType.TabIndex = 1;
            // 
            // label_logType
            // 
            this.label_logType.AutoSize = true;
            this.label_logType.Location = new System.Drawing.Point(23, 48);
            this.label_logType.Name = "label_logType";
            this.label_logType.Size = new System.Drawing.Size(55, 13);
            this.label_logType.TabIndex = 6;
            this.label_logType.Text = "Log Type:";
            // 
            // groupBox_activity
            // 
            this.groupBox_activity.Controls.Add(this.textBox_activityName);
            this.groupBox_activity.Controls.Add(this.label_activityName);
            this.groupBox_activity.Controls.Add(this.label_activity);
            this.groupBox_activity.Controls.Add(this.textBox_streamUrl);
            this.groupBox_activity.Controls.Add(this.label_streamUrl);
            this.groupBox_activity.Controls.Add(this.comboBox_activity);
            this.groupBox_activity.Location = new System.Drawing.Point(12, 175);
            this.groupBox_activity.Name = "groupBox_activity";
            this.groupBox_activity.Size = new System.Drawing.Size(574, 102);
            this.groupBox_activity.TabIndex = 1;
            this.groupBox_activity.TabStop = false;
            this.groupBox_activity.Text = "Activity";
            // 
            // textBox_activityName
            // 
            this.textBox_activityName.Location = new System.Drawing.Point(84, 46);
            this.textBox_activityName.MaxLength = 200;
            this.textBox_activityName.Name = "textBox_activityName";
            this.textBox_activityName.Size = new System.Drawing.Size(430, 20);
            this.textBox_activityName.TabIndex = 1;
            // 
            // label_activityName
            // 
            this.label_activityName.AutoSize = true;
            this.label_activityName.Location = new System.Drawing.Point(40, 49);
            this.label_activityName.Name = "label_activityName";
            this.label_activityName.Size = new System.Drawing.Size(38, 13);
            this.label_activityName.TabIndex = 4;
            this.label_activityName.Text = "Name:";
            // 
            // label_activity
            // 
            this.label_activity.AutoSize = true;
            this.label_activity.Location = new System.Drawing.Point(34, 22);
            this.label_activity.Name = "label_activity";
            this.label_activity.Size = new System.Drawing.Size(44, 13);
            this.label_activity.TabIndex = 3;
            this.label_activity.Text = "Activity:";
            // 
            // textBox_streamUrl
            // 
            this.textBox_streamUrl.Location = new System.Drawing.Point(84, 72);
            this.textBox_streamUrl.Name = "textBox_streamUrl";
            this.textBox_streamUrl.Size = new System.Drawing.Size(430, 20);
            this.textBox_streamUrl.TabIndex = 2;
            // 
            // label_streamUrl
            // 
            this.label_streamUrl.AutoSize = true;
            this.label_streamUrl.Location = new System.Drawing.Point(10, 75);
            this.label_streamUrl.Name = "label_streamUrl";
            this.label_streamUrl.Size = new System.Drawing.Size(68, 13);
            this.label_streamUrl.TabIndex = 5;
            this.label_streamUrl.Text = "Stream URL:";
            // 
            // comboBox_activity
            // 
            this.comboBox_activity.FormattingEnabled = true;
            this.comboBox_activity.Items.AddRange(new object[] {
            "Playing",
            "Streaming",
            "Listening",
            "Watching"});
            this.comboBox_activity.Location = new System.Drawing.Point(84, 19);
            this.comboBox_activity.Name = "comboBox_activity";
            this.comboBox_activity.Size = new System.Drawing.Size(170, 21);
            this.comboBox_activity.TabIndex = 0;
            // 
            // groupBox_general
            // 
            this.groupBox_general.Controls.Add(this.label_adminId);
            this.groupBox_general.Controls.Add(this.textBox_adminId);
            this.groupBox_general.Controls.Add(this.label_discordUrl);
            this.groupBox_general.Controls.Add(this.label_botUrl);
            this.groupBox_general.Controls.Add(this.textBox_discordUrl);
            this.groupBox_general.Controls.Add(this.textBox_botUrl);
            this.groupBox_general.Controls.Add(this.textBox_token);
            this.groupBox_general.Controls.Add(this.label_logType);
            this.groupBox_general.Controls.Add(this.checkBox_revealToken);
            this.groupBox_general.Controls.Add(this.label_token);
            this.groupBox_general.Controls.Add(this.comboBox_logType);
            this.groupBox_general.Location = new System.Drawing.Point(12, 12);
            this.groupBox_general.Name = "groupBox_general";
            this.groupBox_general.Size = new System.Drawing.Size(574, 157);
            this.groupBox_general.TabIndex = 0;
            this.groupBox_general.TabStop = false;
            this.groupBox_general.Text = "General";
            // 
            // label_adminId
            // 
            this.label_adminId.AutoSize = true;
            this.label_adminId.Location = new System.Drawing.Point(25, 127);
            this.label_adminId.Name = "label_adminId";
            this.label_adminId.Size = new System.Drawing.Size(53, 13);
            this.label_adminId.TabIndex = 10;
            this.label_adminId.Text = "Admin ID:";
            // 
            // textBox_adminId
            // 
            this.textBox_adminId.Location = new System.Drawing.Point(84, 124);
            this.textBox_adminId.Name = "textBox_adminId";
            this.textBox_adminId.Size = new System.Drawing.Size(430, 20);
            this.textBox_adminId.TabIndex = 9;
            // 
            // label_discordUrl
            // 
            this.label_discordUrl.AutoSize = true;
            this.label_discordUrl.Location = new System.Drawing.Point(7, 101);
            this.label_discordUrl.Name = "label_discordUrl";
            this.label_discordUrl.Size = new System.Drawing.Size(71, 13);
            this.label_discordUrl.TabIndex = 8;
            this.label_discordUrl.Text = "Discord URL:";
            // 
            // label_botUrl
            // 
            this.label_botUrl.AutoSize = true;
            this.label_botUrl.Location = new System.Drawing.Point(27, 75);
            this.label_botUrl.Name = "label_botUrl";
            this.label_botUrl.Size = new System.Drawing.Size(51, 13);
            this.label_botUrl.TabIndex = 7;
            this.label_botUrl.Text = "Bot URL:";
            // 
            // textBox_discordUrl
            // 
            this.textBox_discordUrl.Location = new System.Drawing.Point(84, 98);
            this.textBox_discordUrl.Name = "textBox_discordUrl";
            this.textBox_discordUrl.Size = new System.Drawing.Size(430, 20);
            this.textBox_discordUrl.TabIndex = 3;
            // 
            // textBox_botUrl
            // 
            this.textBox_botUrl.Location = new System.Drawing.Point(84, 72);
            this.textBox_botUrl.Name = "textBox_botUrl";
            this.textBox_botUrl.Size = new System.Drawing.Size(430, 20);
            this.textBox_botUrl.TabIndex = 2;
            // 
            // button_save
            // 
            this.button_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_save.Location = new System.Drawing.Point(512, 341);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 3;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(431, 341);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 4;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // groupBox_webHost
            // 
            this.groupBox_webHost.Controls.Add(this.label_domain);
            this.groupBox_webHost.Controls.Add(this.textBox_domain);
            this.groupBox_webHost.Location = new System.Drawing.Point(12, 283);
            this.groupBox_webHost.Name = "groupBox_webHost";
            this.groupBox_webHost.Size = new System.Drawing.Size(574, 52);
            this.groupBox_webHost.TabIndex = 2;
            this.groupBox_webHost.TabStop = false;
            this.groupBox_webHost.Text = "Web Host";
            // 
            // label_domain
            // 
            this.label_domain.AutoSize = true;
            this.label_domain.Location = new System.Drawing.Point(32, 22);
            this.label_domain.Name = "label_domain";
            this.label_domain.Size = new System.Drawing.Size(46, 13);
            this.label_domain.TabIndex = 1;
            this.label_domain.Text = "Domain:";
            // 
            // textBox_domain
            // 
            this.textBox_domain.Location = new System.Drawing.Point(84, 19);
            this.textBox_domain.Name = "textBox_domain";
            this.textBox_domain.Size = new System.Drawing.Size(430, 20);
            this.textBox_domain.TabIndex = 0;
            // 
            // linkLabel_discordDev
            // 
            this.linkLabel_discordDev.AutoSize = true;
            this.linkLabel_discordDev.Location = new System.Drawing.Point(10, 346);
            this.linkLabel_discordDev.Name = "linkLabel_discordDev";
            this.linkLabel_discordDev.Size = new System.Drawing.Size(134, 13);
            this.linkLabel_discordDev.TabIndex = 5;
            this.linkLabel_discordDev.TabStop = true;
            this.linkLabel_discordDev.Text = "» Discord Developer Portal";
            this.linkLabel_discordDev.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_discordDev_LinkClicked);
            // 
            // linkLabel_riotDev
            // 
            this.linkLabel_riotDev.AutoSize = true;
            this.linkLabel_riotDev.Location = new System.Drawing.Point(150, 346);
            this.linkLabel_riotDev.Name = "linkLabel_riotDev";
            this.linkLabel_riotDev.Size = new System.Drawing.Size(117, 13);
            this.linkLabel_riotDev.TabIndex = 6;
            this.linkLabel_riotDev.TabStop = true;
            this.linkLabel_riotDev.Text = "» Riot Developer Portal";
            this.linkLabel_riotDev.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_riotDev_LinkClicked);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 371);
            this.Controls.Add(this.linkLabel_riotDev);
            this.Controls.Add(this.linkLabel_discordDev);
            this.Controls.Add(this.groupBox_webHost);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.groupBox_general);
            this.Controls.Add(this.groupBox_activity);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Options_FormClosing);
            this.groupBox_activity.ResumeLayout(false);
            this.groupBox_activity.PerformLayout();
            this.groupBox_general.ResumeLayout(false);
            this.groupBox_general.PerformLayout();
            this.groupBox_webHost.ResumeLayout(false);
            this.groupBox_webHost.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		internal System.Windows.Forms.Label label_token;
		internal System.Windows.Forms.TextBox textBox_token;
		internal System.Windows.Forms.CheckBox checkBox_revealToken;
		internal System.Windows.Forms.ComboBox comboBox_logType;
		internal System.Windows.Forms.Label label_logType;
		internal System.Windows.Forms.TextBox textBox_activityName;
		internal System.Windows.Forms.Label label_activityName;
		internal System.Windows.Forms.Label label_activity;
		internal System.Windows.Forms.TextBox textBox_streamUrl;
		internal System.Windows.Forms.Label label_streamUrl;
		internal System.Windows.Forms.ComboBox comboBox_activity;
		internal System.Windows.Forms.GroupBox groupBox_activity;
		internal System.Windows.Forms.GroupBox groupBox_general;
		internal System.Windows.Forms.Button button_save;
		internal System.Windows.Forms.Button button_cancel;
		internal System.Windows.Forms.GroupBox groupBox_webHost;
		internal System.Windows.Forms.Label label_domain;
		internal System.Windows.Forms.TextBox textBox_domain;
		internal System.Windows.Forms.Label label_discordUrl;
		internal System.Windows.Forms.Label label_botUrl;
		internal System.Windows.Forms.TextBox textBox_discordUrl;
		internal System.Windows.Forms.TextBox textBox_botUrl;
		internal System.Windows.Forms.LinkLabel linkLabel_discordDev;
		internal System.Windows.Forms.Label label_adminId;
		internal System.Windows.Forms.TextBox textBox_adminId;
		internal System.Windows.Forms.LinkLabel linkLabel_riotDev;
	}
}