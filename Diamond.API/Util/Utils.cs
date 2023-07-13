﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Diamond.API.Helpers;

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
			// There is a random glitch where the collection is modified, so this should fix it for now:
			foreach ((string itemName, T value) in new Dictionary<string, T>(searchList))
			{
				string formattedItemName = itemName.ToLower();

				double matches = 0;
				foreach (string word in formattedItemName.Split(" "))
				{
					if (formattedItemName.Replace(" ", "").Replace("-", "").Contains(word))
					{
						matches++;
					}
				}
				matches *= SUtils.CalculateLevenshteinSimilarity(searchCriteria, formattedItemName);
				if (matches == 0) continue;
				bestMatches.Add(new SearchMatchInfo<T>(value, matches));
			}
			if (bestMatches.Count > 0)
			{
				bestMatches = bestMatches.OrderBy(bm => bm.Match).Reverse().ToList();
				return bestMatches;
			}

			return new List<SearchMatchInfo<T>>();
		}

		public static SearchMatchInfo<T>? GetBestMatch<T>(List<SearchMatchInfo<T>> matches)
		{
			if (matches == null || matches.Count == 0) return null;

			IOrderedEnumerable<SearchMatchInfo<T>> orderedMatches = matches.OrderBy(smi => smi.Match);
			return orderedMatches.ElementAt(0);
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

		public static string GetAssemblyVersion()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
			return fvi.FileVersion;
		}

		public static string FormatTime(DateTimeOffset dateTimeOffset)
		{
			return dateTimeOffset.ToString("dd/MM/yyyy, HH:mm:ss");
		}

		public static string FormatTime(long unixTimestamp)
		{
			DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
			return FormatTime(dateTimeOffset);
		}

		private static readonly Dictionary<TimestampSetting, string> _timestampSettingValuesMap = new Dictionary<TimestampSetting, string>
		{
			{ TimestampSetting.FullDate, "f" },
			{ TimestampSetting.FullDateWithWeekday, "F" },
			{ TimestampSetting.DateOnly, "d" },
			{ TimestampSetting.DateWithMonth, "D" },
			{ TimestampSetting.TimeOnly, "t" },
			{ TimestampSetting.TimeOnlyWithSeconds, "T" },
			{ TimestampSetting.TimeSince, "R" },
		};

		public static string GetTimestampBlock(long unixTimestamp, TimestampSetting setting = TimestampSetting.FullDate)
		{
			return $"<t:{unixTimestamp}:{_timestampSettingValuesMap[setting]}>";
		}

		public static string Plural(string @base, string singular, string plural, long elements)
		{
			return @base + ((elements is 1 or -1) ? singular : plural);
		}

		public static string Plural<T>(string @base, string singular, string plural, IEnumerable<T> elements)
		{
			return Plural(@base, singular, plural, elements.Count());
		}

		public static string RemoveHtmlTags(string inputString)
		{
			return Regex.Replace(inputString, "<.*?>", string.Empty);
		}
	}

	public enum TimestampSetting
	{
		FullDate,
		FullDateWithWeekday,
		DateOnly,
		DateWithMonth,
		TimeOnly,
		TimeOnlyWithSeconds,
		TimeSince
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

		#region StringBuilder
		public static StringBuilder Append(this StringBuilder sb, string text, string separator)
		{
			if (sb.Length > 0)
			{
				_ = sb.Append(separator);
			}
			_ = sb.Append(text);
			return sb;
		}

		public static string ToStringOrDefault(this StringBuilder sb, string defaultValue)
		{
			return sb.Length > 0 ? sb.ToString() : defaultValue;
		}
		#endregion

		#region EmbedBuilder
		public static EmbedBuilder AddEmptyField(this EmbedBuilder embedBuilder, bool inline = false)
		{
			return embedBuilder.AddField("‍", "‍", inline);
		}
		#endregion

		#region Dictionary
		public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Dictionary<TKey, TValue> otherDictionary)
		{
			foreach (KeyValuePair<TKey, TValue> item in otherDictionary)
			{
				dictionary.Add(item.Key, item.Value);
			}
		}

		public static void Merge<TKey>(this Dictionary<TKey, double> dictionary, TKey key, double value, DictionaryMergeOperation operation)
		{
			if (!dictionary.ContainsKey(key))
			{
				dictionary.Add(key, value);
				return;
			}

			_ = operation switch
			{
				DictionaryMergeOperation.Sum => dictionary[key] += value,
				DictionaryMergeOperation.Subtract => dictionary[key] -= value,
				DictionaryMergeOperation.Multiply => dictionary[key] *= value,
				DictionaryMergeOperation.Divide => dictionary[key] /= value,
			};
		}

		public static void Merge<TKey>(this Dictionary<TKey, double> dictionary, KeyValuePair<TKey, double> kv, DictionaryMergeOperation operation)
		{
			Merge(dictionary, kv.Key, kv.Value, operation);
		}

		public static void Merge<TKey>(this Dictionary<TKey, double> dictionary, Dictionary<TKey, double> otherDictionary, DictionaryMergeOperation operation)
		{
			foreach (KeyValuePair<TKey, double> kv in otherDictionary)
			{
				Merge(dictionary, kv, operation);
			}
		}
		#endregion

		#region String
		public static string OrDefault(this string @string, string @default)
		{
			return @string.IsEmpty() ? @default : @string;
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

	public enum DictionaryMergeOperation
	{
		Sum,
		Subtract,
		Multiply,
		Divide,
	}
}