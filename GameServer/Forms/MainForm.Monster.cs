using GameServer.Framework;
using GameServer.Framework.Characters;
using System.Diagnostics;

namespace GameServer.Forms;

public partial class MainForm
{
	#region Field/Properties

	private readonly Monster _loadedMonster;

	#endregion


	#region Monster tab events

	private void lbMonsterFiles_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (lbMonsterFiles.SelectedItem is not string fileName)
			return;

		_loadedMonster.Reset();
		_loadedMonster.SetFile(Path.Combine(Globals.MonsterPath, fileName));
		_loadedMonster.Process();

		SetMonsterData();
	}

	private void btnMonsterFilesReload_Click(object sender, EventArgs e)
	{
		LoadMonsterFiles();
	}

	private void btnMonsterFilesNew_Click(object sender, EventArgs e)
	{
		CreateFile(lbMonsterFiles, Globals.MonsterPath, LoadMonsterFiles);
	}

	private void btnMonsterFilesDelete_Click(object sender, EventArgs e)
	{
		DeleteFile(lbMonsterFiles, Globals.MonsterPath, LoadMonsterFiles);
	}

	private void btnMonsterFilesCopy_Click(object sender, EventArgs e)
	{
		CopyFile(lbMonsterFiles, Globals.MonsterPath, LoadMonsterFiles);
	}

	private void btnMonsterFilesSave_Click(object sender, EventArgs e)
	{
		GetMonsterData();

		DialogResult result = MessageBox.Show("Are you sure to save data?", "Save", MessageBoxButtons.YesNo);

		if (result == DialogResult.Yes)
		{
			_loadedMonster.Save();
		}
	}

	private void btnMonsterFilesOpenRaw_Click(object sender, EventArgs e)
	{
		if (lbMonsterFiles.SelectedItem is not string fileName)
			return;

		var path = Path.Combine(Globals.MonsterPath, fileName);
		Process.Start(Globals.NotepadPath, $"\"{path}\"");
	}

	private void txtMonsterFilesSearch_TextChanged(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(txtMonsterFilesSearch.Text))
			return;

		var file = lbMonsterFiles.Items.OfType<string>()
			.Where(x => x.StartsWith(txtMonsterFilesSearch.Text, StringComparison.InvariantCultureIgnoreCase) ||
						x.Contains(txtMonsterFilesSearch.Text, StringComparison.InvariantCultureIgnoreCase))
			.FirstOrDefault();

		if (!string.IsNullOrEmpty(file))
		{
			var index = lbMonsterFiles.Items.IndexOf(file);
			lbMonsterFiles.SelectedIndex = index;
		}
	}

	private void btnMonsterBrowseZhoon_Click(object sender, EventArgs e)
	{
		var fileName = BrowseZhoonFile(Globals.MonsterPath);

		if (!string.IsNullOrEmpty(fileName))
			txtMonsterExternalFile.Text = fileName;
	}

	#endregion


	#region Helper methods

	private void LoadMonsterFiles()
	{
		gbMonsterInformation.Text = "Information";

		if (SetCurrentTabFiles(Globals.MonsterPath, "*.inf"))
		{
			lbMonsterFiles.Items.Clear();
			lbMonsterFiles.Items.AddRange(_currentTabFiles!);
		}
	}

	private void SetMonsterData()
	{
		try
		{
			gbMonsterInformation.Text = "Information of: " + _loadedMonster.Name;

			txtMonsterName.Text = _loadedMonster.Name;
			txtMonsterServerName.Text = _loadedMonster.ServerName;


			txtMonsterExternalFile.Text = _loadedMonster.ExternalFile;
		}
		catch (Exception ex)
		{
			LogError(ex);
		}
	}

	private void GetMonsterData()
	{
		try
		{
			_loadedMonster.Name = txtMonsterName.Text;
			_loadedMonster.ServerName = txtNPCServerName.Text;



			_loadedMonster.ExternalFile = txtMonsterExternalFile.Text;
		}
		catch (Exception ex)
		{
			LogError(ex);
		}
	}

	#endregion
}
