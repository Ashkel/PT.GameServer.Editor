namespace GameServer.Forms
{
	partial class SettingsForm
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
			btnSave = new Button();
			groupBox1 = new GroupBox();
			btnBrowseNotepad = new Button();
			txtNotepadPath = new TextBox();
			label3 = new Label();
			btnBrowseClient = new Button();
			txtClientPath = new TextBox();
			label2 = new Label();
			btnBrowseServer = new Button();
			txtServerPath = new TextBox();
			label1 = new Label();
			groupBox2 = new GroupBox();
			cbbUseLanguage = new ComboBox();
			label4 = new Label();
			ckbKeepComments = new CheckBox();
			groupBox1.SuspendLayout();
			groupBox2.SuspendLayout();
			SuspendLayout();
			// 
			// btnSave
			// 
			btnSave.Enabled = false;
			btnSave.Location = new Point(712, 418);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(80, 24);
			btnSave.TabIndex = 0;
			btnSave.Text = "Save";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += btnSave_Click;
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(btnBrowseNotepad);
			groupBox1.Controls.Add(txtNotepadPath);
			groupBox1.Controls.Add(label3);
			groupBox1.Controls.Add(btnBrowseClient);
			groupBox1.Controls.Add(txtClientPath);
			groupBox1.Controls.Add(label2);
			groupBox1.Controls.Add(btnBrowseServer);
			groupBox1.Controls.Add(txtServerPath);
			groupBox1.Controls.Add(label1);
			groupBox1.Location = new Point(12, 12);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(360, 131);
			groupBox1.TabIndex = 1;
			groupBox1.TabStop = false;
			groupBox1.Text = "Directories";
			// 
			// btnBrowseNotepad
			// 
			btnBrowseNotepad.Location = new Point(326, 86);
			btnBrowseNotepad.Name = "btnBrowseNotepad";
			btnBrowseNotepad.Size = new Size(24, 24);
			btnBrowseNotepad.TabIndex = 8;
			btnBrowseNotepad.Text = "...";
			btnBrowseNotepad.UseVisualStyleBackColor = true;
			btnBrowseNotepad.Click += btnBrowseNotepad_Click;
			// 
			// txtNotepadPath
			// 
			txtNotepadPath.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			txtNotepadPath.AutoCompleteSource = AutoCompleteSource.FileSystemDirectories;
			txtNotepadPath.Location = new Point(80, 87);
			txtNotepadPath.Name = "txtNotepadPath";
			txtNotepadPath.Size = new Size(240, 23);
			txtNotepadPath.TabIndex = 7;
			txtNotepadPath.TextChanged += txtNotepadPath_TextChanged;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(16, 90);
			label3.Name = "label3";
			label3.Size = new Size(53, 15);
			label3.TabIndex = 6;
			label3.Text = "Notepad";
			// 
			// btnBrowseClient
			// 
			btnBrowseClient.Location = new Point(326, 57);
			btnBrowseClient.Name = "btnBrowseClient";
			btnBrowseClient.Size = new Size(24, 24);
			btnBrowseClient.TabIndex = 5;
			btnBrowseClient.Text = "...";
			btnBrowseClient.UseVisualStyleBackColor = true;
			btnBrowseClient.Click += btnBrowseClient_Click;
			// 
			// txtClientPath
			// 
			txtClientPath.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			txtClientPath.AutoCompleteSource = AutoCompleteSource.FileSystemDirectories;
			txtClientPath.Location = new Point(80, 58);
			txtClientPath.Name = "txtClientPath";
			txtClientPath.Size = new Size(240, 23);
			txtClientPath.TabIndex = 4;
			txtClientPath.TextChanged += txtClientPath_TextChanged;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(16, 61);
			label2.Name = "label2";
			label2.Size = new Size(38, 15);
			label2.TabIndex = 3;
			label2.Text = "Client";
			// 
			// btnBrowseServer
			// 
			btnBrowseServer.Location = new Point(326, 28);
			btnBrowseServer.Name = "btnBrowseServer";
			btnBrowseServer.Size = new Size(24, 24);
			btnBrowseServer.TabIndex = 2;
			btnBrowseServer.Text = "...";
			btnBrowseServer.UseVisualStyleBackColor = true;
			btnBrowseServer.Click += btnBrowseServer_Click;
			// 
			// txtServerPath
			// 
			txtServerPath.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			txtServerPath.AutoCompleteSource = AutoCompleteSource.FileSystemDirectories;
			txtServerPath.Location = new Point(80, 29);
			txtServerPath.Name = "txtServerPath";
			txtServerPath.Size = new Size(240, 23);
			txtServerPath.TabIndex = 1;
			txtServerPath.TextChanged += txtServerPath_TextChanged;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(16, 32);
			label1.Name = "label1";
			label1.Size = new Size(39, 15);
			label1.TabIndex = 0;
			label1.Text = "Server";
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(ckbKeepComments);
			groupBox2.Controls.Add(cbbUseLanguage);
			groupBox2.Controls.Add(label4);
			groupBox2.Location = new Point(378, 12);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new Size(410, 131);
			groupBox2.TabIndex = 2;
			groupBox2.TabStop = false;
			groupBox2.Text = "File Parsing";
			// 
			// cbbUseLanguage
			// 
			cbbUseLanguage.FormattingEnabled = true;
			cbbUseLanguage.Location = new Point(80, 29);
			cbbUseLanguage.Name = "cbbUseLanguage";
			cbbUseLanguage.Size = new Size(256, 23);
			cbbUseLanguage.TabIndex = 2;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(16, 32);
			label4.Name = "label4";
			label4.Size = new Size(59, 15);
			label4.TabIndex = 1;
			label4.Text = "Language";
			// 
			// ckbKeepComments
			// 
			ckbKeepComments.AutoSize = true;
			ckbKeepComments.Location = new Point(16, 60);
			ckbKeepComments.Name = "ckbKeepComments";
			ckbKeepComments.Size = new Size(131, 19);
			ckbKeepComments.TabIndex = 3;
			ckbKeepComments.Text = "Keep file comments";
			ckbKeepComments.UseVisualStyleBackColor = true;
			// 
			// SettingsForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(groupBox2);
			Controls.Add(groupBox1);
			Controls.Add(btnSave);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "SettingsForm";
			StartPosition = FormStartPosition.CenterParent;
			Text = "Editor Settings";
			Load += SettingsForm_Load;
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Button btnSave;
		private GroupBox groupBox1;
		private Button btnBrowseServer;
		private TextBox txtServerPath;
		private Label label1;
		private Button btnBrowseClient;
		private TextBox txtClientPath;
		private Label label2;
		private Button btnBrowseNotepad;
		private TextBox txtNotepadPath;
		private Label label3;
		private GroupBox groupBox2;
		private ComboBox cbbUseLanguage;
		private Label label4;
		private CheckBox ckbKeepComments;
	}
}