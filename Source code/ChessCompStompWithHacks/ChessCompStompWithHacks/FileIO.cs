
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

		private string GetFileName(int fileId)
		{
			return this.path + "/file" + fileId.ToStringCultureInvariant();
		}

		public ByteList FetchData(int fileId)
		{
			try
			{
				string fileName = this.GetFileName(fileId: fileId);

				if (!File.Exists(fileName))
					return null;

				ByteList.Builder bytes = new ByteList.Builder();

				using (FileStream fileStream = new FileStream(path: fileName, mode: FileMode.Open, access: FileAccess.Read))
				{
					using (BinaryReader binaryReader = new BinaryReader(fileStream))
					{
						try
						{
							while (true)
							{
								byte b = binaryReader.ReadByte();
								bytes.Add(b);
							}
						}
						catch (EndOfStreamException)
						{
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

		public void PersistData(int fileId, ByteList data)
		{
			try
			{
				ByteList.Iterator iterator = data.GetIterator();

				using (FileStream fileStream = new FileStream(path: this.GetFileName(fileId: fileId), mode: FileMode.Create))
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
