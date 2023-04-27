using System.Threading.Tasks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Math;
public partial class Math
{
	[SlashCommand("sin", "Calculate the sine of the given angle.")]
	public async Task MathSinCommandAsync(
		[Summary("number", "The number to calculate the sine of.")] double angle,
		[Summary("angle-type", "If the angle you provide is in degrees or radians.")] AngleType angleType = AngleType.Degrees,
		[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		(double Radians, double Degrees) angles = ConvertAngle(angle, angleType);
		double sin = System.Math.Sin(angles.Radians);

		DefaultEmbed embed = new DefaultEmbed("Math Sine", "🧮", Context.Interaction)
		{
			Description = $"**Sin({angle}{AngleSymbols[angleType]}) =** {string.Format("{0:N12}", sin)}"
		};
		embed.AddField("📐 Degrees", $"{string.Format("{0:N}", angles.Degrees)}º", true);
		embed.AddField("📐 Radians", $"{string.Format("{0:N}", angles.Radians)}rad", true);

		await embed.SendAsync();
	}
}
