using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Framework.Items
{
	public class Specialization
	{
		#region Fields/Properties

		// Weapons damage
		public Range AttackPower { get; set; } = new();
		public int AttackRange { get; set; }
		public int AttackSpeed { get; set; }
		public Range AttackRate { get; set; }
		public int CriticalRate { get; set; }

		// Defensive
		public Range Defense { get; set; }
		public RangeF Absortion { get; set; }
		public float BlockRate { get; set; }
		public Elemental Resistance { get; set; } = new();

		// Move for boots
		public RangeF MovementSpeed { get; set; }

		// Additional skill mastery of staffs/wand/phantoms
		public Range MagicMastery { get; set; }

		// Add hp/mp/sp and regens
		public Status Additional = new();
		public Status Regeneration = new();

		#endregion
	}
}
