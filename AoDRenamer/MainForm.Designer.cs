namespace AoDRenamer
{
	partial class MainForm
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
			this.folderNameTextBox = new System.Windows.Forms.TextBox();
			this.referenceButton = new System.Windows.Forms.Button();
			this.exitButton = new System.Windows.Forms.Button();
			this.startButton = new System.Windows.Forms.Button();
			this.logRichTextBox = new System.Windows.Forms.RichTextBox();
			this.typeComboBox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// folderNameTextBox
			// 
			this.folderNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.folderNameTextBox.Location = new System.Drawing.Point(12, 43);
			this.folderNameTextBox.Name = "folderNameTextBox";
			this.folderNameTextBox.Size = new System.Drawing.Size(278, 19);
			this.folderNameTextBox.TabIndex = 0;
			// 
			// referenceButton
			// 
			this.referenceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.referenceButton.Location = new System.Drawing.Point(296, 41);
			this.referenceButton.Name = "referenceButton";
			this.referenceButton.Size = new System.Drawing.Size(75, 23);
			this.referenceButton.TabIndex = 1;
			this.referenceButton.Text = "参照";
			this.referenceButton.UseVisualStyleBackColor = true;
			this.referenceButton.Click += new System.EventHandler(this.OnReferenceButtonClick);
			// 
			// exitButton
			// 
			this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.exitButton.Location = new System.Drawing.Point(296, 10);
			this.exitButton.Name = "exitButton";
			this.exitButton.Size = new System.Drawing.Size(75, 23);
			this.exitButton.TabIndex = 2;
			this.exitButton.Text = "終了";
			this.exitButton.UseVisualStyleBackColor = true;
			this.exitButton.Click += new System.EventHandler(this.OnExitButtonClick);
			// 
			// startButton
			// 
			this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.startButton.Location = new System.Drawing.Point(377, 12);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(75, 50);
			this.startButton.TabIndex = 3;
			this.startButton.Text = "開始";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.OnStartButtonClick);
			// 
			// logRichTextBox
			// 
			this.logRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.logRichTextBox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.logRichTextBox.Location = new System.Drawing.Point(12, 70);
			this.logRichTextBox.Name = "logRichTextBox";
			this.logRichTextBox.ReadOnly = true;
			this.logRichTextBox.Size = new System.Drawing.Size(440, 200);
			this.logRichTextBox.TabIndex = 4;
			this.logRichTextBox.Text = "";
			// 
			// typeComboBox
			// 
			this.typeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.typeComboBox.FormattingEnabled = true;
			this.typeComboBox.Items.AddRange(new object[] {
            "自動判別",
            "Arsenal of Democracy",
            "Iron Cross (Armageddon)",
            "Iron Cross (Arsenal of Democracy)",
            "Iron Cross (Darkest Hour)"});
			this.typeComboBox.Location = new System.Drawing.Point(12, 12);
			this.typeComboBox.Name = "typeComboBox";
			this.typeComboBox.Size = new System.Drawing.Size(278, 20);
			this.typeComboBox.TabIndex = 8;
			this.typeComboBox.Text = "自動判別";
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(464, 282);
			this.Controls.Add(this.typeComboBox);
			this.Controls.Add(this.logRichTextBox);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.exitButton);
			this.Controls.Add(this.referenceButton);
			this.Controls.Add(this.folderNameTextBox);
			this.MinimumSize = new System.Drawing.Size(480, 320);
			this.Name = "MainForm";
			this.Text = "AoD Renamer";
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnFormDragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnFormDragEnter);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox folderNameTextBox;
		private System.Windows.Forms.Button referenceButton;
		private System.Windows.Forms.Button exitButton;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.RichTextBox logRichTextBox;
        private System.Windows.Forms.ComboBox typeComboBox;
	}
}