using System.Windows.Controls;

using Discord;

namespace Diamond.GUI.Pages;
/// <summary>
/// Interaction logic for LogsPanelPage.xaml
/// </summary>
public partial class LogsPanelPage : Page
{
	public LogsPanelPage()
	{
		InitializeComponent();
	}

	public void Log(string msg)
	{
		Dispatcher.Invoke(() =>
		{
			listBox_log.Items.Add(msg);
		});
	}

	public void Log(LogMessage msg)
	{
		Log(msg.Message);
	}

	public void Log(object msg)
	{
		Log(msg.ToString());
	}
}
