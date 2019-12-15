#region Using
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using Newtonsoft.Json;

using static DiamondGui.Functions;
using static DiamondGui.Static;
using static ScriptsLib.Network.APIs.RiotAPI;
#endregion



namespace DiamondGui
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		[Command("lol summoner"), Alias("lol sum"), Summary("Gets the main champion of a summoner.")]
		public async Task LolSummoner(string summoner, [Optional] string region)
		{
			Settings.CommandsUsed++;

			Regions? reg = LolRegionParser(region);


			if (reg != null)
			{
				Region = (Regions)reg;
				string reqSum;

				try
				{
					reqSum = Summoner.GetSummonerByName(summoner);
				}
				catch (WebException ex)
				{
					int? error = GetHttpResponseCode(ex);

					if (error != null)
					{
						if (error == 404)
						{
							await ReplyAsync("The summoner you searched for was not found. Double check the summoner name and region.");
						}
						else if (error == 503)
						{
							await ReplyAsync("The API service is currently down, please try again later.");
						}
						else
						{
							await ReplyAsync("Something went wrong with the request, please try again.\nIf the issue persists please contact the support.");
						}
					}
					else
					{
						await ReplyAsync("An unknown error has ocurred.");
					}

					return;
				}

				SummonerJson sum = JsonConvert.DeserializeObject<SummonerJson>(reqSum);

				await ReplyAsync(sum.summonerLevel + " | " + region);
			}
			else
			{
				await ReplyAsync("There was an error with the provided region, please double check.");
			}
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