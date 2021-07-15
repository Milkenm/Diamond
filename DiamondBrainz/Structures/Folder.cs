using System.IO;

namespace Diamond.Brainz.Structures
{
	public class Folder
	{
		public Folder(string path)
		{
			this.Path = path;

			this.CreateFolder();
		}

		public string Path { get; }

		public bool Exists
		{
			get
			{
				return Directory.Exists(this.Path);
			}
		}

		public string CreateFolder()
		{
			if (!this.Exists)
			{
				Directory.CreateDirectory(this.Path);
			}

			return this.Path;
		}
	}

	public enum FolderType
	{
		AppData,
		Temp,
		Data,
	}
}