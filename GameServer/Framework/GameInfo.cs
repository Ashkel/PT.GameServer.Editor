using GameServer.Helpers;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GameServer.Framework
{
	public abstract class GameInfo
	{
		#region Fields/Properties

		[XmlIgnore]
		[JsonIgnore]
		public Encoding Encoding { get; protected set; } = Encoding.GetEncoding(949);

		[XmlIgnore]
		[JsonIgnore]
		public string FileName { get; private set; } = string.Empty;

		public Dictionary<int, string> Comments { get; private set; } = new();

		public List<string> UnhandledKeywords { get; private set; } = new();

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

			Comments.Clear();

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
					
					//if (Globals.Settings.KeepComments &&
					//	(buffer.StartsWith("//") || buffer.StartsWith(';')))
					//	Comments.Add(line, buffer);

					ParseLine(buffer);
				}

				if (UnhandledKeywords.Any())
				{
					using var sw = new StreamWriter($"{this.GetType().Name}-UnhandledKeywords.log");
					sw.WriteLine(string.Join(", ", UnhandledKeywords));
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

		protected abstract void ParseLine(string line);

		public abstract void Save(string? fileName = null);

		// ISSUE:
		// The way I did it, gameinfo can't add information to a file where it doesn't exist.
		// Example: you can't add HP to an item that didn't have it before, because there is not line for that.
		//protected abstract void EditLine(string line, ref StringBuilder sb);

		#endregion
	}
}
