namespace TestingProject
{
	partial class SQLite
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
			this.dataGrid = new System.Windows.Forms.DataGridView();
			this.button_run = new System.Windows.Forms.Button();
			this.textBox_sql = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid
			// 
			this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGrid.Location = new System.Drawing.Point(11, 12);
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.Size = new System.Drawing.Size(503, 313);
			this.dataGrid.TabIndex = 0;
			// 
			// button_run
			// 
			this.button_run.Location = new System.Drawing.Point(439, 341);
			this.button_run.Name = "button_run";
			this.button_run.Size = new System.Drawing.Size(75, 23);
			this.button_run.TabIndex = 1;
			this.button_run.Text = "Run";
			this.button_run.UseVisualStyleBackColor = true;
			this.button_run.Click += new System.EventHandler(this.button_run_Click);
			// 
			// textBox_sql
			// 
			this.textBox_sql.Location = new System.Drawing.Point(11, 341);
			this.textBox_sql.Name = "textBox_sql";
			this.textBox_sql.Size = new System.Drawing.Size(422, 20);
			this.textBox_sql.TabIndex = 2;
			// 
			// SQLite
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(526, 373);
			this.Controls.Add(this.textBox_sql);
			this.Controls.Add(this.button_run);
			this.Controls.Add(this.dataGrid);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "SQLite";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SQLite";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGrid;
		private System.Windows.Forms.Button button_run;
		private System.Windows.Forms.TextBox textBox_sql;
	}
}