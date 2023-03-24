using System.Windows;

namespace Diamond.GUI
{
	/// <summary>
	/// Interaction logic for AppWindow.xaml
	/// </summary>
	public partial class AppWindow : Window
	{
		private static AppWindow Instance;

		public static AppWindow GetInstance()
		{
			return Instance ??= new AppWindow();
		}

		public AppWindow()
		{
			InitializeComponent();
		}
	}
}
