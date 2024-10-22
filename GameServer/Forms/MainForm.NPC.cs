using GameServer.Forms.Controls;
using GameServer.Framework;
using GameServer.Framework.Characters;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace GameServer.Forms;

public partial class MainForm
{
	#region Field/Properties

	private NPC _loadedNPC;

	#endregion


	#region NPC tab events

	private void lbNPCFiles_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (lbNPCFiles.SelectedItem is not string fileName)
			return;

		_loadedNPC.Reset();
		_loadedNPC.SetFile(Path.Combine(Globals.NPCPath, fileName));
		_loadedNPC.Process();

		SetNPCData();
	}

	private void btnNPCFilesReload_Click(object sender, EventArgs e)
	{
		LoadNPCFiles();
	}

	private void btnNPCFilesNew_Click(object sender, EventArgs e)
	{
		CreateFile(lbNPCFiles, Globals.NPCPath, LoadNPCFiles);
	}

	private void btnNPCFilesDelete_Click(object sender, EventArgs e)
	{
		DeleteFile(lbNPCFiles, Globals.NPCPath, LoadNPCFiles);
	}

	private void btnNPCFilesCopy_Click(object sender, EventArgs e)
	{
		CopyFile(lbNPCFiles, Globals.NPCPath, LoadNPCFiles);
	}

	private void btnNPCFilesSave_Click(object sender, EventArgs e)
	{
		GetNPCData();

		DialogResult result = MessageBox.Show("Are you sure to save data?", "Save", MessageBoxButtons.YesNo);

		if (result == DialogResult.Yes)
		{
			_loadedNPC.Save();
		}
	}

	private void btnNPCFilesOpenRaw_Click(object sender, EventArgs e)
	{
		if (lbNPCFiles.SelectedItem is not string fileName)
			return;

		var path = Path.Combine(Globals.NPCPath, fileName);
		Process.Start(Globals.NotepadPath, $"\"{path}\"");
	}

	private void txtNPCFilesSearch_TextChanged(object sender, EventArgs e)
	{
		ListBoxSearch(lbNPCFiles, txtNPCFilesSearch.Text);
	}

	private void btnNPCBrowseZhoon_Click(object sender, EventArgs e)
	{
		var fileName = BrowseZhoonFile(Globals.NPCPath);

		if (!string.IsNullOrEmpty(fileName))
			txtNPCExternalFile.Text = fileName;
	}


	private void btnNPCChatNew_Click(object sender, EventArgs e)
	{
		AddListEntry(lbNPCChat, NPC.MESSAGE_MAX);
	}

	private void btnNPCChatDelete_Click(object sender, EventArgs e)
	{
		DeleteListEntry(lbNPCChat);
	}

	private void btnNPCChatCopy_Click(object sender, EventArgs e)
	{
		CopyListEntry(lbNPCChat, NPC.MESSAGE_MAX);
	}

	private void btnNPCChatEdit_Click(object sender, EventArgs e)
	{
		EditListEntry(lbNPCChat);
	}


	private void btnNPCAttackNew_Click(object sender, EventArgs e)
	{
		AddListEntry(lbNPCSellAttack, NPC.SELLITEM_MAX);
	}

	private void btnNPCAttackDelete_Click(object sender, EventArgs e)
	{
		DeleteListEntry(lbNPCSellAttack);
	}

	private void btnNPCAttackCopy_Click(object sender, EventArgs e)
	{
		CopyListEntry(lbNPCSellAttack, NPC.MESSAGE_MAX);
	}

	private void btnNPCAttackEdit_Click(object sender, EventArgs e)
	{
		EditListEntry(lbNPCSellAttack);
	}


	private void btnNPCDefenseNew_Click(object sender, EventArgs e)
	{
		AddListEntry(lbNPCSellDefense, NPC.SELLITEM_MAX);
	}

	private void btnNPCDefenseDelete_Click(object sender, EventArgs e)
	{
		DeleteListEntry(lbNPCSellDefense);
	}

	private void btnNPCDefenseCopy_Click(object sender, EventArgs e)
	{
		CopyListEntry(lbNPCSellDefense, NPC.SELLITEM_MAX);
	}

	private void btnNPCDefenseEdit_Click(object sender, EventArgs e)
	{
		EditListEntry(lbNPCSellDefense);
	}

	private void btnNPCEtcNew_Click(object sender, EventArgs e)
	{
		AddListEntry(lbNPCSellEtc, NPC.SELLITEM_MAX);
	}

	private void btnNPCEtcDelete_Click(object sender, EventArgs e)
	{
		DeleteListEntry(lbNPCSellEtc);
	}

	private void btnNPCEtcCopy_Click(object sender, EventArgs e)
	{
		CopyListEntry(lbNPCSellEtc, NPC.SELLITEM_MAX);
	}

	private void btnNPCEtcEdit_Click(object sender, EventArgs e)
	{
		EditListEntry(lbNPCSellEtc);
	}


	private void FunctionNPC_ValueChanged(object? sender, EventArgs e)
	{
		if (sender is not NumericUpDown nudSender)
			return;

		if (nudSender.Value == 0)
			return;

		foreach (var control in gbNPCFuntions.Controls)
		{
			if (control is NumericUpDown nud && !nud.Name.Equals(nudSender.Name))
			{
				nud.Value = 0;
			}
			else if (control is RadioButton rb)
			{
				rb.Checked = false;
			}
		}
	}

	private void FunctionNPC_CheckedChanged(object? sender, EventArgs e)
	{
		if (sender is not RadioButton rb)
			return;

		if (rb.Checked == false)
			return;

		nudNPCTeleport.Value = 0;
		nudNPCPolling.Value = 0;
	}

	private void EventNPC_ValueChanged(object? sender, EventArgs e)
	{
		if (sender is not NumericUpDown nudSender)
			return;

		if (nudSender.Value == 0)
			return;

		foreach (var control in gbNPCEvents.Controls)
		{
			if (control is NumericUpDown nud && !nud.Name.Equals(nudSender.Name))
			{
				nud.Value = 0;
			}
		}
	}

	#endregion


	#region Helper methods

	private void LoadNPCFiles()
	{
		gbNPCInformation.Text = "Information";

		if (SetCurrentTabFiles(Globals.NPCPath, "*.npc"))
		{
			lbNPCFiles.Items.Clear();
			lbNPCFiles.Items.AddRange(_currentTabFiles!);
		}
	}

	private void SetNPCData()
	{
		try
		{
			gbNPCInformation.Text = "Information of: " + _loadedNPC.Name;

			txtNPCName.Text = _loadedNPC.Name;
			txtNPCServerName.Text = _loadedNPC.ServerName;

			txtNPCModelFile.Text = _loadedNPC.ModelFile;
			nudNPCModelSize.Value = (decimal)_loadedNPC.ModelSize;
			cbbNPCShadowSize.SelectedItem = _loadedNPC.ShadowSize;
			txtNPCModelEvent.Text = _loadedNPC.ModelEvent;
			nudNPCArrowPosMin.Value = _loadedNPC.ArrowPosition.X;
			nudNPCArrowPosMax.Value = _loadedNPC.ArrowPosition.Y;

			nudNPCLevel.Value = _loadedNPC.Level;

			PopulateList(lbNPCChat, _loadedNPC.Messages);
			PopulateList(lbNPCSellAttack, _loadedNPC.SellAttackItem.ToArray());
			PopulateList(lbNPCSellDefense, _loadedNPC.SellDefenseItem.ToArray());
			PopulateList(lbNPCSellEtc, _loadedNPC.SellEtcItem.ToArray());

			ckbNPCSkillMaster.Checked = _loadedNPC.SkillMaster;
			nudNPCChangeJob.Value = _loadedNPC.SkillChangeJob;

			nudNPCEvent.Value = _loadedNPC.EventNPC;
			nudNPCWingQuest.Value = _loadedNPC.WingQuest;
			nudNPCPuzzleQuest.Value = _loadedNPC.PuzzleQuest;
			nudNPCStarPoint.Value = _loadedNPC.StarPoint;

			rbNPCWarehouseMaster.Checked = _loadedNPC.WarehouseMaster;
			rbNPCItemMixMaster.Checked = _loadedNPC.ItemMix;
			rbNPCForceMaster.Checked = _loadedNPC.ForceMaster;
			rbNPCSmeltingMaster.Checked = _loadedNPC.Smelting;
			rbNPCManufactureMaster.Checked = _loadedNPC.Manufacture;
			rbNPCItemAgingMaster.Checked = _loadedNPC.ItemAging;
			rbNPCMixtureResetMaster.Checked = _loadedNPC.MixtureReset;
			rbNPCSODManager.Checked = _loadedNPC.CollectMoney;
			rbNPCEventGirl.Checked = _loadedNPC.EventGirl;
			rbNPCClanMaster.Checked = _loadedNPC.ClanMaster;
			rbNPCItemDistributor.Checked = _loadedNPC.GiftExpress;
			rbNPCDonationBox.Checked = _loadedNPC.DonationBox;
			rbNPCBlessCastleMaster.Checked = _loadedNPC.BlessCastle;

			nudNPCTeleport.Value = _loadedNPC.Teleport;
			nudNPCPolling.Value = _loadedNPC.Polling;

			txtNPCMediaPlayTitle.Text = _loadedNPC.MediaPlayTitle;
			txtNPCMediaPlayPath.Text = _loadedNPC.MediaPlayPath;

			nudNPCOpenCountMin.Value = _loadedNPC.OpenCount.Min;
			nudNPCOpenCountMax.Value = _loadedNPC.OpenCount.Max;

			nudNPCQuestCode.Value = _loadedNPC.QuestCode;
			nudNPCQuestParam.Value = _loadedNPC.QuestParam;
			nudNPCQuestClass.Value = _loadedNPC.QuestClass;

			txtNPCExternalFile.Text = _loadedNPC.ExternalFile;
		}
		catch (Exception ex)
		{
			LogError(ex);
		}
	}

	private void GetNPCData()
	{
		try
		{
			_loadedNPC.Name = txtNPCName.Text;
			_loadedNPC.ServerName = txtNPCServerName.Text;

			_loadedNPC.ModelFile = txtNPCModelFile.Text;
			_loadedNPC.ModelSize = (float)nudNPCModelSize.Value;
			_loadedNPC.ShadowSize = (ShadowSize)cbbNPCShadowSize.SelectedItem;
			_loadedNPC.ModelEvent = txtNPCModelEvent.Text;
			_loadedNPC.ArrowPosition = new Point((int)nudNPCArrowPosMin.Value, (int)nudNPCArrowPosMax.Value);
			
			_loadedNPC.Level = (int)nudNPCLevel.Value;

			var messages = RetrieveListItems(lbNPCChat, NPC.MESSAGE_MAX);
			if (messages != null)
				_loadedNPC.Messages = messages;

			var attackItems = RetrieveListItems(lbNPCSellAttack, NPC.SELLITEM_MAX);
			if (attackItems != null)
			{
				_loadedNPC.SellAttackItem.Clear();
				_loadedNPC.SellAttackItem.AddRange(attackItems);
			}

			var defenseItems = RetrieveListItems(lbNPCSellDefense, NPC.SELLITEM_MAX);
			if (defenseItems != null)
			{
				_loadedNPC.SellDefenseItem.Clear();
				_loadedNPC.SellDefenseItem.AddRange(defenseItems);
			}

			var etcItems = RetrieveListItems(lbNPCSellEtc, NPC.SELLITEM_MAX);
			if (etcItems != null)
			{
				_loadedNPC.SellEtcItem.Clear();
				_loadedNPC.SellEtcItem.AddRange(etcItems);
			}

			_loadedNPC.SkillMaster = ckbNPCSkillMaster.Checked;
			_loadedNPC.SkillChangeJob = (int)nudNPCChangeJob.Value;

			_loadedNPC.EventNPC = (int)nudNPCEvent.Value;
			_loadedNPC.WingQuest = (int)nudNPCWingQuest.Value;
			_loadedNPC.PuzzleQuest = (int)nudNPCPuzzleQuest.Value;
			_loadedNPC.StarPoint = (int)nudNPCStarPoint.Value;

			_loadedNPC.WarehouseMaster = rbNPCWarehouseMaster.Checked;
			_loadedNPC.ItemMix = rbNPCItemMixMaster.Checked;
			_loadedNPC.ForceMaster = rbNPCForceMaster.Checked;
			_loadedNPC.Smelting = rbNPCSmeltingMaster.Checked;
			_loadedNPC.Manufacture = rbNPCManufactureMaster.Checked;
			_loadedNPC.ItemAging = rbNPCItemAgingMaster.Checked;
			_loadedNPC.MixtureReset = rbNPCMixtureResetMaster.Checked;
			_loadedNPC.CollectMoney = rbNPCSODManager.Checked;
			_loadedNPC.EventGirl = rbNPCEventGirl.Checked;
			_loadedNPC.ClanMaster = rbNPCClanMaster.Checked;
			_loadedNPC.GiftExpress = rbNPCItemDistributor.Checked;
			_loadedNPC.DonationBox = rbNPCDonationBox.Checked;
			_loadedNPC.BlessCastle = rbNPCBlessCastleMaster.Checked;

			_loadedNPC.Teleport = (int)nudNPCTeleport.Value;
			_loadedNPC.Polling = (int)nudNPCPolling.Value;

			_loadedNPC.OpenCount = new Framework.Range((int)nudNPCOpenCountMin.Value, (int)nudNPCOpenCountMax.Value);

			_loadedNPC.QuestCode = (int)nudNPCQuestCode.Value;
			_loadedNPC.QuestParam = (int)nudNPCQuestParam.Value;
			_loadedNPC.QuestClass = (int)nudNPCQuestClass.Value;

			_loadedNPC.ExternalFile = txtNPCExternalFile.Text;
		}
		catch (Exception ex)
		{
			LogError(ex);
		}
	}

	#endregion


	#region Control Helper methods

	private static void PopulateList(ListBox listBox, string[] source)
	{
		listBox.Items.Clear();

		foreach (string item in source)
        {
			if (!string.IsNullOrEmpty(item))
				listBox.Items.Add(item);
        }
    }

	private static string[]? RetrieveListItems(ListBox listBox, int maxCount)
	{
		if (listBox.Items.Count <= 0)
			return null;

		string[] items = new string[listBox.Items.Count];

		int count = 0;

		foreach (var item in listBox.Items)
        {
			if (count >= maxCount)
				break;

			string? str = item.ToString();

			if (!string.IsNullOrEmpty(str))
				items[count++] = str;
		}

		return items;
	}

	private static bool CheckEntryCount(ListBox listBox, int maxCount)
	{
		if (listBox.Items.Count >= maxCount)
		{
			MessageBox.Show("Can't Add more entries!");

			return false;
		}

		return true;
	}

	private static void AddListEntry(ListBox listBox, int maxCount)
	{
		if (!CheckEntryCount(listBox, maxCount))
			return;

		var txt = new TextAreaForm();
		txt.Load += (s, ev) => {
			txt.Title = "Add Entry";
		};

		txt.Save += (s, ev) =>
		{
			listBox.Items.Add(txt.GetText());
		};

		txt.ShowDialog();
	}

	private static void DeleteListEntry(ListBox listBox)
	{
		if (listBox.SelectedItem == null)
			return;

		listBox.Items.Remove(listBox.SelectedItem);
	}

	private static void CopyListEntry(ListBox listBox, int maxCount)
	{
		if (!CheckEntryCount(listBox, maxCount))
			return;

		if (listBox.SelectedItem is not string text)
			return;

		var txt = new TextAreaForm();
		txt.Load += (s, ev) => {
			txt.Title = "Copy Entry";
			txt.SetText(text);
		};

		txt.Save += (s, ev) =>
		{
			listBox.Items.Add(txt.GetText());
		};

		txt.ShowDialog();
	}

	private static void EditListEntry(ListBox listBox)
	{
		if (listBox.SelectedItem is not string text)
			return;

		var txt = new TextAreaForm();
		txt.Load += (s, ev) => {
			txt.Title = "Edit Entry";
			txt.SetText(text);
		};

		txt.Save += (s, ev) =>
		{
			listBox.Items[listBox.SelectedIndex] = txt.GetText();
		};

		txt.ShowDialog();
	}

	#endregion
}
