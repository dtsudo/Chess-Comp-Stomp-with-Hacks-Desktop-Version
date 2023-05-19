
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

		private Dictionary<GameImage, Texture2D> gameImageToTextureMapping;

		private Dictionary<Tuple<GameImage, int, int, int, int>, Texture2D> spriteToTextureMapping;

		public MonoGameDisplayImages(SpriteBatch spriteBatch, int windowHeight)
		{
			this.spriteBatch = spriteBatch;
			this.windowHeight = windowHeight;

			this.gameImageToTextureMapping = new Dictionary<GameImage, Texture2D>();
			this.spriteToTextureMapping = new Dictionary<Tuple<GameImage, int, int, int, int>, Texture2D>();
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

			foreach (GameImage image in Enum.GetValues(typeof(GameImage)))
			{
				if (this.gameImageToTextureMapping.ContainsKey(image))
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

				this.gameImageToTextureMapping[image] = texture;
				return false;
			}

			return true;
		}

		public void DrawImageRotatedClockwise(GameImage image, int x, int y, int degreesScaled, int scalingFactorScaled)
		{
			int height = this.GetHeight(image: image);
			int scaledHeight = height * scalingFactorScaled / 128;

			y = this.windowHeight - y - scaledHeight;

			int width = this.GetWidth(image: image);
			int scaledWidth = width * scalingFactorScaled / 128;
			
			spriteBatch.Draw(
				texture: this.gameImageToTextureMapping[image],
				destinationRectangle: new Rectangle(x + scaledWidth / 2, y + scaledHeight / 2, scaledWidth, scaledHeight),
				sourceRectangle: null,
				color: Color.White,
				rotation: (float) (degreesScaled / 128.0 * 2.0 * Math.PI / 360.0),
				origin: new Vector2(width / 2, height / 2),
				effects: SpriteEffects.None,
				layerDepth: 0);
		}

		public void DrawImageRotatedClockwise(GameImage image, int imageX, int imageY, int imageWidth, int imageHeight, int x, int y, int degreesScaled, int scalingFactorScaled)
		{
			Tuple<GameImage, int, int, int, int> tuple = new Tuple<GameImage, int, int, int, int>(image, imageX, imageY, imageWidth, imageHeight);

			Texture2D texture;

			if (this.spriteToTextureMapping.ContainsKey(tuple))
				texture = this.spriteToTextureMapping[tuple];
			else
			{
				Texture2D originalTexture = this.gameImageToTextureMapping[image];
				Texture2D newTexture = new Texture2D(originalTexture.GraphicsDevice, imageWidth, imageHeight);
				Color[] newTexturePixels = new Color[newTexture.Width * newTexture.Height];

				Color[] originalTexturePixels = new Color[originalTexture.Width * originalTexture.Height];
				originalTexture.GetData(originalTexturePixels);

				int newTextureIndex = 0;
				newTexture.GetData(newTexturePixels);
				for (int i = imageY; i < imageY + imageHeight; i++)
				{
					for (int j = imageX; j < imageX + imageWidth; j++)
					{
						newTexturePixels[newTextureIndex] = originalTexturePixels[i * originalTexture.Width + j];
						newTextureIndex++;
					}
				}
				newTexture.SetData(newTexturePixels);

				texture = newTexture;
				this.spriteToTextureMapping[tuple] = newTexture;
			}

			int height = imageHeight;
			int scaledHeight = height * scalingFactorScaled / 128;

			y = this.windowHeight - y - scaledHeight;

			int width = imageWidth;
			int scaledWidth = width * scalingFactorScaled / 128;

			spriteBatch.Draw(
				texture: texture,
				destinationRectangle: new Rectangle(x + scaledWidth / 2, y + scaledHeight / 2, scaledWidth, scaledHeight),
				sourceRectangle: null,
				color: Color.White,
				rotation: (float) (degreesScaled / 128.0 * 2.0 * Math.PI / 360.0),
				origin: new Vector2(width / 2, height / 2),
				effects: SpriteEffects.None,
				layerDepth: 0);
		}

		public int GetWidth(GameImage image)
		{
			return this.gameImageToTextureMapping[image].Width;
		}

		public int GetHeight(GameImage image)
		{
			return this.gameImageToTextureMapping[image].Height;
		}
	}
}
