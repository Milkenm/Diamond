using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Diamond.API;

using Discord;
using Discord.WebSocket;


namespace Diamond.GUI.Pages
{
	/// <summary>
	/// Interaction logic for GuildsPanelPage.xaml
	/// </summary>
	public partial class GuildsPanelPage : Page
	{
		private readonly List<SocketTextChannel> _validTextChannels = new List<SocketTextChannel>();

		private readonly DiamondClient _client;

		public GuildsPanelPage(DiamondClient client)
		{
			InitializeComponent();

			_client = client;
			_client.MessageReceived += new System.Func<SocketMessage, Task>((socketMessage) =>
			{
				this._client_MessageReceived(socketMessage);
				return Task.CompletedTask;
			});
		}

		public async Task LoadGuildsAsync()
		{
			comboBox_guilds.ItemsSource = _client.Guilds;
			comboBox_textChannels.ItemsSource = _validTextChannels;
			label_guildCount.Content = $"Living in {_client.Guilds.Count} guilds.";
		}

		private async void comboBox_guilds_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			await this.ShowGuildAsync(comboBox_guilds.SelectedItem as SocketGuild);
		}


		private async Task ShowGuildAsync(SocketGuild guild)
		{
			await guild.DownloadUsersAsync();

			// Load guild info
			label_guildName.Content = guild.Name;
			int botCount = guild.Users.Where(u => u.IsBot).Count();
			label_memberCount.Content = $"Users: {guild.Users.Count} ({botCount} bot{(botCount != 1 ? "s" : "")})";

			listBox_users.Items.Clear();
			foreach (var user in guild.Users)
			{
				string userString = $"{user.DisplayName}{(user.IsBot ? " (BOT)" : "")}";
				listBox_users.Items.Add(userString);
			}

			// Load channels the bot has permission to type in
			_validTextChannels.Clear();
			richTextBox_chatLog.Document.Blocks.Clear();
			foreach (SocketTextChannel textChannel in guild.TextChannels)
			{
				ChannelPermissions botPermissions = guild.CurrentUser.GetPermissions(textChannel);
				if (botPermissions.ViewChannel && botPermissions.SendMessages)
				{
					_validTextChannels.Add(textChannel);
				}
			}
			comboBox_textChannels.ItemsSource = _validTextChannels;
		}

		private void _client_MessageReceived(SocketMessage socketMessage)
		{
			Dispatcher.Invoke(() =>
			{
				if (comboBox_textChannels.SelectedItem == null) return;
				if (socketMessage.Channel.Id != (comboBox_textChannels.SelectedItem as SocketTextChannel).Id) return;
				if (socketMessage.Source != MessageSource.User) return;

				PrintMessage(socketMessage);
			});
			return;
		}

		private async void textBox_chat_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key is Key.Enter or Key.Return && textBox_chat.Text is string message && !string.IsNullOrEmpty(message))
			{
				SocketTextChannel selectedChannel = comboBox_textChannels.SelectedItem as SocketTextChannel;
				selectedChannel.SendMessageAsync(message);
				textBox_chat.Clear();
			}
		}

		private async void comboBox_textChannels_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (comboBox_textChannels.SelectedItem == null) return;

			richTextBox_chatLog.Document.Blocks.Clear();

			// Load last 10 messages from the text channel
			SocketTextChannel textChannel = (SocketTextChannel)comboBox_textChannels.SelectedItem;
			List<IMessage> messages = ((ICollection<IMessage>)await textChannel.GetMessagesAsync(10).FlattenAsync()).OrderBy(msg => msg.CreatedAt).ToList();
			foreach (IMessage message in messages)
			{
				PrintMessage(message);
			}
		}

		private void PrintMessage(IMessage msg)
		{
			Utils.AppendText(richTextBox_chatLog, $"{(msg.Author as SocketGuildUser).DisplayName}: {(!string.IsNullOrEmpty(msg.Content) ? msg.Content : "<empty message>")}", printDate: true);
			richTextBox_chatLog.ScrollToEnd();
		}

		private async void button_leaveServer_Click(object sender, RoutedEventArgs e)
		{
			if (comboBox_guilds.SelectedItem is not SocketGuild guild) return;

			if (MessageBox.Show($"Leave '{guild.Name}'?", "Leave guild", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				await guild.LeaveAsync();
			}
		}
	}
}
