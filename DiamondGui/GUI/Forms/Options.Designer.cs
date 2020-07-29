namespace DiamondGui.Forms
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
			this.textBox_activityName = new System.Windows.Forms.TextBox();
			this.label_activityName = new System.Windows.Forms.Label();
			this.label_activity = new System.Windows.Forms.Label();
			this.textBox_streamUrl = new System.Windows.Forms.TextBox();
			this.label_streamUrl = new System.Windows.Forms.Label();
			this.comboBox_activity = new System.Windows.Forms.ComboBox();
			this.label_adminId = new System.Windows.Forms.Label();
			this.textBox_adminId = new System.Windows.Forms.TextBox();
			this.label_discordUrl = new System.Windows.Forms.Label();
			this.label_botUrl = new System.Windows.Forms.Label();
			this.textBox_discordUrl = new System.Windows.Forms.TextBox();
			this.textBox_botUrl = new System.Windows.Forms.TextBox();
			this.button_save = new System.Windows.Forms.Button();
			this.button_cancel = new System.Windows.Forms.Button();
			this.label_domain = new System.Windows.Forms.Label();
			this.textBox_domain = new System.Windows.Forms.TextBox();
			this.linkLabel_discordDev = new System.Windows.Forms.LinkLabel();
			this.linkLabel_riotDev = new System.Windows.Forms.LinkLabel();
			this.tabs_options = new System.Windows.Forms.TabControl();
			this.tab_bot = new System.Windows.Forms.TabPage();
			this.label_commands = new System.Windows.Forms.Label();
			this.checkBox_allowPrefix = new System.Windows.Forms.CheckBox();
			this.checkBox_disableCommands = new System.Windows.Forms.CheckBox();
			this.checkBox_allowMention = new System.Windows.Forms.CheckBox();
			this.label_botPrefix = new System.Windows.Forms.Label();
			this.textBox_botPrefix = new System.Windows.Forms.TextBox();
			this.tab_activity = new System.Windows.Forms.TabPage();
			this.tab_webHost = new System.Windows.Forms.TabPage();
			this.tab_apiKeys = new System.Windows.Forms.TabPage();
			this.checkBox_riotApi = new System.Windows.Forms.CheckBox();
			this.label_riotApi = new System.Windows.Forms.Label();
			this.textBox_riotApi = new System.Windows.Forms.TextBox();
			this.tab_fileSystem = new System.Windows.Forms.TabPage();
			this.label_binaryNameBin = new System.Windows.Forms.Label();
			this.label_binaryName = new System.Windows.Forms.Label();
			this.textBox_binaryName = new System.Windows.Forms.TextBox();
			this.button_searchPath = new System.Windows.Forms.Button();
			this.label_binaryPath = new System.Windows.Forms.Label();
			this.textBox_binaryPath = new System.Windows.Forms.TextBox();
			this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
			this.pixelTitleBar = new ChillinRoomGMG.Controls.PixelTitleBar();
			this.tabs_options.SuspendLayout();
			this.tab_bot.SuspendLayout();
			this.tab_activity.SuspendLayout();
			this.tab_webHost.SuspendLayout();
			this.tab_apiKeys.SuspendLayout();
			this.tab_fileSystem.SuspendLayout();
			this.SuspendLayout();
			// 
			// label_token
			// 
			this.label_token.AutoSize = true;
			this.label_token.Location = new System.Drawing.Point(43, 38);
			this.label_token.Name = "label_token";
			this.label_token.Size = new System.Drawing.Size(41, 13);
			this.label_token.TabIndex = 5;
			this.label_token.Text = "Token:";
			// 
			// textBox_token
			// 
			this.textBox_token.Location = new System.Drawing.Point(90, 35);
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
			this.checkBox_revealToken.Location = new System.Drawing.Point(526, 37);
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
			this.comboBox_logType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_logType.FormattingEnabled = true;
			this.comboBox_logType.Items.AddRange(new object[] {
            "Critical",
            "Debug",
            "Error",
            "Info",
            "Verbose",
            "Warning"});
			this.comboBox_logType.Location = new System.Drawing.Point(90, 61);
			this.comboBox_logType.Name = "comboBox_logType";
			this.comboBox_logType.Size = new System.Drawing.Size(170, 21);
			this.comboBox_logType.TabIndex = 1;
			// 
			// label_logType
			// 
			this.label_logType.AutoSize = true;
			this.label_logType.Location = new System.Drawing.Point(29, 64);
			this.label_logType.Name = "label_logType";
			this.label_logType.Size = new System.Drawing.Size(55, 13);
			this.label_logType.TabIndex = 6;
			this.label_logType.Text = "Log Type:";
			// 
			// textBox_activityName
			// 
			this.textBox_activityName.Location = new System.Drawing.Point(90, 63);
			this.textBox_activityName.MaxLength = 200;
			this.textBox_activityName.Name = "textBox_activityName";
			this.textBox_activityName.Size = new System.Drawing.Size(430, 20);
			this.textBox_activityName.TabIndex = 1;
			// 
			// label_activityName
			// 
			this.label_activityName.AutoSize = true;
			this.label_activityName.Location = new System.Drawing.Point(46, 66);
			this.label_activityName.Name = "label_activityName";
			this.label_activityName.Size = new System.Drawing.Size(38, 13);
			this.label_activityName.TabIndex = 4;
			this.label_activityName.Text = "Name:";
			// 
			// label_activity
			// 
			this.label_activity.AutoSize = true;
			this.label_activity.Location = new System.Drawing.Point(40, 38);
			this.label_activity.Name = "label_activity";
			this.label_activity.Size = new System.Drawing.Size(44, 13);
			this.label_activity.TabIndex = 3;
			this.label_activity.Text = "Activity:";
			// 
			// textBox_streamUrl
			// 
			this.textBox_streamUrl.Location = new System.Drawing.Point(90, 89);
			this.textBox_streamUrl.Name = "textBox_streamUrl";
			this.textBox_streamUrl.Size = new System.Drawing.Size(430, 20);
			this.textBox_streamUrl.TabIndex = 2;
			// 
			// label_streamUrl
			// 
			this.label_streamUrl.AutoSize = true;
			this.label_streamUrl.Location = new System.Drawing.Point(16, 92);
			this.label_streamUrl.Name = "label_streamUrl";
			this.label_streamUrl.Size = new System.Drawing.Size(68, 13);
			this.label_streamUrl.TabIndex = 5;
			this.label_streamUrl.Text = "Stream URL:";
			// 
			// comboBox_activity
			// 
			this.comboBox_activity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_activity.FormattingEnabled = true;
			this.comboBox_activity.Items.AddRange(new object[] {
            "Playing",
            "Streaming",
            "Listening",
            "Watching"});
			this.comboBox_activity.Location = new System.Drawing.Point(90, 35);
			this.comboBox_activity.Name = "comboBox_activity";
			this.comboBox_activity.Size = new System.Drawing.Size(170, 21);
			this.comboBox_activity.TabIndex = 0;
			// 
			// label_adminId
			// 
			this.label_adminId.AutoSize = true;
			this.label_adminId.Location = new System.Drawing.Point(31, 143);
			this.label_adminId.Name = "label_adminId";
			this.label_adminId.Size = new System.Drawing.Size(53, 13);
			this.label_adminId.TabIndex = 10;
			this.label_adminId.Text = "Admin ID:";
			// 
			// textBox_adminId
			// 
			this.textBox_adminId.Location = new System.Drawing.Point(90, 140);
			this.textBox_adminId.Name = "textBox_adminId";
			this.textBox_adminId.Size = new System.Drawing.Size(430, 20);
			this.textBox_adminId.TabIndex = 9;
			// 
			// label_discordUrl
			// 
			this.label_discordUrl.AutoSize = true;
			this.label_discordUrl.Location = new System.Drawing.Point(13, 117);
			this.label_discordUrl.Name = "label_discordUrl";
			this.label_discordUrl.Size = new System.Drawing.Size(71, 13);
			this.label_discordUrl.TabIndex = 8;
			this.label_discordUrl.Text = "Discord URL:";
			// 
			// label_botUrl
			// 
			this.label_botUrl.AutoSize = true;
			this.label_botUrl.Location = new System.Drawing.Point(33, 91);
			this.label_botUrl.Name = "label_botUrl";
			this.label_botUrl.Size = new System.Drawing.Size(51, 13);
			this.label_botUrl.TabIndex = 7;
			this.label_botUrl.Text = "Bot URL:";
			// 
			// textBox_discordUrl
			// 
			this.textBox_discordUrl.Location = new System.Drawing.Point(90, 114);
			this.textBox_discordUrl.Name = "textBox_discordUrl";
			this.textBox_discordUrl.Size = new System.Drawing.Size(430, 20);
			this.textBox_discordUrl.TabIndex = 3;
			// 
			// textBox_botUrl
			// 
			this.textBox_botUrl.Location = new System.Drawing.Point(90, 88);
			this.textBox_botUrl.Name = "textBox_botUrl";
			this.textBox_botUrl.Size = new System.Drawing.Size(430, 20);
			this.textBox_botUrl.TabIndex = 2;
			// 
			// button_save
			// 
			this.button_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button_save.Location = new System.Drawing.Point(526, 297);
			this.button_save.Name = "button_save";
			this.button_save.Size = new System.Drawing.Size(75, 23);
			this.button_save.TabIndex = 3;
			this.button_save.Text = "Save";
			this.button_save.UseVisualStyleBackColor = true;
			this.button_save.Click += new System.EventHandler(this.Save);
			// 
			// button_cancel
			// 
			this.button_cancel.Location = new System.Drawing.Point(607, 297);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(75, 23);
			this.button_cancel.TabIndex = 4;
			this.button_cancel.Text = "Cancel";
			this.button_cancel.UseVisualStyleBackColor = true;
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// label_domain
			// 
			this.label_domain.AutoSize = true;
			this.label_domain.Location = new System.Drawing.Point(38, 38);
			this.label_domain.Name = "label_domain";
			this.label_domain.Size = new System.Drawing.Size(46, 13);
			this.label_domain.TabIndex = 1;
			this.label_domain.Text = "Domain:";
			// 
			// textBox_domain
			// 
			this.textBox_domain.Location = new System.Drawing.Point(90, 35);
			this.textBox_domain.Name = "textBox_domain";
			this.textBox_domain.Size = new System.Drawing.Size(430, 20);
			this.textBox_domain.TabIndex = 0;
			// 
			// linkLabel_discordDev
			// 
			this.linkLabel_discordDev.AutoSize = true;
			this.linkLabel_discordDev.Location = new System.Drawing.Point(12, 301);
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
			this.linkLabel_riotDev.Location = new System.Drawing.Point(152, 301);
			this.linkLabel_riotDev.Name = "linkLabel_riotDev";
			this.linkLabel_riotDev.Size = new System.Drawing.Size(117, 13);
			this.linkLabel_riotDev.TabIndex = 6;
			this.linkLabel_riotDev.TabStop = true;
			this.linkLabel_riotDev.Text = "» Riot Developer Portal";
			this.linkLabel_riotDev.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_riotDev_LinkClicked);
			// 
			// tabs_options
			// 
			this.tabs_options.Controls.Add(this.tab_bot);
			this.tabs_options.Controls.Add(this.tab_activity);
			this.tabs_options.Controls.Add(this.tab_webHost);
			this.tabs_options.Controls.Add(this.tab_apiKeys);
			this.tabs_options.Controls.Add(this.tab_fileSystem);
			this.tabs_options.Location = new System.Drawing.Point(0, 25);
			this.tabs_options.Name = "tabs_options";
			this.tabs_options.SelectedIndex = 0;
			this.tabs_options.Size = new System.Drawing.Size(687, 269);
			this.tabs_options.TabIndex = 7;
			// 
			// tab_bot
			// 
			this.tab_bot.Controls.Add(this.label_commands);
			this.tab_bot.Controls.Add(this.checkBox_allowPrefix);
			this.tab_bot.Controls.Add(this.checkBox_disableCommands);
			this.tab_bot.Controls.Add(this.checkBox_allowMention);
			this.tab_bot.Controls.Add(this.label_botPrefix);
			this.tab_bot.Controls.Add(this.textBox_botPrefix);
			this.tab_bot.Controls.Add(this.label_adminId);
			this.tab_bot.Controls.Add(this.label_token);
			this.tab_bot.Controls.Add(this.textBox_adminId);
			this.tab_bot.Controls.Add(this.comboBox_logType);
			this.tab_bot.Controls.Add(this.label_discordUrl);
			this.tab_bot.Controls.Add(this.checkBox_revealToken);
			this.tab_bot.Controls.Add(this.label_botUrl);
			this.tab_bot.Controls.Add(this.label_logType);
			this.tab_bot.Controls.Add(this.textBox_discordUrl);
			this.tab_bot.Controls.Add(this.textBox_token);
			this.tab_bot.Controls.Add(this.textBox_botUrl);
			this.tab_bot.Location = new System.Drawing.Point(4, 22);
			this.tab_bot.Name = "tab_bot";
			this.tab_bot.Padding = new System.Windows.Forms.Padding(3);
			this.tab_bot.Size = new System.Drawing.Size(679, 243);
			this.tab_bot.TabIndex = 0;
			this.tab_bot.Text = "Bot";
			this.tab_bot.UseVisualStyleBackColor = true;
			// 
			// label_commands
			// 
			this.label_commands.AutoSize = true;
			this.label_commands.Location = new System.Drawing.Point(22, 192);
			this.label_commands.Name = "label_commands";
			this.label_commands.Size = new System.Drawing.Size(62, 13);
			this.label_commands.TabIndex = 16;
			this.label_commands.Text = "Commands:";
			// 
			// checkBox_allowPrefix
			// 
			this.checkBox_allowPrefix.AutoSize = true;
			this.checkBox_allowPrefix.Location = new System.Drawing.Point(212, 192);
			this.checkBox_allowPrefix.Name = "checkBox_allowPrefix";
			this.checkBox_allowPrefix.Size = new System.Drawing.Size(80, 17);
			this.checkBox_allowPrefix.TabIndex = 15;
			this.checkBox_allowPrefix.Text = "Allow Prefix";
			this.checkBox_allowPrefix.UseVisualStyleBackColor = true;
			// 
			// checkBox_disableCommands
			// 
			this.checkBox_disableCommands.AutoSize = true;
			this.checkBox_disableCommands.Location = new System.Drawing.Point(90, 192);
			this.checkBox_disableCommands.Name = "checkBox_disableCommands";
			this.checkBox_disableCommands.Size = new System.Drawing.Size(116, 17);
			this.checkBox_disableCommands.TabIndex = 14;
			this.checkBox_disableCommands.Text = "Disable Commands";
			this.checkBox_disableCommands.UseVisualStyleBackColor = true;
			this.checkBox_disableCommands.CheckedChanged += new System.EventHandler(this.checkBox_disableCommands_CheckedChanged);
			// 
			// checkBox_allowMention
			// 
			this.checkBox_allowMention.AutoSize = true;
			this.checkBox_allowMention.Location = new System.Drawing.Point(298, 192);
			this.checkBox_allowMention.Name = "checkBox_allowMention";
			this.checkBox_allowMention.Size = new System.Drawing.Size(92, 17);
			this.checkBox_allowMention.TabIndex = 13;
			this.checkBox_allowMention.Text = "Allow Mention";
			this.checkBox_allowMention.UseVisualStyleBackColor = true;
			// 
			// label_botPrefix
			// 
			this.label_botPrefix.AutoSize = true;
			this.label_botPrefix.Location = new System.Drawing.Point(29, 169);
			this.label_botPrefix.Name = "label_botPrefix";
			this.label_botPrefix.Size = new System.Drawing.Size(55, 13);
			this.label_botPrefix.TabIndex = 12;
			this.label_botPrefix.Text = "Bot Prefix:";
			// 
			// textBox_botPrefix
			// 
			this.textBox_botPrefix.Location = new System.Drawing.Point(90, 166);
			this.textBox_botPrefix.Name = "textBox_botPrefix";
			this.textBox_botPrefix.Size = new System.Drawing.Size(430, 20);
			this.textBox_botPrefix.TabIndex = 11;
			// 
			// tab_activity
			// 
			this.tab_activity.Controls.Add(this.textBox_activityName);
			this.tab_activity.Controls.Add(this.comboBox_activity);
			this.tab_activity.Controls.Add(this.label_activityName);
			this.tab_activity.Controls.Add(this.label_streamUrl);
			this.tab_activity.Controls.Add(this.label_activity);
			this.tab_activity.Controls.Add(this.textBox_streamUrl);
			this.tab_activity.Location = new System.Drawing.Point(4, 22);
			this.tab_activity.Name = "tab_activity";
			this.tab_activity.Padding = new System.Windows.Forms.Padding(3);
			this.tab_activity.Size = new System.Drawing.Size(679, 243);
			this.tab_activity.TabIndex = 1;
			this.tab_activity.Text = "Activity";
			this.tab_activity.UseVisualStyleBackColor = true;
			// 
			// tab_webHost
			// 
			this.tab_webHost.Controls.Add(this.label_domain);
			this.tab_webHost.Controls.Add(this.textBox_domain);
			this.tab_webHost.Location = new System.Drawing.Point(4, 22);
			this.tab_webHost.Name = "tab_webHost";
			this.tab_webHost.Padding = new System.Windows.Forms.Padding(3);
			this.tab_webHost.Size = new System.Drawing.Size(679, 243);
			this.tab_webHost.TabIndex = 2;
			this.tab_webHost.Text = "Web Host";
			this.tab_webHost.UseVisualStyleBackColor = true;
			// 
			// tab_apiKeys
			// 
			this.tab_apiKeys.Controls.Add(this.checkBox_riotApi);
			this.tab_apiKeys.Controls.Add(this.label_riotApi);
			this.tab_apiKeys.Controls.Add(this.textBox_riotApi);
			this.tab_apiKeys.Location = new System.Drawing.Point(4, 22);
			this.tab_apiKeys.Name = "tab_apiKeys";
			this.tab_apiKeys.Padding = new System.Windows.Forms.Padding(3);
			this.tab_apiKeys.Size = new System.Drawing.Size(679, 243);
			this.tab_apiKeys.TabIndex = 3;
			this.tab_apiKeys.Text = "API Keys";
			this.tab_apiKeys.UseVisualStyleBackColor = true;
			// 
			// checkBox_riotApi
			// 
			this.checkBox_riotApi.AutoSize = true;
			this.checkBox_riotApi.Checked = true;
			this.checkBox_riotApi.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox_riotApi.Location = new System.Drawing.Point(526, 37);
			this.checkBox_riotApi.Name = "checkBox_riotApi";
			this.checkBox_riotApi.Size = new System.Drawing.Size(48, 17);
			this.checkBox_riotApi.TabIndex = 5;
			this.checkBox_riotApi.TabStop = false;
			this.checkBox_riotApi.Text = "Hide";
			this.checkBox_riotApi.UseVisualStyleBackColor = true;
			this.checkBox_riotApi.CheckedChanged += new System.EventHandler(this.checkBox_riotApi_CheckedChanged);
			// 
			// label_riotApi
			// 
			this.label_riotApi.AutoSize = true;
			this.label_riotApi.Location = new System.Drawing.Point(35, 38);
			this.label_riotApi.Name = "label_riotApi";
			this.label_riotApi.Size = new System.Drawing.Size(49, 13);
			this.label_riotApi.TabIndex = 1;
			this.label_riotApi.Text = "Riot API:";
			// 
			// textBox_riotApi
			// 
			this.textBox_riotApi.Location = new System.Drawing.Point(90, 35);
			this.textBox_riotApi.Name = "textBox_riotApi";
			this.textBox_riotApi.PasswordChar = '•';
			this.textBox_riotApi.Size = new System.Drawing.Size(430, 20);
			this.textBox_riotApi.TabIndex = 0;
			// 
			// tab_fileSystem
			// 
			this.tab_fileSystem.Controls.Add(this.label_binaryNameBin);
			this.tab_fileSystem.Controls.Add(this.label_binaryName);
			this.tab_fileSystem.Controls.Add(this.textBox_binaryName);
			this.tab_fileSystem.Controls.Add(this.button_searchPath);
			this.tab_fileSystem.Controls.Add(this.label_binaryPath);
			this.tab_fileSystem.Controls.Add(this.textBox_binaryPath);
			this.tab_fileSystem.Location = new System.Drawing.Point(4, 22);
			this.tab_fileSystem.Name = "tab_fileSystem";
			this.tab_fileSystem.Padding = new System.Windows.Forms.Padding(3);
			this.tab_fileSystem.Size = new System.Drawing.Size(679, 243);
			this.tab_fileSystem.TabIndex = 4;
			this.tab_fileSystem.Text = "File System";
			this.tab_fileSystem.UseVisualStyleBackColor = true;
			// 
			// label_binaryNameBin
			// 
			this.label_binaryNameBin.AutoSize = true;
			this.label_binaryNameBin.Location = new System.Drawing.Point(526, 64);
			this.label_binaryNameBin.Name = "label_binaryNameBin";
			this.label_binaryNameBin.Size = new System.Drawing.Size(24, 13);
			this.label_binaryNameBin.TabIndex = 7;
			this.label_binaryNameBin.Text = ".bin";
			// 
			// label_binaryName
			// 
			this.label_binaryName.AutoSize = true;
			this.label_binaryName.Location = new System.Drawing.Point(14, 64);
			this.label_binaryName.Name = "label_binaryName";
			this.label_binaryName.Size = new System.Drawing.Size(70, 13);
			this.label_binaryName.TabIndex = 6;
			this.label_binaryName.Text = "Binary Name:";
			// 
			// textBox_binaryName
			// 
			this.textBox_binaryName.Location = new System.Drawing.Point(90, 61);
			this.textBox_binaryName.Name = "textBox_binaryName";
			this.textBox_binaryName.Size = new System.Drawing.Size(430, 20);
			this.textBox_binaryName.TabIndex = 5;
			// 
			// button_searchPath
			// 
			this.button_searchPath.Location = new System.Drawing.Point(526, 34);
			this.button_searchPath.Name = "button_searchPath";
			this.button_searchPath.Size = new System.Drawing.Size(63, 22);
			this.button_searchPath.TabIndex = 4;
			this.button_searchPath.Text = ". . . ";
			this.button_searchPath.UseVisualStyleBackColor = true;
			this.button_searchPath.Click += new System.EventHandler(this.button_searchPath_Click);
			// 
			// label_binaryPath
			// 
			this.label_binaryPath.AutoSize = true;
			this.label_binaryPath.Location = new System.Drawing.Point(20, 38);
			this.label_binaryPath.Name = "label_binaryPath";
			this.label_binaryPath.Size = new System.Drawing.Size(64, 13);
			this.label_binaryPath.TabIndex = 3;
			this.label_binaryPath.Text = "Binary Path:";
			// 
			// textBox_binaryPath
			// 
			this.textBox_binaryPath.Location = new System.Drawing.Point(90, 35);
			this.textBox_binaryPath.Name = "textBox_binaryPath";
			this.textBox_binaryPath.Size = new System.Drawing.Size(430, 20);
			this.textBox_binaryPath.TabIndex = 2;
			// 
			// folderBrowser
			// 
			this.folderBrowser.RootFolder = System.Environment.SpecialFolder.CommonDocuments;
			// 
			// pixelTitleBar
			// 
			this.pixelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
			this.pixelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
			this.pixelTitleBar.Location = new System.Drawing.Point(0, 0);
			this.pixelTitleBar.Name = "pixelTitleBar";
			this.pixelTitleBar.ShowMinimizeButton = true;
			this.pixelTitleBar.Size = new System.Drawing.Size(686, 22);
			this.pixelTitleBar.TabIndex = 8;
			this.pixelTitleBar.TitleExtension = null;
			// 
			// Options
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(686, 325);
			this.Controls.Add(this.pixelTitleBar);
			this.Controls.Add(this.tabs_options);
			this.Controls.Add(this.linkLabel_riotDev);
			this.Controls.Add(this.linkLabel_discordDev);
			this.Controls.Add(this.button_cancel);
			this.Controls.Add(this.button_save);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Options";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Diamond 💎 Options";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Options_FormClosing);
			this.Load += new System.EventHandler(this.Options_Load);
			this.tabs_options.ResumeLayout(false);
			this.tab_bot.ResumeLayout(false);
			this.tab_bot.PerformLayout();
			this.tab_activity.ResumeLayout(false);
			this.tab_activity.PerformLayout();
			this.tab_webHost.ResumeLayout(false);
			this.tab_webHost.PerformLayout();
			this.tab_apiKeys.ResumeLayout(false);
			this.tab_apiKeys.PerformLayout();
			this.tab_fileSystem.ResumeLayout(false);
			this.tab_fileSystem.PerformLayout();
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
		internal System.Windows.Forms.Button button_save;
		internal System.Windows.Forms.Button button_cancel;
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
		internal System.Windows.Forms.Label label_botPrefix;
		internal System.Windows.Forms.TextBox textBox_botPrefix;
		internal System.Windows.Forms.TabControl tabs_options;
		internal System.Windows.Forms.TabPage tab_bot;
		internal System.Windows.Forms.TabPage tab_activity;
		internal System.Windows.Forms.TabPage tab_webHost;
		internal System.Windows.Forms.CheckBox checkBox_allowMention;
		internal System.Windows.Forms.CheckBox checkBox_allowPrefix;
		internal System.Windows.Forms.CheckBox checkBox_disableCommands;
		internal System.Windows.Forms.Label label_commands;
		internal System.Windows.Forms.TabPage tab_apiKeys;
		internal System.Windows.Forms.Label label_riotApi;
		internal System.Windows.Forms.TextBox textBox_riotApi;
		internal System.Windows.Forms.CheckBox checkBox_riotApi;
		private System.Windows.Forms.TabPage tab_fileSystem;
		private System.Windows.Forms.Button button_searchPath;
		internal System.Windows.Forms.Label label_binaryPath;
		internal System.Windows.Forms.TextBox textBox_binaryPath;
		private System.Windows.Forms.FolderBrowserDialog folderBrowser;
		internal System.Windows.Forms.Label label_binaryName;
		internal System.Windows.Forms.TextBox textBox_binaryName;
		internal System.Windows.Forms.Label label_binaryNameBin;
		private ChillinRoomGMG.Controls.PixelTitleBar pixelTitleBar;
	}
}