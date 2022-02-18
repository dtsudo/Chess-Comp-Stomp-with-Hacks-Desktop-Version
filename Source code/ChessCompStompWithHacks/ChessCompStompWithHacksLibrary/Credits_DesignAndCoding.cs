
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;

	public class Credits_DesignAndCoding
	{
		private ColorTheme colorTheme;

		private int height;

		private bool isWebBrowserVersion;

		public Credits_DesignAndCoding(ColorTheme colorTheme, int height, bool isWebBrowserVersion)
		{
			this.colorTheme = colorTheme;

			this.height = height;

			this.isWebBrowserVersion = isWebBrowserVersion;
		}
		
		public bool ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput,
			ISoundOutput<ChessSound> soundOutput)
		{
			return false;
		}

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

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			string text = this.isWebBrowserVersion ? GetWebBrowserVersionText() : GetDesktopVersionText();

			displayOutput.DrawText(
				x: 10,
				y: this.height - 10,
				text: text,
				font: ChessFont.ChessFont20Pt,
				color: DTColor.Black());
		}
	}
}
