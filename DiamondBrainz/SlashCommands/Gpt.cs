﻿using System;
using System.Threading.Tasks;

using Discord.Interactions;

using Microsoft.Extensions.DependencyInjection;

using OpenAI_API.Completions;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands
{
    public class GptCommand : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly IServiceProvider _serviceProvider;

        public GptCommand(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [SlashCommand("gpt", "[Hidden] Ask something to ChatGPT-3.")]
        public async Task GptCmd(
            [Summary("prompt", "Your question.")] string prompt,
            [Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
        )
        {
            await DeferAsync(!showEveryone);

            string response = await GenerateContent(prompt);

            DefaultEmbed embed = new DefaultEmbed("ChatGPT", "🗨️", Context);
            if (!response.IsEmpty())
            {
                embed.Description = response;
            }
            else
            {
                embed.Description = ":x: Error generating response.";
            }
            embed.AddField("❓ Prompt", prompt, true);

            await embed.SendAsync();
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
            CompletionResult result = await _serviceProvider.GetRequiredService<OpenAIAPI>().Completions.CreateCompletionsAsync(completionRequest, 1);
            if (result.Completions.Count > 0)
            {
                return result.Completions[0].Text;
            }
            return null;
        }
    }
}