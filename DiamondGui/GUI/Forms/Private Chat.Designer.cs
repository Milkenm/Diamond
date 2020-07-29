namespace DiamondGui.Forms
{
	partial class PrivateChat
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrivateChat));
			this.button_startChat = new System.Windows.Forms.Button();
			this.textBox_userId = new System.Windows.Forms.TextBox();
			this.panel_newPm = new System.Windows.Forms.Panel();
			this.tabs_chats = new System.Windows.Forms.TabControl();
			this.pixelTitleBar = new ChillinRoomGMG.Controls.PixelTitleBar();
			this.panel_newPm.SuspendLayout();
			this.SuspendLayout();
			// 
			// button_startChat
			// 
			this.button_startChat.Location = new System.Drawing.Point(665, 0);
			this.button_startChat.Name = "button_startChat";
			this.button_startChat.Size = new System.Drawing.Size(135, 22);
			this.button_startChat.TabIndex = 0;
			this.button_startChat.Text = "Open Chat";
			this.button_startChat.UseVisualStyleBackColor = true;
			this.button_startChat.Click += new System.EventHandler(this.CreateNewChatTab);
			// 
			// textBox_userId
			// 
			this.textBox_userId.Location = new System.Drawing.Point(1, 1);
			this.textBox_userId.Name = "textBox_userId";
			this.textBox_userId.Size = new System.Drawing.Size(664, 20);
			this.textBox_userId.TabIndex = 1;
			// 
			// panel_newPm
			// 
			this.panel_newPm.Controls.Add(this.textBox_userId);
			this.panel_newPm.Controls.Add(this.button_startChat);
			this.panel_newPm.Location = new System.Drawing.Point(0, 23);
			this.panel_newPm.Name = "panel_newPm";
			this.panel_newPm.Size = new System.Drawing.Size(800, 22);
			this.panel_newPm.TabIndex = 2;
			// 
			// tabs_chats
			// 
			this.tabs_chats.Location = new System.Drawing.Point(1, 44);
			this.tabs_chats.Name = "tabs_chats";
			this.tabs_chats.SelectedIndex = 0;
			this.tabs_chats.Size = new System.Drawing.Size(800, 406);
			this.tabs_chats.TabIndex = 3;
			// 
			// pixelTitleBar
			// 
			this.pixelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
			this.pixelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
			this.pixelTitleBar.Location = new System.Drawing.Point(0, 0);
			this.pixelTitleBar.Name = "pixelTitleBar";
			this.pixelTitleBar.ShowMinimizeButton = true;
			this.pixelTitleBar.Size = new System.Drawing.Size(800, 22);
			this.pixelTitleBar.TabIndex = 2;
			this.pixelTitleBar.TitleExtension = "Private Chat";
			// 
			// PrivateChat
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 451);
			this.Controls.Add(this.pixelTitleBar);
			this.Controls.Add(this.panel_newPm);
			this.Controls.Add(this.tabs_chats);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PrivateChat";
			this.Text = "Private Chat";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PrivateChat_FormClosing);
			this.panel_newPm.ResumeLayout(false);
			this.panel_newPm.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.Button button_startChat;
		public System.Windows.Forms.TextBox textBox_userId;
		public System.Windows.Forms.Panel panel_newPm;
		public System.Windows.Forms.TabControl tabs_chats;
		private ChillinRoomGMG.Controls.PixelTitleBar pixelTitleBar;
	}
}