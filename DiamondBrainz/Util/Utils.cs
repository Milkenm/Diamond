﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

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

		public static bool ChanceOf(double chance)
		{
			double randomDouble = RandomGenerator.GetInstance().Random.NextDouble();
			return (chance / 100) <= Math.Round(randomDouble * 100, 2);
		}

		public static bool IsDebugChannel(string debugChannels, ulong? channelId)
		{
			return channelId != null && debugChannels.Split(',').Any(cId => cId == channelId.ToString());
		}

		public static ulong GetButtonMessageId(SocketInteractionContext context)
		{
			return (context.Interaction as SocketMessageComponent).Message.Id;
		}
	}

	public static class ExtensionUtils
	{
		#region DiamondClient
		public static bool IsLoggedIn(this DiamondClient client)
		{
			return client.LoginState is LoginState.LoggedIn or LoginState.LoggingIn;
		}

		public static async Task BringToLifeAsync(this DiamondClient client, string token)
		{
			if (!client.IsLoggedIn())
			{
				await client.LoginAsync(TokenType.Bot, token);
				await client.StartAsync();
			}
		}

		public static async Task TakeLifeAsync(this DiamondClient client)
		{
			if (client.IsLoggedIn())
			{
				await client.StopAsync();
				await client.LogoutAsync();
			}
		}
		#endregion
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