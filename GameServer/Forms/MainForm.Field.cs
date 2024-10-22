using GameServer.Framework;
using GameServer.Framework.Characters;
using GameServer.Framework.Items;
using System.Diagnostics;

namespace GameServer.Forms;

public partial class MainForm
{
	#region Field/Properties

	private readonly Dictionary<string, string> _monsterNames = new();
	private readonly Dictionary<string, string> _npcNames = new();

	#endregion


	#region Field tab events

	private void lbFieldFiles_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (lbFieldFiles.SelectedItem is not string fileName)
			return;

		// TODO:

		SetFieldData();
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

	private void txtFieldFilesSearch_TextChanged(object sender, EventArgs e)
	{
		ListBoxSearch(lbFieldFiles, txtFieldFilesSearch.Text);
	}


	private void btnFieldMonsterReload_Click(object sender, EventArgs e)
	{
		LoadMonsterSounds();
	}

	private void btnFieldMonsterAdd_Click(object sender, EventArgs e)
	{
		if (lbFieldMonsters.SelectedItem is not string fileName)
			return;

		if(_monsterNames.ContainsKey(fileName))
		{
			MessageBox.Show(_monsterNames[fileName]);
		}
	}

	private void txtFieldMonsterSearch_TextChanged(object sender, EventArgs e)
	{
		ListBoxSearch(lbFieldMonsters, txtFieldMonsterSearch.Text);
	}

	private void btnFieldNPCReload_Click(object sender, EventArgs e)
	{
		LoadNPCList();
	}

	private void btnFieldNPCAdd_Click(object sender, EventArgs e)
	{
		if (lbFieldNPCs.SelectedItem is not string fileName)
			return;

		if (_npcNames.ContainsKey(fileName))
		{
			MessageBox.Show(_npcNames[fileName]);
		}
	}

	private void txtFieldNPCSearch_TextChanged(object sender, EventArgs e)
	{
		ListBoxSearch(lbFieldNPCs, txtFieldNPCSearch.Text);
	}

	#endregion


	#region Helper methods

	private void LoadNPCList()
	{
		if (!SetCurrentTabFiles(Globals.NPCPath, "*.npc"))
			return;

		_npcNames.Clear();

		NPC npc = new();

		foreach (var fileName in _currentTabFiles!)
		{
			npc.Process(Path.Combine(Globals.NPCPath, fileName));

			_npcNames.TryAdd(fileName, npc.ServerName);
		}

		var npcs = _npcNames.Keys.ToList();
		npcs.Sort();

		lbFieldNPCs.DataSource = npcs;
	}

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
