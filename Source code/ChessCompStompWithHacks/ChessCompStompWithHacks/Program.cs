
namespace ChessCompStompWithHacks
{
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

			int fileId = ChessCompStompWithHacksLibrary.ChessCompStompWithHacks.FILE_ID_FOR_GLOBAL_CONFIGURATION;

			GlobalConfigurationManager.GlobalConfiguration globalConfiguration = GlobalConfigurationManager.GetGlobalConfiguration(
				fileIO: fileIO,
				fileId: fileId);

			GlobalConfigurationManager.SaveGlobalConfiguration(globalConfiguration: globalConfiguration, fileIO: fileIO, fileId: fileId);

			using (ChessCompStompWithHacksGame game = new ChessCompStompWithHacksGame(globalConfiguration: globalConfiguration, fileIO: fileIO))
				game.Run();
		}
	}
}
