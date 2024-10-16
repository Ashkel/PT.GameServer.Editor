namespace GameServer.Framework.Items;

public partial class Item
{
	public static class Keywords
	{
		// Identifiers
		public static readonly string Id = "*코드";
		public static readonly string ServerName = "*이름";

		// Misc
		public static readonly string Durability = "*내구력";
		public static readonly string Weight = "*무게";
		public static readonly string Price = "*가격";

		// Unique Items
		public static readonly string Unique = "*유니크";
		public static readonly string DispEffect = "*이펙트설정";
		public static readonly string EffectColor = "*유니크색상";

		// Offensive power
		public static readonly string AttackPower = "*공격력";
		public static readonly string AttackRange = "*사정거리";
		public static readonly string AttackSpeed = "*공격속도";
		public static readonly string AttackRate = "*명중력";
		public static readonly string CriticalRate = "*크리티컬";

		// Defensive power
		public static readonly string Defense = "*방어력";
		public static readonly string Absortion = "*흡수력";
		public static readonly string BlockRate = "*블럭율";
		public static readonly string Organic = "*생체";
		public static readonly string Earth = "*대자연";
		public static readonly string Fire = "*불";
		public static readonly string Ice = "*냉기";
		public static readonly string Lightning = "*번개";
		public static readonly string Poison = "*독";
		public static readonly string Water = "*물";
		public static readonly string Wind = "*바람";

		// Mobility
		public static readonly string MoveSpeed = "*이동속도";

		// Potions
		public static readonly string PotionSpace = "*보유공간";
		public static readonly string[] PotionHP = new string[] { "*라이프상승", "*생명력상승" };
		public static readonly string[] PotionMP = new string[] { "*마나상승", "*기력상승" };
		public static readonly string[] PotionSP = new string[] { "*스테미너상승", "*근력상승" };

		// Skills training
		public static readonly string MagicMastery = "*마법숙련도";

		// Status increase/regeneration
		public static readonly string[] AdditionalHP = new string[] { "*라이프추가", "*생명력추가" };
		public static readonly string[] AdditionalMP = new string[] { "*마나추가", "*기력추가" };
		public static readonly string[] AdditionalSP = new string[] { "*스테미나추가", "*근력추가" };
		public static readonly string[] RegenerationHP = new string[] { "*라이프재생", "*생명력재생" };
		public static readonly string[] RegenerationMP = new string[] { "*마나재생", "*기력재생" };
		public static readonly string[] RegenerationSP = new string[] { "*스테미나재생", "*근력재생" };

		// Requirements for usage
		public static readonly string Level = "*레벨";
		public static readonly string Strength = "*힘";
		public static readonly string Intelligence = "*정신력";
		public static readonly string Talent = "*재능";
		public static readonly string Dexterity = "*민첩성";
		public static readonly string Health = "*건강";

		// Specialization
		public static readonly string MainJob = "**특화";
		public static readonly string AvailableJobs = "**특화랜덤";

		public static readonly string JobAttackPower = "**공격력";
		public static readonly string JobAttackRate = "**명중력";
		public static readonly string JobAttackSpeed = "**공격속도";
		public static readonly string JobAttackRange = "**사정거리";
		public static readonly string JobCriticalRate = "**크리티컬";

		public static readonly string JobAbsortion = "**흡수력";
		public static readonly string JobDefense = "**방어력";
		public static readonly string JobBlockRate = "**블럭율";

		public static readonly string JobMoveSpeed = "**이동속도";

		public static readonly string JobMagicMastery = "**마법숙련도";


		public static readonly string[] JobAdditionalHP = new string[] { "**라이프추가", "**생명력추가" };
		public static readonly string[] JobAdditionalMP = new string[] { "**마나추가", "**기력추가" };

		public static readonly string[] JobRegenerationHP = new string[] { "**라이프재생", "**생명력재생" };
		public static readonly string[] JobRegenerationMP = new string[] { "**마나재생", "**기력재생" };



		// Zhoon File
		public static readonly string ExternalFile = "*연결파일";
	}
}