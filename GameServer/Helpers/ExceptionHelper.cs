using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Helpers
{
	public static class ExceptionHelper
	{
		public static string Format(this Exception exception)
		{
			var sb = new StringBuilder();

			sb.AppendLine($"Exception: {exception.GetType()}");
			sb.AppendLine($" -> Message: \"{exception.Message}\"");
			sb.AppendLine($" -> Source: \"{exception.Source}\"");

			if (exception.InnerException != null)
				sb.AppendLine($" -> Inner: {exception.InnerException.Message}");

			sb.AppendLine(exception.ToString());
			sb.AppendLine();

			return sb.ToString();
		}

		public static string Format(this Exception exception, string fileName, int line, string text)
		{
			var sb = new StringBuilder();

			sb.AppendLine($"Exception: {exception.GetType()}");

			sb.AppendLine($" -> File: \"{fileName}\",");
			sb.AppendLine($" -> Line: {line},");

			if (!string.IsNullOrEmpty(text))
				sb.AppendLine($" -> At: {text},");

			sb.AppendLine($" -> Message: \"{exception.Message}\"");
			sb.AppendLine($" -> Source: \"{exception.Source}\"");

			if (exception.InnerException != null)
				sb.AppendLine($" -> Inner: {exception.InnerException.Message}");

			sb.AppendLine(exception.ToString());
			sb.AppendLine();

			return sb.ToString();
		}
	}
}
