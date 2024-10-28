using GameServer.Framework;
using GameServer.Framework.Characters;
using GameServer.Framework.Fields;
using GameServer.Framework.Items;
using System.Diagnostics;
using System.Threading;

namespace GameServer.Forms;

public partial class MainForm
{
	#region Field/Properties

	private readonly Field.MonsterSettings _loadedMonsterSettings;


	private readonly Dictionary<string, KeyValuePair<string, string>> _monsterNames = new();

	// key => FileName, value => KeyPair with: key => Name, value => ServerName
	private readonly Dictionary<string, KeyValuePair<string, string>> _npcNames = new();

	#endregion


	#region Field tab events

	private void lbFieldFiles_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (lbFieldFiles.SelectedItem is not string fileName)
			return;

		var spmFile = Path.Combine(Globals.FieldPath, fileName + ".spm");
		if (!File.Exists(spmFile))
		{
			var result = MessageBox.Show($"{spmFile} was not found!\r\nWant to create file?", "File not found!", MessageBoxButtons.YesNo);
			if (result == DialogResult.No)
				return;

			File.Create(spmFile);
		}

		_loadedMonsterSettings.Reset();
		_loadedMonsterSettings.SetFile(spmFile);
		_loadedMonsterSettings.Process();

		// TODO: NPCs

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
		ctmFieldMonsterAdd.Show(lbFieldMonsters, Point.Empty);
	}

	private void btnFieldMonsterAddBoss_Click(object sender, EventArgs e)
	{
		if (lbFieldMonsters.SelectedItem is not string fileName)
			return;

		if (_monsterNames.ContainsKey(fileName))
		{
			BossSpawnAddEntry(_monsterNames[fileName].Key);
		}
	}

	private void txtFieldMonsterSearch_TextChanged(object sender, EventArgs e)
	{
		ListBoxSearch(lbFieldMonsters, txtFieldMonsterSearch.Text);
	}

	private void ctmFieldMonsterAdd_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
	{
		if (lbFieldMonsters.SelectedItem is not string fileName)
			return;

		if (!_monsterNames.ContainsKey(fileName))
			return;

		switch (e.ClickedItem)
		{
			case var item when item == miFieldAddMonster:
				MonsterSpawnAddEntry(_monsterNames[fileName]);
				break;

			case var item when item == miFieldAddServant:
				BossSpawnAddServant(_monsterNames[fileName].Key);
				break;
		}
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
			BossSpawnAddEntry(_npcNames[fileName].Key);
		}
	}

	private void txtFieldNPCSearch_TextChanged(object sender, EventArgs e)
	{
		ListBoxSearch(lbFieldNPCs, txtFieldNPCSearch.Text);
	}

	private void tcFieldInfo_Selected(object sender, TabControlEventArgs e)
	{
		switch (e.TabPageIndex)
		{
			case 0:
				break;

			case 1:
				break;

			default:
				break;
		}
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

			_npcNames.TryAdd(fileName, new KeyValuePair<string, string>(npc.Name, npc.ServerName));
		}

		var npcs = _npcNames.Keys.ToList();
		npcs.Sort();

		lbFieldNPCs.DataSource = npcs;
	}

	private void LoadFieldFiles()
	{
		if (SetCurrentTabFiles(Globals.FieldPath, "*.spp"))
		{
			lbFieldFiles.Items.Clear();

			foreach (var fileName in _currentTabFiles!)
			{
				var file = fileName.Remove(fileName.Length - 4);

				lbFieldFiles.Items.Add(file);
			}
		}
	}

	private void SetFieldData()
	{
		try
		{
			// Monster Settings
			nudFieldMonsterMaxFlags.Value = _loadedMonsterSettings.MaxFlags;
			nudFieldMonstersPerFlag.Value = _loadedMonsterSettings.MonstersPerFlag;
			nudFieldMonsterDelayMin.Value = _loadedMonsterSettings.SpawnDelay.Min;
			nudFieldMonsterDelayMax.Value = _loadedMonsterSettings.SpawnDelay.Max;

			PopulateMonsterSpawn();
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
			_loadedMonsterSettings.MaxFlags = (int)nudFieldMonsterMaxFlags.Value;
			_loadedMonsterSettings.MonstersPerFlag = (int)nudFieldMonstersPerFlag.Value;
			_loadedMonsterSettings.SpawnDelay = new Framework.Range((int)nudFieldMonsterDelayMin.Value, (int)nudFieldMonsterDelayMax.Value);

			RetrieveMonsterSpawn();
		}
		catch (Exception ex)
		{
			LogError(ex);
		}
	}

	#endregion


	#region Control Helper methods

	private void MonsterSpawnAddEntry(KeyValuePair<string, string> entry, int rate = 0)
	{
		var index = dgvFieldMonsters.Rows.Add();
		var row = dgvFieldMonsters.Rows[index];

		// Constants for cell indices
		const int nameCellIndex = 0;
		const int serverNameCellIndex = 1;
		const int rateCellIndex = 2;

		row.Cells[nameCellIndex].Value = entry.Key;
		row.Cells[serverNameCellIndex].Value = entry.Value;
		row.Cells[rateCellIndex].Value = rate.ToString();
	}

	private void BossSpawnAddEntry(string name, string? servantName = null, int servantCount = 0, List<int>? time = null)
	{
		var index = dgvFieldBossTime.Rows.Add();
		var row = dgvFieldBossTime.Rows[index];

		// Constants for cell indices
		const int bossNameCellIndex = 0;
		const int servantNameCellIndex = 1;
		const int servantCountCellIndex = 2;
		const int bossTimeCellIndex = 3;

		row.Cells[bossNameCellIndex].Value = name;
		row.Cells[servantNameCellIndex].Value = servantName;
		row.Cells[servantNameCellIndex].Selected = servantName == null;
		row.Cells[servantCountCellIndex].Value = servantCount;
		row.Cells[bossTimeCellIndex].Value = time != null ? string.Join(' ', time) : string.Empty;
	}

	private void BossSpawnAddServant(string name)
	{
		if (dgvFieldBossTime.SelectedRows.Count <= 0)
			return;

		var row = dgvFieldBossTime.SelectedRows[0];
		if (row == null)
			return;

		// Constants for cell indices
		const int servantNameCellIndex = 1;
		const int servantCountCellIndex = 2;
		row.Cells[servantNameCellIndex].Value = name;
		row.Cells[servantCountCellIndex].Value = "1";
	}

	private void PopulateMonsterSpawn()
	{
		dgvFieldMonsters.Rows.Clear();
		dgvFieldBossTime.Rows.Clear();

		if (!_loadedMonsterSettings.Monsters.Any())
			return;

		foreach (var monster in _loadedMonsterSettings.Monsters)
		{
			var keyPair = _monsterNames.Values.Where(v => v.Value == monster.Name).FirstOrDefault();

			MonsterSpawnAddEntry(keyPair, monster.Rate);
		}

		if (!_loadedMonsterSettings.Bosses.Any())
			return;

		foreach (var boss in _loadedMonsterSettings.Bosses)
		{
			var bossPair = _monsterNames.Values.Where(v => v.Value == boss.Name).FirstOrDefault();
			var servant = _monsterNames.Values.Where(v => v.Value == boss.ServantName).FirstOrDefault();

			BossSpawnAddEntry(bossPair.Key, servant.Key, boss.ServantCount, boss.Time);
		}
	}

	private void RetrieveMonsterSpawn()
	{
		// Retrieve monsters
		{
			_loadedMonsterSettings.Monsters.Clear();

			// Constants for cell indices
			//const int nameCellIndex = 0;
			const int serverNameCellIndex = 1;
			const int rateCellIndex = 2;

			foreach (DataGridViewRow row in dgvFieldMonsters.Rows)
			{
				if (row.Cells[serverNameCellIndex].Value is not string name)
					continue;
				if (row.Cells[rateCellIndex].Value is not string str || !int.TryParse(str, out int rate))
					continue;

				var actor = new Actor
				{
					Name = name,
					Rate = rate,
				};

				_loadedMonsterSettings.Monsters.Add(actor);
			}
		}

		// Retrieve bosses
		{
			_loadedMonsterSettings.Bosses.Clear();

			// Constants for cell indices
			const int bossNameCellIndex = 0;
			const int servantNameCellIndex = 1;
			const int servantCountCellIndex = 2;
			const int bossTimeCellIndex = 3;

			foreach (DataGridViewRow row in dgvFieldBossTime.Rows)
			{
				if (row.Cells[bossNameCellIndex].Value is not string bossName)
					continue;
				if (row.Cells[servantNameCellIndex].Value is not string servantName)
					continue;
				if (row.Cells[servantCountCellIndex].Value is not string rateStr || !int.TryParse(rateStr, out int servantCount))
					continue;
				if (row.Cells[bossTimeCellIndex].Value is not string bossTime)
					continue;

				var boss = _monsterNames.Values.Where(v => v.Key == bossName).FirstOrDefault();
				var servant = _monsterNames.Values.Where(v => v.Key == servantName).FirstOrDefault();

				var time = new List<int>();

                foreach (var item in bossTime.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries))
                {
					if (!int.TryParse(item, out int result))
						continue;

					time.Add(result);
                }

                var bossActor = new BossActor
				{
					Name = boss.Key,
					ServantName = servant.Key,
					ServantCount = servantCount,
					Time = time
				};

				_loadedMonsterSettings.Bosses.Add(bossActor);
			}
		}
	}

	#endregion
}
