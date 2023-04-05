using Diamond.API.Schems;
using Diamond.API.Stuff;

using Newtonsoft.Json;

using ScriptsLibV2.Util;

namespace Testing.CsgoTest2;
internal class CsgoTest2
{
	private static readonly Dictionary<Currency, CsgoItemsList?> _itemsList = new Dictionary<Currency, CsgoItemsList?>();
	private static readonly Dictionary<string, CsgoItemMatchInfo> _searchCache = new Dictionary<string, CsgoItemMatchInfo>();

	private static void Main(string[] args)
	{
		Console.WriteLine("Loading items...");
		foreach (Currency currency in Enum.GetValues(typeof(Currency)))
		{
			if (currency != Currency.EUR) continue;
			string response = RequestUtils.Get($"http://csgobackpack.net/api/GetItemsList/v2/?currency={currency.ToString().ToLower()}");
			if (response == null) return;

			CsgoItemsList? itemsList = JsonConvert.DeserializeObject<CsgoItemsList>(response);
			if (itemsList == null || itemsList.Success == false) return;

			_itemsList.Add(currency, itemsList);
		}
		Console.WriteLine("Items loaded!");

		while (true)
		{
			Console.WriteLine("Search for an item:");
			string? searchItemName = Console.ReadLine().ToLower();

			if (searchItemName != null)
			{
				CsgoItemMatchInfo? bestMatch = null;
				if (_searchCache.ContainsKey(searchItemName))
				{
					bestMatch = _searchCache[searchItemName];
				}
				else
				{
					foreach (CsgoItemInfo item in _itemsList.Values.ElementAt(0).ItemsList.Values)
					{
						string itemName = item.Name.Replace("&#39", "\"").Replace(" ", "").ToLower().Trim();

						int matches = 0;
						foreach (string word in searchItemName.Split(" "))
						{
							if (itemName.Contains(word))
							{
								matches++;
							}
						}
						double match = matches;
						if (bestMatch == null || bestMatch.Match < match)
						{
							bestMatch = new CsgoItemMatchInfo(item, match);
						}
					}
				}
				if (bestMatch != null)
				{
					if (!_searchCache.ContainsKey(searchItemName))
					{
						_searchCache.Add(searchItemName, bestMatch);
					}
					Console.WriteLine("Found item: " + bestMatch.CsgoItem.Name + " (match: " + bestMatch.Match + "€)");
				}
			}
		}
	}

	public static class JaroWinklerDistance
	{
		/* The Winkler modification will not be applied unless the 
		 * percent match was at or above the mWeightThreshold percent 
		 * without the modification. 
		 * Winkler's paper used a default value of 0.7
		 */
		private static readonly double mWeightThreshold = 0.7;

		/* Size of the prefix to be concidered by the Winkler modification. 
		 * Winkler's paper used a default value of 4
		 */
		private static readonly int mNumChars = 4;


		/// <summary>
		/// Returns the Jaro-Winkler distance between the specified  
		/// strings. The distance is symmetric and will fall in the 
		/// range 0 (perfect match) to 1 (no match). 
		/// </summary>
		/// <param name="aString1">First String</param>
		/// <param name="aString2">Second String</param>
		/// <returns></returns>
		public static double distance(string aString1, string aString2)
		{
			return 1.0 - proximity(aString1, aString2);
		}


		/// <summary>
		/// Returns the Jaro-Winkler distance between the specified  
		/// strings. The distance is symmetric and will fall in the 
		/// range 0 (no match) to 1 (perfect match). 
		/// </summary>
		/// <param name="aString1">First String</param>
		/// <param name="aString2">Second String</param>
		/// <returns></returns>
		public static double proximity(string aString1, string aString2)
		{
			int lLen1 = aString1.Length;
			int lLen2 = aString2.Length;
			if (lLen1 == 0)
				return lLen2 == 0 ? 1.0 : 0.0;

			int lSearchRange = Math.Max(0, Math.Max(lLen1, lLen2) / 2 - 1);

			// default initialized to false
			bool[] lMatched1 = new bool[lLen1];
			bool[] lMatched2 = new bool[lLen2];

			int lNumCommon = 0;
			for (int i = 0; i < lLen1; ++i)
			{
				int lStart = Math.Max(0, i - lSearchRange);
				int lEnd = Math.Min(i + lSearchRange + 1, lLen2);
				for (int j = lStart; j < lEnd; ++j)
				{
					if (lMatched2[j]) continue;
					if (aString1[i] != aString2[j])
						continue;
					lMatched1[i] = true;
					lMatched2[j] = true;
					++lNumCommon;
					break;
				}
			}
			if (lNumCommon == 0) return 0.0;

			int lNumHalfTransposed = 0;
			int k = 0;
			for (int i = 0; i < lLen1; ++i)
			{
				if (!lMatched1[i]) continue;
				while (!lMatched2[k]) ++k;
				if (aString1[i] != aString2[k])
					++lNumHalfTransposed;
				++k;
			}
			// System.Diagnostics.Debug.WriteLine("numHalfTransposed=" + numHalfTransposed);
			int lNumTransposed = lNumHalfTransposed / 2;

			// System.Diagnostics.Debug.WriteLine("numCommon=" + numCommon + " numTransposed=" + numTransposed);
			double lNumCommonD = lNumCommon;
			double lWeight = (lNumCommonD / lLen1
							 + lNumCommonD / lLen2
							 + (lNumCommon - lNumTransposed) / lNumCommonD) / 3.0;

			if (lWeight <= mWeightThreshold) return lWeight;
			int lMax = Math.Min(mNumChars, Math.Min(aString1.Length, aString2.Length));
			int lPos = 0;
			while (lPos < lMax && aString1[lPos] == aString2[lPos])
				++lPos;
			if (lPos == 0) return lWeight;
			return lWeight + 0.1 * lPos * (1.0 - lWeight);

		}


	}
}
