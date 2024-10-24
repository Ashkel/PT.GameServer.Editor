using GameServer.Framework.Characters;
using GameServer.Helpers;
using System.Xml.Linq;

namespace GameServer.Framework.Fields;

public partial class Field
{
	public class MonsterSettings : GameInfo
	{
		#region Field/Properties

		public int MaxFlags { get; set; }
		public int MonstersPerFlag { get; set; }
		public Range SpawnDelay { get; set; }

		public readonly List<Actor> Monsters = new();
		public readonly List<BossActor> Bosses = new();

        #endregion


        #region Constructor(s)

        public MonsterSettings()
        {
			Reset();
        }

        #endregion


        #region GameInfo Methods

        public override void Reset()
		{
			MaxFlags = 0;
			MonstersPerFlag = 0;
			SpawnDelay = Range.Empty;

			Monsters.Clear();
			Bosses.Clear();
		}

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

			switch (keyword)
			{
				// Identifiers
				case var text when text.Equals(Keywords.MaxFlags[0]) || text.Equals(Keywords.MaxFlags[1]):
					MaxFlags = ParseInteger();
					break;

				case var text when text.Equals(Keywords.MonstersPerFlag[0]) || text.Equals(Keywords.MonstersPerFlag[1]):
					MonstersPerFlag = ParseInteger();
					break;

				case var text when text.Equals(Keywords.SpawnDelay[0]) || text.Equals(Keywords.SpawnDelay[1]):
					SpawnDelay = ParseRange();
					break;

				case var text when text.Equals(Keywords.Monsters[0]) || text.Equals(Keywords.Monsters[1]):
				{
					var monster = new Actor
					{
						Name = ParseString(),
						Rate = ParseInteger(),
					};

					Monsters.Add(monster);

					break;
				}

				case var text when text.Equals(Keywords.Bosses[0]) || text.Equals(Keywords.Bosses[1]):
				{
					var boss = new BossActor
					{
						Name = ParseString(),
						ServantName = ParseString(),
						ServantCount = ParseInteger(),
						Time = new List<int>(),
					};

					string buffer;

					do
					{
						buffer = FileHelper.GetWord(line, ref position);

						if (int.TryParse(buffer, out int result))
						{
							boss.Time.Add(result);
						}
					}
					while (!string.IsNullOrEmpty(buffer));

					Bosses.Add(boss);

					break;
				}

				default:
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
			
		}


		#endregion


		#region Helper methods

		#endregion
	}
}
