
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class Credits_DesignAndCoding
	{
		private static string GetWebBrowserVersionText()
		{
			return "";
		}

		private static string GetDesktopVersionText()
		{
			return "Design and coding by dtsudo (https://github.com/dtsudo)" + "\n"
				+ "\n"
				+ "This game is open source, under the MIT license." + "\n"
				+ "\n"
				+ "The game is written in C# and uses the MonoGame framework." + "\n"
				+ "MonoGame is licensed under the Microsoft Public License.";
		}

		public static void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput, int width, int height, bool isWebBrowserVersion)
		{
			string text = isWebBrowserVersion ? GetWebBrowserVersionText() : GetDesktopVersionText();

			displayOutput.DrawText(
				x: 10,
				y: height - 10,
				text: text,
				font: ChessFont.ChessFont20Pt,
				color: DTColor.Black());
		}
	}
}
