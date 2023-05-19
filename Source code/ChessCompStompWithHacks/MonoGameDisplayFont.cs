
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Content;
	using Microsoft.Xna.Framework.Graphics;
	using System;
	using System.Collections.Generic;
	using System.Globalization;

	public class MonoGameDisplayFont
	{
		private ContentManager contentManager;
		private SpriteBatch spriteBatch;
		private int windowHeight;

		private Dictionary<GameFont, SpriteFont> gameFontToSpriteFontMapping;

		public MonoGameDisplayFont(ContentManager contentManager, SpriteBatch spriteBatch, int windowHeight)
		{
			this.contentManager = contentManager;
			this.spriteBatch = spriteBatch;
			this.windowHeight = windowHeight;

			this.gameFontToSpriteFontMapping = new Dictionary<GameFont, SpriteFont>();
		}

		public void DisposeImages()
		{
		}

		public bool LoadImages()
		{
			foreach (GameFont font in Enum.GetValues(typeof(GameFont)))
			{
				if (this.gameFontToSpriteFontMapping.ContainsKey(font))
					continue;

				SpriteFont spriteFont = this.contentManager.Load<SpriteFont>(font.GetFontInfo().MonoGameSpriteFontName);

				int lineHeight = (int) Math.Round(double.Parse(font.GetFontInfo().LineHeight, CultureInfo.InvariantCulture));
				if (lineHeight < 1)
					lineHeight = 1;
				spriteFont.LineSpacing = lineHeight;

				this.gameFontToSpriteFontMapping[font] = spriteFont;
				return false;
			}

			return true;
		}

		public void DrawText(int x, int y, string text, GameFont font, DTColor color)
		{
			y = this.windowHeight - y - 1;
			Vector2 position = new Vector2(x, y);

			this.spriteBatch.DrawString(
				spriteFont: this.gameFontToSpriteFontMapping[font], 
				text: text, 
				position: position, 
				color: (new Color(r: color.R, g: color.G, b: color.B, alpha: 255)) * (color.Alpha / 255.0f));
		}

		public void TryDrawText(int x, int y, string text, GameFont font, DTColor color)
		{
			if (this.gameFontToSpriteFontMapping.ContainsKey(font))
				this.DrawText(x: x, y: y, text: text, font: font, color: color);
		}
	}
}
