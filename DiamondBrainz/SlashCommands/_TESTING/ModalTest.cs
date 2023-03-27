using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using ScriptsLibV2.ScriptsLib.DiscordBot;

namespace Diamond.API.SlashCommands._TESTING
{
	public class ModalTestCommand : ISlashCommand
	{
		protected override void SlashCommandBuilder()
		{
			Name = "pizza";
			Description = "This is a test command.";
			Options = new List<SlashCommandOptionBuilder> {
				new SlashCommandOptionBuilder()
				{
					Name = "prompt",
					Description = "The prompt",
					Type  = ApplicationCommandOptionType.String,
					IsRequired = true,
				},
			};
		}

		protected override async Task Action(SocketSlashCommand command, DiscordSocketClient client)
		{
			DefaultEmbed embed = new DefaultEmbed("ChatGPT", "🗨️", command);
			embed.Description = "Generating response...";
			//await embed.SendAsync();

			string response = await GenerateResponse((string)command.Data.Options.ElementAt(0).Value);

			await command.ModifyOriginalResponseAsync((og) =>
			{
				DefaultEmbed embed = new DefaultEmbed("ChatGPT", "🗨️", command);
				embed.Description = response;

				og.Embed = embed.Build();
			});
		}

		public static async Task<string> GenerateResponse(string prompt)
		{
			string apiKey = "<API KEY>"; // Replace with your API key
			string apiUrl = "https://api.openai.com/v1/engines/davinci-codex/completions";

			using var client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
			var requestBody = new
			{
				prompt = prompt,
				max_tokens = 50, // Maximum number of tokens to generate in the response
				temperature = 0.7, // Controls the "creativity" of the response
				n = 1, // Number of responses to generate
				stop = "\n" // Stop generating tokens when a newline character is encountered
			};
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await client.PostAsync(apiUrl, content);
			var responseJson = await response.Content.ReadAsStringAsync();

			dynamic responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseJson);
			string responseText = responseObject.choices[0].text;

			return responseText;
		}

	}
}
