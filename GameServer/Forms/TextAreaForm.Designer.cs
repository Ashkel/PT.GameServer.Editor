namespace GameServer.Forms;

partial class TextAreaForm
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
		panel2 = new Panel();
		txtEditText = new TextBox();
		panel3 = new Panel();
		btnCancel = new Button();
		btnSave = new Button();
		panel1 = new Panel();
		lblTitle = new Label();
		panel2.SuspendLayout();
		panel3.SuspendLayout();
		panel1.SuspendLayout();
		SuspendLayout();
		// 
		// panel2
		// 
		panel2.BorderStyle = BorderStyle.FixedSingle;
		panel2.Controls.Add(txtEditText);
		panel2.Controls.Add(panel3);
		panel2.Controls.Add(panel1);
		panel2.Dock = DockStyle.Fill;
		panel2.Location = new Point(0, 0);
		panel2.Name = "panel2";
		panel2.Size = new Size(640, 360);
		panel2.TabIndex = 2;
		// 
		// txtEditText
		// 
		txtEditText.Dock = DockStyle.Fill;
		txtEditText.Location = new Point(0, 32);
		txtEditText.Multiline = true;
		txtEditText.Name = "txtEditText";
		txtEditText.Size = new Size(638, 294);
		txtEditText.TabIndex = 4;
		// 
		// panel3
		// 
		panel3.BorderStyle = BorderStyle.Fixed3D;
		panel3.Controls.Add(btnCancel);
		panel3.Controls.Add(btnSave);
		panel3.Dock = DockStyle.Bottom;
		panel3.Location = new Point(0, 326);
		panel3.Name = "panel3";
		panel3.Size = new Size(638, 32);
		panel3.TabIndex = 3;
		// 
		// btnCancel
		// 
		btnCancel.Location = new Point(490, 4);
		btnCancel.Name = "btnCancel";
		btnCancel.Size = new Size(64, 23);
		btnCancel.TabIndex = 1;
		btnCancel.Text = "Cancel";
		btnCancel.UseVisualStyleBackColor = true;
		btnCancel.Click += btnCancel_Click;
		// 
		// btnSave
		// 
		btnSave.Location = new Point(562, 4);
		btnSave.Name = "btnSave";
		btnSave.Size = new Size(64, 23);
		btnSave.TabIndex = 0;
		btnSave.Text = "Save";
		btnSave.UseVisualStyleBackColor = true;
		btnSave.Click += btnSave_Click;
		// 
		// panel1
		// 
		panel1.BackColor = SystemColors.WindowFrame;
		panel1.BorderStyle = BorderStyle.FixedSingle;
		panel1.Controls.Add(lblTitle);
		panel1.Dock = DockStyle.Top;
		panel1.Location = new Point(0, 0);
		panel1.Name = "panel1";
		panel1.Size = new Size(638, 32);
		panel1.TabIndex = 1;
		// 
		// lblTitle
		// 
		lblTitle.Dock = DockStyle.Fill;
		lblTitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
		lblTitle.ForeColor = SystemColors.HighlightText;
		lblTitle.Location = new Point(0, 0);
		lblTitle.Name = "lblTitle";
		lblTitle.Size = new Size(636, 30);
		lblTitle.TabIndex = 0;
		lblTitle.Text = "Control Tile";
		lblTitle.TextAlign = ContentAlignment.MiddleCenter;
		// 
		// TextAreaForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(640, 360);
		Controls.Add(panel2);
		FormBorderStyle = FormBorderStyle.None;
		Name = "TextAreaForm";
		panel2.ResumeLayout(false);
		panel2.PerformLayout();
		panel3.ResumeLayout(false);
		panel1.ResumeLayout(false);
		ResumeLayout(false);
	}

	#endregion
	private Panel panel2;
	private Panel panel3;
	private Button btnCancel;
	private Button btnSave;
	private Panel panel1;
	private Label lblTitle;
	private TextBox txtEditText;
}