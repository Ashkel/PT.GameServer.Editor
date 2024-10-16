using System.Xml.Serialization;

namespace GameServer.Framework
{
	public struct RangeF : IEquatable<RangeF>, IEquatable<Range>
	{
		#region Field/Properties

		public static readonly RangeF Empty = new(0, 0);

		/// <summary>
		/// The minimum value for the range, inclusively.
		/// </summary>
		public float Min
		{
			readonly get => _min;
			set => _min = value;
		}
		private float _min;

		/// <summary>
		/// The maximum value for the range, inclusively.
		/// </summary>
		public float Max
		{
			readonly get => _max;
			set => _max = value;
		}
		private float _max;

		/// <summary>
		/// Calculate the size of this range.
		/// </summary>
		public readonly float Size => (Max - Min);

		/// <summary>
		/// Calculate the average value returned by this range.
		/// </summary>
		public readonly float Average => Min + Size / 2;

		private static Random? _random;

		#endregion

		#region Constructor(s)

		public RangeF(float min, float max)
		{
			if (min > max)
			{
				throw new ArgumentException("Max must be greater than Min!");
			}

			this._min = min;
			this._max = max;

			_random = null;
		}

		public RangeF(Range range)
			: this(range.Min, range.Max)
		{
		}

		#endregion

		#region Overload

		public static RangeF Add(RangeF lValue, RangeF rValue)
		{
			return new RangeF(
						lValue.Min + rValue.Min,
						lValue.Max + rValue.Max);
		}

		public static RangeF Add(RangeF lValue, float rValue)
		{
			return new RangeF(
						lValue.Min + rValue,
						lValue.Max + rValue);
		}

		public static RangeF operator +(RangeF lValue, RangeF rValue)
		{
			return Add(lValue, rValue);
		}

		public static RangeF operator +(RangeF lValue, float rValue)
		{
			return Add(lValue, rValue);
		}

		public static RangeF Subtract(RangeF lValue, RangeF rValue)
		{
			return new RangeF(
						lValue.Min - rValue.Min,
						lValue.Max - rValue.Max);
		}

		public static RangeF Subtract(RangeF lValue, float rValue)
		{
			return new RangeF(
						lValue.Min - rValue,
						lValue.Max - rValue);
		}

		public static RangeF operator -(RangeF lValue, RangeF rValue)
		{
			return Subtract(lValue, rValue);
		}

		public static RangeF operator -(RangeF lValue, float rValue)
		{
			return Subtract(lValue, rValue);
		}

		public static RangeF Multiply(RangeF lValue, RangeF rValue)
		{
			return new RangeF(
						lValue.Min * rValue.Min,
						lValue.Max * rValue.Max);
		}

		public static RangeF Multiply(RangeF lValue, float rValue)
		{
			return new RangeF(
						lValue.Min * rValue,
						lValue.Max * rValue);
		}

		public static RangeF operator *(RangeF lValue, RangeF rValue)
		{
			return Multiply(lValue, rValue);
		}

		public static RangeF operator *(RangeF lValue, float rValue)
		{
			return Multiply(lValue, rValue);
		}

		public static RangeF Divide(RangeF lValue, RangeF rValue)
		{
			return new RangeF(
						lValue.Min / rValue.Min,
						lValue.Max / rValue.Max);
		}

		public static RangeF Divide(RangeF lValue, float rValue)
		{
			return new RangeF(
						lValue.Min / rValue,
						lValue.Max / rValue);
		}

		public static RangeF operator /(RangeF lValue, RangeF rValue)
		{
			return Divide(lValue, rValue);
		}

		public static RangeF operator /(RangeF lValue, float rValue)
		{
			return Divide(lValue, rValue);
		}

		public static bool operator ==(RangeF lValue, RangeF rValue)
		{
			return lValue.Equals(rValue);
		}

		public static bool operator !=(RangeF lValue, RangeF rValue)
		{
			return !(lValue == rValue);
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Generate a random value between the Min and Max, inclusively.
		/// </summary>
		public readonly float Random()
		{
			_random ??= new Random();

			return (float)(_random.NextDouble() * (double)(this.Max - this.Min)) + this.Min;
		}

		public override readonly int GetHashCode()
		{
			int hash = base.GetHashCode();

			hash += (int)(this.Min * 16) & 0xFF;
			hash += (int)(this.Max * 17) & 0xFF;

			return hash;
		}

		public override readonly bool Equals(object? obj)
		{
			return obj switch
			{
				Range r => Equals(r),
				RangeF f => Equals(f),
				_ => false,
			};
		}

		public readonly bool Equals(RangeF range)
		{
			return ((this.Min == range.Min) &&
					(this.Max == range.Max));
		}

		public readonly bool Equals(Range range)
		{
			return ((this.Min == range.Min) &&
					(this.Max == range.Max));
		}

		public override readonly string ToString()
		{
			return $"Min: {this.Min}, Max: {this.Max}";
		}

		#endregion
	}
}
