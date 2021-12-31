
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using Microsoft.Xna.Framework.Audio;
	using System;
	using System.Collections.Generic;

	public class MonoGameMusic : IMusic<ChessMusic>
    {
		public MonoGameMusic()
		{
		}

        public void DisposeMusic()
        {
        }

        public bool LoadMusic()
        {
			return true;
        }

        public void PlayMusic(ChessMusic music, int volume)
        {
        }

        public void StopMusic()
        {
        }
	}
}
