#region Using

using Discord;
using Discord.Commands;

using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using static DiamondGui.Functions;
using static DiamondGui.Static;
using static ScriptsLib.Network.APIs.RiotAPI;

#endregion Using

namespace DiamondGui
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		[Command("lol summoner"), Alias("lol sum"), Summary("Gets the main champion of a summoner.")]
		public async Task LolSummoner(string summoner, [Optional] string region)
		{
			Regions? reg = LolRegionParser(region);

			if (reg == null)
			{
				await ReplyAsync("There was an error with the provided region, check if the region is correct.").ConfigureAwait(false);
			}
			else
			{
				Region = (Regions)reg;

				try
				{
					SummonerDTO sum = Summoner.GetSummonerByName(summoner, true);

					EmbedBuilder embed = new EmbedBuilder();
					embed.WithThumbnailUrl(settings.Domain + $"lol/icons/{sum.profileIconId}.png");
					embed.WithTitle(sum.name);

					await Context.Channel.SendMessageAsync("", false, FinishEmbed(embed, Context)).ConfigureAwait(false);
				}
				catch (Exception ex)
				{
					int? error = GetHttpResponseCode((WebException)ex);

					string msg;
					if (error == null)
					{
						msg = "An unknown error has ocurred.";
					}
					else
					{
						if (error == 404)
						{
							msg = "The summoner you searched for was not found. Double check the summoner name and region.";
						}
						else if (error == 503)
						{
							msg = "The API service is currently down, try again later.";
						}
						else
						{
							msg = "Something went wrong with the request, please try again.";
						}
					}
					await ReplyAsync(msg).ConfigureAwait(false);

					return;
				}
			}
		}

		public class SummonerJson
		{
			public string id;
			public string accountId;
			public string puuid;
			public string name;
			public string profileIconId;
			public string revisionDate;
			public string summonerLevel;
		}
	}
}