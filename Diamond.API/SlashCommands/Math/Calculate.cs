using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Helpers;

using Discord.Interactions;

using NCalc;

namespace Diamond.API.SlashCommands.Math
{
    public partial class Math
	{
		[DSlashCommand("calculate", "Calculate the value of the math expression.")]
		public async Task CalculateCommandAsync(
			[Summary("expression", "The math expression to calculate.")] string expressionString,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			Expression expression = new Expression(expressionString);

			DefaultEmbed embed = new DefaultEmbed("Calculate", "🧮", this.Context)
			{
				Description = $"`Result:` {expression.Evaluate()}",
			};

			try
			{
				if (!expression.HasErrors())
				{
					_ = embed.AddField("🟰 Expression", expression.ParsedExpression.ToString(), true);
				}
			}
			catch
			{
				_ = embed.WithDescription("❌ **Error:** Invalid expression.");
			}

			_ = await embed.SendAsync();
		}
	}
}
