
namespace ChessCompStompWithHacksLibrary
{
	using ChessCompStompWithHacksEngine;
	using DTLibrary;
	using System.Collections.Generic;

	public class SettingsIcon
	{
		private bool isHover;
		private bool isClicked;

		public SettingsIcon()
		{
			this.isHover = false;
			this.isClicked = false;
		}

		public class SettingsIconStatus
		{
			public SettingsIconStatus(bool hasClicked, bool isHover)
			{
				this.HasClicked = hasClicked;
				this.IsHover = isHover;
			}

			public bool HasClicked { get; private set; }
			public bool IsHover { get; private set; }
		}

		public SettingsIconStatus ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput,
			bool ignoreMouse,
			IDisplayProcessing<ChessImage> displayProcessing)
		{
			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			int settingsIconWidth = displayProcessing.GetWidth(ChessImage.Gear);
			int settingsIconHeight = displayProcessing.GetHeight(ChessImage.Gear);

			bool isHover = ChessCompStompWithHacks.WINDOW_WIDTH - settingsIconWidth <= mouseX
				&& mouseX <= ChessCompStompWithHacks.WINDOW_WIDTH
				&& ChessCompStompWithHacks.WINDOW_HEIGHT - settingsIconHeight <= mouseY
				&& mouseY <= ChessCompStompWithHacks.WINDOW_HEIGHT;

			this.isHover = isHover && !ignoreMouse;

			if (mouseInput.IsLeftMouseButtonPressed() && !previousMouseInput.IsLeftMouseButtonPressed() && !ignoreMouse)
			{
				if (isHover)
					this.isClicked = true;
			}
			
			if (this.isClicked && !mouseInput.IsLeftMouseButtonPressed() && previousMouseInput.IsLeftMouseButtonPressed())
			{
				this.isClicked = false;

				if (isHover && !ignoreMouse)
					return new SettingsIconStatus(hasClicked: true, isHover: this.isHover);
			}

			return new SettingsIconStatus(hasClicked: false, isHover: this.isHover);
		}

		public void Render(IDisplayOutput<ChessImage, ChessFont> displayOutput)
		{
			displayOutput.DrawImage(
				image: this.isClicked ? ChessImage.GearSelected : (this.isHover ? ChessImage.GearHover : ChessImage.Gear),
				x: ChessCompStompWithHacks.WINDOW_WIDTH - displayOutput.GetWidth(ChessImage.Gear),
				y: ChessCompStompWithHacks.WINDOW_HEIGHT - displayOutput.GetHeight(ChessImage.Gear));
		}
	}
}
