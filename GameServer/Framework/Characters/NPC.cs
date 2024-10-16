using GameServer.Helpers;
using System;
using System.Text;

namespace GameServer.Framework.Characters;

public class NPC : GameInfo
{
	#region Field/Properties

	public const int MESSAGE_MAX = 20;
	private int _messageCount = 0;

	#endregion

	#region Monster Data

	// Identifiers
	public string Id { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public string ServerName { get; set; } = string.Empty;

	// Appearance (3D Model Data)
	public string ModelName { get; set; } = string.Empty;

	public ShadowSize ShadowSize { get; set; } = ShadowSize.Unknown;


	// State
	public bool IsNPC { get; set; }

    public string[] Messages { get; set; } = new string[MESSAGE_MAX];

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

		Id = string.Empty;
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

		// Identifiers
		if (string.Compare(keyword, "*코드") == 0)
		{
			Id = FileHelper.ParseString(line, ref position);
		}
		else if (string.Compare(keyword, "*이름") == 0)
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
		else if (string.Compare(keyword, "*모양파일") == 0)
		{
			var model = FileHelper.ParseString(line, ref position);
			if (!string.IsNullOrEmpty(model))
				ModelName = model;
		}
		else if (string.Compare(keyword, "*크기") == 0)
		{
			var str = FileHelper.GetWord(line, ref position);
			var size = Monster.Parse<ShadowSize>(str);

			ShadowSize = (ShadowSize)size;
		}

		// State
		else if (string.Compare(keyword, "*속성") == 0)
		{
			IsNPC = FileHelper.ParseBool(line, ref position, "NPC");
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
		else if (string.Compare(keyword, "*연결파일") == 0)
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



		using var sw = new StreamWriter(fileName ?? FileName, false, Encoding);
		sw.Write(sb);
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
