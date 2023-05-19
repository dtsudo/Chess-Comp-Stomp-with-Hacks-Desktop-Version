
namespace ChessCompStompWithHacks
{
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	using System.IO;

	public class FileIO : IFileIO
	{
		private string path;

		public FileIO(string pathNotIncludingTrailingSlash)
		{
			this.path = pathNotIncludingTrailingSlash;
		}

		private string GetFileName(int fileId, VersionInfo versionInfo)
		{
			return this.path + "/file" + fileId.ToStringCultureInvariant() + "_v" + versionInfo.AlphanumericVersionGuid;
		}

		public ByteList FetchData(int fileId, VersionInfo versionInfo)
		{
			try
			{
				string fileName = this.GetFileName(fileId: fileId, versionInfo: versionInfo);

				if (!File.Exists(fileName))
					return null;

				ByteList.Builder bytes = new ByteList.Builder();

				using (FileStream fileStream = new FileStream(path: fileName, mode: FileMode.Open, access: FileAccess.Read))
				{
					using (BinaryReader binaryReader = new BinaryReader(fileStream))
					{
						byte[] buffer = new byte[1];

						while (true)
						{
							int numBytesRead = binaryReader.Read(buffer, 0, 1);

							if (numBytesRead == 0)
								break;

							byte b = buffer[0];
							bytes.Add(b);
						}
					}
				}

				return bytes.ToByteList();
			}
			catch (Exception)
			{
				return null;
			}
		}

		public void PersistData(int fileId, VersionInfo versionInfo, ByteList data)
		{
			try
			{
				ByteList.Iterator iterator = data.GetIterator();

				using (FileStream fileStream = new FileStream(path: this.GetFileName(fileId: fileId, versionInfo: versionInfo), mode: FileMode.Create))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
					{
						while (true)
						{
							if (!iterator.HasNextByte())
								break;
							byte b = iterator.TryPop();
							binaryWriter.Write(b);
						}
					}
				}
			} catch (Exception)
			{
				// do nothing
			}
		}
	}
}
