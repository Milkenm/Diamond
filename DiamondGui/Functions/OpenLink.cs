#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ScriptsLib.Tools;
using System.Threading.Tasks;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static void OpenLink(Link Link)
		{
			try
			{
				switch (Link)
				{
					case Link.DiscordDev:
						ExecuteCmdCommand("START https://discordapp.com/developers/applications/"); break;
					case Link.RiotDev:
						ExecuteCmdCommand("START https://developer.riotgames.com/"); break;
				}
			}
			catch (Exception _Exception) { ShowException(_Exception, "DiamondGui.Functions.OpenLink()"); }
		}


		internal enum Link
		{
			DiscordDev,
			RiotDev,
		}
	}
}