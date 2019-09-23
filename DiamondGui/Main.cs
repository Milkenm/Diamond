#region Usings
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DiamondGui.Values;
#endregion Usings



namespace DiamondGui
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            new Task(new Action(() =>
            {
                MainAsync().GetAwaiter().GetResult();
            })).Start();
        }

        public async Task MainAsync()
        {
            Client = new DiscordSocketClient();

            Client.Log += Log;

            // Remember to keep token private or to read it from an 
            // external source! In this case, we are reading the token 
            // from an environment variable. If you do not know how to set-up
            // environment variables, you may find more information on the 
            // Internet or by using other methods such as reading from 
            // a configuration.
            await Client.LoginAsync(TokenType.Bot, textBox_token.Text);
            await Client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
