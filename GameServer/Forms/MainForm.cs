using GameServer.Framework;
using GameServer.Helpers;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace GameServer.Forms;

public partial class MainForm : Form
{
	#region Field/Properties

	private string[]? _currentTabFiles = null;

	#endregion


	#region Constructor

	public MainForm()
	{
		Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

		_loadedItem = new();
		_loadedMonster = new();
		_loadedNPC = new();
		//_backupItemHash = _loadedItem.GetHashCode();

		InitializeComponent();
	}

	#endregion


	#region Menu events

	private void MainForm_Load(object sender, EventArgs e)
	{
		//using var sw = new StreamWriter(Environment.CurrentDirectory + "\\ItemTabDesign.itxt");
		//ListControls(tabPage1, sw);

		cbbItemMainJob.DataSource = Enum.GetValues(typeof(JobType));
		cbbItemMainJob.SelectedItem = JobType.Unknown;

		if (!EnsureWorkingDirectory(Globals.GameServerPath))
			return;

		LoadItemFiles();
	}

	private void miFileLoad_Click(object sender, EventArgs e)
	{
		LoadItemFiles();
		LoadMonsterFiles();
		LoadNPCFiles();
		LoadFieldFiles();
	}

	private void miEditSettings_Click(object sender, EventArgs e)
	{
		SettingsShowDialog();
	}

	private void tabControl1_Selected(object sender, TabControlEventArgs e)
	{
		switch (e.TabPageIndex)
		{
			case 0:
				LoadItemFiles();
				break;

			case 1:
				LoadMonsterFiles();
				break;

			case 2:
				LoadNPCFiles();
				break;

			case 3:
				LoadFieldFiles();
				break;

			default:
				break;
		}
	}

	#endregion


	#region Helper methods

	private void LogError(Exception ex)
	{
		var fileName = $"{this.GetType().Name}.log";
		using var sw = new StreamWriter(fileName, true);
		sw.Write($"{DateTime.Now}: {ex.Format()}");

		Process.Start(Globals.NotepadPath, Path.Combine(Application.StartupPath, fileName));
	}

	private void SettingsShowDialog()
	{
		var settings = new SettingsForm();

		if (settings.ShowDialog() == DialogResult.OK)
		{
			// Reload files with correct path
			TabControlEventArgs ev = new(null, tabControl1.TabIndex, TabControlAction.Selected);
			tabControl1_Selected(this, ev);
		}
	}

	private bool EnsureWorkingDirectory(string path)
	{
		if (!Directory.Exists(path))
		{
			MessageBox.Show("Cannot find GameServer path!");

			SettingsShowDialog();

			return false;
		}

		return true;
	}

	#endregion


	#region File helper methods

	private bool SetCurrentTabFiles(string path, string pattern)
	{
		var files = Globals.GetFiles(path, pattern);

		_currentTabFiles = new string[files.Length];

		if (_currentTabFiles != null && _currentTabFiles.Length > 0)
		{
			for (int i = 0; i < files.Length; i++)
			{
				var fi = new FileInfo(files[i]);
				_currentTabFiles[i] = fi.Name;
			}

			return true;
		}

		return false;
	}

	private void CreateFile(ListBox listBox, string initialPath, Action onFinished)
	{
		try
		{
			var sfd = new SaveFileDialog()
			{
				CheckFileExists = false,
				InitialDirectory = initialPath,
			};

			if (listBox.SelectedItem is string fileName)
				sfd.FileName = fileName;

			if (sfd.ShowDialog() == DialogResult.OK)
			{
				using var s = File.Create(sfd.FileName);

				onFinished?.Invoke();
			}
		}
		catch (Exception ex)
		{
			LogError(ex);
		}
	}

	private void DeleteFile(ListBox listBox, string initialPath, Action onFinished)
	{
		try
		{
			if (listBox.SelectedItem is not string fileName)
				return;

			var filePath = Path.Combine(initialPath, fileName);

			DialogResult result = MessageBox.Show(
				"Deleting: " + filePath + "\r\nAre you sure?",
				"Delete File",
				MessageBoxButtons.OKCancel);

			if (result == DialogResult.OK)
			{
				File.Delete(filePath);

				onFinished?.Invoke();
			}
		}
		catch (Exception ex)
		{
			LogError(ex);
		}
	}

	private void CopyFile(ListBox listBox, string initialPath, Action onFinished)
	{
		try
		{
			if (listBox.SelectedItem is not string fileName)
				return;

			var sfd = new SaveFileDialog()
			{
				CheckFileExists = false,
				InitialDirectory = initialPath,
				FileName = fileName,
			};

			if (sfd.ShowDialog() == DialogResult.OK)
			{
				File.Copy(Path.Combine(initialPath, fileName), sfd.FileName, true);

				onFinished?.Invoke();
			}
		}
		catch (Exception ex)
		{
			LogError(ex);
		}
	}

	private string BrowseZhoonFile(string path)
	{
		try
		{
			var ofd = new OpenFileDialog()
			{
				InitialDirectory = Path.Combine(path, "Name"),
				Filter = "zhoon files (*.zhoon)|*.zhoon"
			};

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				var fi = new FileInfo(ofd.FileName);

				return @"Name\" + fi.Name;
			}
		}
		catch (Exception ex)
		{
			LogError(ex);
		}

		return string.Empty;
	}

	private void ListControls(Control control, StreamWriter stream)
	{
		if (stream == null)
			return;

		//var controls = control.Controls.Cast<Control>().ToList();
		//controls.Sort((x, y) =>
		//{
		//	return x.TabIndex > y.TabIndex ? 1 : -1;
		//});

		foreach (Control c in control.Controls)
		{
			if (c is not Label)
			{
				stream.WriteLine(c.Name);

				if (c.Controls.Count > 0)
					ListControls(c, stream);
			}
		}
	}

	#endregion
}
