using GameServer.Helpers;

namespace GameServer
{
	static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				Globals.Load();

				// To customize application configuration such as set high DPI settings or default font,
				// see https://aka.ms/applicationconfiguration.
				ApplicationConfiguration.Initialize();
				Application.Run(new Forms.MainForm());
			}
			catch (Exception ex)
			{
				using (var sw = new StreamWriter("Error.log", true))
				{
					sw.Write(ex.Format());
				}
			}
		}
	}
}