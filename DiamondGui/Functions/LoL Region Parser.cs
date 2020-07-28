#region Usings

using static ScriptsLib.Network.APIs.RiotAPI;

#endregion Usings

namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static Regions? LolRegionParser(string regionString)
		{
			if (!string.IsNullOrEmpty(regionString))
			{
				switch (regionString.ToLower())
				{
					case "br":
						return Regions.BR1;

					case "eune":
						return Regions.EUN1;

					case "euw":
						return Regions.EUW1;

					case "jp":
						return Regions.JP1;

					case "kr":
						return Regions.KR;

					case "lan":
						return Regions.LA1;

					case "las":
						return Regions.LA2;

					case "na":
						return Regions.NA1;

					case "oce":
						return Regions.OC1;

					case "ru":
						return Regions.RU;

					case "tr":
						return Regions.TR1;
				}
			}
			return null;
		}
	}
}