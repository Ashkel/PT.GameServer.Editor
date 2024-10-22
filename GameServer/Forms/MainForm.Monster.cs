using GameServer.Framework;
using GameServer.Framework.Characters;
using GameServer.Helpers;
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
		ListBoxSearch(lbMonsterFiles, txtMonsterFilesSearch.Text);
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

	private void LoadMonsterSounds()
	{
		if (!SetCurrentTabFiles(Globals.MonsterPath, "*.inf"))
			return;

		Monster.SoundList.Clear();
		_monsterNames.Clear();

		Monster monster = new();

		foreach (var fileName in _currentTabFiles!)
		{
			monster.Reset();
			monster.Process(Path.Combine(Globals.MonsterPath, fileName));

			_monsterNames.TryAdd(fileName, monster.ServerName);
		}

		Monster.SoundList.Sort();

		cbbMonsterSoundCode.DataSource = Monster.SoundList;

		var monsters = _monsterNames.Keys.ToList();
		monsters.Sort();

		lbFieldMonsters.DataSource = monsters;
	}

	private void SetMonsterData()
	{
		try
		{
			gbMonsterInformation.Text = "Information of: " + _loadedMonster.Name;

			// Identifiers
			txtMonsterName.Text = _loadedMonster.Name;
			txtMonsterServerName.Text = _loadedMonster.ServerName;
			nudMonsterDistinctionCode.Value = (decimal)_loadedMonster.DistinctionCode;

			// Appearance (3D Model Data)
			txtMonsterModelFile.Text = _loadedMonster.ModelFile;
			nudMonsterModelSize.Value = (decimal)_loadedMonster.ModelSize;
			cbbMonsterShadowSize.SelectedItem = _loadedMonster.ShadowSize;
			txtMonsterModelEvent.Text = _loadedMonster.ModelEvent;
			nudMonsterArrowPosMin.Value = (decimal)_loadedMonster.ArrowPosition.X;
			nudMonsterArrowPosMax.Value = (decimal)_loadedMonster.ArrowPosition.Y;

			// States
			nudMonsterLevel.Value = (decimal)_loadedMonster.Level;
			cbbMonsterClass.SelectedItem = _loadedMonster.MonsterClass;
			ckbMonsterIsBoss.Checked = _loadedMonster.IsBoss;
			cbbMonsterType.SelectedItem = _loadedMonster.Type;
			cbbMonsterActiveTime.SelectedItem = _loadedMonster.ActiveTime;
			cbbMonsterNature.SelectedItem = _loadedMonster.Nature;
			nudMonsterGroupMin.Value = (decimal)_loadedMonster.GenerateGroup.Min;
			nudMonsterGroupMax.Value = (decimal)_loadedMonster.GenerateGroup.Max;
			nudMonsterSight.Value = (decimal)_loadedMonster.RealSight;
			nudMonsterIQ.Value = (decimal)_loadedMonster.IQ;

			// Attack
			nudMonsterAttackPowerMin.Value = (decimal)_loadedMonster.AttackPower.Min;
			nudMonsterAttackPowerMax.Value = (decimal)_loadedMonster.AttackPower.Max;
			nudMonsterAttackSpeed.Value = (decimal)_loadedMonster.AttackSpeed;
			nudMonsterAttackRange.Value = (decimal)_loadedMonster.AttackRange;
			nudMonsterAttackRate.Value = (decimal)_loadedMonster.AttackRate;
			nudMonsterSkillPowerMin.Value = (decimal)_loadedMonster.SkillDamage.Min;
			nudMonsterSkillPowerMax.Value = (decimal)_loadedMonster.SkillDamage.Max;
			nudMonsterSkillDistance.Value = (decimal)_loadedMonster.SkillDistance;
			nudMonsterSkillRange.Value = (decimal)_loadedMonster.SkillRange;
			nudMonsterSkillRate.Value = (decimal)_loadedMonster.SkillRate;
			nudMonsterSkillCurse.Value = (decimal)_loadedMonster.SkillCurse;
			nudMonsterStunRate.Value = (decimal)_loadedMonster.StunRate;
			nudMonsterSpecialAttack.Value = (decimal)_loadedMonster.SpecialAttackRate;

			// Defense
			nudMonsterDefense.Value = (decimal)_loadedMonster.Defense;
			nudMonsterAbsortion.Value = (decimal)_loadedMonster.Absorption;
			nudMonsterBlockRate.Value = (decimal)_loadedMonster.BlockRate;
			nudMonsterLife.Value = (decimal)_loadedMonster.Life;

			nudMonsterElementalOrganic.Value = _loadedMonster.Resistance.Organic;
			nudMonsterElementalFire.Value = _loadedMonster.Resistance.Fire;
			nudMonsterElementalIce.Value = _loadedMonster.Resistance.Ice;
			nudMonsterElementalLightning.Value = _loadedMonster.Resistance.Lightning;
			nudMonsterElementalPoison.Value = _loadedMonster.Resistance.Poison;
			nudMonsterElementalMagic.Value = _loadedMonster.Resistance.Magic;

			// Movement
			nudMonsterMoveSpeed.Value = (decimal)_loadedMonster.MovementSpeed;
			nudMonsterMoveRange.Value = (decimal)_loadedMonster.MovementRange;

			// Potion
			nudMonsterPotionCount.Value = (decimal)_loadedMonster.PotionCount;
			nudMonsterPotionRate.Value = (decimal)_loadedMonster.PotionRate;

			// Sound Effects
			cbbMonsterSoundCode.SelectedItem = _loadedMonster.SoundCode;

			// Events
			nudMonsterEventCode.Value = (decimal)_loadedMonster.EventCode;
			nudMonsterEventInfo.Value = (decimal)_loadedMonster.EventInfo;
			txtMonsterEventItem.Text = _loadedMonster.EventItem;

			// Loots
			nudMonsterExperience.Value = (decimal)_loadedMonster.Experience;
			ckbMonsterAllSeeLoot.Checked = _loadedMonster.AllSeeLoot;
			nudMonsterFallMax.Value = (decimal)_loadedMonster.FallItemMax;

			PopulateLootData(dgvMonsterLoots, _loadedMonster.FallItems);
			PopulateLootData(dgvMonsterLootPlus, _loadedMonster.FallItemsPlus);

			nudMonsterFallMax.Value = (decimal)_loadedMonster.FallItemPerMax;

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
			// Identifiers
			_loadedMonster.Name = txtMonsterName.Text;
			_loadedMonster.ServerName = txtMonsterServerName.Text;
			_loadedMonster.DistinctionCode = (int)nudMonsterDistinctionCode.Value;

			// Appearance (3D Model Data)
			_loadedMonster.ModelFile = txtMonsterModelFile.Text;
			_loadedMonster.ModelSize = (float)nudMonsterModelSize.Value;
			_loadedMonster.ShadowSize = (ShadowSize)cbbMonsterShadowSize.SelectedItem;
			_loadedMonster.ModelEvent = txtMonsterModelEvent.Text;
			_loadedMonster.ArrowPosition = new Point((int)nudMonsterArrowPosMin.Value, (int)nudMonsterArrowPosMax.Value);

			// States
			_loadedMonster.Level = (int)nudMonsterLevel.Value;
			_loadedMonster.MonsterClass = (MonsterClass)cbbMonsterClass.SelectedItem;
			_loadedMonster.IsBoss = ckbMonsterIsBoss.Checked;
			_loadedMonster.Type = (MonsterType)cbbMonsterType.SelectedItem;
			_loadedMonster.ActiveTime = (ActiveTime)cbbMonsterActiveTime.SelectedItem;
			_loadedMonster.Nature = (MonsterNature)cbbMonsterNature.SelectedItem;
			_loadedMonster.GenerateGroup = new Framework.Range((int)nudMonsterGroupMin.Value, (int)nudMonsterGroupMax.Value);
			_loadedMonster.RealSight = (int)nudMonsterSight.Value;
			_loadedMonster.IQ = (int)nudMonsterIQ.Value;

			// Attack
			_loadedMonster.AttackPower = new Framework.Range((int)nudMonsterAttackPowerMin.Value, (int)nudMonsterAttackPowerMax.Value);
			_loadedMonster.AttackSpeed = (float)nudMonsterAttackSpeed.Value;
			_loadedMonster.AttackRange = (int)nudMonsterAttackRange.Value;
			_loadedMonster.AttackRate = (int)nudMonsterAttackRate.Value;
			_loadedMonster.SkillDamage = new Framework.Range((int)nudMonsterSkillPowerMin.Value, (int)nudMonsterSkillPowerMax.Value);
			_loadedMonster.SkillDistance = (int)nudMonsterSkillDistance.Value;
			_loadedMonster.SkillRange = (int)nudMonsterSkillRange.Value;
			_loadedMonster.SkillRate = (int)nudMonsterSkillRate.Value;
			_loadedMonster.SkillCurse = (int)nudMonsterSkillCurse.Value;
			_loadedMonster.StunRate = (int)nudMonsterStunRate.Value;
			_loadedMonster.SpecialAttackRate = (int)nudMonsterSpecialAttack.Value;

			// Defense
			_loadedMonster.Defense = (int)nudMonsterDefense.Value;
			_loadedMonster.Absorption = (float)nudMonsterAbsortion.Value;
			_loadedMonster.BlockRate = (float)nudMonsterBlockRate.Value;
			_loadedMonster.Life = (int)nudMonsterLife.Value;

			_loadedMonster.Resistance = new Elemental()
			{
				Organic = (int)nudMonsterElementalOrganic.Value,
				Fire = (int)nudMonsterElementalFire.Value,
				Ice = (int)nudMonsterElementalIce.Value,
				Lightning = (int)nudMonsterElementalLightning.Value,
				Poison = (int)nudMonsterElementalPoison.Value,
			};

			// Movement
			_loadedMonster.MovementSpeed = (float)nudMonsterMoveSpeed.Value;
			_loadedMonster.MovementRange = (float)nudMonsterMoveRange.Value;

			// Potion
			_loadedMonster.PotionCount = (int)nudMonsterPotionCount.Value;
			_loadedMonster.PotionRate = (int)nudMonsterPotionRate.Value;

			// Sound Effects
			_loadedMonster.SoundCode = (string)cbbMonsterSoundCode.SelectedItem;

			// Events
			_loadedMonster.EventCode = (int)nudMonsterEventCode.Value;
			_loadedMonster.EventInfo = (int)nudMonsterEventInfo.Value;
			_loadedMonster.EventItem = txtMonsterEventItem.Text;

			// Loots
			_loadedMonster.Experience = (int)nudMonsterExperience.Value;
			_loadedMonster.AllSeeLoot = ckbMonsterAllSeeLoot.Checked;
			_loadedMonster.FallItemMax = (int)nudMonsterFallMax.Value;

			RetrieveLootData(dgvMonsterLoots, _loadedMonster.FallItems);
			RetrieveLootData(dgvMonsterLootPlus, _loadedMonster.FallItemsPlus);

			_loadedMonster.ExternalFile = txtMonsterExternalFile.Text;
		}
		catch (Exception ex)
		{
			LogError(ex);
		}
	}

	private static void PopulateLootData(DataGridView dataGridView, List<Loot> loots)
	{
		// Clear existing rows
		dataGridView.Rows.Clear();

		// Early return if no loots
		if (!loots.Any())
			return;

		// Constants for cell indices
		const int rateCellIndex = 0;
		const int typeCellIndex = 1;
		const int valueCellIndex = 2;

		// Loop through each loot and add it to the DataGridView
		foreach (var loot in loots)
		{
			var index = dataGridView.Rows.Add();
			var row = dataGridView.Rows[index];

			// Set the loot rate
			row.Cells[rateCellIndex].Value = loot.Rate.ToString();

			// Use a switch expression to handle loot types
			row.Cells[typeCellIndex].Value = loot switch
			{
				//{ Nothing: true } => Monster.Keywords.LootType[0],
				{ Money: { } } => Monster.Keywords.LootType[1],
				{ Coin: { } } => Monster.Keywords.LootType[2],
				{ Items: { } } => Monster.Keywords.LootType[3],
				_ => Monster.Keywords.LootType[0] // Fallback to "Nothing" type
			};

			// Set the loot value based on type
			row.Cells[valueCellIndex].Value = loot switch
			{
				//{ Nothing: true } => null,
				{ Money: { } } => $"{loot.Money.Value.Min} {loot.Money.Value.Max}",
				{ Coin: { } } => $"{loot.Coin.Value.Min} {loot.Coin.Value.Max}",
				{ Items: { } } => string.Join(" ", loot.Items),
				_ => null
			};
		}
	}

	private static void RetrieveLootData(DataGridView dataGridView, List<Loot> loots)
	{
		// Clear existing loots
		loots.Clear();

		// Early return if no rows exist
		if (dataGridView.Rows.Count <= 0)
			return;

		// Loop through each row in the DataGridView
		foreach (DataGridViewRow row in dataGridView.Rows)
		{
			// Skip the new (empty) row
			if (row.IsNewRow)
				break;

			// Get and parse the rate
			if (row.Cells[0].Value is not string rateStr ||
				!int.TryParse(rateStr, out int rate))
				continue;

			var loot = new Loot { Rate = rate };

			// Get the loot type
			if (row.Cells[1].Value is not string type)
				continue;

			// Get the loot value (column 2)
			var valueStr = row.Cells[2].Value as string;
			if (!string.IsNullOrEmpty(valueStr))
			{
				int position = 0;

				// Switch based on the type of loot
				switch (type.ToLowerInvariant())
				{
					case var t when string.Equals(t, Monster.Keywords.LootType[1], StringComparison.OrdinalIgnoreCase): // Money
						loot.Money = FileHelper.ParseRange(valueStr, ref position);
						break;

					case var t when string.Equals(t, Monster.Keywords.LootType[2], StringComparison.OrdinalIgnoreCase): // Coin
						loot.Coin = FileHelper.ParseRange(valueStr, ref position);
						break;

					case var t when string.Equals(t, Monster.Keywords.LootType[3], StringComparison.OrdinalIgnoreCase): // Items
						loot.Items = valueStr.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();
						break;
				}
			}

			// Add loot to the collection
			loots.Add(loot);
		}
	}

	#endregion
}
