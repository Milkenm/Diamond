#region Usings
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
#endregion Usings



namespace Diamond
{
	class Program
	{
		#region Main
		static void Main(string[] args)
		{
			try
			{
				_DatabasePath = @"C:\Milkenm\Data\DiamondData.mdf";
				new Program().MainAsync().GetAwaiter().GetResult();
			}
			catch (Exception _Exception)
			{
				MessageBox.Show(_Exception.Message, "Main()");
			}
		}

		public async Task MainAsync()
		{
			try
			{
				Scripts.DC dc = new Scripts.DC();
				dc.Load(LogSeverity.Debug);
				Scripts.Client.MessageReceived += this.Client_MessageReceived;
				await Scripts.Commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);

				Scripts.Client.Ready += this.Client_Ready;
				Scripts.Client.Log += this.Client_Log;

				await Scripts.Client.LoginAsync(TokenType.Bot, Select("DConfig", "Value", "Config='Token'")[0]);
				await Scripts.Client.StartAsync();

				AppDomain.CurrentDomain.ProcessExit += this.CurrentDomain_ProcessExit;

				Timer().Start();
				await Task.Delay(-1);
			}
			catch (Exception _Exception)
			{
				MessageBox.Show(_Exception.Message, "MainAsync()");
			}
		}
		#endregion Main



		#region Uptime
		public static int _Uptime { get; set; }

		private async Task Timer()
		{
			try
			{
				_Uptime++;
				Thread.Sleep(1000);
				await Timer();
			}
			catch (Exception _Exception)
			{
				MessageBox.Show(_Exception.Message, "Timer_Tick()");
			}
		}
		#endregion Uptime



		#region Events
		private async Task Client_Log(LogMessage logMessage)
		{
			try
			{
				Console.WriteLine("[" + DateTime.Now + " - " + logMessage.Source + "] " + logMessage.Message);
			}
			catch (Exception _Exception)
			{
				MessageBox.Show(_Exception.Message, "Client_Log()");
			}
		}

		private void CurrentDomain_ProcessExit(object sender, EventArgs e)
		{
			try
			{
				_Database.Update("DData", $"Value = '{_Uptime}'", "Data = 'Uptime'");
				Scripts.Client.LogoutAsync();
			}
			catch (Exception _Exception)
			{
				MessageBox.Show(_Exception.Message, "CurrentDomain_ProcessExit()");
			}
		}

		private async Task Client_Ready()
		{
			try
			{
				await Scripts.Client.SetGameAsync("Hello!", _Database.Select("DConfig", "Value", "Config = 'StreamUrl'")[0], ActivityType.Streaming);
			}
			catch (Exception _Exception)
			{
				MessageBox.Show(_Exception.Message, "Client_Read()");
			}
		}

		private async Task Client_MessageReceived(SocketMessage socketMessage)
		{
			try
			{
				var message = socketMessage as SocketUserMessage;
				var context = new SocketCommandContext(Scripts.Client, message);
				int argPos = 0;
				if (context.Message == null || context.Message.Content == "" || context.User.IsBot == true || !message.HasStringPrefix("!", ref argPos)) return;

				await Scripts.Commands.ExecuteAsync(context, argPos, null);
			}
			catch (Exception _Exception)
			{
				MessageBox.Show(_Exception.Message, "Client_MessageReceived()");
			}
		}
		#endregion Events
	}
}