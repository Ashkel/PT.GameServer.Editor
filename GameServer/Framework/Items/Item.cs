using GameServer.Helpers;
using System.Text;

namespace GameServer.Framework.Items
{
	public partial class Item : GameInfo
	{
		#region Item Data

		// Identifiers

		/// <summary>
		/// Get or Set Item Code
		/// </summary>
		public string Id { get; set; } = string.Empty;

		/// <summary>
		/// Get or Set the name being displayed for players
		/// </summary>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Get or Set the original korean name of the item
		/// </summary>
		public string ServerName { get; set; } = string.Empty;


		// Misc
		public Range Durability
		{
			get { return _durability; }
			set { _durability = value; }
		}
		private Range _durability = new();

		public int Weight { get; set; }

		public int Price { get; set; }

		// Unique item effect
		public long Unique { get; set; }

		public float ScaleBlink { get; set; }

		public Range EffectBlink
		{
			get => _effectBlink;
			set => _effectBlink = value;
		}
		private Range _effectBlink = new();

		public Color EffectColor
		{
			get => _effectColor;
			set => _effectColor = value;
		}
		private Color _effectColor = new();

		public long DispEffect { get; set; }

		// Weapons damage
		public Range AttackPowerMin
		{
			get => _attackPowerMin;
			set => _attackPowerMin = value;
		}
		private Range _attackPowerMin = new();

		public Range AttackPowerMax
		{
			get => _attackPowerMax;
			set => _attackPowerMax = value;
		}
		private Range _attackPowerMax = new();

		public int AttackRange { get; set; }

		public int AttackSpeed { get; set; }

		public Range AttackRate
		{
			get => _attackRate;
			set => _attackRate = value;
		}
		private Range _attackRate = new();

		public int CriticalRate { get; set; }

		// Defensive
		public Range Defense
		{
			get => _defense;
			set => _defense = value;
		}
		private Range _defense = new();

		public RangeF Absortion
		{
			get => _absortion;
			set => _absortion = value;
		}
		private RangeF _absortion = new();

		public RangeF BlockRate
		{
			get => _blockRate;
			set => _blockRate = value;
		}
		private RangeF _blockRate = new();

		public Elemental Resistance
		{
			get => _resistance;
			set => _resistance = value;
		}
		private Elemental _resistance = new();

		// Move for boots
		public RangeF MovementSpeed
		{
			get => _moveSpeed;
			set => _moveSpeed = value;
		}
		private RangeF _moveSpeed = new();

		// Potion space of bracers
		public int PotionSpace { get; set; }

		public Status PotionRecovery
		{
			get => _potionRecovery;
			set => _potionRecovery = value;
		}
		private Status _potionRecovery = new();

		// Additional skill mastery of staffs/wand/phantoms
		public Range MagicMastery
		{
			get => _magicMastery;
			set => _magicMastery = value;
		}
		private Range _magicMastery = new();

		// Add hp/mp/sp and regens
		public Status Additional
		{
			get => _additional;
			set => _additional = value;
		}
		private Status _additional = new();

		public Status Regeneration
		{
			get => _regeneration;
			set => _regeneration = value;
		}
		private Status _regeneration = new();

		// Requeriments to equip item
		public Requeriments Requeriments
		{
			get => _requeriments;
			set => _requeriments = value;
		}
		private Requeriments _requeriments = new();

		// Spec
		public JobType MainJob { get; set; } = JobType.Unknown;
		public List<JobType> AvailableJobs { get; set; } = new();
		public Specialization Specialization
		{
			get => _specialization;
			set => _specialization = value;
		}
		private Specialization _specialization = new();

		public string ExternalFile { get; set; } = string.Empty;

		#endregion


		#region Constructor(s)

		public Item()
		{
			Reset();
		}

		#endregion


		#region Helper methods

		public override void Reset()
		{
			Id = string.Empty;
			Name = string.Empty;
			ServerName = string.Empty;

			Durability = Range.Empty;
			Weight = 0;
			Price = 0;

			Unique = 0;
			ScaleBlink = 0.0f;
			EffectBlink = Range.Empty;
			EffectColor = Color.Transparent;
			DispEffect = 0;

			AttackPowerMin = Range.Empty;
			AttackPowerMax = Range.Empty;
			AttackRange = 0;
			AttackSpeed = 0;
			AttackRate = Range.Empty;
			CriticalRate = 0;

			Defense = Range.Empty;
			Absortion = RangeF.Empty;
			BlockRate = RangeF.Empty;
			Resistance = new Elemental();

			MovementSpeed = RangeF.Empty;

			PotionSpace = 0;
			PotionRecovery = new Status();

			MagicMastery = Range.Empty;

			Additional = new Status();
			Regeneration = new Status();

			Requeriments = new Requeriments();

			MainJob = JobType.Unknown;
			AvailableJobs.Clear();
			Specialization = new Specialization();
		}

		//protected override void ParseLine(string line)
		//{
		//	int position = 0;
		//	string keyword = FileHelper.GetWord(line, ref position);

		//	if (string.IsNullOrEmpty(keyword))
		//		return;


		//	// Identifiers
		//	if (string.Compare(keyword, Keywords.Id) == 0)
		//	{
		//		Id = FileHelper.ParseString(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.ServerName) == 0)
		//	{
		//		ServerName = FileHelper.ParseString(line, ref position);
		//	}
		//	//else if ((string.Compare(keyword, "*B_NAME", true) == 0) ||
		//	//		 (string.Compare(keyword, "*E_NAME", true) == 0) ||
		//	//		 (string.Compare(keyword, "*J_NAME", true) == 0) ||
		//	//		 (string.Compare(keyword, "*K_NAME", true) == 0) ||
		//	//		 (string.Compare(keyword, "*NAME", true) == 0))
		//	if (keyword.StartsWith('*') &&
		//		keyword.ToUpper().Contains("NAME", StringComparison.InvariantCultureIgnoreCase))
		//	{
		//		var name = FileHelper.ParseString(line, ref position);
		//		if (!string.IsNullOrEmpty(name))
		//			Name = name;
		//	}

		//	// Misc
		//	else if (string.Compare(keyword, Keywords.Durability) == 0)
		//	{
		//		Durability = FileHelper.ParseRange(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.Weight) == 0)
		//	{
		//		Weight = FileHelper.ParseInteger(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.Price) == 0)
		//	{
		//		Price = FileHelper.ParseInteger(line, ref position);
		//	}

		//	// Unique Items
		//	else if (string.Compare(keyword, Keywords.Unique) == 0)
		//	{
		//		Unique = FileHelper.ParseLong(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.DispEffect) == 0)
		//	{
		//		DispEffect = FileHelper.ParseLong(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.EffectColor) == 0)
		//	{
		//		int R = FileHelper.ParseInteger(line, ref position);
		//		int G = FileHelper.ParseInteger(line, ref position);
		//		int B = FileHelper.ParseInteger(line, ref position);
		//		int A = FileHelper.ParseInteger(line, ref position);
		//		int effectBlink = FileHelper.ParseInteger(line, ref position);
		//		int effectBlinkMax = (R + G + B + A + (int)DispEffect);

		//		ScaleBlink = FileHelper.ParseFloat(line, ref position);
		//		EffectColor = Color.FromArgb(A, R, G, B);
		//		EffectBlink = new Range(effectBlink, effectBlinkMax);
		//	}

		//	// Offensive power
		//	else if (string.Compare(keyword, Keywords.AttackPower) == 0)
		//	{
		//		AttackPowerMin = FileHelper.ParseRange(line, ref position);
		//		AttackPowerMax = FileHelper.ParseRange(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.AttackRange) == 0)
		//	{
		//		AttackRange = FileHelper.ParseInteger(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.AttackSpeed) == 0)
		//	{
		//		AttackSpeed = FileHelper.ParseInteger(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.AttackRate) == 0)
		//	{
		//		AttackRate = FileHelper.ParseRange(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.CriticalRate) == 0)
		//	{
		//		CriticalRate = FileHelper.ParseInteger(line, ref position);
		//	}

		//	// Defensive power
		//	else if (string.Compare(keyword, Keywords.Defense) == 0)
		//	{
		//		Defense = FileHelper.ParseRange(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.Absortion) == 0)
		//	{
		//		Absortion = FileHelper.ParseRangeF(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.BlockRate) == 0)
		//	{
		//		BlockRate = FileHelper.ParseRangeF(line, ref position);
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

		//	// Mobility
		//	else if (string.Compare(keyword, Keywords.MovementSpeed) == 0)
		//	{
		//		MovementSpeed = FileHelper.ParseRangeF(line, ref position);
		//	}

		//	// Potions
		//	else if (string.Compare(keyword, Keywords.PotionSpace) == 0)
		//	{
		//		PotionSpace = FileHelper.ParseInteger(line, ref position);
		//	}
		//	else if ((string.Compare(keyword, Keywords.PotionHP[0]) == 0) ||
		//			 (string.Compare(keyword, Keywords.PotionHP[1]) == 0))
		//	{
		//		_potionRecovery.Hp = FileHelper.ParseRangeF(line, ref position);
		//	}
		//	else if ((string.Compare(keyword, Keywords.PotionMP[0]) == 0) ||
		//			 (string.Compare(keyword, Keywords.PotionMP[1]) == 0))
		//	{
		//		_potionRecovery.Mp = FileHelper.ParseRangeF(line, ref position);
		//	}
		//	else if ((string.Compare(keyword, Keywords.PotionSP[0]) == 0) ||
		//			 (string.Compare(keyword, Keywords.PotionSP[1]) == 0))
		//	{
		//		_potionRecovery.Sp = FileHelper.ParseRangeF(line, ref position);
		//	}

		//	// Skills training
		//	else if (string.Compare(keyword, Keywords.MagicMastery) == 0)
		//	{
		//		MagicMastery = FileHelper.ParseRange(line, ref position);
		//	}

		//	// Status increase/regeneration
		//	else if ((string.Compare(keyword, Keywords.AdditionalHP[0]) == 0) ||
		//			 (string.Compare(keyword, Keywords.AdditionalHP[1]) == 0))
		//	{
		//		_additional.Hp = FileHelper.ParseRangeF(line, ref position);
		//	}
		//	else if ((string.Compare(keyword, Keywords.AdditionalMP[0]) == 0) ||
		//			 (string.Compare(keyword, Keywords.AdditionalMP[1]) == 0))
		//	{
		//		_additional.Mp = FileHelper.ParseRangeF(line, ref position);
		//	}
		//	else if ((string.Compare(keyword, Keywords.AdditionalSP[0]) == 0) ||
		//			 (string.Compare(keyword, Keywords.AdditionalSP[1]) == 0))
		//	{
		//		_additional.Sp = FileHelper.ParseRangeF(line, ref position);
		//	}
		//	else if ((string.Compare(keyword, Keywords.RegenerationHP[0]) == 0) ||
		//			 (string.Compare(keyword, Keywords.RegenerationHP[1]) == 0))
		//	{
		//		_regeneration.Hp = FileHelper.ParseRangeF(line, ref position);
		//	}
		//	else if ((string.Compare(keyword, Keywords.RegenerationMP[0]) == 0) ||
		//			 (string.Compare(keyword, Keywords.RegenerationMP[1]) == 0))
		//	{
		//		_regeneration.Mp = FileHelper.ParseRangeF(line, ref position);
		//	}
		//	else if ((string.Compare(keyword, Keywords.RegenerationSP[0]) == 0) ||
		//			 (string.Compare(keyword, Keywords.RegenerationSP[1]) == 0))
		//	{
		//		_regeneration.Sp = FileHelper.ParseRangeF(line, ref position);
		//	}


		//	// Requirements for usage
		//	else if (string.Compare(keyword, Keywords.Level) == 0)
		//	{
		//		_requeriments.Level = FileHelper.ParseInteger(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.Strength) == 0)
		//	{
		//		_requeriments.Strength = FileHelper.ParseInteger(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.Intelligence) == 0)
		//	{
		//		_requeriments.Intelligence = FileHelper.ParseInteger(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.Talent) == 0)
		//	{
		//		_requeriments.Talent = FileHelper.ParseInteger(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.Dexterity) == 0)
		//	{
		//		_requeriments.Dexterity = FileHelper.ParseInteger(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.Health) == 0)
		//	{
		//		_requeriments.Health = FileHelper.ParseInteger(line, ref position);
		//	}

		//	// Specialization
		//	else if (string.Compare(keyword, Keywords.MainJob) == 0)
		//	{
		//		string spec = FileHelper.GetWord(line, ref position);

		//		MainJob = Job.Parse(spec);
		//	}
		//	else if (string.Compare(keyword, Keywords.AvailableJobs) == 0)
		//	{
		//		string specs = FileHelper.GetWord(line, ref position);

		//		while (!string.IsNullOrEmpty(specs))
		//		{
		//			var job = Job.Parse(specs);
		//			if (job != JobType.Unknown)
		//				AvailableJobs.Add(job);

		//			specs = FileHelper.GetWord(line, ref position);
		//		}
		//	}

		//	else if (string.Compare(keyword, Keywords.JobAttackPower) == 0)
		//	{
		//		_specialization.AttackPower = FileHelper.ParseRange(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.JobAttackRate) == 0)
		//	{
		//		_specialization.AttackRate = FileHelper.ParseRange(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.JobAttackSpeed) == 0)
		//	{
		//		_specialization.AttackSpeed = FileHelper.ParseInteger(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.JobAttackRange) == 0)
		//	{
		//		_specialization.AttackRange = FileHelper.ParseInteger(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.JobCriticalRate) == 0)
		//	{
		//		_specialization.CriticalRate = FileHelper.ParseInteger(line, ref position);
		//	}

		//	else if (string.Compare(keyword, Keywords.JobDefense) == 0)
		//	{
		//		_specialization.Defense = FileHelper.ParseRange(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.JobAbsortion) == 0)
		//	{
		//		_specialization.Absortion = FileHelper.ParseRangeF(line, ref position);
		//	}
		//	else if (string.Compare(keyword, Keywords.JobBlockRate) == 0)
		//	{
		//		_specialization.BlockRate = FileHelper.ParseFloat(line, ref position);
		//	}

		//	else if (string.Compare(keyword, Keywords.JobMovementSpeed) == 0)
		//	{
		//		_specialization.MovementSpeed = FileHelper.ParseRangeF(line, ref position);
		//	}

		//	else if (string.Compare(keyword, Keywords.JobMagicMastery) == 0)
		//	{
		//		_specialization.MagicMastery = FileHelper.ParseRange(line, ref position);
		//	}

		//	else if ((string.Compare(keyword, Keywords.JobAdditionalHP[0]) == 0) ||
		//			 (string.Compare(keyword, Keywords.JobAdditionalHP[1]) == 0))
		//	{
		//		_specialization.Additional.Hp = FileHelper.ParseRangeF(line, ref position);
		//	}
		//	else if ((string.Compare(keyword, Keywords.JobAdditionalMP[0]) == 0) ||
		//			 (string.Compare(keyword, Keywords.JobAdditionalMP[1]) == 0))
		//	{
		//		_specialization.Additional.Mp = FileHelper.ParseRangeF(line, ref position);
		//	}
		//	//else if ((string.Compare(keyword, "**마나추가") == 0) ||
		//	//		 (string.Compare(keyword, "**기력추가") == 0))
		//	//{
		//	//	_specialization.Additional.Sp = FileHelper.ParseRangeF(line, ref position);
		//	//}
		//	else if ((string.Compare(keyword, Keywords.JobRegenerationHP[0]) == 0) ||
		//			 (string.Compare(keyword, Keywords.JobRegenerationHP[1]) == 0))
		//	{
		//		_specialization.Regeneration.Hp = FileHelper.ParseRangeF(line, ref position);
		//	}
		//	else if ((string.Compare(keyword, Keywords.JobRegenerationMP[0]) == 0) ||
		//			 (string.Compare(keyword, Keywords.JobRegenerationMP[1]) == 0))
		//	{
		//		_specialization.Regeneration.Mp = FileHelper.ParseRangeF(line, ref position);
		//	}
		//	//else if ((string.Compare(keyword, "**마나추가") == 0) ||
		//	//		 (string.Compare(keyword, "**기력추가") == 0))
		//	//{
		//	//	_specialization.Regeneration.Sp = FileHelper.ParseRangeF(line, ref position);
		//	//}

		//	// Zhoon File
		//	else if (string.Compare(keyword, Keywords.ExternalFile) == 0)
		//	{
		//		ExternalFile = FileHelper.ParseString(line, ref position);

		//		var path = Path.Combine(Globals.OpenItemPath, ExternalFile);
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
			long ParseLong() => FileHelper.ParseLong(line, ref position);
			Range ParseRange() => FileHelper.ParseRange(line, ref position);
			RangeF ParseRangeF() => FileHelper.ParseRangeF(line, ref position);

			switch (keyword)
			{
				// Identifiers
				case var text when text.Equals(Keywords.Id):
					Id = ParseString();
					break;

				case var text when text.StartsWith('*') && text.Contains("NAME", StringComparison.OrdinalIgnoreCase):
					var name = ParseString();
					if (!string.IsNullOrEmpty(name))
						Name = name;
					break;

				case var text when text.Equals(Keywords.ServerName):
					ServerName = ParseString();
					break;

				// Misc
				case var text when text.Equals(Keywords.Durability):
					Durability = ParseRange();
					break;

				case var text when text.Equals(Keywords.Weight):
					Weight = ParseInteger();
					break;

				case var text when text.Equals(Keywords.Price):
					Price = ParseInteger();
					break;

				// Unique Items
				case var text when text.Equals(Keywords.Unique):
					Unique = ParseLong();
					break;
					
				case var text when text.Equals(Keywords.DispEffect):
					DispEffect = ParseLong();
					break;
					
				case var text when text.Equals(Keywords.EffectColor):
				{
					int R = ParseInteger();
					int G = ParseInteger();
					int B = ParseInteger();
					int A = ParseInteger();
					int effectBlink = ParseInteger();
					int effectBlinkMax = (R + G + B + A + (int)DispEffect);

					ScaleBlink = ParseInteger();
					EffectColor = Color.FromArgb(A, R, G, B);
					EffectBlink = new Range(effectBlink, effectBlinkMax);

					break;
				}

				// Attack properties
				case var text when text.Equals(Keywords.AttackPower):
					AttackPowerMin = ParseRange();
					AttackPowerMax = ParseRange();
					break;

				case var text when text.Equals(Keywords.AttackRange):
					AttackRange = ParseInteger();
					break;

				case var text when text.Equals(Keywords.AttackSpeed):
					AttackRange = ParseInteger();
					break;

				case var text when text.Equals(Keywords.AttackRate):
					AttackRate = ParseRange();
					break;

				case var text when text.Equals(Keywords.CriticalRate):
					CriticalRate = ParseInteger();
					break;

				// Defense properties
				case var text when text.Equals(Keywords.Defense):
					Defense = ParseRange();
					break;
					
				case var text when text.Equals(Keywords.Absortion):
					Absortion = ParseRangeF();
					break;
					
				case var text when text.Equals(Keywords.BlockRate):
					BlockRate = ParseRangeF();
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
				case var text when text.Equals(Keywords.MovementSpeed):
					MovementSpeed = ParseRangeF();
					break;

				// Potions properties
				case var text when text.Equals(Keywords.PotionSpace):
					PotionSpace = ParseInteger();
					break;

				case var text when text.Equals(Keywords.PotionHP[0]) || text.Equals(Keywords.PotionHP[1]):
					_potionRecovery.Hp = ParseRangeF();
					break;

				case var text when text.Equals(Keywords.PotionMP[0]) || text.Equals(Keywords.PotionMP[1]):
					_potionRecovery.Mp = ParseRangeF();
					break;

				case var text when text.Equals(Keywords.PotionSP[0]) || text.Equals(Keywords.PotionSP[1]):
					_potionRecovery.Sp = ParseRangeF();
					break;

				// Skills training
				case var text when text.Equals(Keywords.MagicMastery[0]) || text.Equals(Keywords.MagicMastery[1]):
					MagicMastery = ParseRange();
					break;

				// Status additional
				case var text when text.Equals(Keywords.AdditionalHP[0]) || text.Equals(Keywords.AdditionalHP[1]):
					_additional.Hp = ParseRangeF();
					break;

				case var text when text.Equals(Keywords.AdditionalMP[0]) || text.Equals(Keywords.AdditionalMP[1]):
					_additional.Mp = ParseRangeF();
					break;

				case var text when text.Equals(Keywords.AdditionalSP[0]) || text.Equals(Keywords.AdditionalSP[1]):
					_additional.Sp = ParseRangeF();
					break;

				// Status regeneration
				case var text when text.Equals(Keywords.RegenerationHP[0]) || text.Equals(Keywords.RegenerationHP[1]):
					_regeneration.Hp = ParseRangeF();
					break;

				case var text when text.Equals(Keywords.RegenerationMP[0]) || text.Equals(Keywords.RegenerationMP[1]):
					_regeneration.Mp = ParseRangeF();
					break;

				case var text when text.Equals(Keywords.RegenerationSP[0]) || text.Equals(Keywords.RegenerationSP[1]):
					_regeneration.Sp = ParseRangeF();
					break;

				// Requirements for usage
				case var text when text.Equals(Keywords.Level):
					_requeriments.Level = ParseInteger();
					break;
					
				case var text when text.Equals(Keywords.Strength):
					_requeriments.Strength = ParseInteger();
					break;
					
				case var text when text.Equals(Keywords.Intelligence):
					_requeriments.Intelligence = ParseInteger();
					break;
					
				case var text when text.Equals(Keywords.Talent):
					_requeriments.Talent = ParseInteger();
					break;
					
				case var text when text.Equals(Keywords.Dexterity):
					_requeriments.Dexterity = ParseInteger();
					break;
					
				case var text when text.Equals(Keywords.Health):
					_requeriments.Health = ParseInteger();
					break;

				// Specialization
				case var text when text.Equals(Keywords.MainJob):
				{
					string spec = FileHelper.GetWord(line, ref position);

					MainJob = Job.Parse(spec);

					break;
				}
				
				case var text when text.Equals(Keywords.AvailableJobs):
				{
					string specs = FileHelper.GetWord(line, ref position);

					while (!string.IsNullOrEmpty(specs))
					{
						var job = Job.Parse(specs);
						if (job != JobType.Unknown)
							AvailableJobs.Add(job);

						specs = FileHelper.GetWord(line, ref position);
					}

					break;
				}

				// Specialization Attack properties
				case var text when text.Equals(Keywords.JobAttackPower):
					_specialization.AttackPower = ParseRange();
					break;

				case var text when text.Equals(Keywords.JobAttackRange):
					_specialization.AttackRange = ParseInteger();
					break;

				case var text when text.Equals(Keywords.JobAttackSpeed):
					_specialization.AttackRange = ParseInteger();
					break;

				case var text when text.Equals(Keywords.JobAttackRate):
					_specialization.AttackRate = ParseRange();
					break;

				case var text when text.Equals(Keywords.JobCriticalRate):
					_specialization.CriticalRate = ParseInteger();
					break;

				// Specialization Defense properties
				case var text when text.Equals(Keywords.JobDefense):
					_specialization.Defense = ParseRange();
					break;

				case var text when text.Equals(Keywords.JobAbsortion):
					_specialization.Absortion = ParseRangeF();
					break;

				case var text when text.Equals(Keywords.JobBlockRate):
					_specialization.BlockRate = ParseFloat();
					break;

				// Specialization Movement properties
				case var text when text.Equals(Keywords.JobMovementSpeed):
					_specialization.MovementSpeed = ParseRangeF();
					break;

				// Specialization Skills training
				case var text when text.Equals(Keywords.JobMagicMastery):
					_specialization.MagicMastery = ParseRange();
					break;

				// Specialization Status additional
				case var text when text.Equals(Keywords.JobAdditionalHP[0]) || text.Equals(Keywords.JobAdditionalHP[1]):
					_specialization.Additional.Hp = ParseRangeF();
					break;

				case var text when text.Equals(Keywords.JobAdditionalMP[0]) || text.Equals(Keywords.JobAdditionalMP[1]):
					_specialization.Additional.Mp = ParseRangeF();
					break;

				// Specialization Status regeneration
				case var text when text.Equals(Keywords.JobRegenerationHP[0]) || text.Equals(Keywords.JobRegenerationHP[1]):
					_specialization.Regeneration.Hp = ParseRangeF();
					break;

				case var text when text.Equals(Keywords.JobRegenerationMP[0]) || text.Equals(Keywords.JobRegenerationMP[1]):
					_specialization.Regeneration.Mp = ParseRangeF();
					break;

				// External file processing
				case var text when text.Equals(Keywords.ExternalFile):
					ExternalFile = ParseString();
					var path = Path.Combine(Globals.OpenItemPath, ExternalFile);
					if (File.Exists(path))
						Process(path);
					break;

				default:
					// Unknown keyword, handle appropriately if needed
					// Handle unknown keywords or skip them
					if (keyword.StartsWith('*'))
					{
						if (!UnhandledKeywords.Contains(keyword))
							UnhandledKeywords.Add(keyword);
					}
					break;
			}
		}

		public override void Save(string? fileName = null)
		{
			StringBuilder sb = new();

			sb.AppendLine("// Priston Tale - Item file");
			sb.AppendLine("// Identifiers");
			sb.AppendLine($"{Keywords.Id}\t\t\"{Id}\"");
			sb.AppendLine($"{Keywords.ServerName}\t\t\"{ServerName}\"");
			sb.AppendLine($"{Keywords.Name[0]}\t\t\"{Name}\"");
			sb.AppendLine();

			sb.AppendLine("// Misc");
			sb.AppendLine($"{Keywords.Durability}\t\t{Durability.Min} {Durability.Max}");
			sb.AppendLine($"{Keywords.Weight}\t\t{Weight}");
			sb.AppendLine($"{Keywords.Price}\t\t{Price}");
			sb.AppendLine();

			sb.AppendLine("// Unique Items");
			
			if (Unique != 0)
				sb.AppendLine($"{Keywords.Unique}\t\t{Unique}");

			if (DispEffect != 0)
				sb.AppendLine($"{Keywords.DispEffect}\t\t{DispEffect}");

			if (EffectBlink.Min != EffectBlink.Max)
				sb.AppendLine($"{Keywords.EffectColor}\t\t{EffectColor.R} {EffectColor.G} {EffectColor.B} {EffectColor.A}\t{EffectBlink.Min}\t{ScaleBlink}");

			sb.AppendLine();

			sb.AppendLine("// Offensive power");
			sb.AppendLine($"{Keywords.AttackPower}\t\t{AttackPowerMin.Min} {AttackPowerMin.Max}\t{AttackPowerMax.Min} {AttackPowerMax.Max}");
			sb.AppendLine($"{Keywords.AttackRange}\t\t{AttackRange}");
			sb.AppendLine($"{Keywords.AttackSpeed}\t\t{AttackSpeed}");
			sb.AppendLine($"{Keywords.AttackSpeed}\t\t{AttackSpeed}");
			sb.AppendLine($"{Keywords.AttackRate}\t\t{AttackRate.Min} {AttackRate.Max}");
			sb.AppendLine($"{Keywords.CriticalRate}\t\t{CriticalRate}");
			sb.AppendLine();

			sb.AppendLine("// Defensive power");
			sb.AppendLine($"{Keywords.Defense}\t\t{Defense.Min} {Defense.Max}");
			sb.AppendLine($"{Keywords.Absortion}\t\t{Absortion.Min} {Absortion.Max}");
			sb.AppendLine($"{Keywords.BlockRate}\t\t{BlockRate.Min} {BlockRate.Max}");
			sb.AppendLine();

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

			sb.AppendLine("// Mobility");
			sb.AppendLine($"{Keywords.MovementSpeed}\t\t{MovementSpeed.Min} {MovementSpeed.Max}");
			sb.AppendLine();

			sb.AppendLine("// Potions");
			sb.AppendLine($"{Keywords.PotionSpace}\t\t{PotionSpace}");
			sb.AppendLine($"{Keywords.PotionHP[0]}\t\t{PotionRecovery.Hp.Min} {PotionRecovery.Hp.Max}");
			sb.AppendLine($"{Keywords.PotionMP[0]}\t\t{PotionRecovery.Mp.Min} {PotionRecovery.Mp.Max}");
			sb.AppendLine($"{Keywords.PotionSP[0]}\t\t{PotionRecovery.Sp.Min} {PotionRecovery.Sp.Max}");
			sb.AppendLine();

			sb.AppendLine("// Skills training");
			sb.AppendLine($"{Keywords.MagicMastery[0]}\t\t{MagicMastery.Min} {MagicMastery.Max}");
			sb.AppendLine();

			sb.AppendLine("// Status increase/regeneration");
			sb.AppendLine($"{Keywords.AdditionalHP[0]}\t\t{Additional.Hp.Min} {Additional.Hp.Max}");
			sb.AppendLine($"{Keywords.AdditionalMP[0]}\t\t{Additional.Mp.Min} {Additional.Mp.Max}");
			sb.AppendLine($"{Keywords.AdditionalSP[0]}\t\t{Additional.Sp.Min} {Additional.Sp.Max}");
			sb.AppendLine($"{Keywords.RegenerationHP[0]}\t\t{Regeneration.Hp.Min} {Regeneration.Hp.Max}");
			sb.AppendLine($"{Keywords.RegenerationMP[0]}\t\t{Regeneration.Mp.Min} {Regeneration.Mp.Max}");
			sb.AppendLine($"{Keywords.RegenerationSP[0]}\t\t{Regeneration.Sp.Min} {Regeneration.Sp.Max}");
			sb.AppendLine();

			sb.AppendLine("// Requirements for usage");
			sb.AppendLine($"{Keywords.Level}\t\t{_requeriments.Level}");
			sb.AppendLine($"{Keywords.Strength}\t\t{_requeriments.Strength}");
			sb.AppendLine($"{Keywords.Intelligence}\t\t{_requeriments.Intelligence}");
			sb.AppendLine($"{Keywords.Talent}\t\t{_requeriments.Talent}");
			sb.AppendLine($"{Keywords.Dexterity}\t\t{_requeriments.Dexterity}");
			sb.AppendLine($"{Keywords.Health}\t\t{_requeriments.Health}");
			sb.AppendLine();

			sb.AppendLine("// Specialization");
			sb.AppendLine($"{Keywords.MainJob}\t\t{MainJob}");
			sb.AppendLine($"{Keywords.AvailableJobs}\t\t{string.Join(' ', AvailableJobs)}");
			sb.AppendLine();
			sb.AppendLine("// Spec Offensive");
			sb.AppendLine($"{Keywords.JobAttackPower}\t\t{_specialization.AttackPower.Min} {_specialization.AttackPower.Max}");
			sb.AppendLine($"{Keywords.JobAttackRate}\t\t{_specialization.AttackRate.Min} {_specialization.AttackRate.Max}");
			sb.AppendLine($"{Keywords.JobAttackSpeed}\t\t{_specialization.AttackSpeed}");
			sb.AppendLine($"{Keywords.JobAttackRange}\t\t{_specialization.AttackRange}");
			sb.AppendLine($"{Keywords.JobCriticalRate}\t\t{_specialization.CriticalRate}");
			sb.AppendLine("// Spec Defensive");
			sb.AppendLine($"{Keywords.JobDefense}\t\t{_specialization.Defense.Min} {_specialization.Defense.Max}");
			sb.AppendLine($"{Keywords.JobAbsortion}\t\t{_specialization.Absortion.Min} {_specialization.Absortion.Max}");
			sb.AppendLine($"{Keywords.JobBlockRate}\t\t{_specialization.BlockRate}");
			sb.AppendLine("// Spec MoveSpeed");
			sb.AppendLine($"{Keywords.JobMovementSpeed}\t\t{_specialization.MovementSpeed.Min} {_specialization.MovementSpeed.Max}");
			sb.AppendLine("// Spec Skill training");
			sb.AppendLine($"{Keywords.JobMagicMastery}\t\t{_specialization.MagicMastery.Min} {_specialization.MagicMastery.Max}");
			sb.AppendLine();
			sb.AppendLine("// Spec Status increase/regeneration");
			sb.AppendLine($"{Keywords.JobAdditionalHP[0]}\t\t{_specialization.Additional.Hp.Min} {_specialization.Additional.Hp.Max}");
			sb.AppendLine($"{Keywords.JobAdditionalMP[0]}\t\t{_specialization.Additional.Mp.Min} {_specialization.Additional.Mp.Max}");
			sb.AppendLine($"{Keywords.JobRegenerationHP[0]}\t\t{_specialization.Regeneration.Hp.Min} {_specialization.Regeneration.Hp.Max}");
			sb.AppendLine($"{Keywords.JobRegenerationMP[0]}\t\t{_specialization.Regeneration.Mp.Min} {_specialization.Regeneration.Mp.Max}");
			sb.AppendLine();

			sb.AppendLine("// Zhoon File");
			sb.AppendLine($"{Keywords.ExternalFile}\t\t\"{ExternalFile}\"");
			sb.AppendLine();

			using var sw = new StreamWriter(fileName ?? FileName, false, Encoding);
			sw.Write(sb);
			sw.Flush();

			var path = Path.Combine(Globals.OpenItemPath, ExternalFile);
			using var zhoon = new StreamWriter(path);

			zhoon.WriteLine("// Priston Tale - Item file");

			var fi = new FileInfo(fileName ?? FileName);
			zhoon.WriteLine($"// {fi.Name}");
			zhoon.WriteLine();

			var langName = FileHelper.GetInfoName(Globals.Settings.Language);
			zhoon.WriteLine($"{langName}\t\t\"{Name}\"");
			zhoon.WriteLine();
			zhoon.Flush();
		}

		//public override int GetHashCode()
		//{
		//	int hash = base.GetHashCode();

		//	hash += Id.GetHashCode() & 0xFF;
		//	hash += Name.GetHashCode() & 0xFF;
		//	hash += ServerName.GetHashCode() & 0xFF;

		//	hash += Durability.GetHashCode() & 0xFF;
		//	hash += Weight.GetHashCode() & 0xFF;
		//	hash += Price.GetHashCode() & 0xFF;

		//	hash += Unique.GetHashCode() & 0xFF;
		//	hash += ScaleBlink.GetHashCode() & 0xFF;
		//	hash += EffectBlink.GetHashCode() & 0xFF;
		//	hash += EffectColor.GetHashCode() & 0xFF;
		//	hash += DispEffect.GetHashCode() & 0xFF;

		//	hash += AttackPowerMin.GetHashCode() & 0xFF;
		//	hash += AttackPowerMax.GetHashCode() & 0xFF;
		//	hash += AttackRange.GetHashCode() & 0xFF;
		//	hash += AttackSpeed.GetHashCode() & 0xFF;
		//	hash += AttackRate.GetHashCode() & 0xFF;
		//	hash += CriticalRate.GetHashCode() & 0xFF;

		//	hash += Defense.GetHashCode() & 0xFF;
		//	hash += Absortion.GetHashCode() & 0xFF;
		//	hash += BlockRate.GetHashCode() & 0xFF;
		//	hash += Resistance.GetHashCode() & 0xFF;

		//	hash += MoveSpeed.GetHashCode() & 0xFF;

		//	hash += PotionSpace.GetHashCode() & 0xFF;
		//	hash += PotionRecovery.GetHashCode() & 0xFF;

		//	hash += MagicMastery.GetHashCode() & 0xFF;

		//	hash += Additional.GetHashCode() & 0xFF;
		//	hash += Regeneration.GetHashCode() & 0xFF;

		//	hash += Requeriments.GetHashCode() & 0xFF;

		//	hash += MainJob.GetHashCode() & 0xFF;
		//	hash += AvailableJobs.GetHashCode() & 0xFF;
		//	hash += Specialization.GetHashCode() & 0xFF;

		//	return hash;
		//}

		//public void CopyTo(ref Item output)
		//{
		//	output.Id = Id;
		//	output.Name = Name;
		//	output.ServerName = ServerName;

		//	output.Durability = Durability;
		//	output.Weight = Weight;
		//	output.Price = Price;

		//	output.Unique = Unique;
		//	output.ScaleBlink = ScaleBlink;
		//	output.EffectBlink = EffectBlink;
		//	output.EffectColor = EffectColor;
		//	output.DispEffect = DispEffect;

		//	output.AttackPowerMin = AttackPowerMin;
		//	output.AttackPowerMax = AttackPowerMax;
		//	output.AttackRange = AttackRange;
		//	output.AttackSpeed = AttackSpeed;
		//	output.AttackRate = AttackRate;
		//	output.CriticalRate = CriticalRate;

		//	output.Defense = Defense;
		//	output.Absortion = Absortion;
		//	output.BlockRate = BlockRate;
		//	output.Resistance = Resistance;

		//	output.MoveSpeed = MoveSpeed;

		//	output.PotionSpace = PotionSpace;
		//	output.PotionRecovery = PotionRecovery;

		//	output.MagicMastery = MagicMastery;

		//	output.Additional = Additional;
		//	output.Regeneration = Regeneration;

		//	output.Requeriments = Requeriments;

		//	output.MainJob = MainJob;
		//	output.AvailableJobs = AvailableJobs;
		//	output.Specialization = Specialization;
		//}
	}

	#endregion
}
