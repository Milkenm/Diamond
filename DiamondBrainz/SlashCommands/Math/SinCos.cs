using System.Threading.Tasks;

using Diamond.API.Attributes;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Math;
public partial class Math
{
	[DSlashCommand("sincos", "Calculate the consine of the given angle.")]
	public async Task MathSinCosCommandAsync(
		[Summary("number", "The number to calculate the sine and cosine of.")] double angle,
		[Summary("angle-type", "If the angle you provide is in degrees or radians.")] AngleType angleType = AngleType.Degrees,
		[ShowEveryone] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		(double Radians, double Degrees) angles = ConvertAngle(angle, angleType);
		(double Sin, double Cos) result = System.Math.SinCos(angles.Radians);

		string angleDescription = $"{angle}{AngleSymbols[angleType]}";
		DefaultEmbed embed = new DefaultEmbed("Math Cosine", "🧮", Context.Interaction)
		{
			Description = $"**Sin({angleDescription}) =** {string.Format("{0:N12}", result.Sin)}\n**Cos({angleDescription}) =** {string.Format("{0:N12}", result.Cos)}"
		};
		embed.AddField("📐 Degrees", $"{string.Format("{0:N}", angles.Degrees)}º", true);
		embed.AddField("📐 Radians", $"{string.Format("{0:N}", angles.Radians)}rad", true);

		await embed.SendAsync();
	}
}