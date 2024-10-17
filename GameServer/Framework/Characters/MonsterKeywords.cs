namespace GameServer.Framework.Characters;

public partial class Monster
{
	public static class Keywords
	{
		// Identifiers
		public static readonly string[] Name = new string[] { "*이름", "*NAME", "*A_NAME", "*B_NAME", "*C_NAME", "*E_NAME", "*J_NAME", "*T_NAME", "*TH_NAME", "*V_NAME" };
		public static readonly string ServerName = "*이름";
		public static readonly string DistinctionCode = "*구별코드";

		// Appearance (3D Model Data)
		public static readonly string ModelFile = "*모양파일";
		public static readonly string ShadowSize = "*크기";
		public static readonly string ModelSize = "*모델크기";
		public static readonly string ModelEvent = "*예비모델";
		public static readonly string ArrowPosition = "*화면보정";

		// State
		public static readonly string State = "*속성";
		public static readonly string Level = "*레벨";
		public static readonly string IsBoss = "*두목";
		public static readonly string MonsterClass = "*계급";
		public static readonly string MonsterType = "*몬스터종족";
		public static readonly string ActiveTime = "*활동시간";
		public static readonly string MonsterNature = "*품성";
		public static readonly string GenerateGroup = "*조직";
		public static readonly string RealSight = "*시야";
		public static readonly string IQ = "*지능";
		public static readonly string IsUndead = "*언데드";
		public static readonly string[] Undead = new string[] { "유", "있음" };

		// Attack
		public static readonly string AttackPower = "*공격력";
		public static readonly string AttackSpeed = "*공격속도";
		public static readonly string AttackRange = "*공격범위";
		public static readonly string AttackRate = "*명중력";
		public static readonly string SkillDamage = "*기술공격력";
		public static readonly string SkillDistance = "*기술공격거리";
		public static readonly string SkillRange = "*기술공격범위";
		public static readonly string SkillRate = "*기술공격률";
		public static readonly string SkillCurse = "*저주기술";
		public static readonly string[] StunRate = new string[] { "*스턴율", "*스턴률" };
		public static readonly string SpecialAttackRate = "*특수공격률";

		// Defense
		public static readonly string Defense = "*방어력";
		public static readonly string Absorption = "*흡수율";
		public static readonly string BlockRate = "*블럭율";
		public static readonly string[] Life = new string[] { "*생명력", "*라이프" };
		public static readonly string Organic = "*생체";
		public static readonly string Water = "*물";
		public static readonly string Lightning = "*번개";
		public static readonly string Ice = "*얼음";
		public static readonly string Wind = "*바람";
		public static readonly string Earth = "*지동력";
		public static readonly string Fire = "*불";
		public static readonly string Poison = "*독";

		// Movement
		// MovementType is in-game disabled, but keep it anyways
		public static readonly string MovementType = "*이동타입";
		public static readonly string MovementSpeed = "*이동력";
		public static readonly string MovementRange = "*이동범위";

		// Potion
		public static readonly string PotionCount = "*물약보유수";
		public static readonly string PotionRate = "*물약보유률";

		// Sound Effects
		public static readonly string[] SoundCode = new string[] { "*소리", "*효과음" };

		// Loot
		public static readonly string EventCode = "*이벤트코드";
		public static readonly string EventInfo = "*이벤트정보";
		public static readonly string EventItem = "*이벤트아이템";

		public static readonly string Experience = "*경험치";

		public static readonly string AllSeeItem = "*아이템모두";
		public static readonly string FallItemMax = "*아이템카운터";

		public static readonly string LootNothing = "없음";
		public static readonly string LootMoney = "돈";
		public static readonly string LootCoin = "동전";

		// Item craft(ores, recipes)
		public static readonly string FallItemsPlus = "*추가아이템";
		public static readonly string FallItems = "*아이템";

		// Zhoon File
		public static readonly string ExternalFile = "*연결파일";

		// Chat of NPC's
		public static readonly string[] Chat = new string[] { "*대화", "*A_CHAT", "*B_CHAT", "*C_CHAT", "*E_CHAT", "*J_CHAT", "*T_CHAT", "*TH_CHAT", "*V_CHAT" };
	}
}