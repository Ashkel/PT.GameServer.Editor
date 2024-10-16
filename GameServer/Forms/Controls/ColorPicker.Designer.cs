namespace GameServer.Forms.Controls
{
	partial class ColorPicker
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			lblDescription = new Label();
			SuspendLayout();
			// 
			// lblDescription
			// 
			lblDescription.BackColor = Color.Transparent;
			lblDescription.Dock = DockStyle.Fill;
			lblDescription.ForeColor = SystemColors.ActiveCaptionText;
			lblDescription.Location = new Point(0, 0);
			lblDescription.Name = "lblDescription";
			lblDescription.Size = new Size(148, 148);
			lblDescription.TabIndex = 0;
			lblDescription.Text = "Color";
			lblDescription.TextAlign = ContentAlignment.MiddleCenter;
			lblDescription.Click += lblDescription_Click;
			// 
			// ColorPicker
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BorderStyle = BorderStyle.FixedSingle;
			Controls.Add(lblDescription);
			Name = "ColorPicker";
			Size = new Size(148, 148);
			BackColorChanged += ColorPicker_BackColorChanged;
			ResumeLayout(false);
		}

		#endregion

		private Label lblDescription;
	}
}
