using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using Newtonsoft.Json;

using ScriptsLibV2.Util;

namespace Diamond.API.SlashCommands.NSFW
{/*
	public class NsfwCommand : ISlashCommand
	{
		protected override void SlashCommandBuilder()
		{
			Name = "nsfw";
			Description = "Shows you a sus image.";
			Options = new List<SlashCommandOptionBuilder>()
			{
				new SlashCommandOptionBuilder()
				{
					Name = "type",
					Description = "Choose if you want butties or boobies",
					Type = ApplicationCommandOptionType.String,
					Choices = new List<ApplicationCommandOptionChoiceProperties>()
					{
						new ApplicationCommandOptionChoiceProperties()
						{
							Name = "butts",
							Value = "butts",
						},
						new ApplicationCommandOptionChoiceProperties()
						{
							Name = "boobs",
							Value = "boobs",
						},
					},
				},
			};
			IsNsfw = true;
		}

		protected override async Task Action(SocketSlashCommand command, DiscordSocketClient client)
		{
			// Parse type
			string nsfwType = (string)command.Data.Options.ElementAt(0).Value;

			// Make the request and deserialize it
			string request = RequestUtils.Get($"http://api.o{nsfwType}.ru/{nsfwType}/0/1/random");
			List<NsfwSchema> nsfwList = JsonConvert.DeserializeObject<List<NsfwSchema>>(request);
			NsfwSchema nsfw = nsfwList[0];

			string imageLink = $"http://media.o{nsfwType}.ru/{nsfw.Preview}";
			byte[] imageData = new WebClient().DownloadData(imageLink);
			int imageSize = imageData.Length;

			// Create the embed
			DefaultEmbed embed = new DefaultEmbed("NSFW", "🍑", command);
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
	}*/
}
