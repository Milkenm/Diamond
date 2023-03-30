﻿using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using Discord.Interactions;

using Newtonsoft.Json;

using ScriptsLibV2.Util;

namespace Diamond.API.SlashCommands.NSFW
{
	public class NsfwCommand : InteractionModuleBase<SocketInteractionContext>
	{
		[RequireNsfw]
		[SlashCommand("nsfw", "Shows you a sus image.")]
		public async Task NsfwCmd(
			[Summary("type", "Choose if you want boobies or butties.")] NSFWType nsfwType,
			[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
		)
		{
			await DeferAsync(!showEveryone);

			// Parse type
			string nsfwTypeString = nsfwType.ToString().ToLower();

			// Make the request and deserialize it
			string request = RequestUtils.Get($"http://api.o{nsfwTypeString}.ru/{nsfwTypeString}/0/1/random");
			List<NsfwSchema> nsfwList = JsonConvert.DeserializeObject<List<NsfwSchema>>(request);
			NsfwSchema nsfw = nsfwList[0];

			string imageLink = $"http://media.o{nsfwTypeString}.ru/{nsfw.Preview}";
			byte[] imageData = new WebClient().DownloadData(imageLink);
			int imageSize = imageData.Length;

			// Create the embed
			DefaultEmbed embed = new DefaultEmbed("NSFW", "🍑", Context);
			embed.AddField("🚺 Model", !string.IsNullOrEmpty(nsfw.Model) ? nsfw.Model : "Unknown model", true);
			embed.AddField("📁 Image Size", Utils.ByteSizeToString(imageSize), true);
			embed.WithImageUrl(imageLink);

			// Reply
			await embed.SendAsync();
		}

		private class NsfwSchema
		{
			[JsonProperty("id")] public int ID;
			[JsonProperty("author")] public string Author;
			[JsonProperty("model")] public string Model;
			[JsonProperty("preview")] public string Preview;
			[JsonProperty("rank")] public int Rank;
		}

		public enum NSFWType
		{
			Boobs,
			Butts,
		}
	}
}
