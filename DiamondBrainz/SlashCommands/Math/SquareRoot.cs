using System.Threading.Tasks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Math;
public partial class Math
{
	[SlashCommand("square-root", "Calculate the square root of the given number.")]
	public async Task MathSquareRootCommandAsync(
		[Summary("number", "The number to calculate the square root of.")] double number,
		[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		double sqrt = System.Math.Sqrt(number);

		DefaultEmbed embed = new DefaultEmbed("Math Square Root", "🧮", Context.Interaction)
		{
			Description = $"**Result**: {string.Format("{0:N}", sqrt)}"
		};
		embed.AddField("#️ Number", number);

		await embed.SendAsync();
	}
}
