using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Framework;

public enum Language
{
	Unknown,
	Chinese,
	ChineseTaiwan,
	English,
	Japanese,
	Korean,
	Portuguese,
	Spanish,
	Thailand,
	Vietnam,
}

public class Settings
{
	#region Data

	public string ServerPath { get; set; } = string.Empty;
	public string ClientPath { get; set; } = string.Empty;
	public string NotepadPath { get; set; } = string.Empty;

	public Language Language { get; set; } = Language.Unknown;
	public bool KeepComments { get; set; }

	#endregion


}
