using GameServer.Helpers;
using System.Text;
using static System.Windows.Forms.AxHost;

namespace GameServer.Framework.Characters;

public partial class Monster : GameInfo
{
	#region Field/Properties

	/// <summary>
	/// Static list to fill with all SoundCodes available,
	/// use it as DataSource for a combobox in gui
	/// </summary>
	public static readonly List<string> SoundList = new();

	public const int FALLITEM_MAX = 200;
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
	public Range GenerateGroup { get; set; } = new();

	/// <summary>
	/// Get or Set the monster field of vision(Sight).
	/// </summary>
	public float RealSight { get; set; }

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
	public Range AttackPower { get; set; } = new();

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
	public Range SkillDamage { get; set; } = new();

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


	// Loot

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
	public List<Loot> FallItems { get; set; } = new();

	/// <summary>
	/// Get or Set additional Loot droped by monster
	/// </summary>
	public List<Loot> FallItemsPlus { get; set; } = new();

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
		ShadowSize = ShadowSize.Unknown;
		ModelSize = 1.0f;
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

		// Loots
		EventCode = 0;
		EventInfo = 0;
		EventItem = string.Empty;
		Experience = 0u;
		AllSeeLoot = false;
		FallItemMax = 0;
		FallItems.Clear();
		FallItemsPlus.Clear();

		// Zhoon File
		ExternalFile = string.Empty;
	}

	protected override void ParseLine(string line)
	{
		int position = 0;
		string keyword = FileHelper.GetWord(line, ref position);

		if (keyword == null)
			return;
		
		// Identifiers
		if (string.Compare(keyword, Keywords.ServerName) == 0)
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
		else if (string.Compare(keyword, Keywords.DistinctionCode) == 0)
		{
			DistinctionCode = FileHelper.ParseInteger(line, ref position);
		}

		// Appearance (3D Model Data)
		else if (string.Compare(keyword, Keywords.ModelFile) == 0)
		{
			var model = FileHelper.ParseString(line, ref position);
			if (!string.IsNullOrEmpty(model))
				ModelFile = model;
		}
		else if (string.Compare(keyword, Keywords.ShadowSize) == 0)
		{
			var str = FileHelper.GetWord(line, ref position);
			var size = Parse<ShadowSize>(str);

			ShadowSize = (ShadowSize)size;
		}
		else if (string.Compare(keyword, Keywords.ModelSize) == 0)
		{
			ModelSize = FileHelper.ParseFloat(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.ModelEvent) == 0)
		{
			var model = FileHelper.ParseString(line, ref position);
			if (!string.IsNullOrEmpty(model))
				ModelEvent = model;
		}
		else if (string.Compare(keyword, Keywords.ArrowPosition) == 0)
		{
			var x = FileHelper.ParseInteger(line, ref position);
			var y = FileHelper.ParseInteger(line, ref position);

			ArrowPosition = new Point(x, y);
		}

		// States
		else if (string.Compare(keyword, Keywords.State) == 0)
		{
			State = FileHelper.ParseBool(line, ref position, "적");
		}
		else if (string.Compare(keyword, Keywords.Level) == 0)
		{
			Level = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.IsBoss) == 0)
		{
			IsBoss = true;
			MonsterClass = MonsterClass.Boss;
		}
		else if (string.Compare(keyword, Keywords.MonsterClass) == 0)
		{
			var mc = FileHelper.ParseInteger(line, ref position);
			MonsterClass = (MonsterClass)mc;
		}
		else if (string.Compare(keyword, Keywords.MonsterType) == 0)
		{
			var str = FileHelper.GetWord(line, ref position);
			var type = Parse<MonsterType>(str);

			Type = (MonsterType)type;
		}
		else if (string.Compare(keyword, Keywords.ActiveTime) == 0)
		{
			var str = FileHelper.GetWord(line, ref position);
			var hour = Parse<ActiveTime>(str);

			ActiveTime = (ActiveTime)hour;
		}
		else if (string.Compare(keyword, Keywords.MonsterNature) == 0)
		{
			var str = FileHelper.GetWord(line, ref position);
			var nature = Parse<MonsterNature>(str);

			Nature = (MonsterNature)nature;
		}
		else if (string.Compare(keyword, Keywords.GenerateGroup) == 0)
		{
			GenerateGroup = FileHelper.ParseRange(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.RealSight) == 0)
		{
			RealSight = FileHelper.ParseFloat(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.IQ) == 0)
		{
			IQ = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.IsUndead) == 0)
		{
			IsUndead = FileHelper.ParseBool(line, ref position, Keywords.Undead[0]) ||
						FileHelper.ParseBool(line, ref position, Keywords.Undead[1]);

			Type = IsUndead ? MonsterType.Undead : MonsterType.Normal;
		}

		// Attack
		else if (string.Compare(keyword, Keywords.AttackPower) == 0)
		{
			AttackPower = FileHelper.ParseRange(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.AttackSpeed) == 0)
		{
			AttackSpeed = FileHelper.ParseFloat(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.AttackRange) == 0)
		{
			AttackRange = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.AttackRate) == 0)
		{
			AttackRate = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.SkillDamage) == 0)
		{
			SkillDamage = FileHelper.ParseRange(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.SkillDistance) == 0)
		{
			SkillDistance = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.SkillRange) == 0)
		{
			SkillRange = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.SkillRate) == 0)
		{
			SkillRate = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.SkillCurse) == 0)
		{
			SkillCurse = FileHelper.ParseInteger(line, ref position);
		}
		else if ((string.Compare(keyword, Keywords.StunRate[0]) == 0) ||
				 (string.Compare(keyword, Keywords.StunRate[1]) == 0))
		{
			StunRate = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.SpecialAttackRate) == 0)
		{
			SpecialAttackRate = FileHelper.ParseInteger(line, ref position);
		}

		// Defense
		else if (string.Compare(keyword, Keywords.Defense) == 0)
		{
			Defense = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.Absorption) == 0)
		{
			Absorption = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.BlockRate) == 0)
		{
			BlockRate = FileHelper.ParseInteger(line, ref position);
		}
		else if ((string.Compare(keyword, Keywords.Life[0]) == 0) ||
				 (string.Compare(keyword, Keywords.Life[1]) == 0))
		{
			Life = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.Organic) == 0)
		{
			_resistance.Organic = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.Water) == 0)
		{
			_resistance.Water = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.Lightning) == 0)
		{
			_resistance.Lightning = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.Ice) == 0)
		{
			_resistance.Ice = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.Wind) == 0)
		{
			_resistance.Wind = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.Earth) == 0)
		{
			_resistance.Earth = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.Fire) == 0)
		{
			_resistance.Fire = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.Poison) == 0)
		{
			_resistance.Poison = FileHelper.ParseInteger(line, ref position);
		}

		// Movement
		else if (string.Compare(keyword, Keywords.MovementType) == 0)
		{
			MovementType = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.MovementSpeed) == 0)
		{
			MovementSpeed = FileHelper.ParseFloat(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.MovementRange) == 0)
		{
			MovementRange = FileHelper.ParseFloat(line, ref position);
		}

		// Potion
		else if (string.Compare(keyword, Keywords.PotionCount) == 0)
		{
			PotionCount = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.PotionRate) == 0)
		{
			PotionRate = FileHelper.ParseInteger(line, ref position);
		}

		// Sound
		else if ((string.Compare(keyword, Keywords.SoundCode[0]) == 0) ||
				 (string.Compare(keyword, Keywords.SoundCode[1]) == 0))
		{
			var sound = FileHelper.GetWord(line, ref position);

			if (!string.IsNullOrEmpty(sound))
			{
				SoundCode = FileHelper.GetWord(line, ref position);

				if (!SoundList.Contains(sound))
					SoundList.Add(sound);
			}
		}

		// Loot
		else if (string.Compare(keyword, Keywords.EventCode) == 0)
		{
			EventCode = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.EventInfo) == 0)
		{
			EventInfo = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.EventItem) == 0)
		{
			string str = FileHelper.GetWord(line, ref position);

			if (!string.IsNullOrEmpty(str))
				EventItem = str;
		}
		else if (string.Compare(keyword, Keywords.Experience) == 0)
		{
			Experience = FileHelper.ParseLong(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.AllSeeItem) == 0)
		{
			AllSeeLoot = true;
		}
		else if (string.Compare(keyword, Keywords.FallItemMax) == 0)
		{
			FallItemMax = FileHelper.ParseInteger(line, ref position);
		}
		else if (string.Compare(keyword, Keywords.FallItemsPlus) == 0)
		{
			if (FallItemsPlus.Count < FALLITEMPLUS_MAX)
			{
				int rate = FileHelper.ParseInteger(line, ref position);
				string item = FileHelper.GetWord(line, ref position);

				var loot = new Loot()
				{
					Rate = rate,
					Items = new List<string>()
				};
				loot.Items.Add(item);

				FallItemsPlus.Add(loot);
			}
		}
		else if (string.Compare(keyword, Keywords.FallItems) == 0)
		{
			if (FallItems.Count < FALLITEM_MAX)
			{
				int rate = FileHelper.ParseInteger(line, ref position);
				var str = FileHelper.GetWord(line, ref position);

				Loot loot = new()
				{
					Rate = rate,
				};

				if (string.Compare(str, Keywords.LootNothing) == 0)
				{
					// Nothing
				}
				else if (string.Compare(str, Keywords.LootMoney) == 0)
				{
					loot.Money = FileHelper.ParseRange(line, ref position);
				}
				else
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
		}

		// Zhoon File
		else if (string.Compare(keyword, Keywords.ExternalFile) == 0)
		{
			ExternalFile = FileHelper.ParseString(line, ref position);

			var path = Path.Combine(Globals.MonsterPath, ExternalFile);
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
	//	if (string.Compare(keyword, "*이름") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t\"{ServerName}\"");
	//	}
	//	else if ((string.Compare(keyword, "*B_NAME", true) == 0) ||
	//			 (string.Compare(keyword, "*E_NAME", true) == 0) ||
	//			 (string.Compare(keyword, "*J_NAME", true) == 0) ||
	//			 (string.Compare(keyword, "*K_NAME", true) == 0) ||
	//			 (string.Compare(keyword, "*NAME", true) == 0))
	//	{
	//		sb.AppendLine($"{keyword}\t\t\"{Name}\"");
	//	}
	//	else if (string.Compare(keyword, "*구별코드") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{DistinctionCode}");
	//	}

	//	// Appearance (3D Model Data)
	//	else if (string.Compare(keyword, "*모양파일") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t\"{ModelFile}\"");
	//	}
	//	else if (string.Compare(keyword, "*크기") == 0)
	//	{
	//		var size = Parse(ShadowSize);
	//		if (!string.IsNullOrEmpty(size))
	//			sb.AppendLine($"{keyword}\t\t{size}");
	//	}
	//	else if (string.Compare(keyword, "*모델크기") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{ModelSize}");
	//	}
	//	else if (string.Compare(keyword, "*예비모델") == 0)
	//	{
	//		if (!string.IsNullOrEmpty(ModelEvent))
	//			sb.AppendLine($"{keyword}\t\t\"{ModelEvent}\"");
	//	}
	//	else if (string.Compare(keyword, "*화면보정") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{ArrowPosition.X}\t{ArrowPosition.Y}");
	//	}

	//	// States
	//	else if (string.Compare(keyword, "*속성") == 0)
	//	{
	//		if (State)
	//			sb.AppendLine($"{keyword}\t\t적");
	//	}
	//	else if (string.Compare(keyword, "*레벨") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{Level}");
	//	}
	//	else if (string.Compare(keyword, "*두목") == 0)
	//	{
	//		if(IsBoss)
	//			sb.AppendLine($"{keyword}");
	//	}
	//	else if (string.Compare(keyword, "*계급") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{(int)MonsterClass}");
	//	}
	//	else if (string.Compare(keyword, "*몬스터종족") == 0)
	//	{
	//		var type = Parse(Type);

	//		sb.AppendLine($"{keyword}\t\t{type}");
	//	}
	//	else if (string.Compare(keyword, "*활동시간") == 0)
	//	{
	//		var time = Parse(ActiveTime);

	//		sb.AppendLine($"{keyword}\t\t{time}");
	//	}
	//	else if (string.Compare(keyword, "*품성") == 0)
	//	{
	//		var nature = Parse(Nature);

	//		sb.AppendLine($"{keyword}\t\t{nature}");
	//	}
	//	else if (string.Compare(keyword, "*조직") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{GenerateGroup.Min}\t{GenerateGroup.Max}");
	//	}
	//	else if (string.Compare(keyword, "*시야") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{RealSight}");
	//	}
	//	else if (string.Compare(keyword, "*지능") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{IQ}");
	//	}
	//	else if (string.Compare(keyword, "*언데드") == 0)
	//	{
	//		if(IsUndead)
	//			sb.AppendLine($"{keyword}\t\t있음");
	//	}

	//	// Attack
	//	else if (string.Compare(keyword, "*공격력") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{AttackPower.Min} {AttackPower.Max}");
	//	}
	//	else if (string.Compare(keyword, "*공격속도") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{AttackSpeed}");
	//	}
	//	else if (string.Compare(keyword, "*공격범위") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{AttackRange}");
	//	}
	//	else if (string.Compare(keyword, "*명중력") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{AttackRate}");
	//	}
	//	else if (string.Compare(keyword, "*기술공격력") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{SkillDamage.Min} {SkillDamage.Max}");
	//	}
	//	else if (string.Compare(keyword, "*기술공격거리") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{SkillDistance}");
	//	}
	//	else if (string.Compare(keyword, "*기술공격범위") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{SkillRange}");
	//	}
	//	else if (string.Compare(keyword, "*기술공격률") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{SkillRate}");
	//	}
	//	else if (string.Compare(keyword, "*저주기술") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{SkillCurse}");
	//	}
	//	else if ((string.Compare(keyword, "*스턴율") == 0) ||
	//			 (string.Compare(keyword, "*라이프") == 0))
	//	{
	//		sb.AppendLine($"{keyword}\t\t{StunRate}");
	//	}
	//	else if (string.Compare(keyword, "*특수공격률") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t{SpecialAttackRate}");
	//	}



	//	// Sound
	//	else if ((string.Compare(keyword, "*소리") == 0) ||
	//			 (string.Compare(keyword, "*효과음") == 0))
	//	{
	//		sb.AppendLine($"{keyword}\t\t{SoundCode}");
	//	}

	//	// Zhoon File
	//	else if (string.Compare(keyword, "*연결파일") == 0)
	//	{
	//		sb.AppendLine($"{keyword}\t\t\" {ExternalFile} \"");

	//		var path = Path.Combine(Globals.MonsterPath, ExternalFile);
	//		if (File.Exists(path))
	//			Process(path, path);
	//	}
	//	else
	//		sb.AppendLine(line);
	//}

	#endregion
}
