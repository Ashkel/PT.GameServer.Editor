using GameServer.Helpers;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace GameServer.Framework
{
	public abstract class GameInfo
	{
		#region Fields/Properties

		public string FileName { get; private set; } = string.Empty;

		[XmlIgnore]
		[JsonIgnore]
		public Encoding Encoding { get; protected set; } = Encoding.GetEncoding(949);

		#endregion


		#region Helper methods

		public void SetFile(string filename)
		{
			FileName = filename;
		}

		public void Process(string? fileName = null)
		{
			int line = 0;
			string text = string.Empty;

			try
			{
				using var sr = new StreamReader(fileName ?? FileName, Encoding);

				while (!sr.EndOfStream)
				{
					++line;

					string? buffer = sr.ReadLine();

					if (string.IsNullOrEmpty(buffer))
						continue;

					text = buffer;

					ParseLine(buffer);
				}
			}
			catch (Exception ex)
			{
				LogError(ex, fileName ?? FileName, line, text);
			}
		}

		private void LogError(Exception ex, string fileName, int line, string text)
		{
			using var sw = new StreamWriter($"{this.GetType().Name}.log", true);
			var error = ex.Format(fileName, line, text);

			sw.Write($"{DateTime.Now}: {error}");
		}

		#endregion


		#region Abstract methods

		public abstract void Reset();
		public abstract void Save(string? fileName = null);

		protected abstract void ParseLine(string line);

		// ISSUE:
		// The way I did it, gameinfo can't add information to a file where it doesn't exist.
		// Example: you can't add HP to an item that didn't have it before, because there is not line for that.
		//protected abstract void EditLine(string line, ref StringBuilder sb);

		#endregion
	}
}
