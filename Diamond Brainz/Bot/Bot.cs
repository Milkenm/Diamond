using Diamond.Brainz.Data;
using Diamond.Brainz.Structures;

using System;
using System.Collections.Generic;
using System.Text;

using static Diamond.Brainz.Utils.Folders;

namespace Diamond.Brainz
{
	public partial class Bot
	{
		public Bot()
		{
			// INITIALIZE
			GlobalData.Bot = this;

			// CREATE FOLDERS
			GlobalData.Folders.Add(EFolder.AppData, new Folder(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Milkenm\Diamond\"));
			GlobalData.Folders.Add(EFolder.Temp, new Folder(GlobalData.Folders[EFolder.AppData].Path + @"Temp\"));
			GlobalData.Folders.Add(EFolder.Data, new Folder(GlobalData.Folders[EFolder.AppData].Path + @"Data\"));
			CreateFolders();

			// LOAD DATABASE
			GlobalData.DB = new SQLiteDB(GlobalData.Folders[EFolder.Data].Path + "DiamondDB.db");

			// LOAD DIAMOND CORE
			LoadCore();

			// LOAD DATATABLES
			DataTablesManager(DataTableAction.Read);

			// PREPARE DATATABLE AUTO SAVE
			Timer timer = new Timer();
			timer.Interval = TimeToMilliseconds.ConvertToMilliseconds(TimeToMilliseconds.TimeUnits.Minutes, 30);
			timer.AutoReset = true;
			timer.Elapsed += new ElapsedEventHandler((s, e) => DataTablesManager(DataTableAction.Save));
		}
	}
}
