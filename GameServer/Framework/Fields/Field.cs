using GameServer.Framework.Characters;
using GameServer.Helpers;
using System.Text;
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
			StringBuilder sb = new();

			sb.AppendLine("// Priston Tale - SPM file");
			sb.AppendLine("// Settings");
			sb.AppendLine($"{Keywords.MaxFlags[0]}\t{MaxFlags}");
			sb.AppendLine($"{Keywords.MonstersPerFlag[0]}\t{MonstersPerFlag}");
			sb.AppendLine($"{Keywords.SpawnDelay[0]}\t{SpawnDelay.Min} {SpawnDelay.Max}");
			sb.AppendLine();

			sb.AppendLine("// Monsters");
            foreach (var monster in Monsters)
            {
				sb.AppendLine($"{Keywords.Monsters[0]}\t\"{monster.Name}\" {monster.Rate}");
			}

			sb.AppendLine();

			sb.AppendLine("// Bosses");
			foreach (var boss in Bosses)
			{
				sb.AppendLine($"{Keywords.Bosses[0]}\t\"{boss.Name}\" \"{boss.ServantName}\" {boss.ServantCount} {string.Join(' ', boss.Time)}");
			}

			sb.AppendLine();
		}


		#endregion


			#region Helper methods

			#endregion
	}

	public class SpawnSettings
	{
		#region Field/Properties

		public const int SPAWNPOINT_MAX = 200;

		public string FileName { get; set; } = string.Empty;

		public readonly List<Point> Points = new();

		#endregion


		#region Constructor(s)

		#endregion


		#region Helper methods

		public void Reset()
		{
			Points.Clear();
		}

		public void SetFile(string fileName) => FileName = fileName;

		public void Load(string? fileName = null)
		{
			try
			{
				using var fs = new FileStream(fileName ?? FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
				using var br = new BinaryReader(fs);

				int index = 0;

				do
				{
					int state = br.ReadInt32();
					if (state != 1)
						break;

					int x = br.ReadInt32();
					int y = br.ReadInt32();

					Points.Add(new Point(x, y));
				}
				while (index++ >= SPAWNPOINT_MAX || br.BaseStream.Position >= br.BaseStream.Length);
			}
			catch (Exception ex)
			{
				using var sw = new StreamWriter($"{GetType().Name}.log", true);
				sw.Write(ex.Format());
			}
		}

		public void Save(string? fileName = null)
		{
			try
			{
				if (!Points.Any())
					return;

				using var fs = new FileStream(fileName ?? FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
				using var bw = new BinaryWriter(fs);

				int index = 0;

				foreach (var point in Points)
				{
					bw.Write(1); // state
					bw.Write(point.X);
					bw.Write(point.Y);

					if (index++ >= SPAWNPOINT_MAX)
						break;
				}
			}
			catch (Exception ex)
			{
				using var sw = new StreamWriter($"{GetType().Name}.log", true);
				sw.Write(ex.Format());
			}
		}

		#endregion
	}

	public struct CharacterInfo
	{
		public string Name;
		public string BodyModel;
		public string HeadModel;
		public int HeadModelCode;

		public int ObjectSerial;

		public int ClanCode;
		public int State;
		public ShadowSize ShadowSize;
		public int SoundCode;

		public JobType JobType;
		public int Level;
		public int Strength;
		public int Intelligence;
		public int Talent;
		public int Dexterity;
		public int Health;

		public int Accuracy;
		public int AttackRate;
		public Range AttackPower;
		public int AttackSpeed;
		public int AttackRange;
		public int CriticalRate;

		public int Defense;
		public int BlockRate;
		public int Absorption;

		public int MovementSpeed;

		public int Sight;
		public Range Weight;

		public Elemental DefenseElemental;
		public Elemental AttackElemental;

		public Range Life;
		public Range Mana;
		public Range Stamina;

		public float RegenHP;
		public float RegenMP;
		public float RegenSP;

		public int Exp;
		public int NextExp;

		public int Money;

		public int MonsterLink;

		public int Brood;

		public int StatePoint;
		public byte[] UpdateInfo;

		public Range ArrowPosition;
		public int PotionSpace;

		public int LifeFunction;
		public int ManaFunction;
		public int StaminaFunction;
		public Range DamageFunction;

		public int RefomCode;

		public int ChangeJob;
		public int JobBitMask;

		public Range PlayerKilling;
		public Range PlayClass;

		public int ExpHigh;
		public int EventTime;
		public Range EventParam;

		public Range PresentItem;

		public Range GravityScrollCheck;

		public int[] Placeholder;

		public int LoginServerIP;
		public int LoginServerSafeKey;

		public Range Version;
	}

	public struct PlayerInfo
	{
		public int PacketCode;
		public int PacketSize;

		public CharacterInfo Info;

		public int ObjectSerial;

		public int X, Y, Z;
		public int AX, AY, AZ;
		public int State;
	}

	public class NpcSettings
	{
		#region Field/Properties

		public const int NPC_MAX = 100;
		public const int DATA_SIZE = 504;

		public readonly List<PlayerInfo> Characters = new();

		public string FileName { get; set; } = string.Empty;

		#endregion


		#region Helper methods

		public void Reset()
		{
			Characters.Clear();
		}

		public void SetFile(string fileName) => FileName = fileName;

		public void Load(string? fileName = null)
		{
			try
			{
				using var fs = new FileStream(fileName ?? FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
				using var br = new BinaryReader(fs);

				int index = 0;

				do
				{
					long offset = index * DATA_SIZE;
					
					br.BaseStream.Seek(offset, SeekOrigin.Begin);

					int size = br.ReadInt32();
					if (size == 0)
						continue;

					// smTRNAS_PLAYERINFO packet data
					var player = new PlayerInfo();

					player.PacketSize = size; // size
					player.PacketCode = br.ReadInt32(); // code

					// smCHAR_INFO smCharInfo
					var info = new CharacterInfo();
					
					info.Name = ReadString(br, 32);    // szName
					info.BodyModel = ReadString(br, 64);   // szModelName
					info.HeadModel = ReadString(br, 60);   // szModelName2
					info.HeadModelCode = br.ReadInt32(); // ModelNameCode2

					info.ObjectSerial = br.ReadInt32(); // dwObjectSerial

					info.ClanCode = br.ReadInt32(); // ClassClan
					info.State = br.ReadInt32(); // State
					info.ShadowSize = (ShadowSize)br.ReadInt32(); // SizeLevel
					info.SoundCode = br.ReadInt32(); // dwCharSoundCode

					info.JobType = (JobType)br.ReadInt32(); // JOB_CODE

					info.Level = br.ReadInt32(); // Level
					info.Strength = br.ReadInt32(); // Strength
					info.Intelligence = br.ReadInt32(); // Spirit
					info.Talent = br.ReadInt32(); // Talent
					info.Dexterity = br.ReadInt32(); // Dexterity
					info.Health = br.ReadInt32(); // Health

					info.Accuracy = br.ReadInt32(); // Accuracy
					info.AttackRate = br.ReadInt32(); // Attack_Rating

					int min = br.ReadInt32();
					int max = br.ReadInt32();
					info.AttackPower = new Range(Math.Min(min, max), Math.Max(min, max)); // Attack_Damage

					info.AttackSpeed = br.ReadInt32(); // Attack_Speed
					info.AttackRange = br.ReadInt32(); // Shooting_Range
					info.CriticalRate = br.ReadInt32(); // Critical_Hit

					info.Defense = br.ReadInt32(); // Defence
					info.BlockRate = br.ReadInt32(); // Chance_Block
					info.Absorption = br.ReadInt32(); // Absorption

					info.MovementSpeed = br.ReadInt32(); // Move_Speed

					info.Sight = br.ReadInt32(); // Sight

					min = br.ReadInt16();
					max = br.ReadInt16();
					info.Weight = new Range(Math.Min(min, max), Math.Max(min, max)); // Weight

					short organic = br.ReadInt16();
					short earth = br.ReadInt16();
					short fire = br.ReadInt16();
					short ice = br.ReadInt16();
					short lightning = br.ReadInt16();
					short poison = br.ReadInt16();
					short water = br.ReadInt16();
					short wind = br.ReadInt16();
					info.DefenseElemental = new Elemental() // Resistance
					{
						Organic = organic,
						Earth = earth,
						Fire = fire,
						Ice = ice,
						Lightning = lightning,
						Poison = poison,
						Water = water,
						Wind = wind
					};

					organic = br.ReadInt16();
					earth = br.ReadInt16();
					fire = br.ReadInt16();
					ice = br.ReadInt16();
					lightning = br.ReadInt16();
					poison = br.ReadInt16();
					water = br.ReadInt16();
					wind = br.ReadInt16();
					info.AttackElemental = new Elemental() // Attack_Resistance
					{
						Organic = organic,
						Earth = earth,
						Fire = fire,
						Ice = ice,
						Lightning = lightning,
						Poison = poison,
						Water = water,
						Wind = wind
					};

					min = br.ReadInt16();
					max = br.ReadInt16();
					info.Life = new Range(Math.Min(min, max), Math.Max(min, max)); // Life

					min = br.ReadInt16();
					max = br.ReadInt16();
					info.Mana = new Range(Math.Min(min, max), Math.Max(min, max)); // Mana

					min = br.ReadInt16();
					max = br.ReadInt16();
					info.Stamina = new Range(Math.Min(min, max), Math.Max(min, max)); // Stamina

					info.RegenHP = br.ReadSingle(); // Life_Regen
					info.RegenMP = br.ReadSingle(); // Mana_Regen
					info.RegenSP = br.ReadSingle(); // Stamina_Regen

					info.Exp = br.ReadInt32(); // Exp
					info.NextExp = br.ReadInt32(); // NextExp

					info.Money = br.ReadInt32(); // Money

					info.MonsterLink = br.ReadInt32(); // lpMonInfo

					info.Brood = br.ReadInt32(); // Brood

					info.StatePoint = br.ReadInt32(); // StatePoint

					info.UpdateInfo = new byte[4]; // bUpdateInfo
					info.UpdateInfo[0] = br.ReadByte();
					info.UpdateInfo[1] = br.ReadByte();
					info.UpdateInfo[2] = br.ReadByte();
					info.UpdateInfo[3] = br.ReadByte();

					min = br.ReadInt16();
					max = br.ReadInt16();
					info.ArrowPosition = new Range(Math.Min(min, max), Math.Max(min, max)); // ArrowPosi

					info.PotionSpace = br.ReadInt32(); // Potion_Space

					info.LifeFunction = br.ReadInt32(); // LifeFunction
					info.ManaFunction = br.ReadInt32(); // ManaFunction
					info.StaminaFunction = br.ReadInt32(); // StaminaFunction

					min = br.ReadInt16();
					max = br.ReadInt16();
					info.DamageFunction = new Range(Math.Min(min, max), Math.Max(min, max)); // DamageFunction

					info.RefomCode = br.ReadInt32(); // RefomCode

					info.ChangeJob = br.ReadInt32(); // ChangeJob
					info.JobBitMask = br.ReadInt32(); // JobBitMask

					min = br.ReadInt16();
					max = br.ReadInt16();
					info.PlayerKilling = new Range(Math.Min(min, max), Math.Max(min, max)); // wPlayerKilling

					min = br.ReadInt16();
					max = br.ReadInt16();
					info.PlayClass = new Range(Math.Min(min, max), Math.Max(min, max)); // wPlayClass

					info.ExpHigh = br.ReadInt32(); // Exp_High
					info.EventTime = br.ReadInt32(); // dwEventTime_T

					min = br.ReadInt16();
					max = br.ReadInt16();
					info.EventParam = new Range(Math.Min(min, max), Math.Max(min, max)); // sEventParam

					min = br.ReadInt16();
					max = br.ReadInt16();
					info.PresentItem = new Range(Math.Min(min, max), Math.Max(min, max)); // sPresentItem

					min = br.ReadInt16();
					max = br.ReadInt16();
					info.GravityScrollCheck = new Range(Math.Min(min, max), Math.Max(min, max)); // GravityScroolCheck

					int tempSize = 11;
					info.Placeholder = new int[tempSize]; // dwTemp

					for (int i = 0; i < tempSize; i++)
					{
						info.Placeholder[i] = br.ReadInt32();
					}

					info.LoginServerIP = br.ReadInt32(); // dwLoginServerIP
					info.LoginServerSafeKey = br.ReadInt32(); // dwLoginServerSafeKey

					min = br.ReadInt16();
					max = br.ReadInt16();
					info.Version = new Range(Math.Min(min, max), Math.Max(min, max)); // wVersion

					player.Info = info;

					player.ObjectSerial = br.ReadInt32(); // dwObjectSerial

					player.X = br.ReadInt32(); // x
					player.Y = br.ReadInt32(); // y
					player.Z = br.ReadInt32(); // z
					
					player.AX = br.ReadInt32(); // ax
					player.AY = br.ReadInt32(); // ay
					player.AZ = br.ReadInt32(); // az

					player.State = br.ReadInt32(); // state

					Characters.Add(player);
				}
				while (index++ < NPC_MAX || br.BaseStream.Position < br.BaseStream.Length);
			}
			catch (Exception ex)
			{
				using var sw = new StreamWriter($"{GetType().Name}.log", true);
				sw.Write(ex.Format());
			}
		}

		public void Save(string? fileName = null)
		{
			try
			{
				using var fs = new FileStream(fileName ?? FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
				using var bw = new BinaryWriter(fs);

				int index = 0;

				foreach (var info in Characters)
				{
					bw.Write(info.PacketSize);
					bw.Write(info.PacketCode);

					bw.Write(GetBytes(info.Info.Name, 32));
					bw.Write(GetBytes(info.Info.BodyModel, 64));
					bw.Write(GetBytes(info.Info.HeadModel, 60));
					bw.Write(info.Info.HeadModelCode);

					bw.Write(info.Info.ObjectSerial);

					bw.Write(info.Info.ClanCode);
					bw.Write(info.Info.State);
					bw.Write((int)info.Info.ShadowSize);
					bw.Write(info.Info.SoundCode);

					bw.Write((int)info.Info.JobType);

					bw.Write(info.Info.Level);
					bw.Write(info.Info.Strength);
					bw.Write(info.Info.Intelligence);
					bw.Write(info.Info.Talent);
					bw.Write(info.Info.Dexterity);
					bw.Write(info.Info.Health);

					bw.Write(info.Info.Accuracy);
					bw.Write(info.Info.AttackRate);
					bw.Write(info.Info.AttackPower.Min);
					bw.Write(info.Info.AttackPower.Max);
					bw.Write(info.Info.AttackSpeed);
					bw.Write(info.Info.AttackRange);
					bw.Write(info.Info.CriticalRate);

					bw.Write(info.Info.Defense);
					bw.Write(info.Info.BlockRate);
					bw.Write(info.Info.Absorption);

					bw.Write(info.Info.MovementSpeed);

					bw.Write(info.Info.Sight);

					bw.Write((short)info.Info.Weight.Min);
					bw.Write((short)info.Info.Weight.Max);

					bw.Write((short)info.Info.DefenseElemental.Organic);
					bw.Write((short)info.Info.DefenseElemental.Earth);
					bw.Write((short)info.Info.DefenseElemental.Fire);
					bw.Write((short)info.Info.DefenseElemental.Ice);
					bw.Write((short)info.Info.DefenseElemental.Lightning);
					bw.Write((short)info.Info.DefenseElemental.Poison);
					bw.Write((short)info.Info.DefenseElemental.Water);
					bw.Write((short)info.Info.DefenseElemental.Wind);

					bw.Write((short)info.Info.AttackElemental.Organic);
					bw.Write((short)info.Info.AttackElemental.Earth);
					bw.Write((short)info.Info.AttackElemental.Fire);
					bw.Write((short)info.Info.AttackElemental.Ice);
					bw.Write((short)info.Info.AttackElemental.Lightning);
					bw.Write((short)info.Info.AttackElemental.Poison);
					bw.Write((short)info.Info.AttackElemental.Water);
					bw.Write((short)info.Info.AttackElemental.Wind);

					bw.Write((short)info.Info.Life.Min);
					bw.Write((short)info.Info.Life.Max);
					
					bw.Write((short)info.Info.Mana.Min);
					bw.Write((short)info.Info.Mana.Max);
					
					bw.Write((short)info.Info.Stamina.Min);
					bw.Write((short)info.Info.Stamina.Max);

					bw.Write(info.Info.RegenHP);
					bw.Write(info.Info.RegenMP);
					bw.Write(info.Info.RegenSP);
					
					bw.Write(info.Info.Exp);
					bw.Write(info.Info.NextExp);

					bw.Write(info.Info.Money);

					bw.Write(info.Info.MonsterLink);

					bw.Write(info.Info.Brood);

					bw.Write(info.Info.StatePoint);
					
					bw.Write(info.Info.UpdateInfo);

					bw.Write((short)info.Info.ArrowPosition.Min);
					bw.Write((short)info.Info.ArrowPosition.Max);

					bw.Write(info.Info.PotionSpace);
					
					bw.Write(info.Info.LifeFunction);
					bw.Write(info.Info.ManaFunction);
					bw.Write(info.Info.StaminaFunction);

					bw.Write((short)info.Info.DamageFunction.Min);
					bw.Write((short)info.Info.DamageFunction.Max);

					bw.Write(info.Info.RefomCode);

					bw.Write(info.Info.ChangeJob);
					bw.Write(info.Info.JobBitMask);

					bw.Write((short)info.Info.PlayerKilling.Min);
					bw.Write((short)info.Info.PlayerKilling.Max);

					bw.Write((short)info.Info.PlayClass.Min);
					bw.Write((short)info.Info.PlayClass.Max);

					bw.Write(info.Info.ExpHigh);
					bw.Write(info.Info.EventTime);

					bw.Write((short)info.Info.EventParam.Min);
					bw.Write((short)info.Info.EventParam.Max);

					bw.Write((short)info.Info.PresentItem.Min);
					bw.Write((short)info.Info.PresentItem.Max);

					bw.Write((short)info.Info.GravityScrollCheck.Min);
					bw.Write((short)info.Info.GravityScrollCheck.Max);

					int tempSize = 11;
					for (int i = 0; i < tempSize; i++)
					{
						bw.Write(info.Info.Placeholder[i]);
					}

					bw.Write(info.Info.LoginServerIP);
					bw.Write(info.Info.LoginServerSafeKey);

					bw.Write((short)info.Info.Version.Min);
					bw.Write((short)info.Info.Version.Max);

					bw.Write(info.ObjectSerial);
					
					bw.Write(info.X);
					bw.Write(info.Y);
					bw.Write(info.Z);
					
					bw.Write(info.AX);
					bw.Write(info.AY);
					bw.Write(info.AZ);

					bw.Write(info.State);

					if (index++ >= NPC_MAX)
						break;
				}
			}
			catch (Exception ex)
			{
				using var sw = new StreamWriter($"{GetType().Name}.log", true);
				sw.Write(ex.Format());
			}
		}

		private string ReadString(BinaryReader br, int count)
		{
			var buffer = br.ReadBytes(count);

			return GameInfo.Encoding.GetString(buffer);
		}

		private byte[] GetBytes(string value, int count)
		{
			if (string.IsNullOrEmpty(value))
				throw new ArgumentException("GetBytes => string value was null or empty!");

			if (count <= 0)
				throw new ArgumentException("GetBytes => int count was less or equal zero!");

			var buffer = new byte[count];
			GameInfo.Encoding.GetBytes(value, 0, value.Length, buffer, 0);

			return buffer;
		}

		#endregion
	}
}
