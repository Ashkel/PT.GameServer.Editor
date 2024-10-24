namespace GameServer.Framework.Fields;

public struct Actor
{
	#region Field/Properties

	public string Name;
	public int Rate;

	#endregion

	#region Constructor(s)

	public Actor()
	{
		Name = string.Empty;
		Rate = 0;
	}

	#endregion
}

public struct BossActor
{
	#region Field/Properties

	public string Name;
	public string ServantName;
	public int ServantCount;

	public List<int> Time;

	#endregion


	#region Constructor(s)

	public BossActor()
	{
		Name = string.Empty;
		ServantName = string.Empty;
		ServantCount = 0;
		Time = new List<int>();
	}

	#endregion
}
