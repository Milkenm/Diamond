using Diamond.Brainz.Data;
using Diamond.Brainz.Events;
using Diamond.Brainz.Structures;
using Diamond.Brainz.Utils;
using Diamond.Core;

using Discord;

using System;
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

		public void LoadCore()
		{
			if (GlobalData.DiamondCore == null)
			{
				string token = GetBotToken();

				if (token != null)
				{
					GlobalData.DiamondCore = new DiamondCore(token, LogSeverity.Info, "!", Assembly.GetAssembly(typeof(Brainz)), new MiscEvents().Log);

					// SUB EVENTS
					GlobalData.DiamondCore.Client.MessageReceived += new ClientEvents().MessageReceived;
					GlobalData.DiamondCore.Client.ReactionAdded += new ClientEvents().ReactionAdded;
					GlobalData.DiamondCore.Client.ReactionRemoved += new ClientEvents().ReactionRemoved;
					GlobalData.DiamondCore.Commands.CommandExecuted += new CommandEvents().CommandExecuted;
					GlobalData.DiamondCore.AddDebugChannels(657392886966517782, 622150096720756736, 681532995374415895, 738383172084957254);
				}
			}
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