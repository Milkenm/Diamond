using System.Threading.Tasks;
using System.Windows.Controls;

using Discord;

namespace Diamond.GUI.Pages;
/// <summary>
/// Interaction logic for LogsPanelPage.xaml
/// </summary>
public partial class LogsPanelPage : Page
{
	private static LogsPanelPage Instance;

	public static LogsPanelPage GetInstance()
	{
		return Instance ?? new LogsPanelPage();
	}

	public LogsPanelPage()
	{
		InitializeComponent();

		Instance = this;
	}

	public async Task Log(LogMessage msg)
	{
		Dispatcher.Invoke(() =>
		{
			listBox_log.Items.Add(msg.Message);
		});
	}
}
