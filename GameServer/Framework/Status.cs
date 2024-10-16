using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Framework
{
	public struct Status
	{
		#region Field/Properties

		public RangeF Hp { get; set; }
		public RangeF Mp { get; set; }
		public RangeF Sp { get; set; }

		#endregion
	}
}
