
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using Microsoft.Xna.Framework.Audio;
	using System;
	using System.Collections.Generic;
	using System.IO;

	public class MonoGameSoundOutput : ISoundOutput<GameSound>
    {
        private Dictionary<GameSound, SoundEffect> gameSoundToSoundEffectMapping;
		private int desiredSoundVolume;
		private int currentSoundVolume;
		private int elapsedMicrosPerFrame;

        public MonoGameSoundOutput(int elapsedMicrosPerFrame)
        {
			this.gameSoundToSoundEffectMapping = new Dictionary<GameSound, SoundEffect>();
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
			string monoGameSoundDirectory = null;

            foreach (GameSound sound in Enum.GetValues(typeof(GameSound)))
            {
				if (this.gameSoundToSoundEffectMapping.ContainsKey(sound))
					continue;

				string soundFilename = sound.GetSoundFilename().WavFilename;

				SoundEffect soundEffect;

				if (File.Exists(soundDirectory + soundFilename))
				{
					soundEffect = SoundEffect.FromFile(soundDirectory + soundFilename);
				}
				else
				{
					if (monoGameSoundDirectory == null)
						monoGameSoundDirectory = Util.GetMonoGameSoundDirectory();
					soundEffect = SoundEffect.FromFile(monoGameSoundDirectory + soundFilename);
				}

				this.gameSoundToSoundEffectMapping[sound] = soundEffect;
				return false;
            }

			return true;
        }

        public void PlaySound(GameSound sound)
        {
			float finalVolume = sound.GetSoundVolume() / 100.0f * this.currentSoundVolume / 100.0f;

			if (finalVolume > 1.0f)
				finalVolume = 1.0f;

			if (finalVolume < 0.0f)
				finalVolume = 0.0f;

			if (finalVolume > 0.0f)
				this.gameSoundToSoundEffectMapping[sound].Play(volume: finalVolume, pitch: 0.0f, pan: 0.0f);
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

		public void SetSoundVolumeImmediately(int volume)
		{
			if (volume < 0)
				throw new Exception();

			if (volume > 100)
			{
				throw new Exception();
			}

			this.desiredSoundVolume = volume;
			this.currentSoundVolume = volume;
		}
	}
}
