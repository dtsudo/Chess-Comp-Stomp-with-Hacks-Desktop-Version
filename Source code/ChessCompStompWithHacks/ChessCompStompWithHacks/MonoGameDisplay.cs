
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using Microsoft.Xna.Framework.Content;
	using Microsoft.Xna.Framework.Graphics;

	public class MonoGameDisplay : DTDisplay<ChessImage, ChessFont>
    {
		private MonoGameDisplayRectangle monoGameDisplayRectangle;
		private MonoGameDisplayImages monoGameDisplayImages;
		private MonoGameDisplayFont monoGameDisplayFont;

		private bool hasFinishedLoading;

        public MonoGameDisplay(ContentManager contentManager, SpriteBatch spriteBatch, int windowHeight)
		{
			this.monoGameDisplayRectangle = new MonoGameDisplayRectangle(spriteBatch: spriteBatch, windowHeight: windowHeight);
			this.monoGameDisplayImages = new MonoGameDisplayImages(spriteBatch: spriteBatch, windowHeight: windowHeight);
			this.monoGameDisplayFont = new MonoGameDisplayFont(contentManager: contentManager, spriteBatch: spriteBatch, windowHeight: windowHeight);

			this.hasFinishedLoading = false;
        }

		public bool HasFinishedLoading()
		{
			return this.hasFinishedLoading;
		}

        public override void DisposeImages()
		{
			this.monoGameDisplayRectangle.DisposeImages();
			this.monoGameDisplayImages.DisposeImages();
			this.monoGameDisplayFont.DisposeImages();
        }

        public override void DrawImageRotatedClockwise(ChessImage image, int x, int y, int degreesScaled, int scalingFactorScaled)
        {
			this.monoGameDisplayImages.DrawImageRotatedClockwise(
				image: image,
				x: x,
				y: y,
				degreesScaled: degreesScaled,
				scalingFactorScaled: scalingFactorScaled);
        }

        public override void DrawInitialLoadingScreen()
        {
			this.monoGameDisplayImages.DrawInitialLoadingScreen();
        }

        public override void DrawRectangle(int x, int y, int width, int height, DTColor color, bool fill)
        {
			this.monoGameDisplayRectangle.DrawRectangle(
				x: x,
				y: y,
				width: width,
				height: height,
				color: color,
				fill: fill);
        }

        public override void DrawText(int x, int y, string text, ChessFont font, DTColor color)
        {
			this.monoGameDisplayFont.DrawText(
				x: x,
				y: y,
				text: text,
				font: font,
				color: color);
		}

		public override int GetWidth(ChessImage image)
		{
			return this.monoGameDisplayImages.GetWidth(image: image);
		}

		public override int GetHeight(ChessImage image)
        {
			return this.monoGameDisplayImages.GetHeight(image: image);
        }

        public override bool LoadImages()
        {
			bool hasFinishedLoadingRectangles = this.monoGameDisplayRectangle.LoadImages();
			if (!hasFinishedLoadingRectangles)
				return false;

			bool hasFinishedLoadingImages = this.monoGameDisplayImages.LoadImages();
			if (!hasFinishedLoadingImages)
				return false;

			bool hasFinishedLoadingFonts = this.monoGameDisplayFont.LoadImages();
			if (!hasFinishedLoadingFonts)
				return false;

			this.hasFinishedLoading = true;
			return true;
        }
    }
}
