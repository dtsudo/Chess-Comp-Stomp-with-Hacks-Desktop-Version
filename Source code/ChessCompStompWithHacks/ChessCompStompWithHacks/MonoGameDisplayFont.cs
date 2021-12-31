
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

		private Dictionary<ChessFont, SpriteFont> chessFontToSpriteFontMapping;

		public MonoGameDisplayFont(ContentManager contentManager, SpriteBatch spriteBatch, int windowHeight)
		{
			this.contentManager = contentManager;
			this.spriteBatch = spriteBatch;
			this.windowHeight = windowHeight;

			this.chessFontToSpriteFontMapping = new Dictionary<ChessFont, SpriteFont>();
		}

		public void DisposeImages()
		{
		}

		public bool LoadImages()
		{
			foreach (ChessFont font in Enum.GetValues(typeof(ChessFont)))
			{
				if (this.chessFontToSpriteFontMapping.ContainsKey(font))
					continue;

				SpriteFont spriteFont = this.contentManager.Load<SpriteFont>(font.GetFontInfo().MonoGameSpriteFontName);

				int lineHeight = (int) Math.Round(double.Parse(font.GetFontInfo().LineHeight, CultureInfo.InvariantCulture));
				if (lineHeight < 1)
					lineHeight = 1;
				spriteFont.LineSpacing = lineHeight;

				this.chessFontToSpriteFontMapping[font] = spriteFont;
				return false;
			}

			return true;
		}

		public void DrawText(int x, int y, string text, ChessFont font, DTColor color)
		{
			y = this.windowHeight - y - 1;
			Vector2 position = new Vector2(x, y);

			spriteBatch.DrawString(
				spriteFont: this.chessFontToSpriteFontMapping[font], 
				text: text, 
				position: position, 
				color: (new Color(r: color.R, g: color.G, b: color.B, alpha: 255)) * (color.Alpha / 255.0f));
		}
	}
}
