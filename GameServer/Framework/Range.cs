using System.Xml.Serialization;

namespace GameServer.Framework
{
	public struct Range : IEquatable<Range>, IEquatable<RangeF>
	{
		#region Field/Properties

		public static readonly Range Empty = new(0, 0);

		/// <summary>
		/// The minimum value for the range, inclusively.
		/// </summary>
		public int Min
		{
			readonly get => _min;
			set => _min = value;
		}
		private int _min;

		/// <summary>
		/// The maximum value for the range, inclusively.
		/// </summary>
		public int Max
		{
			readonly get => _max;
			set => _max = value;
		}
		private int _max;

		/// <summary>
		/// Calculate the size of this range.
		/// </summary>
		public readonly int Size => (Max - Min);

		/// <summary>
		/// Calculate the average value returned by this range.
		/// </summary>
		public readonly int Average => Min + (Size / 2);

		private static Random? _random;

		#endregion

		#region Constructor(s)

		public Range(int min, int max)
		{
			if (min > max)
			{
				throw new ArgumentException("Max must be greater than Min!");
			}

			this._min = min;
			this._max = max;

			_random = null;
		}

		public Range(RangeF range)
			: this((int)range.Min, (int)range.Max)
		{
		}

		#endregion

		#region Overload

		public static Range Add(Range lValue, Range rValue)
		{
			return new Range(
						lValue.Min + rValue.Min,
						lValue.Max + rValue.Max);
		}

		public static Range Add(Range lValue, int rValue)
		{
			return new Range(
						lValue.Min + rValue,
						lValue.Max + rValue);
		}

		public static Range operator +(Range lValue, Range rValue)
		{
			return Add(lValue, rValue);
		}

		public static Range operator +(Range lValue, int rValue)
		{
			return Add(lValue, rValue);
		}

		public static Range Subtract(Range lValue, Range rValue)
		{
			return new Range(
						lValue.Min - rValue.Min,
						lValue.Max - rValue.Max);
		}

		public static Range Subtract(Range lValue, int rValue)
		{
			return new Range(
						lValue.Min - rValue,
						lValue.Max - rValue);
		}

		public static Range operator -(Range lValue, Range rValue)
		{
			return Subtract(lValue, rValue);
		}

		public static Range operator -(Range lValue, int rValue)
		{
			return Subtract(lValue, rValue);
		}

		public static Range Multiply(Range lValue, Range rValue)
		{
			return new Range(
						lValue.Min * rValue.Min,
						lValue.Max * rValue.Max);
		}

		public static Range Multiply(Range lValue, int rValue)
		{
			return new Range(
						lValue.Min * rValue,
						lValue.Max * rValue);
		}

		public static Range operator *(Range lValue, Range rValue)
		{
			return Multiply(lValue, rValue);
		}

		public static Range operator *(Range lValue, int rValue)
		{
			return Multiply(lValue, rValue);
		}

		public static Range Divide(Range lValue, Range rValue)
		{
			return new Range(
						lValue.Min / rValue.Min,
						lValue.Max / rValue.Max);
		}

		public static Range Divide(Range lValue, int rValue)
		{
			return new Range(
						lValue.Min / rValue,
						lValue.Max / rValue);
		}

		public static Range operator /(Range lValue, Range rValue)
		{
			return Divide(lValue, rValue);
		}

		public static Range operator /(Range lValue, int rValue)
		{
			return Divide(lValue, rValue);
		}

		public static bool operator ==(Range lValue, Range rValue)
		{
			return lValue.Equals(rValue);
		}

		public static bool operator !=(Range lValue, Range rValue)
		{
			return !(lValue == rValue);
		}

		#endregion

		#region Helper Methods

		/// <summary>
		/// Generate a random value between the Min and Max, inclusively.
		/// </summary>
		public readonly int Random()
		{
			_random ??= new Random();

			lock (_random)
			{
				return _random.Next(this.Min, this.Max + 1);
			}
		}

		public override readonly int GetHashCode()
		{
			int hash = base.GetHashCode();

			hash += (this.Min * 16) & 0xFF;
			hash += (this.Max * 17) & 0xFF;

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

		public readonly bool Equals(Range range)
		{
			return ((this.Min == range.Min) &&
					(this.Max == range.Max));
		}

		public readonly bool Equals(RangeF range)
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
