using Diamond.Core;
using Diamond.WPF.Data;
using Diamond.WPF.Events;
using Diamond.WPF.Structures;

using Discord;

using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Diamond.WPF.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        public enum EFolder
        {
            AppData,
            Temp,
        }

        public static async void CreateFolders()
        {
            await Task.Run(new Action(() =>
            {
                foreach (Folder folder in GlobalData.Folders.Values)
                {
                    folder.CreateFolder();
                }
            })).ConfigureAwait(false);
        }

        public Main()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalData.Main = this;

            GlobalData.DB = new SQLiteDB(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Milkenm\Diamond\DiamondDB.db");

            string token = null;
            try
            {
                DataTable dt = GlobalData.DB.ExecuteSQL("SELECT Value FROM Configs WHERE Config = 'BotToken'");
                token = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                listBox_log.Items.Add(ex.Message);
            }

            if (!string.IsNullOrEmpty(token))
            {
                GlobalData.DiamondCore = new DiamondCore(token, LogSeverity.Info, "!", Assembly.GetEntryAssembly(), new MiscEvents().Log);
            }

            GlobalData.DiamondCore.Client.ReactionAdded += new ClientEvents().ReactionAdded;
            GlobalData.DiamondCore.Commands.CommandExecuted += new CommandEvents().CommandExecuted;
            GlobalData.DiamondCore.Client.MessageReceived += new ClientEvents().MessageReceived;
            GlobalData.DiamondCore.AddDebugChannels(657392886966517782, 622150096720756736, 681532995374415895);

            GlobalData.Folders.Add(EFolder.AppData, new Folder(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Milkenm\Diamond\"));
            GlobalData.Folders.Add(EFolder.Temp, new Folder(GlobalData.Folders[EFolder.AppData].Path + @"Temp\"));
            CreateFolders();
        }

        public async Task LogToListBox(object item)
        {
            await Task.Run(() => Dispatcher.Invoke(() => listBox_log.Items.Add(item))).ConfigureAwait(false);
        }

        private void Button_start_Click(object sender, RoutedEventArgs e)
        {
            if (!GlobalData.DiamondCore.IsRunning)
            {
                GlobalData.DiamondCore.Start();
                button_start.Content = "Stop";
            }
            else
            {
                GlobalData.DiamondCore.Stop();
                button_start.Content = "Start";
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GlobalData.DiamondCore.Stop();
        }
    }
}