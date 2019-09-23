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
            this.textBox_token.Location = new System.Drawing.Point(59, 308);
            this.textBox_token.Name = "textBox_token";
            this.textBox_token.Size = new System.Drawing.Size(466, 20);
            this.textBox_token.TabIndex = 1;
            // 
            // label_token
            // 
            this.label_token.AutoSize = true;
            this.label_token.Location = new System.Drawing.Point(12, 311);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 377);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.label_token);
            this.Controls.Add(this.textBox_token);
            this.Controls.Add(this.listBox_output);
            this.Name = "Form1";
            this.Text = "Diamond 💎";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_output;
        private System.Windows.Forms.TextBox textBox_token;
        private System.Windows.Forms.Label label_token;
        private System.Windows.Forms.Button button_start;
    }
}

