using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.Interactions;

using ScriptsLibV2.Extensions;

using SUtils = ScriptsLibV2.Util.Utils;

namespace Diamond.API.Util
{
	public static class Utils
	{
		public static string GetMessageContent(IMessage message)
		{
			if (message.Content.IsEmpty())
				// Get content from embed
				return message.Embeds.Count == 0 ? null : message.Embeds.ElementAt(0).Description;
			return message.Content;
		}

		#region Search
		public static List<SearchMatchInfo<T>> Search<T>(Dictionary<string, T> searchList, string searchCriteria)
		{
			List<SearchMatchInfo<T>> bestMatches = new List<SearchMatchInfo<T>>();
			foreach ((string itemName, T value) in searchList)
			{
				string formattedItemName = itemName.ToLower();

				double matches = 0;
				foreach (string word in formattedItemName.Split(" "))
				{
					if (formattedItemName.Replace(" ", "").Contains(word))
						matches++;
				}
				matches *= SUtils.CalculateLevenshteinSimilarity(searchCriteria, formattedItemName);
				if (matches == 0)
					continue;
				bestMatches.Add(new SearchMatchInfo<T>(value, matches));
			}
			if (bestMatches.Count > 0)
			{
				bestMatches = bestMatches.OrderBy(bm => bm.Match).Reverse().ToList();
				return bestMatches;
			}

			return new List<SearchMatchInfo<T>>();
		}

		private static SearchMatchInfo<T> GetBestMatch<T>(List<SearchMatchInfo<T>> matches)
		{
			SearchMatchInfo<T> bestMatch = default;
			foreach (SearchMatchInfo<T> matchInfo in matches)
			{
				if (bestMatch == null || bestMatch.Match < matchInfo.Match)
					bestMatch = matchInfo;
			}
			return bestMatch;
		}
		#endregion

		public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
		{
			DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
			return dtDateTime;
		}

		public static async Task DeleteResponseAsync(SocketInteractionContext context)
		{
			await (await context.Interaction.GetOriginalResponseAsync()).DeleteAsync();
		}
	}

	public class SearchMatchInfo<T>
	{
		public T Item { get; set; }
		public double Match { get; set; }

		public SearchMatchInfo(T item, double match)
		{
			this.Item = item;
			this.Match = match;
		}
	}
}