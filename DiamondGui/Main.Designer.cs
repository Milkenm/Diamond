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
			this.listBox_output = new System.Windows.Forms.ListBox();
			this.textBox_token = new System.Windows.Forms.TextBox();
			this.label_token = new System.Windows.Forms.Label();
			this.button_start = new System.Windows.Forms.Button();
			this.comboBox_logType = new System.Windows.Forms.ComboBox();
			this.label_logType = new System.Windows.Forms.Label();
			this.button_revealToken = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listBox_output
			// 
			this.listBox_output.FormattingEnabled = true;
			this.listBox_output.Location = new System.Drawing.Point(12, 12);
			this.listBox_output.Name = "listBox_output";
			this.listBox_output.Size = new System.Drawing.Size(776, 290);
			this.listBox_output.TabIndex = 0;
			// 
			// textBox_token
			// 
			this.textBox_token.Location = new System.Drawing.Point(76, 308);
			this.textBox_token.MaxLength = 200;
			this.textBox_token.Name = "textBox_token";
			this.textBox_token.PasswordChar = '•';
			this.textBox_token.Size = new System.Drawing.Size(466, 20);
			this.textBox_token.TabIndex = 1;
			// 
			// label_token
			// 
			this.label_token.AutoSize = true;
			this.label_token.Location = new System.Drawing.Point(29, 311);
			this.label_token.Name = "label_token";
			this.label_token.Size = new System.Drawing.Size(41, 13);
			this.label_token.TabIndex = 2;
			this.label_token.Text = "Token:";
			// 
			// button_start
			// 
			this.button_start.Location = new System.Drawing.Point(713, 342);
			this.button_start.Name = "button_start";
			this.button_start.Size = new System.Drawing.Size(75, 23);
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
			this.comboBox_logType.Location = new System.Drawing.Point(76, 334);
			this.comboBox_logType.Name = "comboBox_logType";
			this.comboBox_logType.Size = new System.Drawing.Size(170, 21);
			this.comboBox_logType.TabIndex = 4;
			// 
			// label_logType
			// 
			this.label_logType.AutoSize = true;
			this.label_logType.Location = new System.Drawing.Point(15, 337);
			this.label_logType.Name = "label_logType";
			this.label_logType.Size = new System.Drawing.Size(55, 13);
			this.label_logType.TabIndex = 5;
			this.label_logType.Text = "Log Type:";
			// 
			// button_revealToken
			// 
			this.button_revealToken.Location = new System.Drawing.Point(542, 307);
			this.button_revealToken.Name = "button_revealToken";
			this.button_revealToken.Size = new System.Drawing.Size(22, 22);
			this.button_revealToken.TabIndex = 6;
			this.button_revealToken.UseVisualStyleBackColor = true;
			this.button_revealToken.Click += new System.EventHandler(this.button_revealToken_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 377);
			this.Controls.Add(this.button_revealToken);
			this.Controls.Add(this.label_logType);
			this.Controls.Add(this.comboBox_logType);
			this.Controls.Add(this.button_start);
			this.Controls.Add(this.label_token);
			this.Controls.Add(this.textBox_token);
			this.Controls.Add(this.listBox_output);
			this.Name = "Main";
			this.Text = "Diamond 💎";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		internal System.Windows.Forms.ListBox listBox_output;
		internal System.Windows.Forms.TextBox textBox_token;
		internal System.Windows.Forms.Label label_token;
		internal System.Windows.Forms.Button button_start;
		internal System.Windows.Forms.ComboBox comboBox_logType;
		internal System.Windows.Forms.Label label_logType;
		private System.Windows.Forms.Button button_revealToken;
	}
}

