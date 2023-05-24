using Diamond.API.Data;

using static Diamond.API.Data.DiamondDatabase;

namespace Diamond.API.APIs;
public class LeagueOfLegendsAPI
{
	public LeagueOfLegendsAPI()
	{
		string apiKey;
		using (DiamondDatabase db = new DiamondDatabase())
		{
			apiKey = db.GetSetting(ConfigSetting.RiotAPI_Key);
		}
	}
}
