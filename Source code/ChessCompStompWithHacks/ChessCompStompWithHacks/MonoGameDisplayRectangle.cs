
namespace ChessCompStompWithHacks
{
	using DTLibrary;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;

	public class MonoGameDisplayRectangle
	{
		private SpriteBatch spriteBatch;
		private Texture2D whiteTexture;

		private int windowHeight;

		public MonoGameDisplayRectangle(SpriteBatch spriteBatch, int windowHeight)
		{
			this.spriteBatch = spriteBatch;
			this.whiteTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
			this.whiteTexture.SetData<Color>(new Color[] { Color.White });

			this.windowHeight = windowHeight;
		}

		public void DisposeImages()
		{
		}

		public bool LoadImages()
		{
			return true;
		}

		public void DrawRectangle(int x, int y, int width, int height, DTColor color, bool fill)
		{
			y = this.windowHeight - y - height;

			Texture2D texture = this.whiteTexture;
			Color monoGameColor = (new Color(r: color.R, g: color.G, b: color.B)) * (color.Alpha / 255.0f);

			if (fill)
			{
				spriteBatch.Draw(
					texture: texture,
					destinationRectangle: new Rectangle(x, y, width, height),
					color: monoGameColor);
				return;
			}

			spriteBatch.Draw(
				texture: texture,
				destinationRectangle: new Rectangle(x, y, width, 1),
				color: monoGameColor);
			spriteBatch.Draw(
				texture: texture,
				destinationRectangle: new Rectangle(x, y + 1, 1, height - 1),
				color: monoGameColor);
			spriteBatch.Draw(
				texture: texture,
				destinationRectangle: new Rectangle(x + 1, y + height - 1, width - 1, 1),
				color: monoGameColor);
			spriteBatch.Draw(
				texture: texture,
				destinationRectangle: new Rectangle(x + width - 1, y + 1, 1, height - 2),
				color: monoGameColor);
		}
	}
}
