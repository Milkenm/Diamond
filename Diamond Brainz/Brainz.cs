using Diamond.Brainz.Data;
using Diamond.Brainz.Events;
using Diamond.Brainz.Structures;
using Diamond.Brainz.Utils;
using Diamond.Core;

using Discord;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;

using static Diamond.Brainz.Utils.Folders;

namespace Diamond.Brainz
{
	public class Brainz
	{
		public void Start()
		{
			// INITIALIZE
			GlobalData.Brainz = this;

			// CREATE FOLDERS
			GlobalData.Folders.Add(EFolder.AppData, new Folder(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Milkenm\Diamond\"));
			GlobalData.Folders.Add(EFolder.Temp, new Folder(GlobalData.Folders[EFolder.AppData].Path + @"Temp\"));
			GlobalData.Folders.Add(EFolder.Data, new Folder(GlobalData.Folders[EFolder.AppData].Path + @"Data\"));
			CreateFolders();

			// LOAD DATABASE
			GlobalData.DB = new SQLiteDB(GlobalData.Folders[EFolder.Data].Path + "DiamondDB.db");

			// GET TOKEN
			string token = null;
			try
			{
				DataTable dt = GlobalData.DB.ExecuteSQL("SELECT Value FROM Configs WHERE Config = 'BotToken'");
				token = dt.Rows[0][0].ToString();
			}
			catch (Exception ex)
			{
				Error?.Invoke(ex);
			}

			// LOAD DIAMOND CORE
			if (!string.IsNullOrEmpty(token))
			{
				GlobalData.DiamondCore = new DiamondCore(token, LogSeverity.Info, "!", Assembly.GetAssembly(typeof(Brainz)), new MiscEvents().Log);
			}

			// SUB EVENTS
			GlobalData.DiamondCore.Client.MessageReceived += new ClientEvents().MessageReceived;
			GlobalData.DiamondCore.Client.ReactionAdded += new ClientEvents().ReactionAdded;
			GlobalData.DiamondCore.Client.ReactionRemoved += new ClientEvents().ReactionRemoved;
			GlobalData.DiamondCore.Commands.CommandExecuted += new CommandEvents().CommandExecuted;
			GlobalData.DiamondCore.AddDebugChannels(657392886966517782, 622150096720756736, 681532995374415895, 738383172084957254);

			// LOAD DATATABLES
			DataTablesManager(DataTableAction.Read);

			// PREPARE DATATABLE AUTO SAVE
			Timer timer = new Timer();
			timer.Interval = TimeToMilliseconds.ConvertToMilliseconds(TimeToMilliseconds.TimeUnits.Minutes, 30);
			timer.AutoReset = true;
			timer.Elapsed += new ElapsedEventHandler((s, e) => DataTablesManager(DataTableAction.Save));
		}

		public void DataTablesManager(DataTableAction action)
		{
			if (action == DataTableAction.Save)
			{
				DataTableSaver.SaveToFile(@"C:\Testing\Kek.bin", GlobalData.RRMessagesDataTable.DTable);
			}
			else if (action == DataTableAction.Read)
			{
				if (File.Exists(@"C:\Testing\Kek.bin"))
				{
					GlobalData.RRMessagesDataTable.DTable = DataTableSaver.ReadFromFile(@"C:\Testing\Kek.bin");
				}
			}
			return;

			List<DataTable> dataTables = new List<DataTable>()
			{
				GlobalData.TTTGamesDataTable.GetDataTable(),
				GlobalData.RRMessagesDataTable.DTable,
			};

			for (int i = 0; i < dataTables.Count; i++)
			{
				string fileName = GlobalData.Folders[EFolder.Data].Path + dataTables[i].TableName;

				if (action == DataTableAction.Save)
				{
					if (File.Exists(fileName))
					{
						File.Delete(fileName);
					}
					DataTableSaver.SaveToFile(GlobalData.Folders[EFolder.Data].Path + dataTables[i].TableName, dataTables[i]);
				}
				else
				{
					if (File.Exists(fileName))
					{
						dataTables[i] = DataTableSaver.ReadFromFile(fileName);
					}
				}
			}
		}

		public enum DataTableAction
		{
			Save,
			Read,
		}

		internal async Task TriggerLogEventAsync(object message)
		{
			await Task.Run(new Action(() => Log?.Invoke(message))).ConfigureAwait(false);
		}

		public delegate void LogEvent(object message);
		public event LogEvent Log;

		public delegate void ErrorEvent(Exception exception);
		public event ErrorEvent Error;
	}
}