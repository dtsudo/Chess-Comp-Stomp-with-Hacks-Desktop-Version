
namespace ChessCompStompWithHacksLibrary
{
	using DTLibrary;
	using System;

	public class Credits_DesignAndCoding
	{
		private ColorTheme colorTheme;
		private Button viewLicenseButton;

		private int height;

		private BuildType buildType;

		private bool isHoverOverGitHubUrl;

		public Credits_DesignAndCoding(ColorTheme colorTheme, int height, BuildType buildType)
		{
			this.colorTheme = colorTheme;

			this.height = height;

			this.buildType = buildType;

			this.isHoverOverGitHubUrl = false;

			if (buildType == BuildType.WebStandAlone || buildType == BuildType.Electron)
			{
			}
			else if (buildType == BuildType.WebEmbedded)
			{
			}
			else if (buildType == BuildType.Desktop)
			{
				this.viewLicenseButton = null;
			}
			else
			{
				throw new Exception();
			}
		}

		private static bool IsHoverOverGitHubUrl(IMouse mouseInput, int height)
		{
			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			return 107 <= mouseX && mouseX <= 107 + 353
				&& height - 265 <= mouseY && mouseY <= height - 265 + 32;
		}

		public class Result
		{
			public Result(bool clickedButton, string clickUrl)
			{
				this.ClickedButton = clickedButton;
				this.ClickUrl = clickUrl;
			}

			public bool ClickedButton { get; private set; }
			public string ClickUrl { get; private set; }
		}

		public Result ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput,
			ISoundOutput<GameSound> soundOutput)
		{
			bool clickedButton;
			if (this.viewLicenseButton != null)
				clickedButton = this.viewLicenseButton.ProcessFrame(mouseInput: mouseInput, previousMouseInput: previousMouseInput);
			else
				clickedButton = false;

			if (this.buildType == BuildType.WebEmbedded)
				this.isHoverOverGitHubUrl = IsHoverOverGitHubUrl(mouseInput: mouseInput, height: this.height);

			string clickUrl = null;

			if (this.isHoverOverGitHubUrl)
			{
				string a = "htt";
				string b = "/githu";
				string c = "udo";
				string d = "ps:/";
				string e = "dts";
				string f = "b.com/";

				clickUrl = a + d + b + f + e + c;
			}

			return new Result(clickedButton: clickedButton, clickUrl: clickUrl);
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

		private static string GetWebStandAloneVersionText()
		{
			return "";
		}

		private static string GetWebEmbeddedVersionText()
		{
			return "";
		}

		private static string GetElectronVersionText()
		{
			return "";
		}

		private static string GetInfo()
		{
			string str = "ddit";

			string str2 = "tsudo";

			str = "Re" + str;

			return str + ": /u/d" + str2;
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			if (this.buildType == BuildType.Desktop)
			{
				string text = GetDesktopVersionText();

				displayOutput.DrawText(
					x: 10,
					y: this.height - 10,
					text: text,
					font: GameFont.GameFont20Pt,
					color: DTColor.Black());
			}
			else if (this.buildType == BuildType.WebStandAlone)
			{
			}
			else if (this.buildType == BuildType.WebEmbedded)
			{
			}
			else if (this.buildType == BuildType.Electron)
			{
			}
			else
			{
				throw new Exception();
			}

			if (this.viewLicenseButton != null)
				this.viewLicenseButton.Render(displayOutput: displayOutput);
		}
	}
}
