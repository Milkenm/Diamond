using Diamond.Brainz.Data;
using Diamond.Brainz.Events;
using Diamond.Brainz.Structures;
using Diamond.Core;

using Discord;

using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;

using static Diamond.Brainz.Utils.Folders;

namespace Diamond.Brainz
{
    public class Brainz
    {
        public void Start()
        {
            GlobalData.Brainz = this;
            GlobalData.DB = new SQLiteDB(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Milkenm\Diamond\DiamondDB.db");

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

            if (!string.IsNullOrEmpty(token))
            {
                GlobalData.DiamondCore = new DiamondCore(token, LogSeverity.Info, "!", Assembly.GetAssembly(typeof(Brainz)), new MiscEvents().Log);
            }

            GlobalData.DiamondCore.Client.ReactionAdded += new ClientEvents().ReactionAdded;
            GlobalData.DiamondCore.Commands.CommandExecuted += new CommandEvents().CommandExecuted;
            GlobalData.DiamondCore.Client.MessageReceived += new ClientEvents().MessageReceived;
            GlobalData.DiamondCore.AddDebugChannels(657392886966517782, 622150096720756736, 681532995374415895, 738383172084957254);

            GlobalData.Folders.Add(EFolder.AppData, new Folder(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Milkenm\Diamond\"));
            GlobalData.Folders.Add(EFolder.Temp, new Folder(GlobalData.Folders[EFolder.AppData].Path + @"Temp\"));
            CreateFolders();
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