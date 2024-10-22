using GameServer.Helpers;
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


	// Merchants

	public const int SELLITEM_MAX = 32;

	/// <summary>
	/// List of <see cref="Item"/>s being sold by this NPC of type weapon.
	/// </summary>
	public readonly List<string> SellAttackItem = new();

	/// <summary>
	/// List of <see cref="Item"/>s being sold by this NPC of type defense.
	/// </summary>
	public readonly List<string> SellDefenseItem = new();

	/// <summary>
	/// List of <see cref="Item"/>s being sold by this NPC.
	/// </summary>
	public readonly List<string> SellEtcItem = new();

	// SkillMaster

	/// <summary>
	/// Get or Set the NPC to be a skill master.
	/// </summary>
	public bool SkillMaster { get; set; }

	/// <summary>
	/// Get or Set skill master max job available to change.
	/// </summary>
	public int SkillChangeJob { get; set; }

	// Events
	// NPC Functions

	/// <summary>
	/// Get or Set NPC event code.
	/// </summary>
	public int EventNPC { get; set; }

	/// <summary>
	/// Get or Set NPC to be a warehouse master.
	/// </summary>
	public bool WarehouseMaster { get; set; }

	/// <summary>
	/// Get or Set NPC to be a item mix master.
	/// </summary>
	public bool ItemMix { get; set; }
	
	/// <summary>
	/// Get or Set NPC to be a force alchemy master.
	/// </summary>
	public bool ForceMaster { get; set; }

	/// <summary>
	/// Get or Set NPC to be a Smelting master.
	/// </summary>
	public bool Smelting { get; set; }

	/// <summary>
	/// Get or Set NPC to be a Manufacture master.
	/// </summary>
	public bool Manufacture { get; set; }

	/// <summary>
	/// Get or Set NPC to be a item aging master.
	/// </summary>
	public bool ItemAging { get; set; }
	
	/// <summary>
	/// Get or Set NPC to be a item mix reset master.
	/// </summary>
	public bool MixtureReset { get; set; }
	
	/// <summary>
	/// Get or Set NPC to be a SOD manager.
	/// </summary>
	public bool CollectMoney { get; set; }

	/// <summary>
	/// Get or Set NPC to be a EventGirl, to reset status/skill.
	/// </summary>
	public bool EventGirl { get; set; }
	
	/// <summary>
	/// Get or Set NPC to be a Clan manager.
	/// </summary>
	public bool ClanMaster { get; set; }
	
	/// <summary>
	/// Get or Set NPC to be a item distributor.
	/// </summary>
	public bool GiftExpress { get; set; }

	/// <summary>
	/// Get or Set NPC for Wing quest or events.
	/// </summary>
	public int WingQuest { get; set; }

	/// <summary>
	/// Get or Set NPC to do events like Puzzle.
	/// </summary>
	public int PuzzleQuest { get; set; }
	
	/// <summary>
	/// Get or Set NPC to do star point event.
	/// </summary>
	public int StarPoint { get; set; }

	/// <summary>
	/// Get or Set NPC to do Donation box event.
	/// </summary>
	public bool DonationBox { get; set; }

	/// <summary>
	/// Get or Set NPC to be a Teleport master.
	/// </summary>
	public int Teleport { get; set; }

	/// <summary>
	/// Get or Set NPC to be a BlessCastle master.
	/// </summary>
	public bool BlessCastle { get; set; }

	/// <summary>
	/// Get or Set NPC to do Pollings.
	/// </summary>
	public int Polling { get; set; }

	// Video

	/// <summary>
	/// Get or Set NPC title for video media.
	/// </summary>
	public string MediaPlayTitle { get; set; } = string.Empty;

	/// <summary>
	/// Get or Set NPC path for video media.
	/// </summary>
	public string MediaPlayPath { get; set; } = string.Empty;

	// Don't know what it is

	/// <summary>
	/// Get or Set NPC 
	/// </summary>
	public Range OpenCount { get; set; }

	// Quests

	/// <summary>
	/// Get or Set NPC Quest Code.
	/// </summary>
	public int QuestCode { get; set; }

	/// <summary>
	/// Get or Set NPC Quest Params.
	/// </summary>
	public int QuestParam { get; set; }

	/// <summary>
	/// Get or Set NPC Quest Class
	/// </summary>
	public int QuestClass { get; set; }

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
		// Identifiers
		Name = string.Empty;
		ServerName = string.Empty;

		// Appearance (3D Model Data)
		ModelFile = string.Empty;
		ModelSize = 1.000f;
		ShadowSize = ShadowSize.Unknown;
		ModelEvent = string.Empty;
		ArrowPosition = Point.Empty;

		// States
		State = false;
		Level = 0;

		// Merchants
		SellAttackItem.Clear();
		SellDefenseItem.Clear();
		SellEtcItem.Clear();

		SkillMaster = false;
		SkillChangeJob = 0;

		// Events
		// NPC Functions
		EventNPC = 0;
		WarehouseMaster = false;
		ItemMix = false;
		ForceMaster = false;
		Smelting = false;
		Manufacture = false;
		ItemAging = false;
		MixtureReset = false;
		CollectMoney = false;
		EventGirl = false;
		ClanMaster = false;
		GiftExpress = false;
		WingQuest = 0;
		PuzzleQuest = 0;
		StarPoint = 0;
		DonationBox = false;
		Teleport = 0;
		BlessCastle = false;
		Polling = 0;
		MediaPlayTitle = string.Empty;
		MediaPlayPath = string.Empty;

		OpenCount = Range.Empty;

		QuestCode = 0;
		QuestParam = 0;
		QuestClass = 0;

		// Chat
		_messageCount = 0;

		for (int i = 0; i < MESSAGE_MAX; i++)
		{
			Messages[i] = string.Empty;
		}

		// Zhoon File
		ExternalFile = string.Empty;
	}

	//protected override void ParseLine(string line)
	//{
	//	int position = 0;
	//	string keyword = FileHelper.GetWord(line, ref position);

	//	if (string.IsNullOrEmpty(keyword))
	//		return;

	//	// Identifiers
	//	if (string.Compare(keyword, Monster.Keywords.ServerName) == 0)
	//	{
	//		ServerName = FileHelper.ParseString(line, ref position);
	//	}
	//	//else if ((string.Compare(keyword, "*B_NAME", true) == 0) ||
	//	//		 (string.Compare(keyword, "*E_NAME", true) == 0) ||
	//	//		 (string.Compare(keyword, "*J_NAME", true) == 0) ||
	//	//		 (string.Compare(keyword, "*K_NAME", true) == 0) ||
	//	//		 (string.Compare(keyword, "*NAME", true) == 0))
	//	else if (keyword.StartsWith('*') &&
	//			 keyword.ToUpper().Contains("NAME", StringComparison.InvariantCultureIgnoreCase))
	//	{
	//		var name = FileHelper.ParseString(line, ref position);
	//		if (!string.IsNullOrEmpty(name))
	//			Name = name;
	//	}

	//	// Appearance (3D Model Data)
	//	else if (string.Compare(keyword, Monster.Keywords.ModelFile) == 0)
	//	{
	//		var model = FileHelper.ParseString(line, ref position);
	//		if (!string.IsNullOrEmpty(model))
	//			ModelFile = model;
	//	}
	//	else if (string.Compare(keyword, Monster.Keywords.ModelSize) == 0)
	//	{
	//		ModelSize = FileHelper.ParseFloat(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Monster.Keywords.ShadowSize) == 0)
	//	{
	//		var str = FileHelper.GetWord(line, ref position);
	//		var size = Monster.Parse<ShadowSize>(str);

	//		ShadowSize = (ShadowSize)size;
	//	}
	//	else if (string.Compare(keyword, Monster.Keywords.ModelEvent) == 0)
	//	{
	//		var model = FileHelper.ParseString(line, ref position);
	//		if (!string.IsNullOrEmpty(model))
	//			ModelEvent = model;
	//	}
	//	else if (string.Compare(keyword, Monster.Keywords.ArrowPosition) == 0)
	//	{
	//		var x = FileHelper.ParseInteger(line, ref position);
	//		var y = FileHelper.ParseInteger(line, ref position);

	//		ArrowPosition = new Point(x, y);
	//	}

	//	// States
	//	else if (string.Compare(keyword, Monster.Keywords.State) == 0)
	//	{
	//		State = !FileHelper.ParseBool(line, ref position, "NPC");
	//	}
	//	else if (string.Compare(keyword, Monster.Keywords.Level) == 0)
	//	{
	//		Level = FileHelper.ParseInteger(line, ref position);
	//	}

	//	// Events
	//	else if (string.Compare(keyword, Monster.Keywords.EventCode) == 0)
	//	{
	//		EventCode = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Monster.Keywords.EventInfo) == 0)
	//	{
	//		EventInfo = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Monster.Keywords.EventItem) == 0)
	//	{
	//		string str = FileHelper.GetWord(line, ref position);

	//		if (!string.IsNullOrEmpty(str))
	//			EventItem = str;
	//	}

	//	// Merchants
	//	else if (string.Compare(keyword, Keywords.SellAttackItem) == 0)
	//	{
	//		if(SellAttackItem.Count < SELLITEM_MAX)
	//		{
	//			string item = FileHelper.GetWord(line, ref position);

	//			SellAttackItem.Add(item);
	//		}
	//	}
	//	else if (string.Compare(keyword, Keywords.SellDefenseItem) == 0)
	//	{
	//		if(SellDefenseItem.Count < SELLITEM_MAX)
	//		{
	//			string item = FileHelper.GetWord(line, ref position);

	//			SellDefenseItem.Add(item);
	//		}
	//	}
	//	else if (string.Compare(keyword, Keywords.SellEtcItem) == 0)
	//	{
	//		if(SellEtcItem.Count < SELLITEM_MAX)
	//		{
	//			string item = FileHelper.GetWord(line, ref position);

	//			SellEtcItem.Add(item);
	//		}
	//	}

	//	// SkillMaster
	//	else if (string.Compare(keyword, Keywords.SkillMaster) == 0)
	//	{
	//		SkillMaster = true;
	//	}
	//	else if (string.Compare(keyword, Keywords.SkillChangeJob) == 0)
	//	{
	//		SkillChangeJob = FileHelper.ParseInteger(line, ref position);
	//	}

	//	// Events/Functions
	//	else if (string.Compare(keyword, Keywords.EventNPC) == 0)
	//	{
	//		EventNPC = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.WarehouseMaster) == 0)
	//	{
	//		WarehouseMaster = true;
	//	}
	//	else if (string.Compare(keyword, Keywords.ItemMix) == 0)
	//	{
	//		ItemMix = true;
	//	}
	//	else if (string.Compare(keyword, Keywords.ForceMaster) == 0)
	//	{
	//		ForceMaster = true;
	//	}
	//	else if (string.Compare(keyword, Keywords.Smelting) == 0)
	//	{
	//		Smelting = true;
	//	}
	//	else if (string.Compare(keyword, Keywords.Manufacture) == 0)
	//	{
	//		Manufacture = true;
	//	}
	//	else if (string.Compare(keyword, Keywords.ItemAging) == 0)
	//	{
	//		ItemAging = true;
	//	}
	//	else if (string.Compare(keyword, Keywords.MixtureReset) == 0)
	//	{
	//		MixtureReset = true;
	//	}
	//	else if (string.Compare(keyword, Keywords.CollectMoney) == 0)
	//	{
	//		CollectMoney = true;
	//	}
	//	else if (string.Compare(keyword, Keywords.EventGirl) == 0)
	//	{
	//		EventGirl = true;
	//	}
	//	else if (string.Compare(keyword, Keywords.ClanMaster) == 0)
	//	{
	//		ClanMaster = true;
	//	}
	//	else if (string.Compare(keyword, Keywords.GiftExpress) == 0)
	//	{
	//		GiftExpress = true;
	//	}
	//	else if (string.Compare(keyword, Keywords.WingQuest) == 0)
	//	{
	//		WingQuest = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.PuzzleQuest) == 0)
	//	{
	//		PuzzleQuest = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.StarPoint) == 0)
	//	{
	//		StarPoint = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.DonationBox) == 0)
	//	{
	//		DonationBox = true;
	//	}
	//	else if (string.Compare(keyword, Keywords.Teleport) == 0)
	//	{
	//		Teleport = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.BlessCastle) == 0)
	//	{
	//		BlessCastle = true;
	//	}
	//	else if (string.Compare(keyword, Keywords.Polling) == 0)
	//	{
	//		Polling = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.MediaPlayTitle) == 0)
	//	{
	//		MediaPlayTitle = FileHelper.ParseString(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.MediaPlayPath) == 0)
	//	{
	//		MediaPlayPath = FileHelper.ParseString(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.OpenCount) == 0)
	//	{
	//		OpenCount = FileHelper.ParseRange(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.QuestCode) == 0)
	//	{
	//		QuestCode = FileHelper.ParseInteger(line, ref position);
	//		QuestParam = FileHelper.ParseInteger(line, ref position);
	//	}

	//	// Messages
	//	//else if ((string.Compare(keyword, "*B_CHAT", true) == 0) ||
	//	//		 (string.Compare(keyword, "*E_CHAT", true) == 0) ||
	//	//		 (string.Compare(keyword, "*J_CHAT", true) == 0) ||
	//	//		 (string.Compare(keyword, "*K_CHAT", true) == 0) ||
	//	//		 (string.Compare(keyword, "*CHAT", true) == 0) ||
	//	//		 (string.Compare(keyword, "*대화", true) == 0))
	//	else if (keyword.StartsWith('*') &&
	//			 keyword.ToUpper().Contains("CHAT", StringComparison.InvariantCultureIgnoreCase))
	//	{
	//		if (_messageCount < MESSAGE_MAX)
	//		{
	//			var message = FileHelper.ParseString(line, ref position);

	//			if (!string.IsNullOrEmpty(message))
	//			{
	//				Messages[_messageCount++] = message;
	//			}
	//		}
	//	}

	//	// Zhoon File
	//	else if (string.Compare(keyword, Monster.Keywords.ExternalFile) == 0)
	//	{
	//		ExternalFile = FileHelper.ParseString(line, ref position);

	//		var path = Path.Combine(Globals.NPCPath, ExternalFile);
	//		if (File.Exists(path))
	//			Process(path);
	//	}
	//}

	protected override void ParseLine(string line)
	{
		int position = 0;
		string keyword = FileHelper.GetWord(line, ref position);

		if (string.IsNullOrEmpty(keyword))
			return;

		// Helper functions for parsing data
		string ParseString() => FileHelper.ParseString(line, ref position);
		int ParseInteger() => FileHelper.ParseInteger(line, ref position);
		float ParseFloat() => FileHelper.ParseFloat(line, ref position);
		bool ParseBool(string condition) => FileHelper.ParseBool(line, ref position, condition);
		Range ParseRange() => FileHelper.ParseRange(line, ref position);

		switch (keyword)
		{
			// Identifiers
			case var text when text.StartsWith('*') && text.Contains("name", StringComparison.OrdinalIgnoreCase):
				Name = ParseString();
				break;

			case var text when text.Equals(Monster.Keywords.ServerName):
				ServerName = ParseString();
				break;

			// Appearance (3D Model Data)
			case var text when text.Equals(Monster.Keywords.ModelFile):
				ModelFile = ParseString();
				break;

			case var text when text.Equals(Monster.Keywords.ModelSize):
				ModelSize = ParseFloat();
				break;

			case var text when text.Equals(Monster.Keywords.ShadowSize):
				string shadowSize = FileHelper.GetWord(line, ref position);
				ShadowSize = (ShadowSize)Monster.Parse<ShadowSize>(shadowSize);
				break;

			case var text when text.Equals(Monster.Keywords.ModelEvent):
				ModelEvent = ParseString();
				break;

			case var text when text.Equals(Monster.Keywords.ArrowPosition):
				int x = FileHelper.ParseInteger(line, ref position);
				int y = FileHelper.ParseInteger(line, ref position);
				ArrowPosition = new Point(x, y);
				break;

			// States
			case var text when text.Equals(Monster.Keywords.State):
				State = !ParseBool("NPC");
				break;

			case var text when text.Equals(Monster.Keywords.Level):
				Level = ParseInteger();
				break;

			// Merchants
			case var text when text.Equals(Keywords.SellAttackItem):
				while (SellAttackItem.Count < SELLITEM_MAX)
				{
					var item = FileHelper.GetWord(line, ref position);
					if (string.IsNullOrEmpty(item))
						break;

					SellAttackItem.Add(item);
				}
				break;

			case var text when text.Equals(Keywords.SellDefenseItem):
				while (SellDefenseItem.Count < SELLITEM_MAX)
				{
					var item = FileHelper.GetWord(line, ref position);
					if (string.IsNullOrEmpty(item))
						break;

					SellDefenseItem.Add(item);
				}
				break;

			case var text when text.Equals(Keywords.SellEtcItem):
				while (SellEtcItem.Count < SELLITEM_MAX)
				{
					var item = FileHelper.GetWord(line, ref position);
					if (string.IsNullOrEmpty(item))
						break;

					SellEtcItem.Add(item);
				}
				break;

			// Skills
			case var text when text.Equals(Keywords.SkillMaster):
				SkillMaster = true;
				break;

			case var text when text.Equals(Keywords.SkillChangeJob):
				SkillChangeJob = ParseInteger();
				break;

			// Events/Functions
			case var text when text.Equals(Keywords.EventNPC):
				EventNPC = ParseInteger();
				break;

			case var text when text.Equals(Keywords.WarehouseMaster):
				WarehouseMaster = true;
				break;

			case var text when text.Equals(Keywords.ItemMix):
				ItemMix = true;
				break;

			case var text when text.Equals(Keywords.ForceMaster):
				ForceMaster = true;
				break;

			case var text when text.Equals(Keywords.Smelting):
				Smelting = true;
				break;

			case var text when text.Equals(Keywords.Manufacture):
				Manufacture = true;
				break;

			case var text when text.Equals(Keywords.ItemAging):
				ItemAging = true;
				break;

			case var text when text.Equals(Keywords.MixtureReset):
				MixtureReset = true;
				break;

			case var text when text.Equals(Keywords.CollectMoney):
				CollectMoney = true;
				break;

			case var text when text.Equals(Keywords.EventGirl):
				EventGirl = true;
				break;

			case var text when text.Equals(Keywords.ClanMaster):
				ClanMaster = true;
				break;

			case var text when text.Equals(Keywords.GiftExpress):
				GiftExpress = true;
				break;

			case var text when text.Equals(Keywords.WingQuest):
				WingQuest = ParseInteger();
				break;

			case var text when text.Equals(Keywords.PuzzleQuest):
				PuzzleQuest = ParseInteger(	);
				break;

			case var text when text.Equals(Keywords.StarPoint):
				StarPoint = ParseInteger();
				break;

			case var text when text.Equals(Keywords.DonationBox):
				DonationBox = true;
				break;

			case var text when text.Equals(Keywords.Teleport):
				Teleport = ParseInteger();
				break;

			case var text when text.Equals(Keywords.BlessCastle):
				BlessCastle = true;
				break;

			case var text when text.Equals(Keywords.Polling):
				Polling = ParseInteger(	);
				break;

			// Media
			case var text when text.Equals(Keywords.MediaPlayTitle):
				MediaPlayTitle = ParseString();
				break;

			case var text when text.Equals(Keywords.MediaPlayPath):
				MediaPlayPath = ParseString();
				break;

			case var text when text.Equals(Keywords.OpenCount):
				OpenCount = ParseRange();
				break;

			case var text when text.Equals(Keywords.QuestCode):
				QuestCode = ParseInteger();
				QuestParam = ParseInteger();
				break;

			case var text when text.Equals(Monster.Keywords.MonsterClass):
				QuestClass = ParseInteger();
				break;

			// Messages
			case var text when (text.StartsWith('*') && text.Contains("chat", StringComparison.OrdinalIgnoreCase)) ||
								text.Equals(Keywords.Chat[0]):
				if (_messageCount < MESSAGE_MAX)
					Messages[_messageCount++] = ParseString();
				break;

			// Zhoon File
			case var text when text.Equals(Monster.Keywords.ExternalFile):
				ExternalFile = ParseString();
				var path = Path.Combine(Globals.NPCPath, ExternalFile);
				if (File.Exists(path))
					Process(path);
				break;

			default:
				// Handle unknown keywords or skip them
				if (keyword.StartsWith('*'))
				{
					if(!UnhandledKeywords.Contains(keyword))
						UnhandledKeywords.Add(keyword);
				}
				break;
		}
	}

	public override void Save(string? fileName = null)
	{
		// write file .inf
		{
			StringBuilder sb = new();

			sb.AppendLine("// Priston Tale - NPC file");
			sb.AppendLine("// Identifiers");
			sb.AppendLine($"{Monster.Keywords.ServerName}\t\t\"{ServerName}\"");
			sb.AppendLine($"{Monster.Keywords.Name[0]}\t\t\"{Name}\"");
			sb.AppendLine();


			sb.AppendLine("// Appearance (3D Model Data)");
			sb.AppendLine($"{Monster.Keywords.ModelFile}\t\t\"{ModelFile}\"");

			if (ModelSize != 1.000f)
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


			sb.AppendLine("// Merchants");
			if (SellEtcItem.Any())
				sb.AppendLine($"{Keywords.SellEtcItem}\t\t{string.Join(' ', SellEtcItem)}");
			else
			{
				if (SellAttackItem.Any())
					sb.AppendLine($"{Keywords.SellAttackItem}\t\t{string.Join(' ', SellAttackItem)}");
				if (SellDefenseItem.Any())
					sb.AppendLine($"{Keywords.SellDefenseItem}\t\t{string.Join(' ', SellDefenseItem)}");
			}
			sb.AppendLine();

			if (SkillMaster)
			{
				sb.AppendLine("// SkillMaster");
				sb.AppendLine(Keywords.SkillMaster);
				sb.AppendLine($"{Keywords.SkillChangeJob}\t\t{SkillChangeJob}");
				sb.AppendLine();
			}

			sb.AppendLine("// NPC Functions");
			if (EventNPC != 0)
				sb.AppendLine($"{Keywords.EventNPC}\t\t{EventNPC}");
		
			if (WarehouseMaster)
				sb.AppendLine(Keywords.WarehouseMaster);
		
			if (ForceMaster)
				sb.AppendLine(Keywords.ForceMaster);
		
			if (Smelting)
				sb.AppendLine(Keywords.Smelting);
		
			if (Manufacture)
				sb.AppendLine(Keywords.Manufacture);

			if (ItemAging)
				sb.AppendLine(Keywords.ItemAging);
		
			if (MixtureReset)
				sb.AppendLine(Keywords.MixtureReset);
		
			if (CollectMoney)
				sb.AppendLine(Keywords.CollectMoney);
		
			if (EventGirl)
				sb.AppendLine(Keywords.EventGirl);
		
			if (ClanMaster)
				sb.AppendLine(Keywords.ClanMaster);
		
			if (GiftExpress)
				sb.AppendLine(Keywords.GiftExpress);

			if (WingQuest != 0)
				sb.AppendLine($"{Keywords.WingQuest}\t\t{WingQuest}");

			if (PuzzleQuest != 0)
				sb.AppendLine($"{Keywords.PuzzleQuest}\t\t{PuzzleQuest}");
		
			if (StarPoint != 0)
				sb.AppendLine($"{Keywords.StarPoint}\t\t{StarPoint}");

			if (DonationBox)
				sb.AppendLine(Keywords.DonationBox);

			if (Teleport != 0)
				sb.AppendLine($"{Keywords.Teleport}\t\t{Teleport}");

			if (BlessCastle)
				sb.AppendLine(Keywords.BlessCastle);
		
			if (Polling != 0)
				sb.AppendLine($"{Keywords.Polling}\t\t{Polling}");
		
			if (!string.IsNullOrEmpty(MediaPlayTitle))
				sb.AppendLine($"{Keywords.MediaPlayTitle}\t\t{MediaPlayTitle}");
		
			if (!string.IsNullOrEmpty(MediaPlayPath))
				sb.AppendLine($"{Keywords.MediaPlayPath}\t\t{MediaPlayPath}");

			if(OpenCount != Range.Empty)
				sb.AppendLine($"{Keywords.OpenCount}\t\t{OpenCount.Min} {OpenCount.Max}");

			if (QuestCode != 0)
				sb.AppendLine($"{Keywords.QuestCode}\t\t{QuestCode} {QuestParam}");

			if (QuestClass != 0)
				sb.AppendLine($"{Monster.Keywords.MonsterClass}\t\t{QuestClass}");

			sb.AppendLine();

			sb.AppendLine("// Zhoon File");
			sb.AppendLine($"{Monster.Keywords.ExternalFile}\t\t\"{ExternalFile}\"");
			sb.AppendLine();

			using var sw = new StreamWriter(fileName ?? FileName, false, Encoding);
			sw.Write(sb);
			sw.Flush();
		}

		// write file .zhoon
		{
			StringBuilder sb = new();

			sb.AppendLine("// Priston Tale - NPC file zhoon");
			var fi = new FileInfo(fileName ?? FileName);
			sb.AppendLine($"// {fi.Name}");
			sb.AppendLine();

			var langName = FileHelper.GetInfoName(Globals.Settings.Language);
			sb.AppendLine($"{langName}\t\t\"{Name}\"");
			sb.AppendLine();

			var messageLanguage = FileHelper.GetInfoChat(Globals.Settings.Language);

			foreach (var message in Messages)
			{
				if (!string.IsNullOrEmpty(message))
					sb.AppendLine($"{messageLanguage}\t\t\"{message}\"");
			}

			var path = Path.Combine(Globals.NPCPath, ExternalFile);
			using var sw = new StreamWriter(path);
			sw.Write(sb);
			sw.Flush();
		}
	}

	#endregion
}
