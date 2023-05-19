
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using Microsoft.Xna.Framework.Audio;
	using System;
	using System.Collections.Generic;
	using System.IO;

	public class MonoGameMusic : IMusic<GameMusic>
    {
		private Dictionary<GameMusic, SoundEffectInstance> gameMusicToSoundEffectInstanceMapping;

		public MonoGameMusic()
		{
			this.gameMusicToSoundEffectInstanceMapping = new Dictionary<GameMusic, SoundEffectInstance>();
		}

        public void DisposeMusic()
        {
        }

        public bool LoadMusic()
        {
			string musicDirectory = Util.GetMusicDirectory();
			string monoGameMusicDirectory = null;

			foreach (GameMusic music in Enum.GetValues(typeof(GameMusic)))
			{
				if (this.gameMusicToSoundEffectInstanceMapping.ContainsKey(music))
					continue;

				string wavFilename = music.GetMusicFilename().WavFilename;

				SoundEffect soundEffect;

				if (File.Exists(musicDirectory + wavFilename))
				{
					soundEffect = SoundEffect.FromFile(musicDirectory + wavFilename);
				}
				else
				{
					if (monoGameMusicDirectory == null)
						monoGameMusicDirectory = Util.GetMonoGameMusicDirectory();
					soundEffect = SoundEffect.FromFile(monoGameMusicDirectory + wavFilename);
				}

				this.gameMusicToSoundEffectInstanceMapping[music] = soundEffect.CreateInstance();
				this.gameMusicToSoundEffectInstanceMapping[music].Stop();
				this.gameMusicToSoundEffectInstanceMapping[music].IsLooped = true;

				return false;
			}

			return true;
        }

        public void PlayMusic(GameMusic music, int volume)
        {
			foreach (KeyValuePair<GameMusic, SoundEffectInstance> mapEntry in this.gameMusicToSoundEffectInstanceMapping)
			{
				if (mapEntry.Key != music)
					mapEntry.Value.Stop();
			}

			float finalVolume = (music.GetMusicVolume() / 100.0f) * (volume / 100.0f);
			if (finalVolume > 1.0f)
				finalVolume = 1.0f;
			if (finalVolume < 0.0f)
				finalVolume = 0.0f;

			this.gameMusicToSoundEffectInstanceMapping[music].Volume = finalVolume;

			try
			{
				this.gameMusicToSoundEffectInstanceMapping[music].Play();
			}
			catch (Exception)
			{
			}
        }

        public void StopMusic()
        {
			foreach (KeyValuePair<GameMusic, SoundEffectInstance> mapEntry in this.gameMusicToSoundEffectInstanceMapping)
				mapEntry.Value.Stop();
        }
	}
}
