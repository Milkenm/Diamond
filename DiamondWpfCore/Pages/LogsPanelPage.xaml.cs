using System.Threading.Tasks;
using System.Windows.Controls;

using Diamond.API.Bot;

using Discord;

namespace Diamond.GUI.Pages;
/// <summary>
/// Interaction logic for LogsPanelPage.xaml
/// </summary>
public partial class LogsPanelPage : Page
{
	private readonly DiamondBot _bot;

	public LogsPanelPage(DiamondBot bot )
	{
		InitializeComponent();

		_bot = bot;
	}

	private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
	{

	}

	public async Task Log(LogMessage msg)
	{
		Dispatcher.Invoke(() =>
		{
			listBox_log.Items.Add(msg.Message);
		});
	}
}
