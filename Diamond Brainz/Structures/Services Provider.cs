using Diamond.Brainz.Structures;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;

namespace Diamond.Brainz
{
	public class ServicesProvider
	{
		public static ServiceProvider GetServiceProvider()
		{
			Config config = new Config(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/config.json");

			Dictionary<FolderType, Folder> foldersDictionary = new Dictionary<FolderType, Folder>();
			foldersDictionary.Add(FolderType.AppData, new Folder(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Milkenm\Diamond\"));
			foldersDictionary.Add(FolderType.Temp, new Folder(foldersDictionary[FolderType.AppData].Path + @"Temp\"));
			foldersDictionary.Add(FolderType.Data, new Folder(foldersDictionary[FolderType.AppData].Path + @"Data\"));



			ServiceCollection sc = new ServiceCollection();

			sc.AddSingleton<Dictionary<FolderType, Folder>>();
			sc.AddSingleton(foldersDictionary);
			sc.AddSingleton(config);
			sc.AddSingleton(new Database(config.JsonConfig.DatabaseConfig));
			sc.AddSingleton(new Logger());




			return sc.BuildServiceProvider();
		}
	}
}
