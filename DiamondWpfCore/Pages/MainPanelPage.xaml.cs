using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

	public MainPanelPage(DiscordSocketClient client, DiamondDatabase database)
	{
		this.InitializeComponent();

		this._client = client;
		this._database = database;
	}

	private async void ButtonStart_Click(object sender, RoutedEventArgs e)
	{
		// Start
		if (this._client.LoginState != LoginState.LoggedIn || this._client.LoginState != LoginState.LoggingIn)
		{
			string token = this._database.GetSetting(DiamondDatabase.ConfigSetting.Token);
			if (token.IsEmpty())
			{
				MessageBox.Show("Bot token is not set.");
				return;
			}
			await this._client.LoginAsync(TokenType.Bot, token);
			await this._client.StartAsync();
			this.button_start.Content = "Stop";
		}
		// Stop
		else
		{
			await this._client.LogoutAsync();
			await this._client.StopAsync();
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
			this.listBox_output.Items.Add(message.ToString());
			this.ScrollToEnd(this.listBox_output);
		});
	}

	private void ScrollToEnd(ListBox listBox)
	{
		if (VisualTreeHelper.GetChildrenCount(listBox) > 0)
		{
			Border border = (Border)VisualTreeHelper.GetChild(listBox, 0);
			ScrollViewer scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
			scrollViewer.ScrollToBottom();
		}
	}
}
