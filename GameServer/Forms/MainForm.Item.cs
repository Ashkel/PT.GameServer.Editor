using GameServer.Framework;
using GameServer.Framework.Items;
using System.Diagnostics;

namespace GameServer.Forms;

public partial class MainForm
{
	#region Field/Properties

	private readonly Item _loadedItem;
	//private int _backupItemHash;

	#endregion


	#region Item tab events

	private void lbItemFiles_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (lbItemFiles.SelectedItem is not string fileName)
			return;

		//GetItemData();

		//if (_loadedItem.GetHashCode() != _backupItemHash)
		//{
		//	DialogResult result = MessageBox.Show("There are unsaved modifications!\n\rWant to save?", "Save File", MessageBoxButtons.YesNoCancel);

		//	switch (result)
		//	{
		//		case DialogResult.Cancel:
		//			return;

		//		case DialogResult.Yes:
		//			btnItemFilesSave_Click(sender, e);
		//			break;

		//		default:
		//			break;
		//	}
		//}

		_loadedItem.Reset();
		_loadedItem.SetFile(Path.Combine(Globals.OpenItemPath, fileName));
		_loadedItem.Process();

		//_backupItemHash = _loadedItem.GetHashCode();

		SetItemData();
	}

	private void btnItemFilesReload_Click(object sender, EventArgs e)
	{
		LoadItemFiles();
	}

	private void btnItemFilesNew_Click(object sender, EventArgs e)
	{
		CreateFile(lbItemFiles, Globals.OpenItemPath, LoadItemFiles);
	}

	private void btnItemFilesDelete_Click(object sender, EventArgs e)
	{
		DeleteFile(lbItemFiles, Globals.OpenItemPath, LoadItemFiles);
	}

	private void btnItemFilesCopy_Click(object sender, EventArgs e)
	{
		CopyFile(lbItemFiles, Globals.OpenItemPath, LoadItemFiles);
	}

	private void btnItemFilesSave_Click(object sender, EventArgs e)
	{
		GetItemData();

		DialogResult result = MessageBox.Show("Are you sure to save data?", "Save", MessageBoxButtons.YesNo);

		if (result == DialogResult.Yes)
		{
			_loadedItem.Save();
		}
	}

	private void btnItemFilesOpenRaw_Click(object sender, EventArgs e)
	{
		if (lbItemFiles.SelectedItem is not string fileName)
			return;

		var path = Path.Combine(Globals.OpenItemPath, fileName);
		Process.Start(Globals.NotepadPath, $"\"{path}\"");
	}

	private void txtItemFilesSearch_TextChanged(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(txtItemFilesSearch.Text))
			return;

		var file = lbItemFiles.Items.OfType<string>()
			.Where(x => x.StartsWith(txtItemFilesSearch.Text, StringComparison.InvariantCultureIgnoreCase) ||
						x.Contains(txtItemFilesSearch.Text, StringComparison.InvariantCultureIgnoreCase))
			.FirstOrDefault();

		if(!string.IsNullOrEmpty(file))
		{
			var index = lbItemFiles.Items.IndexOf(file);
			lbItemFiles.SelectedIndex = index;
		}
	}

	private void btnItemBrowseZhoon_Click(object sender, EventArgs e)
	{
		var fileName = BrowseZhoonFile(Globals.OpenItemPath);

		if (!string.IsNullOrEmpty(fileName))
			txtItemExternalFile.Text = fileName;
	}

	private void nudItemEffectBlinkMin_ValueChanged(object sender, EventArgs e) => SetEffectBlinkMax();
	private void cpItemEffectColor_BackColorChanged(object sender, EventArgs e) => SetEffectBlinkMax();

	#endregion


	#region Event methods

	#endregion


	#region Helper methods

	private void LoadItemFiles()
	{
		gbItemInformation.Text = "Information";

		if (SetCurrentTabFiles(Globals.OpenItemPath, "*.txt"))
		{
			lbItemFiles.Items.Clear();
			lbItemFiles.Items.AddRange(_currentTabFiles!);
		}
	}

	private void SetEffectBlinkMax()
	{
		Color color = cpItemEffectColor.Color;
		int effectBlinkMax = 0;
		effectBlinkMax += color.R;
		effectBlinkMax += color.G;
		effectBlinkMax += color.B;
		effectBlinkMax += color.A;
		effectBlinkMax += (int)nudItemEffectBlinkMin.Value;

		nudItemEffectBlinkMax.Value = effectBlinkMax;
	}

	private void SetItemData()
	{
		try
		{
			gbItemInformation.Text = "Information of: " + _loadedItem.Name;

			txtItemId.Text = _loadedItem.Id;
			txtItemName.Text = _loadedItem.Name;
			txtItemServerName.Text = _loadedItem.ServerName;

			nudItemDurabilityMin.Value = _loadedItem.Durability.Min;
			nudItemDurabilityMax.Value = _loadedItem.Durability.Max;
			nudItemWeight.Value = _loadedItem.Weight;
			nudItemPrice.Value = _loadedItem.Price;

			nudItemUnique.Value = _loadedItem.Unique;
			nudItemScaleBlink.Value = (decimal)_loadedItem.ScaleBlink;
			nudItemEffectBlinkMin.Value = _loadedItem.EffectBlink.Min;
			nudItemEffectBlinkMax.Value = _loadedItem.EffectBlink.Max;
			cpItemEffectColor.BackColor = _loadedItem.EffectColor;
			nudItemDispEffect.Value = _loadedItem.DispEffect;

			nudItemAttackPowerMinMin.Value = _loadedItem.AttackPowerMin.Min;
			nudItemAttackPowerMinMax.Value = _loadedItem.AttackPowerMin.Max;
			nudItemAttackPowerMaxMin.Value = _loadedItem.AttackPowerMax.Min;
			nudItemAttackPowerMaxMax.Value = _loadedItem.AttackPowerMax.Max;
			nudItemAttackRange.Value = _loadedItem.AttackRange;
			nudItemAttackSpeed.Value = _loadedItem.AttackSpeed;
			nudItemAttackRateMin.Value = _loadedItem.AttackRate.Min;
			nudItemAttackRateMax.Value = _loadedItem.AttackRate.Max;
			nudItemCriticalRate.Value = _loadedItem.CriticalRate;

			nudItemDefenseMin.Value = _loadedItem.Defense.Min;
			nudItemDefenseMax.Value = _loadedItem.Defense.Max;
			nudItemAbsortionMin.Value = (decimal)_loadedItem.Absortion.Min;
			nudItemAbsortionMax.Value = (decimal)_loadedItem.Absortion.Max;
			nudItemBlockRateMin.Value = (decimal)_loadedItem.BlockRate.Min;
			nudItemBlockRateMax.Value = (decimal)_loadedItem.BlockRate.Max;

			nudItemElementalOrganic.Value = _loadedItem.Resistance.Organic;
			nudItemElementalFire.Value = _loadedItem.Resistance.Fire;
			nudItemElementalIce.Value = _loadedItem.Resistance.Ice;
			nudItemElementalLightning.Value = _loadedItem.Resistance.Lightning;
			nudItemElementalPoison.Value = _loadedItem.Resistance.Poison;

			nudItemMoveSpeedMin.Value = (decimal)_loadedItem.MoveSpeed.Min;
			nudItemMoveSpeedMax.Value = (decimal)_loadedItem.MoveSpeed.Max;

			nudItemPotionSpace.Value = _loadedItem.PotionSpace;
			nudItemPotionHpMin.Value = (decimal)_loadedItem.PotionRecovery.Hp.Min;
			nudItemPotionHpMax.Value = (decimal)_loadedItem.PotionRecovery.Hp.Max;
			nudItemPotionMpMin.Value = (decimal)_loadedItem.PotionRecovery.Mp.Min;
			nudItemPotionMpMax.Value = (decimal)_loadedItem.PotionRecovery.Mp.Max;
			nudItemPotionSpMin.Value = (decimal)_loadedItem.PotionRecovery.Sp.Min;
			nudItemPotionSpMax.Value = (decimal)_loadedItem.PotionRecovery.Sp.Max;

			nudItemMagicMasteryMin.Value = _loadedItem.MagicMastery.Min;
			nudItemMagicMasteryMax.Value = _loadedItem.MagicMastery.Max;

			nudItemAddHpMin.Value = (decimal)_loadedItem.Additional.Hp.Min;
			nudItemAddHpMax.Value = (decimal)_loadedItem.Additional.Hp.Max;
			nudItemAddMpMin.Value = (decimal)_loadedItem.Additional.Mp.Min;
			nudItemAddMpMax.Value = (decimal)_loadedItem.Additional.Mp.Max;
			nudItemAddSpMin.Value = (decimal)_loadedItem.Additional.Sp.Min;
			nudItemAddSpMax.Value = (decimal)_loadedItem.Additional.Sp.Max;

			nudItemRegenHpMin.Value = (decimal)_loadedItem.Regeneration.Hp.Min;
			nudItemRegenHpMax.Value = (decimal)_loadedItem.Regeneration.Hp.Max;
			nudItemRegenMpMin.Value = (decimal)_loadedItem.Regeneration.Mp.Min;
			nudItemRegenMpMax.Value = (decimal)_loadedItem.Regeneration.Mp.Max;
			nudItemRegenSpMin.Value = (decimal)_loadedItem.Regeneration.Sp.Min;
			nudItemRegenSpMax.Value = (decimal)_loadedItem.Regeneration.Sp.Max;

			nudItemLevel.Value = _loadedItem.Requeriments.Level;
			nudItemStrength.Value = _loadedItem.Requeriments.Strength;
			nudItemIntelligence.Value = _loadedItem.Requeriments.Intelligence;
			nudItemTalent.Value = _loadedItem.Requeriments.Talent;
			nudItemDexterity.Value = _loadedItem.Requeriments.Dexterity;
			nudItemHealth.Value = _loadedItem.Requeriments.Health;

			cbbItemMainJob.SelectedItem = _loadedItem.MainJob;

			CkbItemJob_CheckItems(_loadedItem.AvailableJobs);

			nudItemSpecAttackMin.Value = _loadedItem.Specialization.AttackPower.Min;
			nudItemSpecAttackMax.Value = _loadedItem.Specialization.AttackPower.Max;
			nudItemSpecAttackRange.Value = _loadedItem.Specialization.AttackRange;
			nudItemSpecAttackSpeed.Value = _loadedItem.Specialization.AttackSpeed;
			nudItemSpecAttackRateMin.Value = _loadedItem.Specialization.AttackRate.Min;
			nudItemSpecAttackRateMax.Value = _loadedItem.Specialization.AttackRate.Max;
			nudItemSpecCriticalRate.Value = _loadedItem.Specialization.CriticalRate;

			nudItemSpecDefenseMin.Value = _loadedItem.Specialization.Defense.Min;
			nudItemSpecDefenseMax.Value = _loadedItem.Specialization.Defense.Max;
			nudItemSpecAbsortionMin.Value = (decimal)_loadedItem.Specialization.Absortion.Min;
			nudItemSpecAbsortionMax.Value = (decimal)_loadedItem.Specialization.Absortion.Max;
			nudItemSpecBlockRate.Value = (decimal)_loadedItem.Specialization.BlockRate;
			nudItemSpecElemOrganic.Value = _loadedItem.Specialization.Resistance.Organic;
			nudItemSpecElemFire.Value = _loadedItem.Specialization.Resistance.Fire;
			nudItemSpecElemIce.Value = _loadedItem.Specialization.Resistance.Ice;
			nudItemSpecElemLightning.Value = _loadedItem.Specialization.Resistance.Lightning;
			nudItemSpecElemPoison.Value = _loadedItem.Specialization.Resistance.Poison;
			nudItemSpecMoveSpeedMin.Value = (decimal)_loadedItem.Specialization.MoveSpeed.Min;
			nudItemSpecMoveSpeedMax.Value = (decimal)_loadedItem.Specialization.MoveSpeed.Max;
			nudItemSpecMasteryMin.Value = (decimal)_loadedItem.Specialization.MagicMastery.Min;
			nudItemSpecMasteryMax.Value = (decimal)_loadedItem.Specialization.MagicMastery.Max;

			nudItemSpecAddHpMin.Value = (decimal)_loadedItem.Specialization.Additional.Hp.Min;
			nudItemSpecAddHpMax.Value = (decimal)_loadedItem.Specialization.Additional.Hp.Max;
			nudItemSpecAddMpMin.Value = (decimal)_loadedItem.Specialization.Additional.Mp.Min;
			nudItemSpecAddMpMax.Value = (decimal)_loadedItem.Specialization.Additional.Mp.Max;
			nudItemSpecRegenHpMin.Value = (decimal)_loadedItem.Specialization.Regeneration.Hp.Min;
			nudItemSpecRegenHpMax.Value = (decimal)_loadedItem.Specialization.Regeneration.Hp.Max;
			nudItemSpecRegenMpMin.Value = (decimal)_loadedItem.Specialization.Regeneration.Mp.Min;
			nudItemSpecRegenMpMax.Value = (decimal)_loadedItem.Specialization.Regeneration.Mp.Max;

			txtItemExternalFile.Text = _loadedItem.ExternalFile;
		}
		catch (Exception ex)
		{
			LogError(ex);
		}
	}

	private void GetItemData()
	{
		try
		{
			_loadedItem.Id = txtItemId.Text;
			_loadedItem.Name = txtItemName.Text;
			_loadedItem.ServerName = txtItemServerName.Text;

			_loadedItem.Durability = new Framework.Range((int)nudItemDurabilityMin.Value, (int)nudItemDurabilityMax.Value);
			_loadedItem.Weight = (int)nudItemWeight.Value;
			_loadedItem.Price = (int)nudItemPrice.Value;

			_loadedItem.Unique = (long)nudItemUnique.Value;
			_loadedItem.ScaleBlink = (float)nudItemScaleBlink.Value;
			_loadedItem.EffectBlink = new Framework.Range((int)nudItemEffectBlinkMin.Value, (int)nudItemEffectBlinkMax.Value);
			_loadedItem.EffectColor = cpItemEffectColor.Color;
			_loadedItem.DispEffect = (long)nudItemDispEffect.Value;

			_loadedItem.AttackPowerMin = new Framework.Range((int)nudItemAttackPowerMinMin.Value, (int)nudItemAttackPowerMinMax.Value);
			_loadedItem.AttackPowerMax = new Framework.Range((int)nudItemAttackPowerMaxMin.Value, (int)nudItemAttackPowerMaxMax.Value);
			_loadedItem.AttackRange = (int)nudItemAttackRange.Value;
			_loadedItem.AttackSpeed = (int)nudItemAttackSpeed.Value;
			_loadedItem.AttackRate = new Framework.Range((int)nudItemAttackRateMin.Value, (int)nudItemAttackRateMax.Value);
			_loadedItem.CriticalRate = (int)nudItemCriticalRate.Value;

			_loadedItem.Defense = new Framework.Range((int)nudItemDefenseMin.Value, (int)nudItemDefenseMax.Value);
			_loadedItem.Absortion = new Framework.RangeF((float)nudItemAbsortionMin.Value, (float)nudItemAbsortionMax.Value);
			_loadedItem.BlockRate = new Framework.RangeF((float)nudItemBlockRateMin.Value, (float)nudItemBlockRateMax.Value);
			_loadedItem.Resistance = new Elemental()
			{
				Organic = (int)nudItemElementalOrganic.Value,
				Fire = (int)nudItemElementalFire.Value,
				Ice = (int)nudItemElementalIce.Value,
				Lightning = (int)nudItemElementalLightning.Value,
				Poison = (int)nudItemElementalPoison.Value,
			};

			_loadedItem.MoveSpeed = new Framework.RangeF((float)nudItemMoveSpeedMin.Value, (float)nudItemMoveSpeedMax.Value);

			_loadedItem.PotionSpace = (int)nudItemPotionSpace.Value;
			_loadedItem.PotionRecovery = new Status()
			{
				Hp = new Framework.RangeF((float)nudItemPotionHpMin.Value, (float)nudItemPotionHpMax.Value),
				Mp = new Framework.RangeF((float)nudItemPotionMpMin.Value, (float)nudItemPotionMpMax.Value),
				Sp = new Framework.RangeF((float)nudItemPotionSpMin.Value, (float)nudItemPotionSpMax.Value),
			};

			_loadedItem.MagicMastery = new Framework.Range((int)nudItemMagicMasteryMin.Value, (int)nudItemMagicMasteryMax.Value);

			_loadedItem.Additional = new Status()
			{
				Hp = new Framework.RangeF((float)nudItemAddHpMin.Value, (float)nudItemAddHpMax.Value),
				Mp = new Framework.RangeF((float)nudItemAddMpMin.Value, (float)nudItemAddMpMax.Value),
				Sp = new Framework.RangeF((float)nudItemAddSpMin.Value, (float)nudItemAddSpMax.Value),
			};

			_loadedItem.Regeneration = new Status()
			{
				Hp = new Framework.RangeF((float)nudItemRegenHpMin.Value, (float)nudItemRegenHpMax.Value),
				Mp = new Framework.RangeF((float)nudItemRegenMpMin.Value, (float)nudItemRegenMpMax.Value),
				Sp = new Framework.RangeF((float)nudItemRegenSpMin.Value, (float)nudItemRegenSpMax.Value),
			};

			_loadedItem.Requeriments = new Requeriments()
			{
				Level = (int)nudItemLevel.Value,
				Strength = (int)nudItemStrength.Value,
				Intelligence = (int)nudItemIntelligence.Value,
				Talent = (int)nudItemTalent.Value,
				Dexterity = (int)nudItemDexterity.Value,
				Health = (int)nudItemHealth.Value,
			};

			_loadedItem.MainJob = (JobType)cbbItemMainJob.SelectedItem;

			CkbItemJob_GetCheckedItems(_loadedItem.AvailableJobs);

			_loadedItem.Specialization = new Specialization()
			{
				AttackPower = new Framework.Range((int)nudItemSpecAttackMin.Value, (int)nudItemSpecAttackMax.Value),
				AttackRange = (int)nudItemSpecAttackRange.Value,
				AttackSpeed = (int)nudItemSpecAttackSpeed.Value,
				AttackRate = new Framework.Range((int)nudItemSpecAttackRateMin.Value, (int)nudItemSpecAttackRateMax.Value),
				CriticalRate = (int)nudItemSpecCriticalRate.Value,

				Defense = new Framework.Range((int)nudItemSpecDefenseMin.Value, (int)nudItemSpecDefenseMax.Value),
				Absortion = new Framework.RangeF((int)nudItemSpecAbsortionMin.Value, (int)nudItemSpecAbsortionMax.Value),
				BlockRate = (float)nudItemSpecBlockRate.Value,
				Resistance = new Elemental()
				{
					Organic = (int)nudItemSpecElemOrganic.Value,
					Fire = (int)nudItemSpecElemFire.Value,
					Ice = (int)nudItemSpecElemIce.Value,
					Lightning = (int)nudItemSpecElemLightning.Value,
					Poison = (int)nudItemSpecElemPoison.Value,
				},

				MoveSpeed = new Framework.RangeF((int)nudItemSpecMoveSpeedMin.Value, (int)nudItemSpecMoveSpeedMax.Value),

				MagicMastery = new Framework.Range((int)nudItemSpecMasteryMin.Value, (int)nudItemSpecMasteryMax.Value),

				Additional = new Status()
				{
					Hp = new Framework.RangeF((float)nudItemSpecAddHpMin.Value, (float)nudItemSpecAddHpMax.Value),
					Mp = new Framework.RangeF((float)nudItemSpecAddMpMin.Value, (float)nudItemSpecAddMpMax.Value),
				},

				Regeneration = new Status()
				{
					Hp = new Framework.RangeF((float)nudItemSpecRegenHpMin.Value, (float)nudItemSpecRegenHpMax.Value),
					Mp = new Framework.RangeF((float)nudItemSpecRegenMpMin.Value, (float)nudItemSpecRegenMpMax.Value),
				},
			};

			_loadedItem.ExternalFile = txtItemExternalFile.Text;
		}
		catch (Exception ex)
		{
			LogError(ex);
		}
	}

	private void CkbItemJob_CheckItems(List<JobType> availableJobs)
	{
		foreach (var checkbox in gbItemAvailableJobs.Controls.OfType<CheckBox>())
		{
			checkbox.Checked = false;

			foreach (var job in availableJobs)
			{
				if (checkbox.Name.ToLower().Contains(job.ToString().ToLower()))
				{
					checkbox.Checked = true;
				}
			}
		}
	}

	private void CkbItemJob_GetCheckedItems(List<JobType> availableJobs)
	{
		availableJobs.Clear();

		foreach (var checkbox in gbItemAvailableJobs.Controls.OfType<CheckBox>())
		{
			if (!checkbox.Checked)
				continue;

			JobType job = Job.Parse(checkbox.Name);
			if (job != JobType.Unknown)
			{
				availableJobs.Add(job);
			}
		}
	}

	#endregion
}
