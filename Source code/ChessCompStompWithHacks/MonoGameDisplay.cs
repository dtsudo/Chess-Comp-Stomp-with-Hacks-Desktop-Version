
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using Microsoft.Xna.Framework.Content;
	using Microsoft.Xna.Framework.Graphics;

	public class MonoGameDisplay : DTDisplay<GameImage, GameFont>
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

        public override void DrawImageRotatedClockwise(GameImage image, int x, int y, int degreesScaled, int scalingFactorScaled)
        {
			this.monoGameDisplayImages.DrawImageRotatedClockwise(
				image: image,
				x: x,
				y: y,
				degreesScaled: degreesScaled,
				scalingFactorScaled: scalingFactorScaled);
		}

		public override void DrawImageRotatedClockwise(GameImage image, int imageX, int imageY, int imageWidth, int imageHeight, int x, int y, int degreesScaled, int scalingFactorScaled)
		{
			this.monoGameDisplayImages.DrawImageRotatedClockwise(
				image: image,
				imageX: imageX,
				imageY: imageY,
				imageWidth: imageWidth,
				imageHeight: imageHeight,
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

        public override void DrawText(int x, int y, string text, GameFont font, DTColor color)
        {
			this.monoGameDisplayFont.DrawText(
				x: x,
				y: y,
				text: text,
				font: font,
				color: color);
		}

		public override void TryDrawText(int x, int y, string text, GameFont font, DTColor color)
		{
			this.monoGameDisplayFont.TryDrawText(
				x: x,
				y: y,
				text: text,
				font: font,
				color: color);
		}

		public override int GetWidth(GameImage image)
		{
			return this.monoGameDisplayImages.GetWidth(image: image);
		}

		public override int GetHeight(GameImage image)
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
