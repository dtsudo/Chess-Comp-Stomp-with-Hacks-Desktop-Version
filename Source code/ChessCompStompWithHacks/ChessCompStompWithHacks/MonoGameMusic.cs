
namespace ChessCompStompWithHacks
{
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using Microsoft.Xna.Framework.Audio;
	using System;
	using System.Collections.Generic;

	public class MonoGameMusic : IMusic<ChessMusic>
    {
		private Dictionary<ChessMusic, SoundEffectInstance> chessMusicToSoundEffectInstanceMapping;

		public MonoGameMusic()
		{
			this.chessMusicToSoundEffectInstanceMapping = new Dictionary<ChessMusic, SoundEffectInstance>();
		}

        public void DisposeMusic()
        {
        }

        public bool LoadMusic()
        {
			string musicDirectory = Util.GetMusicDirectory();

			foreach (ChessMusic music in Enum.GetValues(typeof(ChessMusic)))
			{
				if (this.chessMusicToSoundEffectInstanceMapping.ContainsKey(music))
					continue;

				SoundEffect soundEffect = SoundEffect.FromFile(musicDirectory + music.GetMusicFilename());

				this.chessMusicToSoundEffectInstanceMapping[music] = soundEffect.CreateInstance();
				this.chessMusicToSoundEffectInstanceMapping[music].Stop();
				this.chessMusicToSoundEffectInstanceMapping[music].IsLooped = true;

				return false;
			}

			return true;
        }

        public void PlayMusic(ChessMusic music, int volume)
        {
			foreach (KeyValuePair<ChessMusic, SoundEffectInstance> mapEntry in this.chessMusicToSoundEffectInstanceMapping)
			{
				if (mapEntry.Key != music)
					mapEntry.Value.Stop();
			}

			float finalVolume = (music.GetMusicVolume() / 100.0f) * (volume / 100.0f);
			if (finalVolume > 1.0f)
				finalVolume = 1.0f;
			if (finalVolume < 0.0f)
				finalVolume = 0.0f;

			this.chessMusicToSoundEffectInstanceMapping[music].Volume = finalVolume;

			try
			{
				this.chessMusicToSoundEffectInstanceMapping[music].Play();
			}
			catch (Exception)
			{
			}
        }

        public void StopMusic()
        {
			foreach (KeyValuePair<ChessMusic, SoundEffectInstance> mapEntry in this.chessMusicToSoundEffectInstanceMapping)
				mapEntry.Value.Stop();
        }
	}
}
