using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Framework.Items
{
	public struct Requeriments
	{
		#region Field/Properties

		public int Level { get; set; }
		public int Strength { get; set; }
		public int Intelligence { get; set; }
		public int Talent { get; set; }
		public int Dexterity { get; set; }
		public int Health { get; set; }

		#endregion

		public override string ToString()
		{
			return $"Level: {Level}, Strength: {Strength}, Intelligence: {Intelligence}, Talent: {Talent}, Dexterity: {Dexterity}, Health: {Health}";
		}
	}
}
