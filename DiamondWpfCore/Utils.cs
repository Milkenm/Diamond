using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

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
}
