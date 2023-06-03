using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Diamond.API;
using Diamond.API.APIs;
using Diamond.API.Data;
using Diamond.API.Util;
using Diamond.GUI.Pages;

using Microsoft.Extensions.DependencyInjection;

using Image = System.Windows.Controls.Image;
using SUtils = ScriptsLibV2.Util.Utils;

namespace Diamond.GUI
{
	/// <summary>
	/// Interaction logic for AppWindow.xaml
	/// </summary>
	public partial class AppWindow : Window
	{
		private readonly DiamondClient _client;
		private readonly IServiceProvider _serviceProvider;
		private readonly MainPanelPage _mainPanel;
		private readonly LogsPanelPage _logsPanel;

		public AppWindow(DiamondClient client, IServiceProvider serviceProvier, MainPanelPage mainPanel, LogsPanelPage logsPanel)
		{
			this._client = client;
			this._serviceProvider = serviceProvier;
			this._mainPanel = mainPanel;
			this._logsPanel = logsPanel;

			this.InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			using DiamondContext db = new DiamondContext();

			if (SUtils.IsDebugEnabled())
			{
				this.Title = $"{this.Title} (Debug)";
			}

			MainPanelPage mainPanel = this._serviceProvider.GetRequiredService<MainPanelPage>();
			LogsPanelPage logsPanel = this._serviceProvider.GetRequiredService<LogsPanelPage>();

			// Disable guilds tab
			this.ToggleUIElement(this.image_guilds, this.tabItem_guilds, false);

			// Global exception handler
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((s, e) =>
			{
				logsPanel.Log(e.ExceptionObject);
			});

			// Load CS:GO items
			CsgoBackpack csgoBackpack = this._serviceProvider.GetRequiredService<CsgoBackpack>();
			csgoBackpack.OnCheckingForUpdate += new CsgoItemsStateChanged(async () => await mainPanel.LogAsync("Checking for CS:GO items update..."));
			csgoBackpack.OnUpdateCancelled += new CsgoItemsStateChanged(async () => await mainPanel.LogAsync("No update needed for CS:GO items!"));
			csgoBackpack.OnUpdateStart += new CsgoItemsStateChanged(async () => await mainPanel.LogAsync("Downloading CS:GO items..."));
			csgoBackpack.OnUpdateEnd += new CsgoItemsStateChanged(async () => await mainPanel.LogAsync("CS:GO items loaded!"));
			_ = csgoBackpack.LoadItems(/*SUtils.IsDebugEnabled()*/);
			// Load Pokemons
			PokemonAPI pokeApi = this._serviceProvider.GetRequiredService<PokemonAPI>();
			_ = pokeApi.LoadPokemonsAsync();

			// Initalize bot
			this._client.Initialize();

			// Bot connect
			this._client.Connected += new Func<Task>(() =>
			{
				// Enable guilds tab
				_ = this.Dispatcher.Invoke(async () =>
				{
					this.ToggleUIElement(this.image_guilds, this.tabItem_guilds, true);

					// Refesh guilds tab
					GuildsPanelPage guildsPanel = this._serviceProvider.GetRequiredService<GuildsPanelPage>();
					await guildsPanel.LoadGuildsAsync();
				});

				return Task.CompletedTask;
			});
			// Bot disconnect
			this._client.Disconnected += new Func<Exception, Task>((_) =>
			{
				// Disable guilds tab
				this.Dispatcher.Invoke(() =>
				{
					this.ToggleUIElement(this.image_guilds, this.tabItem_guilds, false);
				});
				return Task.CompletedTask;
			});
			// Bot log
			this._client.OnLog += new BotLogEvent((logMessage, isError) =>
			{
				_ = this.Dispatcher.Invoke(async () =>
				{
					if (!isError)
					{
						await this._mainPanel.LogAsync(logMessage);
					}
					else
					{
						this._logsPanel.Log(logMessage);
					}
				});
			});
			// Bot login
			this._client.LoggedIn += new Func<Task>(async () =>
			{
				await Utils.SetClientActivityAsync(this._client);
			});

			// App events
			this.Closing += new CancelEventHandler(async (se, ev) =>
			{
				await this._client.StopAsync();
			});

			// Set frames
			_ = this.frame_main.Navigate(mainPanel);
			_ = this.frame_logs.Navigate(this._serviceProvider.GetRequiredService<LogsPanelPage>());
			_ = this.frame_guilds.Navigate(this._serviceProvider.GetRequiredService<GuildsPanelPage>());
			_ = this.frame_lavalink.Navigate(this._serviceProvider.GetRequiredService<LavalinkPanelPage>());
			_ = this.frame_remote.Navigate(this._serviceProvider.GetRequiredService<RemotePanelPage>());
			_ = this.frame_settings.Navigate(this._serviceProvider.GetRequiredService<SettingsPanelPage>());

			// Check if settings are valid
			if (!db.AreSettingsValid())
			{
				this.ToggleUI(false);
				this.tabControl_main.SelectedIndex = this.tabControl_main.Items.Count - 1;
			}
		}

		public void ToggleUI(bool enabled)
		{
			if (this.tabItem_main.IsEnabled == enabled)
			{
				return;
			}

			// Main
			this.ToggleUIElement(this.image_main, this.tabItem_main, enabled);
			// Logs
			this.ToggleUIElement(this.image_logs, this.tabItem_logs, enabled);
			// Lavalink
			this.ToggleUIElement(this.image_lavalink, this.tabItem_lavalink, enabled);
			// RCON
			this.ToggleUIElement(this.image_rcon, this.tabItem_rcon, enabled);
		}

		private void ToggleUIElement(Image image, TabItem tab, bool isEnabled)
		{
			DisableImage(image, !isEnabled);
			tab.IsEnabled = isEnabled;
		}

		private static void DisableImage(Image image, bool grayout)
		{
			if (grayout)
			{
				// Get the source bitmap                        
				if (image.Source is BitmapSource bitmapImage)
				{
					ImageBrush OpacityMask = new ImageBrush(bitmapImage);
					image.Source = new FormatConvertedBitmap(bitmapImage, PixelFormats.Gray8, null, 0);
					// Reuse the opacity mask from the original image as FormatConvertedBitmap does not keep transparency info
					image.OpacityMask = OpacityMask;
				}
			}
			else
			{
				image.Source = ((FormatConvertedBitmap)image.Source).Source;
			}
		}

		private async void Window_Closing(object sender, CancelEventArgs e)
		{
			// Logout
			await this._client.TakeLifeAsync();
			// Stop Lavalink
			this._serviceProvider.GetRequiredService<Lava>().Dispose();
		}
	}
}
