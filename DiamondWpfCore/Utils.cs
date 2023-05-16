using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

using Discord;
using Discord.WebSocket;

using ScriptsLibV2.Extensions;

namespace Diamond.GUI;
public static class Utils
{
	#region RichTextBox
	public static void AppendText(this RichTextBox richTextBox, string text, Brush? brush = null, bool? printDate = null)
	{
		// Add new line
		if (!richTextBox.IsEmpty() && printDate != null)
		{
			richTextBox.AppendText(Environment.NewLine);
		}
		// Print date
		if (printDate == true)
		{
			richTextBox.PrintDate();
		}
		// Add text
		brush ??= richTextBox.Foreground;
		TextRange textRange = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd);
		textRange.Text = text;
		try
		{
			textRange.ApplyPropertyValue(TextElement.ForegroundProperty, brush);
		}
		catch (FormatException) { }
	}

	public static bool IsEmpty(this RichTextBox richTextBox)
	{
		string text = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
		return text.IsEmpty();
	}

	public static void PrintDate(this RichTextBox richTextBox)
	{
		richTextBox.AppendText($"[{DateTimeOffset.Now:HH:mm:ss}] ", new SolidColorBrush(System.Windows.Media.Color.FromRgb(100, 100, 100)), printDate: null);
	}
	#endregion

	#region DiscordSocketClient
	public static bool IsLoggedIn(this DiscordSocketClient client)
	{
		return client.LoginState is LoginState.LoggedIn or LoginState.LoggingIn;
	}

	public static async Task BringToLifeAsync(this DiscordSocketClient client, string token)
	{
		if (!client.IsLoggedIn())
		{
			await client.LoginAsync(TokenType.Bot, token);
			await client.StartAsync();
		}
	}

	public static async Task TakeLifeAsync(this DiscordSocketClient client)
	{
		if (client.IsLoggedIn())
		{
			await client.StopAsync();
			await client.LogoutAsync();
		}
	}
	#endregion

	#region Debug
	public static bool IsDebugChannel(string debugChannels, ulong? channelId)
	{
		if (channelId == null) return false;

		return debugChannels.Split(',').Any(cId => cId == channelId.ToString());
	}
	#endregion
}
