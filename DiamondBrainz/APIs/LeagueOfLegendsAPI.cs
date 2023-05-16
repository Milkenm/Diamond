using Diamond.API.Data;

using static Diamond.API.Data.DiamondDatabase;

namespace Diamond.API.APIs;
public class LeagueOfLegendsAPI
{
	public LeagueOfLegendsAPI(DiamondDatabase database)
	{
		string apiKey = database.GetSetting(ConfigSetting.RiotAPI_Key);
	}
}
