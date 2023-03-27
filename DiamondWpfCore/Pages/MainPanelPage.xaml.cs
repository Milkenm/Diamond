using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Diamond.API.Bot;

using Discord;

using ScriptsLibV2.Extensions;
namespace Diamond.GUI.Pages;
/// <summary>
/// Interaction logic for MainPanelPage.xaml
/// </summary>
public partial class MainPanelPage : Page
{
	private readonly DiamondBot _bot;

	public MainPanelPage(DiamondBot bot)
	{
		InitializeComponent();

		this._bot = bot;
	}

	private void Page_Loaded(object sender, RoutedEventArgs e)
	{
		_bot.Client.Log += new Func<LogMessage, Task>( (logMessage) => this.Log(logMessage.Message));
	}

	private void ButtonStart_Click(object sender, RoutedEventArgs e)
	{
		// Start
		if (!_bot.IsRunning)
		{
			if (_bot.Token.IsEmpty())
			{
				MessageBox.Show("Bot token is not set.");
				return;
			}
			_bot.StartAsync().GetAwaiter();
			button_start.Content = "Stop";
		}
		// Stop
		else
		{
			_bot.StopAsync().GetAwaiter();
			button_start.Content = "Start";
		}
	}

	public async Task Log(object message)
	{
		Dispatcher.Invoke(() =>
		{
			listBox_output.Items.Add(message.ToString());
			ScrollToEnd(listBox_output);
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
