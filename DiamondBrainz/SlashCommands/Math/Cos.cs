using System.Threading.Tasks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Math;
public partial class Math
{
	[SlashCommand("cos", "Calculate the consine of the given angle.")]
	public async Task MathCosCommandAsync(
		[Summary("number", "The number to calculate the cosine of.")] double angle,
		[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		double sin = System.Math.Cos(angle);

		DefaultEmbed embed = new DefaultEmbed("Math Cosine", "📐", Context.Interaction)
		{
			Description = $"**Result**: {string.Format("{0:N}", sin)}"
		};
		embed.AddField("#️ Angle", angle);

		await embed.SendAsync();
	}
}
