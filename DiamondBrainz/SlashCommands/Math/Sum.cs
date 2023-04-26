using System.Threading.Tasks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Math;
public partial class Math
{
	[SlashCommand("sum", "Sum multiple numbers.")]
	public async Task MathSumCommandAsync(
		[Summary("first-number", "The first (left) number.")] double firstNumber,
		[Summary("second-number", "The second (right) number.")] double secondNumber,
		[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		double sum = firstNumber + secondNumber;

		DefaultEmbed embed = new DefaultEmbed("Math Sum", "🧮", Context.Interaction)
		{
			Description = $"**Result**: {string.Format("{0:N}", sum)}"
		};
		embed.AddField("1️⃣ First number", string.Format("{0:N}", firstNumber), true);
		embed.AddField("2️⃣ Second number", string.Format("{0:N}", secondNumber), true);

		await embed.SendAsync();
	}
}
