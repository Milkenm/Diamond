using System.Threading.Tasks;

using Diamond.API.Attributes;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Math;
public partial class Math
{
	[DSlashCommand("power", "Calculate the power of a given number.")]
	public async Task MathPowerCommandAsync(
		[Summary("base", "The base number.")] int @base,
		[Summary("power", "The power number")] int power,
		[ShowEveryone] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		int result = (int)System.Math.Pow(@base, power);

		DefaultEmbed embed = new DefaultEmbed("Math Power", "✊", Context.Interaction)
		{
			Description = $"**Result**: {result}"
		};
		embed.AddField("Base", @base, true);
		embed.AddField("Power", power, true);

		await embed.SendAsync();
	}
}
