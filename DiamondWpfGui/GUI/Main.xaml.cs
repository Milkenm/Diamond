using Diamond.Core;
using Diamond.WPF.Structures;

using Discord;

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace Diamond.WPF.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        public static DiamondCore diamondCore;
        public static SQLiteDB database;

        public static readonly Dictionary<EFolder, Folder> folders = new Dictionary<EFolder, Folder>();

        public enum EFolder
        {
            AppData,
            Temp,
        }

        public static async void CreateFolders()
        {
            await Task.Run(new Action(() =>
            {
                foreach (Folder folder in folders.Values)
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
            database = new SQLiteDB(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Milkenm\Diamond\DiamondDB.db");

            string token = null;
            try
            {
                DataTable dt = database.ExecuteSQL("SELECT Value FROM Configs WHERE Config = 'BotToken'");

                token = dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                listBox_log.Items.Add(ex.Message);
            }

            if (!string.IsNullOrEmpty(token))
            {
                diamondCore = new DiamondCore(token, LogSeverity.Info, "!", Assembly.GetEntryAssembly(), Client_Log);
            }

            diamondCore.Client.ReactionAdded += Client_ReactionAdded;
            diamondCore.Commands.CommandExecuted += Commands_CommandExecuted;
            diamondCore.Client.MessageReceived += Client_MessageReceived;

            folders.Add(EFolder.AppData, new Folder(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Milkenm\Diamond\"));
            folders.Add(EFolder.Temp, new Folder(folders[EFolder.AppData].Path + @"Temp\"));
            CreateFolders();
        }

        private async Task Client_MessageReceived(Discord.WebSocket.SocketMessage msg)
        {
            if (msg.Content == "(╯°□°）╯︵ ┻━┻")
            {
                await msg.Channel.SendMessageAsync("┬─┬ ノ( ゜-゜ノ)").ConfigureAwait(false);
            }
        }

        private async Task Commands_CommandExecuted(Optional<Discord.Commands.CommandInfo> command, Discord.Commands.ICommandContext context, Discord.Commands.IResult result)
        {
            if (!string.IsNullOrEmpty(result?.ErrorReason) && command.IsSpecified)
            {
                await context.Channel.SendMessageAsync(result.ErrorReason).ConfigureAwait(false);

                await LogToListBox($"{context.User.Username} executed the command \"{command.Value.Name}\" on server \"{context.Guild.Name}\".").ConfigureAwait(false);
            }
        }

        private async Task Client_ReactionAdded(Cacheable<IUserMessage, ulong> message, Discord.WebSocket.ISocketMessageChannel channel, Discord.WebSocket.SocketReaction reaction)
        {
            await LogToListBox(message).ConfigureAwait(false);
        }

        private async Task Client_Log(LogMessage arg)
        {
            await LogToListBox(arg.Message).ConfigureAwait(false);
        }

        public async Task LogToListBox(object item)
        {
            await Task.Run(new Action(() => Dispatcher.Invoke(new Action(() => listBox_log.Items.Add(item))))).ConfigureAwait(false);
        }

        private void Button_start_Click(object sender, RoutedEventArgs e)
        {
            if (!diamondCore.IsRunning)
            {
                diamondCore.Start();
                button_start.Content = "Stop";
            }
            else
            {
                diamondCore.Stop();
                button_start.Content = "Start";
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            diamondCore.Stop();
        }
    }
}