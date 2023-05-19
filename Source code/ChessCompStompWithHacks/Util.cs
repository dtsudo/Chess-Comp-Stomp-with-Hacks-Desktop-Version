
namespace ChessCompStompWithHacks
{
	using System;
	using System.Diagnostics;
	using System.IO;

	public class Util
	{
		public static string GetImagesDirectory()
		{
			string path = GetExecutablePath();

			// The images are expected to be in the /Data/Images/ folder
			// relative to the executable.
			if (Directory.Exists(path + "/Data/Images"))
				return path + "/Data/Images" + "/";

			// However, if the folder doesn't exist, search for the /Data/Images folder
			// using some heuristic.
			while (true)
			{
				int i = Math.Max(path.LastIndexOf("/", StringComparison.Ordinal), path.LastIndexOf("\\", StringComparison.Ordinal));

				if (i == -1)
					throw new Exception("Cannot find images directory");

				path = path.Substring(0, i);

				if (Directory.Exists(path + "/Data/Images"))
					return path + "/Data/Images" + "/";
			}
		}

		public static string GetMusicDirectory()
		{
			string path = GetExecutablePath();

			// The music files are expected to be in the /Data/Music/ folder
			// relative to the executable.
			if (Directory.Exists(path + "/Data/Music"))
				return path + "/Data/Music" + "/";

			// However, if the folder doesn't exist, search for the /Data/Music folder
			// using some heuristic.
			while (true)
			{
				int i = Math.Max(path.LastIndexOf("/", StringComparison.Ordinal), path.LastIndexOf("\\", StringComparison.Ordinal));

				if (i == -1)
					throw new Exception("Cannot find music directory");

				path = path.Substring(0, i);

				if (Directory.Exists(path + "/Data/Music"))
					return path + "/Data/Music" + "/";
			}
		}

		public static string GetMonoGameMusicDirectory()
		{
			string path = GetExecutablePath();

			if (Directory.Exists(path + "/Data_MonoGame/Music"))
				return path + "/Data_MonoGame/Music" + "/";

			while (true)
			{
				int i = Math.Max(path.LastIndexOf("/", StringComparison.Ordinal), path.LastIndexOf("\\", StringComparison.Ordinal));

				if (i == -1)
					throw new Exception("Cannot find MonoGame music directory");

				path = path.Substring(0, i);

				if (Directory.Exists(path + "/Data_MonoGame/Music"))
					return path + "/Data_MonoGame/Music" + "/";
			}
		}

		public static string GetSoundDirectory()
		{
			string path = GetExecutablePath();

			// The sound files are expected to be in the /Data/Sound/ folder
			// relative to the executable.
			if (Directory.Exists(path + "/Data/Sound"))
				return path + "/Data/Sound" + "/";

			// However, if the folder doesn't exist, search for the /Data/Sound folder
			// using some heuristic.
			while (true)
			{
				int i = Math.Max(path.LastIndexOf("/", StringComparison.Ordinal), path.LastIndexOf("\\", StringComparison.Ordinal));

				if (i == -1)
					throw new Exception("Cannot find sound directory");

				path = path.Substring(0, i);

				if (Directory.Exists(path + "/Data/Sound"))
					return path + "/Data/Sound" + "/";
			}
		}

		public static string GetMonoGameSoundDirectory()
		{
			string path = GetExecutablePath();

			if (Directory.Exists(path + "/Data_MonoGame/Sound"))
				return path + "/Data_MonoGame/Sound" + "/";

			while (true)
			{
				int i = Math.Max(path.LastIndexOf("/", StringComparison.Ordinal), path.LastIndexOf("\\", StringComparison.Ordinal));

				if (i == -1)
					throw new Exception("Cannot find MonoGame sound directory");

				path = path.Substring(0, i);

				if (Directory.Exists(path + "/Data_MonoGame/Sound"))
					return path + "/Data_MonoGame/Sound" + "/";
			}
		}

		/// <summary>
		/// Returns the path of where the executable is located; should not include the trailing
		/// slash or backslash.
		/// 
		/// Note that the location of where the executable is has no relationship with the current
		/// working directory (usually denoted as ".").  It is possible to execute a program from
		/// any current working directory.
		/// </summary>
		public static string GetExecutablePath()
		{
			string executablePath = Process.GetCurrentProcess().MainModule.FileName;
			DirectoryInfo executableDirectory = Directory.GetParent(executablePath);

			return executableDirectory.FullName;
		}
	}
}
