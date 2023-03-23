using Diamond.Brainz.Classes;
using Diamond.Brainz.Structures;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;

namespace Diamond.Brainz
{
	public class GlobalData
	{
		public GlobalData(string dbConfigPath = @"\db_config.json")
		{
			FoldersDictionary = new Dictionary<FolderType, Folder>();
			Logger = new Logger();

			FoldersDictionary.Add(FolderType.AppData, new Folder(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Milkenm\Diamond\"));
			FoldersDictionary.Add(FolderType.Temp, new Folder(FoldersDictionary[FolderType.AppData].Path + @"Temp\"));
			FoldersDictionary.Add(FolderType.Data, new Folder(FoldersDictionary[FolderType.AppData].Path + @"Data\"));
		}

		public readonly Dictionary<FolderType, Folder> FoldersDictionary;
		public readonly Logger Logger;
		public readonly Database Database;
	}
}
