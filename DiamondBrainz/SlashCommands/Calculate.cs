using System.Threading.Tasks;

using Discord.Interactions;

using NCalc;

namespace Diamond.API.SlashCommands
{
	public class Calculate : InteractionModuleBase<SocketInteractionContext>
	{
		[SlashCommand("calculate", "[Hidden] Calculate the value of the math expression.")]
		public async Task CalculateCommandAsync(
			[Summary("expression", "The math expression to calculate.")] string expressionString,
			[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
		)
		{
			await DeferAsync(!showEveryone);

			Expression expression = new Expression(expressionString);

			DefaultEmbed embed = new DefaultEmbed("Calculate", "🧮", Context.Interaction)
			{
				Description = $"`Result:` {expression.Evaluate()}",
			};

			try
			{
				if (!expression.HasErrors())
				{
					embed.AddField("🟰 Expression", expression.ParsedExpression.ToString(), true);
				}
			}
			catch
			{
				embed.WithDescription("❌ **Error:** Invalid expression.");
			}

			await embed.SendAsync();
		}
	}
}
