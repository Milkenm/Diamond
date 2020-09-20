using Diamond.Brainz.Data;
using Diamond.Core;

using Discord;

using System;
using System.Threading.Tasks;
using System.Windows;

namespace Diamond.WPFCore.GUI
{
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitBrainz();
        }

        void InitBrainz()
        {
            Brainz.Brainz bz = new Brainz.Brainz();
            bz.Log += new Brainz.Brainz.LogEvent(async (msg) => await LogToListBox(msg).ConfigureAwait(false));
            bz.Error += new Brainz.Brainz.ErrorEvent(async (ex) => await LogToListBox(ex.Message).ConfigureAwait(false));
            bz.Start();
        }

        public async Task LogToListBox(object message)
        {
            await Task.Run(() => this.Dispatcher.Invoke(() => listBox_log.Items.Add(message))).ConfigureAwait(false);
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
