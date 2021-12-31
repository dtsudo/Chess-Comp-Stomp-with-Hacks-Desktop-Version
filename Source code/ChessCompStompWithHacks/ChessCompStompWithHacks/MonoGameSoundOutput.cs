
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using Microsoft.Xna.Framework.Audio;
	using System;
	using System.Collections.Generic;

	public class MonoGameSoundOutput : ISoundOutput<ChessSound>
    {
		private int desiredSoundVolume;
		private int currentSoundVolume;
		private int elapsedMicrosPerFrame;

        public MonoGameSoundOutput(int elapsedMicrosPerFrame)
        {
			this.desiredSoundVolume = GlobalState.DEFAULT_VOLUME;
			this.currentSoundVolume = GlobalState.DEFAULT_VOLUME;
			this.elapsedMicrosPerFrame = elapsedMicrosPerFrame;
        }

        public void DisposeSounds()
        {
        }

        public int GetSoundVolume()
        {
			return this.desiredSoundVolume;
        }

        public bool LoadSounds()
        {
			return true;
        }

        public void PlaySound(ChessSound sound)
        {
        }

        public void ProcessFrame()
        {
			this.currentSoundVolume = VolumeUtil.GetVolumeSmoothed(
				elapsedMicrosPerFrame: this.elapsedMicrosPerFrame,
				currentVolume: this.currentSoundVolume,
				desiredVolume: this.desiredSoundVolume);
        }

        public void SetSoundVolume(int volume)
        {
			if (volume < 0)
				throw new Exception();

			if (volume > 100)
			{
				throw new Exception();
			}

			this.desiredSoundVolume = volume;
        }
    }
}
