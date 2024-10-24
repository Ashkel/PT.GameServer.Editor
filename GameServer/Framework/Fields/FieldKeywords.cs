namespace GameServer.Framework.Fields;

public partial class Field
{
	public static class Keywords
	{
		public static readonly string[] MaxFlags = new string[] { "*MAX_ACTOR_POS", "*최대동시출현수" };
		public static readonly string[] MonstersPerFlag = new string[] { "*MAX_ACTOR", "*출현수" };
		public static readonly string[] SpawnDelay = new string[] { "*DELAY", "*출현간격" };
		public static readonly string[] Monsters = new string[] { "*ACTOR", "*출연자" };
		public static readonly string[] Bosses = new string[] { "*BOSS_ACTOR", "*출연자두목" };
	}

}