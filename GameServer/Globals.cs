using GameServer.Framework;
using GameServer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
	public static class Globals
	{
		public static Settings Settings { get; set; } = new();

		public static string GameServerPath
		{
			get => Path.Combine(Settings.ServerPath, @"GameServer");
		}

		public static string FieldPath
		{
			get => Path.Combine(Settings.ServerPath, @"GameServer\Field");
		}
		
		public static string MonsterPath
		{
			get => Path.Combine(Settings.ServerPath, @"GameServer\Monster");
		}

		public static string NPCPath
		{
			get => Path.Combine(Settings.ServerPath, @"GameServer\NPC");
		}
		
		public static string OpenItemPath
		{
			get => Path.Combine(Settings.ServerPath, @"GameServer\OpenItem");
		}

		public static string NotepadPath
		{
			get
			{
				string notepad = Settings.NotepadPath;
				if (string.IsNullOrEmpty(notepad))
					notepad = "notepad.exe";

				return notepad;
			}
		}

		public static void Load()
		{
			Settings = Serializer<Settings>.LoadXml("Settings.xml");
		}
		
		public static void Save()
		{
			Serializer<Settings>.SaveXml("Settings.xml", Settings);
		}

		public static string[] GetFiles(string path, string pattern, bool recursive = false)
		{
			var files = Directory.GetFiles(path, pattern, recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

			return files;
		}

	}
}
