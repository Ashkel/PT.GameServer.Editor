using GameServer.Helpers;
using System.Text;

namespace GameServer.Framework.Characters;

public partial class Monster : GameInfo
{
	#region Field/Properties

	/// <summary>
	/// Static list to fill with all SoundCodes available,
	/// use it as DataSource for a combobox in gui
	/// </summary>
	public static readonly List<string> SoundList = new();

	public const int FALLITEM_MAX = 20;
	public const int FALLITEMPLUS_MAX = 3;

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

	/// <summary>
	/// Get or Set the monster Distinction code.
	/// </summary>
	public int DistinctionCode { get; set; }


	// Appearance (3D Model Data)

	/// <summary>
	/// Get or Set the path for the .ini file responsible for calling
	/// the 3D model
	/// </summary>
	public string ModelFile { get; set; } = string.Empty;

	/// <summary>
	/// Get or Set the size multiplier of the model being used
	/// </summary>
	public float ModelSize { get; set; }

	/// <summary>
	/// Get or Set size type, controls size of shadows.
	/// </summary>
	public ShadowSize ShadowSize { get; set; } = ShadowSize.Unknown;

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
	/// Get or Set monster "Property", kind of is this a enemy?
	/// </summary>
	/// <remarks>True for enemy, False for NPC</remarks>
	public bool State { get; set; }

	/// <summary>
	/// Get or Set the character level
	/// </summary>
	public int Level { get; set; }

	/// <summary>
	/// Get or Set if the mob is a boss
	/// </summary>
	public bool IsBoss { get; set; }

	/// <summary>
	/// Get or Set the type class of a monster, divided in
	/// normal, boss, hammer(big head event) or ghost(big ghost event)
	/// </summary>
	public MonsterClass MonsterClass { get; set; } = MonsterClass.Normal;

	/// <summary>
	/// Get of Set type of a monster (Normal, Undead, Mutant, Demon, Mechanic).
	/// </summary>
	public MonsterType Type { get; set; } = MonsterType.Normal;

	/// <summary>
	/// Get or Set the active hours of a monster (All day, Day only or Night only).
	/// </summary>
	public ActiveTime ActiveTime { get; set; } = ActiveTime.All;

	/// <summary>
	/// Get or Set the default nature of a monster (Neutral, Good or Evil).
	/// </summary>
	public MonsterNature Nature { get; set; } = MonsterNature.Neutral;

	/// <summary>
	/// Get or Set the size of group generation.
	/// </summary>
	public Range GenerateGroup { get; set; }

	/// <summary>
	/// Get or Set the monster field of vision(Sight).
	/// </summary>
	public int RealSight { get; set; }

	/// <summary>
	/// Get or Set the monster IQ.
	/// </summary>
	public int IQ { get; set; }

	/// <summary>
	/// Get or Set if the monster is of undead type.
	/// </summary>
	public bool IsUndead { get; set; }


	// Attack

	/// <summary>
	/// Get or Set the attack power
	/// </summary>
	public Range AttackPower { get; set; }

	/// <summary>
	/// Get or Set the attack speed
	/// </summary>
	public float AttackSpeed { get; set; }

	/// <summary>
	/// Get or Set the attack speed
	/// </summary>
	public int AttackRange { get; set; }

	/// <summary>
	/// Get or Set the attack rate
	/// </summary>
	public int AttackRate { get; set; }

	/// <summary>
	/// Get or Set the monster skill damage.
	/// </summary>
	public Range SkillDamage { get; set; }

	/// <summary>
	/// Get or Set the monster skill distance.
	/// </summary>
	public int SkillDistance { get; set; }

	/// <summary>
	/// Get or Set the monster skill range
	/// </summary>
	public int SkillRange { get; set; }

	/// <summary>
	/// Get or Set the monster skill rate.
	/// </summary>
	public int SkillRate { get; set; }

	/// <summary>
	/// Get or Set the monster skill curse(rate?)
	/// </summary>
	public int SkillCurse { get; set; }

	/// <summary>
	/// Get or Set the monster rate of stuning
	/// </summary>
	public int StunRate { get; set; }

	/// <summary>
	/// Get or Set the monster rate of using special attacks
	/// </summary>
	public int SpecialAttackRate { get; set; }


	// Defensive

	/// <summary>
	/// Get or Set the defense
	/// </summary>
	public int Defense { get; set; }

	/// <summary>
	/// Get or Set the absorption
	/// </summary>
	public float Absorption { get; set; }

	/// <summary>
	/// Get or Set the block rate
	/// </summary>
	public float BlockRate { get; set; }

	/// <summary>
	/// Get or Set the max Life(HP)
	/// </summary>
	public int Life { get; set; }

	/// <summary>
	/// Get or Set the elemental resistance
	/// </summary>
	public Elemental Resistance
	{
		get { return _resistance; }
		set { _resistance = value; }
	}
	private Elemental _resistance = new();


	// Moving

	/// <summary>
	/// Get or Set the movement type
	/// </summary>
	public int MovementType { get; set; }

	/// <summary>
	/// Get or Set the movement speed
	/// </summary>
	public float MovementSpeed { get; set; }

	/// <summary>
	/// Get or Set the movement range
	/// </summary>
	public float MovementRange { get; set; }


	// Potion

	/// <summary>
	/// Get or Set the rate of the monster using potion.
	/// </summary>
	public int PotionRate { get; set; }

	/// <summary>
	/// Get or Set how many potions the monster is holding.
	/// </summary>
	public int PotionCount { get; set; }


	// Sound Effec

	/// <summary>
	/// Get or Set sound code for effects
	/// </summary>
	public string SoundCode { get; set; } = string.Empty;

	// Events

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

	// Loots

	/// <summary>
	/// Get or Set the experience given by the monster.
	/// </summary>
	public long Experience { get; set; }

	/// <summary>
	/// Get or Set if all players can see the loot when it drops.
	/// </summary>
	public bool AllSeeLoot { get; set; }

	/// <summary>
	/// Get or Set Item Counter.
	/// </summary>
	public int FallItemMax { get; set; }

	/// <summary>
	/// Get or Set Loot droped by monster
	/// </summary>
	public readonly List<Loot> FallItems = new();

	/// <summary>
	/// Get or Set additional Loot droped by monster
	/// </summary>
	public readonly List<Loot> FallItemsPlus = new();

	/// <summary>
	/// Get the sum of all drop rate
	/// </summary>
	public int FallItemPerMax
	{
		get
		{
			if (FallItems.Count != 0)
			{
				int maxPercent = 0;

				foreach (var drop in FallItems)
				{
					maxPercent += drop.Rate;
				}

				return maxPercent;
			}

			return 0;
		}
	}

	// Zhoon File

	/// <summary>
	/// Get or Set the external file holding the display name(Zhoon file)
	/// </summary>
	public string ExternalFile { get; set; } = string.Empty;

	#endregion


	#region Constructor(s)

	public Monster()
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
		DistinctionCode = 0;

		// Appearance (3D Model Data)
		ModelFile = string.Empty;
		ModelSize = 1.0f;
		ShadowSize = ShadowSize.Unknown;
		ModelEvent = string.Empty;
		ArrowPosition = Point.Empty;

		// States
		State = false;
		Level = 0;
		IsBoss = false;
		MonsterClass = MonsterClass.Normal;
		Type = MonsterType.Normal;
		ActiveTime = ActiveTime.All;
		Nature = MonsterNature.Neutral;
		GenerateGroup = new Framework.Range();
		RealSight = 0;
		IQ = 0;
		IsUndead = false;

		// Attack
		AttackPower = new Framework.Range();
		AttackSpeed = 0.0f;
		AttackRange = 0;
		AttackRate = 0;
		SkillDamage = new Framework.Range();
		SkillDistance = 0;
		SkillRange = 0;
		SkillRate = 0;
		SkillCurse = 0;
		StunRate = 0;
		SpecialAttackRate = 0;

		// Defense
		Defense = 0;
		Absorption = 0.0f;
		BlockRate = 0.0f;
		Life = 0;
		_resistance = new Elemental();

		// Movement
		MovementType = 0;
		MovementSpeed = 0;
		MovementRange = 0;

		// Potion
		PotionCount = 0;
		PotionRate = 0;

		// Sound Effects
		SoundCode = string.Empty;

		// Events
		EventCode = 0;
		EventInfo = 0;
		EventItem = string.Empty;

		// Loots
		Experience = 0u;
		AllSeeLoot = false;
		FallItemMax = 0;
		FallItems.Clear();
		FallItemsPlus.Clear();

		// Zhoon File
		ExternalFile = string.Empty;
	}

	#endregion


	#region Parsing method

	//protected override void ParseLine(string line)
	//{
	//	int position = 0;
	//	string keyword = FileHelper.GetWord(line, ref position);

	//	if (string.IsNullOrEmpty(keyword))
	//		return;

	//	// Identifiers
	//	if (string.Compare(keyword, Keywords.ServerName) == 0)
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
	//	else if (string.Compare(keyword, Keywords.DistinctionCode) == 0)
	//	{
	//		DistinctionCode = FileHelper.ParseInteger(line, ref position);
	//	}

	//	// Appearance (3D Model Data)
	//	else if (string.Compare(keyword, Keywords.ModelFile) == 0)
	//	{
	//		var model = FileHelper.ParseString(line, ref position);
	//		if (!string.IsNullOrEmpty(model))
	//			ModelFile = model;
	//	}
	//	else if (string.Compare(keyword, Keywords.ModelSize) == 0)
	//	{
	//		ModelSize = FileHelper.ParseFloat(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.ShadowSize) == 0)
	//	{
	//		var str = FileHelper.GetWord(line, ref position);
	//		var size = Parse<ShadowSize>(str);

	//		ShadowSize = (ShadowSize)size;
	//	}
	//	else if (string.Compare(keyword, Keywords.ModelEvent) == 0)
	//	{
	//		var model = FileHelper.ParseString(line, ref position);
	//		if (!string.IsNullOrEmpty(model))
	//			ModelEvent = model;
	//	}
	//	else if (string.Compare(keyword, Keywords.ArrowPosition) == 0)
	//	{
	//		var x = FileHelper.ParseInteger(line, ref position);
	//		var y = FileHelper.ParseInteger(line, ref position);

	//		ArrowPosition = new Point(x, y);
	//	}

	//	// States
	//	else if (string.Compare(keyword, Keywords.State) == 0)
	//	{
	//		State = FileHelper.ParseBool(line, ref position, "적");
	//	}
	//	else if (string.Compare(keyword, Keywords.Level) == 0)
	//	{
	//		Level = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.IsBoss) == 0)
	//	{
	//		IsBoss = true;
	//	}
	//	else if (string.Compare(keyword, Keywords.MonsterClass) == 0)
	//	{
	//		var mc = FileHelper.ParseInteger(line, ref position);
	//		MonsterClass = (MonsterClass)mc;
	//	}
	//	else if (string.Compare(keyword, Keywords.MonsterType) == 0)
	//	{
	//		var str = FileHelper.GetWord(line, ref position);
	//		var type = Parse<MonsterType>(str);

	//		Type = (MonsterType)type;
	//	}
	//	else if (string.Compare(keyword, Keywords.ActiveTime) == 0)
	//	{
	//		var str = FileHelper.GetWord(line, ref position);
	//		var hour = Parse<ActiveTime>(str);

	//		ActiveTime = (ActiveTime)hour;
	//	}
	//	else if (string.Compare(keyword, Keywords.MonsterNature) == 0)
	//	{
	//		var str = FileHelper.GetWord(line, ref position);
	//		var nature = Parse<MonsterNature>(str);

	//		Nature = (MonsterNature)nature;
	//	}
	//	else if (string.Compare(keyword, Keywords.GenerateGroup) == 0)
	//	{
	//		GenerateGroup = FileHelper.ParseRange(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.RealSight) == 0)
	//	{
	//		RealSight = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.IQ) == 0)
	//	{
	//		IQ = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.IsUndead) == 0)
	//	{
	//		IsUndead = FileHelper.ParseBool(line, ref position, Keywords.Undead[0]) ||
	//					FileHelper.ParseBool(line, ref position, Keywords.Undead[1]);

	//		Type = IsUndead ? MonsterType.Undead : MonsterType.Normal;
	//	}

	//	// Attack
	//	else if (string.Compare(keyword, Keywords.AttackPower) == 0)
	//	{
	//		AttackPower = FileHelper.ParseRange(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.AttackSpeed) == 0)
	//	{
	//		AttackSpeed = FileHelper.ParseFloat(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.AttackRange) == 0)
	//	{
	//		AttackRange = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.AttackRate) == 0)
	//	{
	//		AttackRate = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.SkillDamage) == 0)
	//	{
	//		SkillDamage = FileHelper.ParseRange(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.SkillDistance) == 0)
	//	{
	//		SkillDistance = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.SkillRange) == 0)
	//	{
	//		SkillRange = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.SkillRate) == 0)
	//	{
	//		SkillRate = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.SkillCurse) == 0)
	//	{
	//		SkillCurse = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if ((string.Compare(keyword, Keywords.StunRate[0]) == 0) ||
	//			 (string.Compare(keyword, Keywords.StunRate[1]) == 0))
	//	{
	//		StunRate = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.SpecialAttackRate) == 0)
	//	{
	//		SpecialAttackRate = FileHelper.ParseInteger(line, ref position);
	//	}

	//	// Defense
	//	else if (string.Compare(keyword, Keywords.Defense) == 0)
	//	{
	//		Defense = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.Absorption) == 0)
	//	{
	//		Absorption = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.BlockRate) == 0)
	//	{
	//		BlockRate = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if ((string.Compare(keyword, Keywords.Life[0]) == 0) ||
	//			 (string.Compare(keyword, Keywords.Life[1]) == 0))
	//	{
	//		Life = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.Organic) == 0)
	//	{
	//		_resistance.Organic = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.Earth) == 0)
	//	{
	//		_resistance.Earth = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.Fire) == 0)
	//	{
	//		_resistance.Fire = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.Ice) == 0)
	//	{
	//		_resistance.Ice = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.Lightning) == 0)
	//	{
	//		_resistance.Lightning = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.Poison) == 0)
	//	{
	//		_resistance.Poison = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.Water) == 0)
	//	{
	//		_resistance.Water = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.Wind) == 0)
	//	{
	//		_resistance.Wind = FileHelper.ParseInteger(line, ref position);
	//	}

	//	// Movement
	//	else if (string.Compare(keyword, Keywords.MovementType) == 0)
	//	{
	//		MovementType = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.MovementSpeed) == 0)
	//	{
	//		MovementSpeed = FileHelper.ParseFloat(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.MovementRange) == 0)
	//	{
	//		MovementRange = FileHelper.ParseFloat(line, ref position);
	//	}

	//	// Potion
	//	else if (string.Compare(keyword, Keywords.PotionCount) == 0)
	//	{
	//		PotionCount = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.PotionRate) == 0)
	//	{
	//		PotionRate = FileHelper.ParseInteger(line, ref position);
	//	}

	//	// Sound
	//	else if ((string.Compare(keyword, Keywords.SoundCode[0]) == 0) ||
	//			 (string.Compare(keyword, Keywords.SoundCode[1]) == 0))
	//	{
	//		var sound = FileHelper.GetWord(line, ref position);

	//		if (!string.IsNullOrEmpty(sound))
	//		{
	//			SoundCode = sound;

	//			if (!SoundList.Contains(sound, StringComparer.InvariantCultureIgnoreCase))
	//				SoundList.Add(sound);
	//		}
	//	}

	//	// Events
	//	else if (string.Compare(keyword, Keywords.EventCode) == 0)
	//	{
	//		EventCode = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.EventInfo) == 0)
	//	{
	//		EventInfo = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.EventItem) == 0)
	//	{
	//		string str = FileHelper.GetWord(line, ref position);

	//		if (!string.IsNullOrEmpty(str))
	//			EventItem = str;
	//	}

	//	// Loots
	//	else if (string.Compare(keyword, Keywords.Experience) == 0)
	//	{
	//		Experience = FileHelper.ParseLong(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.AllSeeItem) == 0)
	//	{
	//		AllSeeLoot = true;
	//	}
	//	else if (string.Compare(keyword, Keywords.FallItemMax) == 0)
	//	{
	//		FallItemMax = FileHelper.ParseInteger(line, ref position);
	//	}
	//	else if (string.Compare(keyword, Keywords.FallItemsPlus) == 0)
	//	{
	//		if (FallItemsPlus.Count < FALLITEMPLUS_MAX)
	//		{
	//			int rate = FileHelper.ParseInteger(line, ref position);
	//			string item = FileHelper.GetWord(line, ref position);

	//			var loot = new Loot()
	//			{
	//				Rate = rate,
	//				Items = new List<string>()
	//			};
	//			loot.Items.Add(item);

	//			FallItemsPlus.Add(loot);
	//		}
	//	}
	//	else if (string.Compare(keyword, Keywords.FallItems) == 0)
	//	{
	//		if (FallItems.Count < FALLITEM_MAX)
	//		{
	//			int rate = FileHelper.ParseInteger(line, ref position);
	//			var str = FileHelper.GetWord(line, ref position);

	//			Loot loot = new()
	//			{
	//				Rate = rate,
	//			};

	//			if (string.Compare(str, Keywords.LootMoney) == 0)
	//			{
	//				loot.Money = FileHelper.ParseRange(line, ref position);
	//			}
	//			else if (string.Compare(str, Keywords.LootCoin) == 0)
	//			{
	//				loot.Coin = FileHelper.ParseRange(line, ref position);
	//			}
	//			else if (string.Compare(str, Keywords.LootNothing) != 0) // Not Nothing
	//			{
	//				loot.Items = new List<string>();

	//				while (!string.IsNullOrEmpty(str))
	//				{
	//					loot.Items.Add(str);

	//					str = FileHelper.GetWord(line, ref position);
	//				}
	//			}

	//			FallItems.Add(loot);
	//		}
	//	}

	//	// Zhoon File
	//	else if (string.Compare(keyword, Keywords.ExternalFile) == 0)
	//	{
	//		ExternalFile = FileHelper.ParseString(line, ref position);

	//		var path = Path.Combine(Globals.MonsterPath, ExternalFile);
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

		// Switch-based parsing
		switch (keyword)
		{
			// Identifiers
			case var text when text.StartsWith('*') && text.Contains("NAME", StringComparison.OrdinalIgnoreCase):
				var name = ParseString();
				if (!string.IsNullOrEmpty(name))
					Name = name;
				break;

			case var text when text.Equals(Keywords.ServerName):
				ServerName = ParseString();
				break;

			case var text when text.Equals(Keywords.DistinctionCode):
				DistinctionCode = ParseInteger();
				break;

			// Appearance (3D Model Data)
			case var text when text.Equals(Keywords.ModelFile):
				ModelFile = ParseString();
				break;

			case var text when text.Equals(Keywords.ModelSize):
				ModelSize = ParseFloat();
				break;

			case var text when text.Equals(Keywords.ShadowSize):
				ShadowSize = (ShadowSize)Parse<ShadowSize>(FileHelper.GetWord(line, ref position));
				break;

			case var text when text.Equals(Keywords.ModelEvent):
				ModelEvent = ParseString();
				break;

			case var text when text.Equals(Keywords.ArrowPosition):
				int x = ParseInteger();
				int y = ParseInteger();
				ArrowPosition = new Point(x, y);
				break;

			// States
			case var text when text.Equals(Keywords.State):
				State = ParseBool("적");
				break;

			case var text when text.Equals(Keywords.Level):
				Level = ParseInteger();
				break;

			case var text when text.Equals(Keywords.IsBoss):
				IsBoss = true;
				break;

			// Monster attributes
			case var text when text.Equals(Keywords.MonsterClass):
				MonsterClass = (MonsterClass)ParseInteger();
				break;

			case var text when text.Equals(Keywords.MonsterType):
				Type = (MonsterType)Parse<MonsterType>(FileHelper.GetWord(line, ref position));
				break;

			case var text when text.Equals(Keywords.ActiveTime):
				ActiveTime = (ActiveTime)Parse<ActiveTime>(FileHelper.GetWord(line, ref position));
				break;

			case var text when text.Equals(Keywords.MonsterNature):
				Nature = (MonsterNature)Parse<MonsterNature>(FileHelper.GetWord(line, ref position));
				break;

			case var text when text.Equals(Keywords.GenerateGroup):
				GenerateGroup = ParseRange();
			break;

			case var text when text.Equals(Keywords.RealSight):
				RealSight = ParseInteger();
				break;

			case var text when text.Equals(Keywords.IQ):
				IQ = ParseInteger();
				break;

			case var text when text.Equals(Keywords.IsUndead):
				IsUndead = ParseBool(Keywords.Undead[0]) || ParseBool(Keywords.Undead[1]);
				Type = IsUndead ? MonsterType.Undead : Type;
				break;

			// Attack properties
			case var text when text.Equals(Keywords.AttackPower):
				AttackPower = ParseRange();
				break;

			case var text when text.Equals(Keywords.AttackSpeed):
				AttackSpeed = ParseFloat();
				break;

			case var text when text.Equals(Keywords.AttackRange):
				AttackRange = ParseInteger();
				break;

			case var text when text.Equals(Keywords.AttackRate):
				AttackRate = ParseInteger();
				break;

			case var text when text.Equals(Keywords.SkillDamage):
				SkillDamage = ParseRange();
				break;

			case var text when text.Equals(Keywords.SkillDistance):
				SkillDistance = ParseInteger();
				break;

			case var text when text.Equals(Keywords.SkillRange):
				SkillRange = ParseInteger();
				break;

			case var text when text.Equals(Keywords.SkillRate):
				SkillRate = ParseInteger();
				break;

			case var text when text.Equals(Keywords.SkillCurse):
				SkillCurse = ParseInteger();
				break;

			case var text when text.Equals(Keywords.StunRate[0]) || text.Equals(Keywords.StunRate[1]):
				StunRate = ParseInteger();
				break;

			case var text when text.Equals(Keywords.SpecialAttackRate):
				SpecialAttackRate = ParseInteger();
				break;

			// Defense properties
			case var text when text.Equals(Keywords.Defense):
				Defense = ParseInteger();
				break;

			case var text when text.Equals(Keywords.Absorption):
				Absorption = ParseInteger();
				break;

			case var text when text.Equals(Keywords.BlockRate):
				BlockRate = ParseInteger();
				break;

			case var text when text.Equals(Keywords.Life[0]) || text.Equals(Keywords.Life[1]):
				Life = ParseInteger();
				break;

			// Magic Resistance properties
			case var text when text.Equals(Keywords.Organic):
				_resistance.Organic = ParseInteger();
				break;

			case var text when text.Equals(Keywords.Earth):
				_resistance.Earth = ParseInteger();
				break;

			case var text when text.Equals(Keywords.Fire):
				_resistance.Fire = ParseInteger();
				break;

			case var text when text.Equals(Keywords.Ice):
				_resistance.Ice = ParseInteger();
				break;

			case var text when text.Equals(Keywords.Lightning):
				_resistance.Lightning = ParseInteger();
				break;

			case var text when text.Equals(Keywords.Poison):
				_resistance.Poison = ParseInteger();
				break;

			case var text when text.Equals(Keywords.Water):
				_resistance.Water = ParseInteger();
				break;

			case var text when text.Equals(Keywords.Wind):
				_resistance.Wind = ParseInteger();
				break;

			// Movement properties
			case var text when text.Equals(Keywords.MovementType):
				MovementType = ParseInteger();
				break;

			case var text when text.Equals(Keywords.MovementSpeed):
				MovementSpeed = ParseFloat();
				break;

			case var text when text.Equals(Keywords.MovementRange):
				MovementRange = ParseFloat();
				break;

			// Potion properties
			case var text when text.Equals(Keywords.PotionCount):
				PotionCount = ParseInteger();
				break;

			case var text when text.Equals(Keywords.PotionRate):
				PotionRate = ParseInteger();
				break;

			// Sound Effects
			case var text when text.Equals(Keywords.SoundCode[0]) || text.Equals(Keywords.SoundCode[1]):
				string sound = FileHelper.GetWord(line, ref position);
				if (!string.IsNullOrEmpty(sound))
				{
					SoundCode = sound;

					if (!SoundList.Contains(sound, StringComparer.OrdinalIgnoreCase))
						SoundList.Add(sound);
				}
				break;

			// Events properties
			case var text when text.Equals(Keywords.EventCode):
				EventCode = ParseInteger();
				break;

			case var text when text.Equals(Keywords.EventInfo):
				EventInfo = ParseInteger();
				break;

			case var text when text.Equals(Keywords.EventItem):
			{
				string item = FileHelper.GetWord(line, ref position);
				if (!string.IsNullOrEmpty(item))
					EventItem = item;

				break;
			}

			// Loot properties
			case var text when text.Equals(Keywords.Experience):
				Experience = FileHelper.ParseLong(line, ref position);
				break;

			case var text when text.Equals(Keywords.AllSeeItem):
				AllSeeLoot = true;
				break;

			case var text when text.Equals(Keywords.FallItemMax):
				FallItemMax = ParseInteger();
				break;

			case var text when text.Equals(Keywords.FallItemsPlus):
				if (FallItemsPlus.Count < FALLITEMPLUS_MAX)
				{
					int rate = ParseInteger();
					string item = FileHelper.GetWord(line, ref position);

					if (string.IsNullOrEmpty(item) ||
						string.Equals(item, Keywords.LootNothing) ||
						string.Equals(item, Keywords.LootMoney) ||
						string.Equals(item, Keywords.LootCoin))
						break;

					var loot = new Loot
					{
						Rate = rate,
						Items = new List<string>()
					};

					loot.Items.Add(item);

					FallItemsPlus.Add(loot);
				}
				break;

			case var text when text.Equals(Keywords.FallItems):
				if (FallItems.Count < FALLITEM_MAX)
				{
					int rate = ParseInteger();
					var str = FileHelper.GetWord(line, ref position);
					var loot = new Loot { Rate = rate };

					if (string.Equals(str, Keywords.LootMoney))
					{
						loot.Money = ParseRange();
					}
					else if (string.Equals(str, Keywords.LootCoin))
					{
						loot.Coin = ParseRange();
					}
					else if (!string.Equals(str, Keywords.LootNothing))
					{
						loot.Items = new List<string>();

						while (!string.IsNullOrEmpty(str))
						{
							loot.Items.Add(str);
							str = FileHelper.GetWord(line, ref position);
						}
					}

					FallItems.Add(loot);
				}
				break;

			// External file processing
			case var text when text.Equals(Keywords.ExternalFile):
				ExternalFile = ParseString();
				var path = Path.Combine(Globals.MonsterPath, ExternalFile);
				if (File.Exists(path))
					Process(path);
				break;

			default:
				// Unknown keyword, handle appropriately if needed
				break;
		}
	}

	#endregion


	#region Saving method

	public override void Save(string? fileName = null)
	{
		StringBuilder sb = new();

		sb.AppendLine("// Priston Tale - Monster file");
		sb.AppendLine("// Identifiers");
		sb.AppendLine($"{Keywords.ServerName}\t\t\"{ServerName}\"");
		sb.AppendLine($"{Keywords.Name[0]}\t\t\"{Name}\"");

		if(DistinctionCode != 0)
			sb.AppendLine($"{Keywords.DistinctionCode}\t\t{DistinctionCode}");

		sb.AppendLine();


		sb.AppendLine("// Appearance (3D Model Data)");
		sb.AppendLine($"{Keywords.ModelFile}\t\t\"{ModelFile}\"");

		if (ModelSize != 1.000f)
			sb.AppendLine($"{Keywords.ModelSize}\t\t{ModelSize}");

		sb.AppendLine($"{Keywords.ShadowSize}\t\t{Parse(ShadowSize)}");

		if(!string.IsNullOrEmpty(ModelEvent))
			sb.AppendLine($"{Keywords.ModelEvent}\t\t\"{ModelEvent}\"");

		if(ArrowPosition != Point.Empty)
			sb.AppendLine($"{Keywords.ArrowPosition}\t\t{ArrowPosition.X} {ArrowPosition.Y}");
		
		sb.AppendLine();


		sb.AppendLine("// States");
		sb.AppendLine($"{Keywords.State}\t\t" + (State ? "적" : "NPC"));
		sb.AppendLine($"{Keywords.Level}\t\t{Level}");

		if (IsBoss)
			sb.AppendLine(Keywords.IsBoss);
		else if (MonsterClass != MonsterClass.Normal)
			sb.AppendLine($"{Keywords.MonsterClass}\t\t{(int)MonsterClass}");

		var type = Parse(Type);
		if (!string.IsNullOrEmpty(type))
			sb.AppendLine($"{Keywords.MonsterType}\t\t{type}");

		if (ActiveTime != ActiveTime.All)
			sb.AppendLine($"{Keywords.ActiveTime}\t\t{ActiveTime}");

		if (Nature != MonsterNature.Neutral)
			sb.AppendLine($"{Keywords.MonsterNature}\t\t{Nature}");

		if (GenerateGroup != Framework.Range.Empty)
			sb.AppendLine($"{Keywords.GenerateGroup}\t\t{GenerateGroup.Min} {GenerateGroup.Max}");

		sb.AppendLine($"{Keywords.RealSight}\t\t{RealSight}");
		sb.AppendLine($"{Keywords.IQ}\t\t{IQ}");

		if (IsUndead)
			sb.AppendLine($"{Keywords.IsUndead}\t\t{Keywords.Undead[0]}");

		sb.AppendLine();


		sb.AppendLine("// Offensive Stats");
		sb.AppendLine($"{Keywords.AttackPower}\t\t{AttackPower.Min} {AttackPower.Max}");
		sb.AppendLine($"{Keywords.AttackSpeed}\t\t{AttackSpeed}");
		sb.AppendLine($"{Keywords.AttackRange}\t\t{AttackRange}");
		sb.AppendLine($"{Keywords.AttackRate}\t\t{AttackRate}");

		sb.AppendLine("// Offensive Skill Stats");
		sb.AppendLine($"{Keywords.SkillDamage}\t\t{SkillDamage.Min} {SkillDamage.Max}");
		sb.AppendLine($"{Keywords.SkillDistance}\t\t{SkillDistance}");
		sb.AppendLine($"{Keywords.SkillRange}\t\t{SkillRange}");
		sb.AppendLine($"{Keywords.SkillRate}\t\t{SkillRate}");
		sb.AppendLine($"{Keywords.SkillCurse}\t\t{SkillCurse}");
		sb.AppendLine($"{Keywords.StunRate[0]}\t\t{StunRate}");
		sb.AppendLine($"{Keywords.SpecialAttackRate}\t\t{SpecialAttackRate}");
		sb.AppendLine();


		sb.AppendLine("// Defensive Stats");
		sb.AppendLine($"{Keywords.Defense}\t\t{Defense}");
		sb.AppendLine($"{Keywords.Absorption}\t\t{Absorption}");
		sb.AppendLine($"{Keywords.BlockRate}\t\t{BlockRate}");
		sb.AppendLine($"{Keywords.Life[0]}\t\t{Life}");

		sb.AppendLine("// Magic Resistance");
		sb.AppendLine($"{Keywords.Organic}\t\t{_resistance.Organic}");
		sb.AppendLine($"{Keywords.Earth}\t\t{_resistance.Earth}");
		sb.AppendLine($"{Keywords.Fire}\t\t{_resistance.Fire}");
		sb.AppendLine($"{Keywords.Ice}\t\t{_resistance.Ice}");
		sb.AppendLine($"{Keywords.Lightning}\t\t{_resistance.Lightning}");
		sb.AppendLine($"{Keywords.Poison}\t\t{_resistance.Poison}");
		sb.AppendLine($"{Keywords.Water}\t\t{_resistance.Water}");
		sb.AppendLine($"{Keywords.Wind}\t\t{_resistance.Wind}");
		sb.AppendLine();


		sb.AppendLine("// Movement");
		if(MovementType != 0)
			sb.AppendLine($"{Keywords.MovementType}\t\t{MovementType}");
		if (MovementSpeed != 0)
			sb.AppendLine($"{Keywords.MovementSpeed}\t\t{MovementSpeed}");
		if (MovementRange != 0)
			sb.AppendLine($"{Keywords.MovementRange}\t\t{MovementRange}");
		sb.AppendLine();


		sb.AppendLine("// Potion");
		if (PotionCount != 0)
			sb.AppendLine($"{Keywords.PotionCount}\t\t{PotionCount}");
		if (PotionRate != 0)
			sb.AppendLine($"{Keywords.PotionRate}\t\t{PotionRate}");
		sb.AppendLine();

		sb.AppendLine("// Sound Effects");
		if (!string.IsNullOrEmpty(SoundCode))
			sb.AppendLine($"{Keywords.SoundCode[0]}\t\t{SoundCode}");

		sb.AppendLine();


		sb.AppendLine("// Events");
		if (EventCode != 0)
			sb.AppendLine($"{Keywords.EventCode}\t\t{EventCode}");
		if (EventInfo != 0)
			sb.AppendLine($"{Keywords.EventInfo}\t\t{EventInfo}");

		if (!string.IsNullOrEmpty(EventItem))
		{
			sb.AppendLine($"{Keywords.EventItem}\t\t{EventItem}");
			sb.AppendLine();
		}

		sb.AppendLine("// Loots");

		sb.AppendLine($"{Keywords.Experience}\t\t{Experience}");

		sb.AppendLine();

		if (AllSeeLoot)
			sb.AppendLine(Keywords.AllSeeItem);

		if (FallItemMax > 0)
			sb.AppendLine($"{Keywords.FallItemMax}\t\t{FallItemMax}");

		int lootCounter = 0;

		foreach (var loot in FallItems)
		{
			if (lootCounter >= FALLITEM_MAX)
				break;

			var lootStr = loot switch
			{
				{ Money: { } } => $"{Keywords.LootMoney}\t{loot.Money.Value.Min} {loot.Money.Value.Max}",
				{ Coin: { } } => $"{Keywords.LootCoin}\t{loot.Coin.Value.Min} {loot.Coin.Value.Max}",
				{ Items: { } } => string.Join(" ", loot.Items),
				_ => Keywords.LootNothing
			};

			if(!string.IsNullOrEmpty(lootStr))
				sb.AppendLine($"{Keywords.FallItems}\t\t{loot.Rate}\t{lootStr}");

			lootCounter++;
		}

		sb.AppendLine();

		lootCounter = 0;

		foreach (var loot in FallItemsPlus)
		{
			if (loot.Items == null || !loot.Items.Any())
				continue;

			if (lootCounter >= FALLITEMPLUS_MAX)
				break;

			sb.AppendLine($"{Keywords.FallItemsPlus}\t\t{loot.Rate}\t{string.Join(' ', loot.Items)}");

			lootCounter++;
		}

		sb.AppendLine();


		sb.AppendLine("// Zhoon File");
		sb.AppendLine($"{Keywords.ExternalFile}\t\t\"{ExternalFile}\"");
		sb.AppendLine();


		using var sw = new StreamWriter(fileName ?? FileName, false, Encoding);
		sw.Write(sb);
		sw.Flush();

		var path = Path.Combine(Globals.MonsterPath, ExternalFile);
		using var zhoon = new StreamWriter(path);

		zhoon.WriteLine("// Priston Tale - Monster file");

		var fi = new FileInfo(fileName ?? FileName);
		zhoon.WriteLine($"// {fi.Name}");
		zhoon.WriteLine();

		var langName = FileHelper.GetInfoName(Globals.Settings.Language);
		zhoon.WriteLine($"{langName}\t\t\"{Name}\"");
		zhoon.WriteLine();
		zhoon.Flush();
	}

	#endregion
}
