using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Math;
public partial class Math
{
	[SlashCommand("sin", "Calculate the sine of the given angle.")]
	public async Task MathSinCommandAsync(
		[Summary("number", "The number to calculate the sine of.")] double angle,
		[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		double sin = System.Math.Sin(angle);

		DefaultEmbed embed = new DefaultEmbed("Math Sine", "📐", Context.Interaction)
		{
			Description = $"**Result**: {string.Format("{0:N}", sin)}"
		};
		embed.AddField("#️ Angle", angle);

		await embed.SendAsync();
	}
}
