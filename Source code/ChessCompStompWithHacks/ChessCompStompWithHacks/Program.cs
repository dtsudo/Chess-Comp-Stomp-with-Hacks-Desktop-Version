
namespace ChessCompStompWithHacks
{
	using System;
	
	public static class Program
	{
		[STAThread]
		static void Main()
		{
			bool debugMode = false;
			int fps = 60;

			using (ChessCompStompWithHacksGame game = new ChessCompStompWithHacksGame(debugMode: debugMode, fps: fps))
				game.Run();
		}
	}
}
