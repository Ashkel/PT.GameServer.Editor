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

	private void btnNPCBrowseZhoon_Click(object sender, EventArgs e)
	{
		var fileName = BrowseZhoonFile(Globals.NPCPath);

		if (!string.IsNullOrEmpty(fileName))
			txtNPCExternalFile.Text = fileName;
	}

	private void btnNPCChatNew_Click(object sender, EventArgs e)
	{
		if (lbNPCChat.Items.Count >= NPC.MESSAGE_MAX)
        {
			MessageBox.Show("Can't add more Chat texts!");

			return;
		}

        var txt = new TextAreaForm();
		txt.Load += (s, ev) => {
			txt.Title = "Add Text to Chat";
		};

		txt.Save += (s, ev) =>
		{
			lbNPCChat.Items.Add(txt.GetText());
		};

		txt.ShowDialog();
	}

	private void btnNPCChatDelete_Click(object sender, EventArgs e)
	{
		if (lbNPCChat.SelectedItem == null)
			return;

		lbNPCChat.Items.Remove(lbNPCChat.SelectedItem);
	}

	private void btnNPCChatCopy_Click(object sender, EventArgs e)
	{
		if (lbNPCChat.Items.Count >= NPC.MESSAGE_MAX)
		{
			MessageBox.Show("Can't add more Chat texts!");

			return;
		}

		if (lbNPCChat.SelectedItem is not string text)
			return;

		var txt = new TextAreaForm();
		txt.Load += (s, ev) => {
			txt.Title = "Copy Text to Chat";
			txt.SetText(text);
		};

		txt.Save += (s, ev) =>
		{
			lbNPCChat.Items.Add(txt.GetText());
		};

		txt.ShowDialog();
	}

	private void btnNPCChatEdit_Click(object sender, EventArgs e)
	{
		if (lbNPCChat.SelectedItem is not string text)
			return;

		var txt = new TextAreaForm();
		txt.Load += (s, ev) => {
			txt.Title = "Edit Text to Chat";
			txt.SetText(text);
		};

		txt.Save += (s, ev) =>
		{
			lbNPCChat.Items[lbNPCChat.SelectedIndex] = txt.GetText();
			//lbNPCChat.Items.Remove(lbNPCChat.SelectedItem);
			//lbNPCChat.Items.Add(txt.GetText());
		};

		txt.ShowDialog();
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

			txtNPCModelName.Text = _loadedNPC.ModelFile;


			lbNPCChat.Items.Clear();

            foreach (var message in _loadedNPC.Messages)
            {
				if (string.IsNullOrEmpty(message))
					continue;

				lbNPCChat.Items.Add(message);
            }

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

			_loadedNPC.ModelFile = txtNPCModelName.Text;


            if (lbNPCChat.Items.Count > 0)
            {
				int count = 0;

                foreach (var item in lbNPCChat.Items)
                {
					if (count >= NPC.MESSAGE_MAX)
						break;

					string? message = item.ToString();
					if (!string.IsNullOrEmpty(message))
					{
						_loadedNPC.Messages[count++] = message;
					}
				}
            }
            


            _loadedNPC.ExternalFile = txtNPCExternalFile.Text;
		}
		catch (Exception ex)
		{
			LogError(ex);
		}
	}

	#endregion
}
