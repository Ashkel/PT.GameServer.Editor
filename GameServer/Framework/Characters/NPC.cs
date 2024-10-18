using GameServer.Helpers;
using System;
using System.Text;

namespace GameServer.Framework.Characters;

public partial class NPC : GameInfo
{
	#region Field/Properties

	public const int MESSAGE_MAX = 20;
	private int _messageCount = 0;

	#endregion

	#region Monster Data

	// Identifiers

	/// <summary>
	/// Get or Set the name being displayed for players
	/// </summary>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Get or Set the original korean name of the monster
	/// </summary>
	public string ServerName { get; set; } = string.Empty;

	// Appearance (3D Model Data)

	/// <summary>
	/// Get or Set the path for the .ini file responsible for calling
	/// the 3D model
	/// </summary>
	public string ModelFile { get; set; } = string.Empty;

	/// <summary>
	/// Get or Set size type, controls size of shadows.
	/// </summary>
	public ShadowSize ShadowSize { get; set; } = ShadowSize.Unknown;

	/// <summary>
	/// Get or Set the size multiplier of the model being used
	/// </summary>
	public float ModelSize { get; set; }

	/// <summary>
	/// Get or Set the path for the .ini file responsible for calling
	/// the 3D model, used only during events
	/// </summary>
	public string ModelEvent { get; set; } = string.Empty;

	/// <summary>
	/// Get or Set the Screen Calibration offset of a monster.
	/// </summary>
	public Point ArrowPosition { get; set; } = Point.Empty;

	// States

	/// <summary>
	/// Get or Set NPC "Property", kind of is this a enemy?
	/// </summary>
	/// <remarks>True for enemy, False for NPC</remarks>
	public bool State { get; set; }

	/// <summary>
	/// Get or Set the character level
	/// </summary>
	public int Level { get; set; }

	// Event

	/// <summary>
	/// Get or Set Event Code (generally item drop for quest)
	/// </summary>
	public int EventCode { get; set; }

	/// <summary>
	/// Get or Set Event Information (generally item drop for quest)
	/// </summary>
	public int EventInfo { get; set; }

	/// <summary>
	/// Get or Set Event Item (generally item drop for quest)
	/// </summary>
	public string EventItem { get; set; } = string.Empty;



	// Chat

	/// <summary>
	/// Get or Set messages for this NPC
	/// </summary>
	public string[] Messages { get; set; } = new string[MESSAGE_MAX];

	// Zhoon File

	/// <summary>
	/// Get or Set the external file holding the display name(Zhoon file)
	/// </summary>
	public string ExternalFile { get; set; } = string.Empty;

	#endregion


	#region Constructor(s)

	public NPC()
	{
		Reset();
	}

	#endregion


	#region Helper methods

	public override void Reset()
	{
		_messageCount = 0;

		Name = string.Empty;
		ServerName = string.Empty;

		for (int i = 0; i < MESSAGE_MAX; i++)
		{
			Messages[i] = string.Empty;
		}

		ExternalFile = string.Empty;
	}

	protected override void ParseLine(string line)
	{
		int position = 0;
		string keyword = FileHelper.GetWord(line, ref position);

		if (keyword == null)
			return;

		if (string.Compare(keyword, Monster.Keywords.ServerName) == 0)
		{
			ServerName = FileHelper.ParseString(line, ref position);
		}
		//else if ((string.Compare(keyword, "*B_NAME", true) == 0) ||
		//		 (string.Compare(keyword, "*E_NAME", true) == 0) ||
		//		 (string.Compare(keyword, "*J_NAME", true) == 0) ||
		//		 (string.Compare(keyword, "*K_NAME", true) == 0) ||
		//		 (string.Compare(keyword, "*NAME", true) == 0))
		else if (keyword.StartsWith('*') &&
				 keyword.ToUpper().Contains("NAME"))
		{
			var name = FileHelper.ParseString(line, ref position);
			if (!string.IsNullOrEmpty(name))
				Name = name;
		}

		// Appearance (3D Model Data)
		else if (string.Compare(keyword, Monster.Keywords.ModelFile) == 0)
		{
			var model = FileHelper.ParseString(line, ref position);
			if (!string.IsNullOrEmpty(model))
				ModelFile = model;
		}
		else if (string.Compare(keyword, Monster.Keywords.ModelSize) == 0)
		{
			ModelSize = FileHelper.ParseFloat(line, ref position);
		}
		else if (string.Compare(keyword, Monster.Keywords.ShadowSize) == 0)
		{
			var str = FileHelper.GetWord(line, ref position);
			var size = Monster.Parse<ShadowSize>(str);

			ShadowSize = (ShadowSize)size;
		}
		else if (string.Compare(keyword, Monster.Keywords.ModelEvent) == 0)
		{
			var model = FileHelper.ParseString(line, ref position);
			if (!string.IsNullOrEmpty(model))
				ModelEvent = model;
		}
		else if (string.Compare(keyword, Monster.Keywords.ArrowPosition) == 0)
		{
			var x = FileHelper.ParseInteger(line, ref position);
			var y = FileHelper.ParseInteger(line, ref position);

			ArrowPosition = new Point(x, y);
		}

		// States
		else if (string.Compare(keyword, Monster.Keywords.State) == 0)
		{
			State = !FileHelper.ParseBool(line, ref position, "NPC");
		}
		else if (string.Compare(keyword, Monster.Keywords.Level) == 0)
		{
			Level = FileHelper.ParseInteger(line, ref position);
		}


		// Events
		else if (string.Compare(keyword, Monster.Keywords.EventCode) == 0)
		{
			EventCode = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Monster.Keywords.EventInfo) == 0)
		{
			EventInfo = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Monster.Keywords.EventItem) == 0)
		{
			string str = FileHelper.GetWord(line, ref position);

			if (!string.IsNullOrEmpty(str))
				EventItem = str;
		}

		// Messages
		//else if ((string.Compare(keyword, "*B_CHAT", true) == 0) ||
		//		 (string.Compare(keyword, "*E_CHAT", true) == 0) ||
		//		 (string.Compare(keyword, "*J_CHAT", true) == 0) ||
		//		 (string.Compare(keyword, "*K_CHAT", true) == 0) ||
		//		 (string.Compare(keyword, "*CHAT", true) == 0) ||
		//		 (string.Compare(keyword, "*대화", true) == 0))
		else if (keyword.StartsWith('*') &&
				 keyword.ToUpper().Contains("CHAT"))
		{
			if (_messageCount < MESSAGE_MAX)
			{
				var message = FileHelper.ParseString(line, ref position);

				if (!string.IsNullOrEmpty(message))
				{
					Messages[_messageCount++] = message;
				}
			}
		}

		// Zhoon File
		else if (string.Compare(keyword, Monster.Keywords.ExternalFile) == 0)
		{
			ExternalFile = FileHelper.ParseString(line, ref position);

			var path = Path.Combine(Globals.NPCPath, ExternalFile);
			if (File.Exists(path))
				Process(path);
		}
	}

	public override void Save(string? fileName = null)
	{
		StringBuilder sb = new();

		sb.AppendLine("// Priston Tale - NPC file");
		sb.AppendLine("// Identifiers");
		sb.AppendLine($"{Monster.Keywords.ServerName}\t\t\"{ServerName}\"");
		sb.AppendLine($"{Monster.Keywords.Name[0]}\t\t\"{Name}\"");
		sb.AppendLine();


		sb.AppendLine("// Appearance (3D Model Data)");
		sb.AppendLine($"{Monster.Keywords.ModelFile}\t\t\"{ModelFile}\"");

		if (ModelSize > 0.000f)
			sb.AppendLine($"{Monster.Keywords.ModelSize}\t\t{ModelSize}");

		sb.AppendLine($"{Monster.Keywords.ShadowSize}\t\t{Monster.Parse(ShadowSize)}");

		if (!string.IsNullOrEmpty(ModelEvent))
			sb.AppendLine($"{Monster.Keywords.ModelEvent}\t\t\"{ModelEvent}\"");

		if (ArrowPosition != Point.Empty)
			sb.AppendLine($"{Monster.Keywords.ArrowPosition}\t\t{ArrowPosition.X} {ArrowPosition.Y}");

		sb.AppendLine();

		sb.AppendLine("// States");
		sb.AppendLine($"{Monster.Keywords.State}\t\t" + (State ? "적" : "NPC"));
		sb.AppendLine($"{Monster.Keywords.Level}\t\t{Level}");
		sb.AppendLine();


		sb.AppendLine("// Events");

		if (EventCode != 0)
			sb.AppendLine($"{Monster.Keywords.EventCode}\t\t{EventCode}");

		if (EventInfo != 0)
			sb.AppendLine($"{Monster.Keywords.EventInfo}\t\t{EventInfo}");

		if (!string.IsNullOrEmpty(EventItem))
		{
			sb.AppendLine($"{Monster.Keywords.EventItem}\t\t{EventItem}");
			sb.AppendLine();
		}


		sb.AppendLine("// Zhoon File");
		sb.AppendLine($"{Monster.Keywords.ExternalFile}\t\t\"{ExternalFile}\"");
		sb.AppendLine();

		using var sw = new StreamWriter(fileName ?? FileName, false, Encoding);
		sw.Write(sb);
		sw.Flush();


		var path = Path.Combine(Globals.NPCPath, ExternalFile);
		using var zhoon = new StreamWriter(path);

		zhoon.WriteLine("// Priston Tale - NPC file");

		var fi = new FileInfo(fileName ?? FileName);
		zhoon.WriteLine($"// {fi.Name}");
		zhoon.WriteLine();

		var langName = FileHelper.GetInfoName(Globals.Settings.Language);
		zhoon.WriteLine($"{langName}\t\t\"{Name}\"");
		zhoon.WriteLine();

		var messageLanguage = FileHelper.GetInfoChat(Globals.Settings.Language);

		foreach (var message in Messages)
		{
			if (!string.IsNullOrEmpty(message))
				zhoon.WriteLine($"{messageLanguage}\t\t\"{message}\"");
		}

		zhoon.Flush();
	}

	//protected override void EditLine(string line, ref StringBuilder sb)
	//{
	//	int position = 0;
	//	string keyword = FileHelper.GetWord(line, ref position);

	//	if (keyword == null)
	//		return;

	//	// Identifiers
	//	if (string.Compare(keyword, "*코드") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t\"{Id}\"");
	//	}
	//	else if (string.Compare(keyword, "*이름") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t\"{ServerName}\"");
	//	}
	//	//else if ((string.Compare(keyword, "*B_NAME", true) == 0) ||
	//	//		 (string.Compare(keyword, "*E_NAME", true) == 0) ||
	//	//		 (string.Compare(keyword, "*J_NAME", true) == 0) ||
	//	//		 (string.Compare(keyword, "*K_NAME", true) == 0) ||
	//	//		 (string.Compare(keyword, "*NAME", true) == 0))
	//	else if (keyword.StartsWith('*') &&
	//			 keyword.ToUpper().Contains("NAME"))
	//	{
	//		sb.AppendLine($"{keyword}\t\t\"{Name}\"");
	//	}

	//	// Appearance (3D Model Data)
	//	else if (string.Compare(keyword, "*모양파일") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t\"{ModelName}\"");
	//	}
	//	else if (string.Compare(keyword, "*크기") == 0)
	//	{
	//		var size = Monster.Parse(ShadowSize);
	//		if (size != null)
	//			sb.AppendLine($"{keyword}\t\t{size}");
	//	}



	//	// State
	//	else if (string.Compare(keyword, "*속성") == 0)
	//	{
	//		if (IsNPC)
	//			sb.AppendLine($"{keyword}\t\tNPC");
	//	}

	//	// Messages
	//	//else if ((string.Compare(keyword, "*B_CHAT", true) == 0) ||
	//	//		 (string.Compare(keyword, "*E_CHAT", true) == 0) ||
	//	//		 (string.Compare(keyword, "*J_CHAT", true) == 0) ||
	//	//		 (string.Compare(keyword, "*K_CHAT", true) == 0) ||
	//	//		 (string.Compare(keyword, "*CHAT", true) == 0) ||
	//	//		 (string.Compare(keyword, "*대화", true) == 0))
	//	else if (keyword.StartsWith('*') &&
	//			 keyword.ToUpper().Contains("CHAT"))
	//	{
	//		for (int i = 0; i < MESSAGE_MAX; i++)
	//		{
	//			if (!string.IsNullOrEmpty(Messages[i]))
	//			{
	//				sb.AppendLine($"{keyword}\t\t\"{Messages[i]}\"");

	//				Messages[i] = string.Empty;

	//				break;
	//			}
	//		}
	//	}

	//	// Zhoon File
	//	else if (string.Compare(keyword, "*연결파일") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t\" {ExternalFile} \"");

	//		var path = Path.Combine(Application.StartupPath, ExternalFile);
	//		if (File.Exists(path))
	//			Process(path, path);
	//	}
	//	else
	//		sb.AppendLine(line);
	//}

	#endregion
}
