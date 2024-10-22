using GameServer.Framework;
using GameServer.Helpers;

namespace GameServer.Forms
{
	public partial class SettingsForm : Form
	{
		public SettingsForm()
		{
			InitializeComponent();
		}

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			cbbUseLanguage.DataSource = Enum.GetValues(typeof(Language));

			txtServerPath.Text = Globals.Settings.ServerPath;
			txtClientPath.Text = Globals.Settings.ClientPath;
			txtNotepadPath.Text = Globals.Settings.NotepadPath;
			ckbKeepComments.Checked = Globals.Settings.KeepComments;
			cbbUseLanguage.SelectedItem = Globals.Settings.Language;
		}

		private void btnBrowseServer_Click(object sender, EventArgs e)
		{
			txtServerPath.Text = GetFolderPath();
		}

		private void btnBrowseClient_Click(object sender, EventArgs e)
		{
			txtClientPath.Text = GetFolderPath();
		}

		private void txtServerPath_TextChanged(object sender, EventArgs e) => btnSave_SetState();
		private void txtClientPath_TextChanged(object sender, EventArgs e) => btnSave_SetState();
		private void txtNotepadPath_TextChanged(object sender, EventArgs e) => btnSave_SetState();

		private void btnSave_SetState()
		{
			bool state = true;

			state &= Directory.Exists(txtClientPath.Text);
			state &= Directory.Exists(txtServerPath.Text);
			state &= Directory.Exists(txtNotepadPath.Text) || File.Exists(txtNotepadPath.Text);

			btnSave.Enabled = state;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			Globals.Settings.ClientPath = txtClientPath.Text;
			Globals.Settings.ServerPath = txtServerPath.Text;
			Globals.Settings.NotepadPath = txtNotepadPath.Text;
			Globals.Settings.Language = (Language)cbbUseLanguage.SelectedItem;
			Globals.Settings.KeepComments = ckbKeepComments.Checked;

			Serializer<Settings>.SaveXml("Settings.xml", Globals.Settings);

			Close();
		}

		private void btnBrowseNotepad_Click(object sender, EventArgs e)
		{
			try
			{
				var ofd = new OpenFileDialog
				{
					InitialDirectory = Environment.SystemDirectory,
				};

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					txtNotepadPath.Text = ofd.FileName;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static string GetFolderPath()
		{
			try
			{
				var fbd = new FolderBrowserDialog
				{
					InitialDirectory = Environment.CurrentDirectory,
				};

				if (fbd.ShowDialog() == DialogResult.OK)
				{
					return fbd.SelectedPath;
				}
			}
			catch (Exception)
			{
				throw;
			}

			return string.Empty;
		}
	}
}
