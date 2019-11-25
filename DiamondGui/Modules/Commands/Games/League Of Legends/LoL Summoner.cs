#region Using
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Discord.Commands;
using Newtonsoft.Json;
using static DiamondGui.Functions;
using static ScriptsLib.Network.APIs.RiotAPI;
#endregion



namespace DiamondGui
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		[Command("lol summoner"), Alias("lol sum"), Summary("Gets the main champion of a summoner.")]
		public async Task LolSummoner(string summoner, [Optional] string region)
		{
			try
			{
				Regions? reg = LolRegionParser(region);


				if (reg != null)
				{
					Region = (Regions)reg;
				}
				string reqSum = Summoner.GetSummonerByName(summoner);

				SummonerJson sum = JsonConvert.DeserializeObject<SummonerJson>(reqSum);

				await ReplyAsync(sum.summonerLevel + " | " + region);
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}

		public class SummonerJson
		{
			public string id { get; set; }
			public string accountId { get; set; }
			public string puuid { get; set; }
			public string name { get; set; }
			public string profileIconId { get; set; }
			public string revisionDate { get; set; }
			public string summonerLevel { get; set; }
		}
	}
}