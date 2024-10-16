namespace GameServer.Framework.Characters;

public enum ShadowSize
{
	Unknown,
	Small,
	Medium,
	Large,
	Giant,
};

public enum MonsterClass
{
	Normal = 0,
	Boss = 1,
	Hammer = 200,
	Ghost = 300
};

public enum MonsterType
{
	Normal,
	Undead,
	Mutant,
	Demon,
	Mechanic
}

public enum MonsterNature
{
	Neutral,
	Good,
	Evil
}

public enum ActiveTime
{
	All = 0,
	Day = 1,
	Night = -1
}

public partial class Monster
{
	private static readonly Dictionary<string, ShadowSize> ShadowSizes = new()
	{
		{ "소형", ShadowSize.Small },
		{ "중형" , ShadowSize.Medium },
		{ "중대형" , ShadowSize.Large },
		{ "대형" , ShadowSize.Giant },
	};

	private static readonly Dictionary<string, MonsterNature> MonsterNatures = new()
	{
		{ "good", MonsterNature.Good },
		{ "evil", MonsterNature.Evil }
	};

	private static readonly Dictionary<string, MonsterType> MonsterTypes = new()
	{
		{ "언데드", MonsterType.Undead },
		{ "뮤턴트", MonsterType.Mutant },
		{ "디먼", MonsterType.Demon },
		{ "메카닉", MonsterType.Mechanic },
	};

	private static readonly Dictionary<string, ActiveTime> ActiveTimes = new()
	{
		{ "낮", ActiveTime.Day },
		{ "밤", ActiveTime.Night },
	};

	public static Enum Parse<T>(string key) where T : struct, Enum
	{
		if (string.IsNullOrEmpty(key))
			return default(T);

		object obj = typeof(T) switch
		{
			Type type when type == typeof(ShadowSize) => ShadowSizes,
			Type type when type == typeof(MonsterNature) => MonsterNatures,
			Type type when type == typeof(MonsterType) => MonsterTypes,
			Type type when type == typeof(ActiveTime) => ActiveTimes,
			_ => throw new TypeAccessException(),
		};

		if (obj is Dictionary<string, T> keyValues &&
			keyValues.ContainsKey(key))
		{
			return (T)keyValues[key];
		}

		return default(T);
	}

	public static string Parse<T>(T value) where T : Enum
	{
		object obj = typeof(T) switch
		{
			Type type when type == typeof(ShadowSize) => ShadowSizes,
			Type type when type == typeof(MonsterNature) => MonsterNatures,
			Type type when type == typeof(MonsterType) => MonsterTypes,
			Type type when type == typeof(ActiveTime) => ActiveTimes,
			_ => throw new TypeAccessException(),
		};

        if (obj is Dictionary<string, T> keyValues)
        {
            foreach (var pair in keyValues)
            {
                if (EqualityComparer<T>.Default.Equals(pair.Value, value))
                {
                    return pair.Key;
                }
            }
        }

        return string.Empty;
	}
}