using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using Newtonsoft.Json;

using ScriptsLibV2.ScriptsLib.DiscordBot;
using ScriptsLibV2.Util;

namespace Diamond.API.SlashCommands.NSFW
{
	public class NsfwCommand : ISlashCommand
	{
		protected override void SlashCommandBuilder()
		{
			Name = "nsfw";
			Description = "Send sus stuff to the channel.";
			Options = new List<SlashCommandOptionBuilder>()
			{
				new SlashCommandOptionBuilder()
				{
					Name = "type",
					Description = "sus type.",
					Type = ApplicationCommandOptionType.String,
					Choices = new List<ApplicationCommandOptionChoiceProperties>()
					{
						new ApplicationCommandOptionChoiceProperties()
						{
							Name = "butts",
							Value = NsfwType.Butt.ToString().ToLower(),
						},
						new ApplicationCommandOptionChoiceProperties()
						{
							Name = "boobs",
							Value = NsfwType.Boobs.ToString().ToLower(),
						},
					},
				},
			};
		}

		protected override async Task Action(SocketSlashCommand command, DiscordSocketClient client)
		{
			// Parse type
			NsfwType nsfwType = (NsfwType)command.Data.Options.ElementAt(0).Value;

			// Make the request and deserialize it
			string request = RequestUtils.Get($"http://api.o{nsfwType.ToString().ToLower()}.ru/{nsfwType.ToString().ToLower()}/0/1/random");
			List<NsfwSchema> nsfwList = JsonConvert.DeserializeObject<List<NsfwSchema>>(request);
			NsfwSchema nsfw = nsfwList[0];

			// Create the embed
			EmbedBuilder embed = new EmbedBuilder();
			embed.WithAuthor("NSFW", TwemojiUtils.GetUrlFromEmoji("🔞"));
			embed.AddField("**Model**", !string.IsNullOrEmpty(nsfw.Model) ? nsfw.Model : "Unknown model");
			embed.WithImageUrl($"http://media.o{nsfwType.ToString().ToLower()}.ru/" + nsfw.Preview);

			// Reply
			await command.RespondAsync(embed: embed.FinishEmbed(command));
		}

		private class NsfwSchema
		{
			[JsonProperty("id")] public int ID;
			[JsonProperty("author")] public string Author;
			[JsonProperty("model")] public string Model;
			[JsonProperty("preview")] public string Preview;
			[JsonProperty("rank")] public int Rank;
		}

		private enum NsfwType
		{
			Butt,
			Boobs,
		}
	}
}
