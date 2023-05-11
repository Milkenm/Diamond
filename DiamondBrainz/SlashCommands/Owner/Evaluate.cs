using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Discord;
using Discord.Interactions;

using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace Diamond.API.SlashCommands.Owner;
public partial class Owner
{
	[SlashCommand("eval", "Evaluate a C# expression.")]
	public async Task EvalCommandAsync(
		[Summary("expression", "The expression to evaluate.")] string expression,
		[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		if (expression.StartsWith("```cs"))
		{
			expression = expression.Remove(0, 5);
		}
		if (expression.StartsWith("```"))
		{
			expression = expression.Remove(0, 3);
		}
		if (expression.EndsWith("```"))
		{
			expression = expression.Remove(expression.Length - 3, 3);
		}
		if (expression.StartsWith("`"))
		{
			expression = expression.Remove(0, 1);
		}
		if (expression.EndsWith("`"))
		{
			expression = expression.Remove(expression.Length - 2, 1);
		}

		object? result = null;
		string executionTime = "0";
		string? error = null;
		try
		{
			Stopwatch sw = Stopwatch.StartNew();
			result = await CSharpScript.EvaluateAsync(expression);
			sw.Stop();
			executionTime = $"{System.Math.Round((decimal)sw.ElapsedTicks / 10000, 0)}ms";
		}
		catch (Exception ex)
		{
			error = ex.Message;
		}

		DefaultEmbed embed = new DefaultEmbed("Evaluate", "💻", Context.Interaction)
		{
			Title = error != null ? "Error!" : (result == null ? "No output" : result.ToString()),
			Description = error != null ? error : $"```cs\n{expression}```\n**Execution time:** {executionTime}",
		};
		await embed.SendAsync();
	}

	[MessageCommand("Evaluate")]
	public async Task EvaluateMessageCommandAsync(IMessage message)
	{
		string content = Utils.GetMessageContent(message);
		if (content == null)
		{
			DefaultEmbed embed = new DefaultEmbed("Evaluate", "💻", Context.Interaction)
			{
				Title = "Error",
				Description = "No content was found on the selected message.",
			};
			await embed.SendAsync();
			return;
		}
		await this.EvalCommandAsync(message.Content);
	}
}
