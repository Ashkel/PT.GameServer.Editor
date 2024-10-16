using GameServer.Framework;
using System.Text;

namespace GameServer.Helpers
{
	public static class FileHelper
	{
		public static string GetWord(string line, ref int position)
		{
			if (string.IsNullOrWhiteSpace(line) || position >= line.Length)
				return string.Empty;

			var sb = new StringBuilder();

			while((position < line.Length) && char.IsWhiteSpace(line[position]))
				position++;
			
			while ((position < line.Length) && !char.IsWhiteSpace(line[position]))
			{
				sb.Append(line[position++]);
			}

			return sb.ToString();
		}

		public static string GetString(string line, ref int position)
		{
			if (string.IsNullOrWhiteSpace(line) || (position >= line.Length))
				return string.Empty;

			var buffer = new StringBuilder();

			// Skip leading characters until a double quote is encountered
			while ((position < line.Length) && line[position] != '"')
			{
				if ((line[position] == '\r') ||
					(line[position] == '\n') ||
					(line[position] == '\0'))
				{
					break;
				}

				position++;
			}

			// Move past the opening double quote
			position++;

			// Extract characters until the closing double quote is encountered
			while ((position < line.Length) && line[position] != '"')
			{
				if ((line[position] == '\r') ||
					(line[position] == '\n') ||
					(line[position] == '\0'))
				{
					break;
				}

				buffer.Append(line[position++]);
			}

			// Move past the closing double quote
			position++;

			return buffer.ToString();
		}

		public static string ParseString(string buffer, ref int position)
		{
			int tmp = position;

			if (GetWord(buffer, ref tmp).FirstOrDefault() == '"')
			{
				return GetString(buffer, ref position);
			}

			return string.Empty;
		}

		public static int ParseInteger(string buffer, ref int position)
		{
			var str = GetWord(buffer, ref position);

			if (string.IsNullOrEmpty(str))
				return 0;

			// Remove trailing percent sign
			str = str.TrimEnd('%');

			if (int.TryParse(str, out int result))
				return result;

			return 0;
		}

		public static long ParseLong(string buffer, ref int position)
		{
			var str = GetWord(buffer, ref position);

			if (string.IsNullOrEmpty(str))
				return 0;

			// Remove trailing percent sign
			str = str.TrimEnd('%');

			if (long.TryParse(str, out long result))
				return result;

			return 0;
		}

		public static float ParseFloat(string buffer, ref int position)
		{
			var str = GetWord(buffer, ref position);

			if (!string.IsNullOrEmpty(str) &&
				float.TryParse(str, out float result))
			{
				return result;
			}

			return 0.000f;
		}

		public static bool ParseBool(string buffer, ref int position, string? condition = null)
		{
			if (!string.IsNullOrEmpty(condition))
			{
				var str = GetWord(buffer, ref position);

				// Use String.Equals for case-insensitive comparison
				if (!string.Equals(str, condition, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
			}

			return true;
		}

		public static Framework.Range ParseRange(string buffer, ref int position)
		{
			int start = ParseInteger(buffer, ref position);
			int end = ParseInteger(buffer, ref position);

			// Swap values if necessary
			return (end > start) ? new(start, end) : new(end, start);
		}

		public static RangeF ParseRangeF(string buffer, ref int position)
		{
			float start = ParseFloat(buffer, ref position);
			float end = ParseFloat(buffer, ref position);

			// Swap values if necessary
			return (end > start) ? new(start, end) : new(end, start);
		}

		public static string GetInfoName(Language language)
		{
			return language switch
			{
				Language.Chinese => "*C_NAME",
				Language.ChineseTaiwan => "*T_NAME",
				Language.English => "*E_NAME",
				Language.Japanese => "*J_NAME",
				Language.Korean => "*이름",
				Language.Portuguese => "*B_NAME",
				Language.Spanish => "*A_NAME",
				Language.Thailand => "*TH_NAME",
				Language.Vietnam => "*V_NAME",
				_ => "*NAME",
			};
		}

		public static string GetInfoChat(Language language)
		{
			return language switch
			{
				Language.Chinese => "*C_CHAT",
				Language.ChineseTaiwan => "*T_CHAT",
				Language.English => "*E_CHAT",
				Language.Japanese => "*J_CHAT",
				Language.Korean => "*대화",
				Language.Portuguese => "*B_CHAT",
				Language.Spanish => "*A_CHAT",
				Language.Thailand => "*TH_CHAT",
				Language.Vietnam => "*V_CHAT",
				_ => throw new NotImplementedException(),
			};
		}
	}
}