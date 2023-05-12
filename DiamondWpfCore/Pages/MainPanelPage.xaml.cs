﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using Diamond.API.ConsoleCommands;
using Diamond.API.Data;

using Discord;
using Discord.WebSocket;

using ScriptsLibV2.Extensions;

namespace Diamond.GUI.Pages;
/// <summary>
/// Interaction logic for MainPanelPage.xaml
/// </summary>
public partial class MainPanelPage : Page
{
	private readonly DiscordSocketClient _client;
	private readonly DiamondDatabase _database;
	private readonly ConsoleCommandsManager _consoleCommandsManager;

	public MainPanelPage(DiscordSocketClient client, DiamondDatabase database, ConsoleCommandsManager consoleCommandsManager)
	{
		this.InitializeComponent();

		this._client = client;
		this._database = database;
		this._consoleCommandsManager = consoleCommandsManager;
	}

	private async void ButtonStart_Click(object sender, RoutedEventArgs e)
	{
		// Start
		if (this._client.LoginState is not LoginState.LoggedIn and not LoginState.LoggingIn)
		{
			string token = this._database.GetSetting(DiamondDatabase.ConfigSetting.Token);
			if (token.IsEmpty())
			{
				MessageBox.Show("Bot token is not set.");
				return;
			}
			await this._client.BringToLifeAsync(token);
			this.button_start.Content = "Stop";
		}
		// Stop
		else
		{
			await this._client.TakeLifeAsync();
			this.button_start.Content = "Start";
		}
	}

	public async Task LogAsync(object message)
	{
		if (message == null)
		{
			return;
		}

		this.Dispatcher.Invoke(() =>
		{
			this.richTextBox_output.AppendText(message.ToString(), printDate: true);
			this.richTextBox_output.ScrollToEnd();
		});
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		string command = this.textBox_command.Text;
		if (command.IsEmpty())
		{
			return;
		}

		string[] splitCommand = command.Split(' ');
		string commandName = splitCommand[0];

		string result;
		if (this._consoleCommandsManager.CommandExists(commandName))
		{
			this.richTextBox_output.AppendText($"> {command}", new SolidColorBrush(System.Windows.Media.Color.FromRgb(7, 242, 72)), true);
			this.richTextBox_output.ScrollToEnd();

			string[] args = splitCommand.Skip(1).ToArray();
			try
			{
				result = this._consoleCommandsManager.RunCommand(commandName, args);
			}
			catch (Exception ex)
			{
				result = $"Error running command '{commandName}': {ex.Message}";
			}
		}
		else
		{
			result = $"The command '{commandName}' was not found.";
		}

		this.textBox_command.Text = string.Empty;

		this.richTextBox_output.AppendText(result, printDate: true);
		this.richTextBox_output.ScrollToEnd();
	}

	private void textBox_command_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key is Key.Enter or Key.Return)
		{
			this.Button_Click(sender, null);
		}
	}
}
