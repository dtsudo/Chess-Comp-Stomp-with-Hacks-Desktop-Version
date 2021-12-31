
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using System;
	using System.Collections.Generic;

	public class MonoGameDisplayImages
	{
		private SpriteBatch spriteBatch;
		private int windowHeight;

		private Dictionary<ChessImage, Texture2D> chessImageToTextureMapping;

		public MonoGameDisplayImages(SpriteBatch spriteBatch, int windowHeight)
		{
			this.spriteBatch = spriteBatch;
			this.windowHeight = windowHeight;

			this.chessImageToTextureMapping = new Dictionary<ChessImage, Texture2D>();
		}

		public void DisposeImages()
		{
		}

		public void DrawInitialLoadingScreen()
		{
		}

		public bool LoadImages()
		{
			string imagesDirectory = Util.GetImagesDirectory();

			foreach (ChessImage image in Enum.GetValues(typeof(ChessImage)))
			{
				if (this.chessImageToTextureMapping.ContainsKey(image))
					continue;

				Texture2D texture = Texture2D.FromFile(graphicsDevice: spriteBatch.GraphicsDevice, path: imagesDirectory + image.GetImageFilename());

				Color[] texturePixels = new Color[texture.Width * texture.Height];
				texture.GetData(texturePixels);
				for (int i = 0; i < texturePixels.Length; i++)
				{
					Color pixel = texturePixels[i];
					int red = pixel.R;
					int green = pixel.G;
					int blue = pixel.B;
					int alpha = pixel.A;
					texturePixels[i] = (new Color(r: red, g: green, b: blue, alpha: 255)) * (alpha / 255.0f);
				}
				texture.SetData(texturePixels);

				this.chessImageToTextureMapping[image] = texture;
				return false;
			}

			return true;
		}

		public void DrawImageRotatedClockwise(ChessImage image, int x, int y, int degreesScaled, int scalingFactorScaled)
		{
			int height = this.GetHeight(image: image);
			int scaledHeight = height * scalingFactorScaled / 128;

			y = this.windowHeight - y - scaledHeight;

			int width = this.GetWidth(image: image);
			int scaledWidth = width * scalingFactorScaled / 128;
			
			spriteBatch.Draw(
				texture: this.chessImageToTextureMapping[image],
				destinationRectangle: new Rectangle(x + scaledWidth / 2, y + scaledHeight / 2, scaledWidth, scaledHeight),
				sourceRectangle: null,
				color: Color.White,
				rotation: (float) (degreesScaled / 128.0 * 2.0 * Math.PI / 360.0),
				origin: new Vector2(width / 2, height / 2),
				effects: SpriteEffects.None,
				layerDepth: 0);
		}

		public int GetWidth(ChessImage image)
		{
			return this.chessImageToTextureMapping[image].Width;
		}

		public int GetHeight(ChessImage image)
		{
			return this.chessImageToTextureMapping[image].Height;
		}
	}
}
