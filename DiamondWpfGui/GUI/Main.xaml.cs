using Diamond.Core;
using Diamond.WPF.SQLite;
using Discord;
using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace DiamondWpfGui
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class Main : Window
	{
		private static DiamondCore diamondCore;
		private static SQLiteDB database;

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
		}

		private Task Client_Log(LogMessage arg)
		{
			Dispatcher.Invoke(new Action(() => listBox_log.Items.Add(arg.Message)));
			return Task.CompletedTask;
		}

		private void Button_start_Click(object sender, RoutedEventArgs e)
		{
			if (!diamondCore.IsRunning)
			{
				diamondCore.Start();
			}
			else
			{
				diamondCore.Stop();
			}
		}
	}
}