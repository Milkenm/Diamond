#region Usings
using System;

using static ScriptsLib.Tools;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Options
	{
		internal static void OptionsOpenLink(Link Link)
		{
			try
			{
				switch (Link)
				{
					case Link.DiscordDev:
						ExecuteCmdCommand("START https://discordapp.com/developers/applications/");
						break;
					case Link.RiotDev:
						ExecuteCmdCommand("START https://developer.riotgames.com/");
						break;
				}
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}


		internal enum Link
		{
			DiscordDev,
			RiotDev,
		}
	}
}