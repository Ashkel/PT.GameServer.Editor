using GameServer.Framework;
using GameServer.Framework.Items;
using System.Diagnostics;

namespace GameServer.Forms;

public partial class MainForm
{
	#region Field/Properties

	#endregion


	#region Field tab events

	private void lbFieldFiles_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (lbFieldFiles.SelectedItem is not string fileName)
			return;

		// TODO:

		SetNPCData();
	}

	private void btnFieldFilesReload_Click(object sender, EventArgs e)
	{
		LoadFieldFiles();
	}

	private void btnFieldFilesNew_Click(object sender, EventArgs e)
	{
		CreateFile(lbFieldFiles, Globals.FieldPath, LoadFieldFiles);
	}

	private void btnFieldFilesDelete_Click(object sender, EventArgs e)
	{
		DeleteFile(lbFieldFiles, Globals.FieldPath, LoadFieldFiles);
	}

	private void btnFieldFilesCopy_Click(object sender, EventArgs e)
	{
		CopyFile(lbFieldFiles, Globals.FieldPath, LoadFieldFiles);
	}

	private void btnFieldFilesSave_Click(object sender, EventArgs e)
	{
		GetFieldData();

		DialogResult result = MessageBox.Show("Are you sure to save data?", "Save", MessageBoxButtons.YesNo);

		if (result == DialogResult.Yes)
		{
		}
	}

	private void btnFieldFilesOpenRaw_Click(object sender, EventArgs e)
	{
		if (lbFieldFiles.SelectedItem is not string fileName)
			return;

		var path = Path.Combine(Globals.FieldPath, fileName);
		Process.Start(Globals.NotepadPath, $"\"{path}\"");
	}

	#endregion


	#region Helper methods

	private void LoadFieldFiles()
	{
		if (SetCurrentTabFiles(Globals.FieldPath, "*.spm"))
		{
			lbFieldFiles.Items.Clear();
			lbFieldFiles.Items.AddRange(_currentTabFiles!);
		}
	}

	private void SetFieldData()
	{
		try
		{

		}
		catch (Exception ex)
		{
			LogError(ex);
		}
	}
	
	private void GetFieldData()
	{
		try
		{

		}
		catch (Exception ex)
		{
			LogError(ex);
		}
	}

	#endregion
}
