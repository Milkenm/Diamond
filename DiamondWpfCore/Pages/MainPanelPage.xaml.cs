using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Discord;

namespace Diamond.GUI.Pages;
/// <summary>
/// Interaction logic for MainPanelPage.xaml
/// </summary>
public partial class MainPanelPage : Page
{
	private static MainPanelPage Instance;
	public static MainPanelPage GetInstance()
	{
		return Instance ?? new MainPanelPage();
	}
	public MainPanelPage()
	{
		InitializeComponent();
		Instance = this;
	}

	private void ButtonStart_Click(object sender, RoutedEventArgs e)
	{
		// Start
		if (!AppWindow.GetInstance().Bot.IsRunning)
		{
			AppWindow.GetInstance().Bot.StartAsync().GetAwaiter();
			button_start.Content = "Stop";
		}
		// Stop
		else
		{
			AppWindow.GetInstance().Bot.StopAsync().GetAwaiter();
			button_start.Content = "Start";
		}
	}

	public async Task Log(LogMessage message)
	{
		Dispatcher.Invoke(() =>
		{
			listBox_output.Items.Add(message.Message);
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
