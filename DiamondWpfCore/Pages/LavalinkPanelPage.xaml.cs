using System.Diagnostics;
using System.Windows.Controls;

using Diamond.API;

namespace Diamond.GUI.Pages;
/// <summary>
/// Interaction logic for LavalinkPanelPage.xaml
/// </summary>
public partial class LavalinkPanelPage : Page
{
	private readonly Lava _lava;

	public LavalinkPanelPage(Lava lava)
	{
		InitializeComponent();

		_lava = lava;
		_lava.GetLavalinkProcess().OutputDataReceived += new DataReceivedEventHandler((s, d) =>
		{
			if (d.Data == null) return;

			try
			{
				Dispatcher.Invoke(() =>
				{
					richTextBox_lavalink.AppendText(d.Data, printDate: true);
					richTextBox_lavalink.ScrollToEnd();
				});
			}
			catch { }
		});
	}
}
