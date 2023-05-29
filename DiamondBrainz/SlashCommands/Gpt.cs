using System;
using System.Threading.Tasks;

using Diamond.API.APIs;
using Diamond.API.Attributes;
using Diamond.API.Util;

using Discord.Interactions;

using Microsoft.Extensions.DependencyInjection;

using OpenAI_API.Completions;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands
{
	public class Gpt : InteractionModuleBase<SocketInteractionContext>
	{
		private readonly IServiceProvider _serviceProvider;

		public Gpt(IServiceProvider serviceProvider)
		{
			this._serviceProvider = serviceProvider;
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
			CompletionResult result = await this._serviceProvider.GetRequiredService<OpenAIAPI>().Completions.CreateCompletionsAsync(completionRequest, 1);
			return result.Completions.Count > 0 ? result.Completions[0].Text : null;
		}
	}
}