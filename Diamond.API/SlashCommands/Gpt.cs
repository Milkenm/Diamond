﻿using System.Threading.Tasks;
using Diamond.API.APIs.OpenAi;
using Diamond.API.Attributes;
using Diamond.API.Helpers;

using Discord.Interactions;

using OpenAI_API.Completions;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands
{
    public class Gpt : InteractionModuleBase<SocketInteractionContext>
	{
		private readonly OpenAIAPI _openaiApi;

		public Gpt(OpenAIAPI openaiApi)
		{
			this._openaiApi = openaiApi;
		}

		[DSlashCommand("gpt", "Ask something to ChatGPT-3.")]
		public async Task GptCommandAsync(
			[Summary("prompt", "Your question.")] string prompt,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			string response = await this.GenerateContent(prompt);

			DefaultEmbed embed = new DefaultEmbed("ChatGPT", "🗨️", this.Context);
			// First row
			_ = embed.AddField("❓ Prompt/Question", prompt);
			// Second row
			_ = embed.AddField("🤖 Response", !response.IsEmpty() ? response : ":x: There was an error generating the response.\nPlease try again.");

			_ = await embed.SendAsync();
		}

		/// <remarks>From <see href="https://ozpeace.hashnode.dev/how-to-integrate-chat-gpt-using-c-and-net-core"/>.</remarks>
		public async Task<string> GenerateContent(string prompt)
		{
			CompletionRequest completionRequest = new CompletionRequest()
			{
				Prompt = prompt,
				Model = "text-davinci-003",
				Temperature = 0.5,
				MaxTokens = 300,
			};
			CompletionResult result = await this._openaiApi.Completions.CreateCompletionsAsync(completionRequest, 1);
			return result.Completions.Count > 0 ? result.Completions[0].Text : null;
		}
	}
}