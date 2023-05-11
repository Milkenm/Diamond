using System.Linq;

using Discord;

using ScriptsLibV2.Extensions;

namespace Diamond.API;
public static class Utils
{
	public static string? GetMessageContent(IMessage message)
	{
		if (message.Content.IsEmpty())
		{
			// Get content from embed
			if (message.Embeds.Count == 0)
			{
				return null;
			}

			return message.Embeds.ElementAt(0).Description;
		}
		return message.Content;
	}
}
