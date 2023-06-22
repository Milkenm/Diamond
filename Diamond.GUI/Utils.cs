using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

using Diamond.API;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Enums;

using Discord;

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
		TextRange textRange = new TextRange(richTextBox.Document.ContentEnd, richTextBox.Document.ContentEnd)
		{
			Text = text
		};
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

	#region DiamondClient
	public static async Task SetClientActivityAsync(DiamondClient client)
	{
		if (!client.IsLoggedIn()) return;

		using DiamondContext db = new DiamondContext();

		string activityType = db.GetSetting(ConfigSetting.ActivityType, string.Empty);
		string activityName = db.GetSetting(ConfigSetting.ActivityText, string.Empty);
		string activityUrl = db.GetSetting(ConfigSetting.ActivityStreamURL, string.Empty);

		if (activityType.IsEmpty() || activityName.IsEmpty() || activityUrl.IsEmpty())
		{
			await client.SetActivityAsync(null);
			return;
		}

		ActivityType activity = (ActivityType)Enum.Parse(typeof(ActivityType), activityType);
		if (activity == ActivityType.Streaming)
		{
			StreamingGame botStreamingActivity = new StreamingGame(activityName, activityUrl);
			await client.SetActivityAsync(botStreamingActivity);
		}
		else
		{
			Game botActivity = new Game(activityName, activity);
			await client.SetActivityAsync(botActivity);
		}
	}
	#endregion
}
