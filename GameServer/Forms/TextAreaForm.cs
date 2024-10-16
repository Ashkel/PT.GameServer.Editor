using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameServer.Forms;
public partial class TextAreaForm : Form
{
	public string Title
	{
		get => lblTitle.Text;
		set => lblTitle.Text = value;
	}

	public event EventHandler? Save;

	public TextAreaForm()
	{
		InitializeComponent();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		DialogResult result = MessageBox.Show("Are you sure to save?", "Save", MessageBoxButtons.YesNo);

		if (result == DialogResult.Yes)
		{
			Save?.Invoke(sender, e);
			Close();
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		if (Text.CompareTo(txtEditText.Text) != 0)
		{
			DialogResult result = MessageBox.Show("There are unsaved data!\r\nAre you sure?", "Cancel", MessageBoxButtons.YesNo);

			if (result == DialogResult.No)
			{
				return;
			}
		}

		Close();
	}

	public void SetText(string text)
	{
		Text = text;
		txtEditText.Text = text;
	}

	public string GetText()
	{
		return txtEditText.Text;
	}
}
