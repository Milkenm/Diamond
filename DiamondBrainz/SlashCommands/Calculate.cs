using System.Threading.Tasks;

using Diamond.API.Attributes;

using Discord.Interactions;

using NCalc;

namespace Diamond.API.SlashCommands
{
	public class Calculate : InteractionModuleBase<SocketInteractionContext>
	{
		[DSlashCommand("calculate", "Calculate the value of the math expression.")]
		public async Task CalculateCommandAsync(
			[Summary("expression", "The math expression to calculate.")] string expressionString,
			[ShowEveryone] bool showEveryone = false
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
