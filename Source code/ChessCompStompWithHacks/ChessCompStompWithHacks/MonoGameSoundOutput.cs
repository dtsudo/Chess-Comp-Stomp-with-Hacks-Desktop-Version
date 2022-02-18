
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using Microsoft.Xna.Framework.Audio;
	using System;
	using System.Collections.Generic;

	public class MonoGameSoundOutput : ISoundOutput<ChessSound>
    {
        private Dictionary<ChessSound, SoundEffect> chessSoundToSoundEffectMapping;
		private int desiredSoundVolume;
		private int currentSoundVolume;
		private int elapsedMicrosPerFrame;

        public MonoGameSoundOutput(int elapsedMicrosPerFrame)
        {
			this.chessSoundToSoundEffectMapping = new Dictionary<ChessSound, SoundEffect>();
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
            string soundDirectory = Util.GetSoundDirectory();
            foreach (ChessSound sound in Enum.GetValues(typeof(ChessSound)))
            {
				if (this.chessSoundToSoundEffectMapping.ContainsKey(sound))
					continue;

				string soundFilename = sound.GetSoundFilename().WavFilename;
				SoundEffect soundEffect = SoundEffect.FromFile(soundDirectory + soundFilename);
				this.chessSoundToSoundEffectMapping[sound] = soundEffect;
				return false;
            }

			return true;
        }

        public void PlaySound(ChessSound sound)
        {
			float finalVolume = sound.GetSoundVolume() / 100.0f * this.currentSoundVolume / 100.0f;

			if (finalVolume > 1.0f)
				finalVolume = 1.0f;

			if (finalVolume < 0.0f)
				finalVolume = 0.0f;

			if (finalVolume > 0.0f)
				this.chessSoundToSoundEffectMapping[sound].Play(volume: finalVolume, pitch: 0.0f, pan: 0.0f);
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
