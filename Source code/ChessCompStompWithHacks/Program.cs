
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using System;
	using System.IO;

	public static class Program
	{
		[STAThread]
		static void Main()
		{
			string executablePath = Util.GetExecutablePath();

			string savedDataPath = executablePath + "/savedData";

			try
			{
				if (!Directory.Exists(savedDataPath))
					Directory.CreateDirectory(savedDataPath);
			}
			catch (Exception)
			{
			}

			IFileIO fileIO = new FileIO(pathNotIncludingTrailingSlash: savedDataPath);

			int fileId = GlobalConstants.FILE_ID_FOR_GLOBAL_CONFIGURATION;
			VersionInfo versionInfo = VersionHistory.GetVersionInfo();

			GlobalConfigurationManager.GlobalConfiguration globalConfiguration = GlobalConfigurationManager.GetGlobalConfiguration(
				fileIO: fileIO,
				fileId: fileId,
				versionInfo: versionInfo);

			GlobalConfigurationManager.SaveGlobalConfiguration(globalConfiguration: globalConfiguration, fileIO: fileIO, fileId: fileId, versionInfo: versionInfo);

			bool logAchievementsToConsole = false;

			using (GameImplementation game = new GameImplementation(globalConfiguration: globalConfiguration, fileIO: fileIO, logAchievementsToConsole: logAchievementsToConsole))
				game.Run();
		}
	}
}
