namespace GameServer.Framework.Characters;

public partial class NPC
{
	public static class Keywords
	{
		// Messages
		public static readonly string[] Chat = new string[] { "*대화", "*A_CHAT", "*B_CHAT", "*C_CHAT", "*E_CHAT", "*J_CHAT", "*T_CHAT", "*TH_CHAT", "*V_CHAT" };

		// NPC Functions

		// Merchant
		public static readonly string SellAttackItem = "*무기판매";
		public static readonly string SellDefenseItem = "*방어구판매";
		public static readonly string SellEtcItem = "*잡화판매";

		// Skill Master
		public static readonly string SkillMaster = "*스킬수련";
		public static readonly string SkillChangeJob = "*직업전환";

		public static readonly string EventNPC = "*이벤트매표소";
		public static readonly string WarehouseMaster = "*아이템보관";
		public static readonly string ItemMix = "*아이템조합";
		public static readonly string ForceMaster = "*아이템연금";
		public static readonly string Smelting = "*아이템제련";
		public static readonly string Manufacture = "*아이템제작";
		public static readonly string ItemAging = "*아이템에이징";
		public static readonly string MixtureReset = "*믹스처리셋";
		public static readonly string CollectMoney = "*모금함";
		//public static readonly string WowEvent = "*꽝이지롱";
		public static readonly string EventGirl = "*경품추첨";
		public static readonly string ClanMaster = "*클랜기능";
		public static readonly string GiftExpress = "*경품배달";
		public static readonly string WingQuest = "*윙퀘스트";
		public static readonly string PuzzleQuest = "*퀘스트이벤트";
		public static readonly string StarPoint = "*별포인트적립";
		public static readonly string DonationBox = "*기부함";
		public static readonly string Teleport = "*텔레포트";
		public static readonly string BlessCastle = "*블레스캐슬";
		public static readonly string Polling = "*설문조사";
		public static readonly string MediaPlayTitle = "*동영상제목";
		public static readonly string MediaPlayPath = "*동영상경로";
		public static readonly string OpenCount = "*출현간격";
		public static readonly string QuestCode = "*퀘스트코드";
	}
}
