namespace GameServer.Framework
{
	public enum JobType
	{
		Unknown,

		Fighter,
		Mechanician,
		Archer,
		Pikeman,

		Atalanta,
		Knight,
		Magician,
		Priestess,

		Assassin,
		Shaman,
		MartialArtist,
		PureBody,
	}

	public static class Job
	{
		public static JobType Parse(string str)
		{
			foreach (JobType job in Enum.GetValues(typeof(JobType)))
			{
				if (str.ToLower().Contains(job.ToString().ToLower()))
					return job;
			}

			return JobType.Unknown;
		}
	}
}
