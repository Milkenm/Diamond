using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Util;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Math;
public partial class Math
{
	[DSlashCommand("cos", "Calculate the consine of the given angle.")]
	public async Task MathCosCommandAsync(
		[Summary("number", "The number to calculate the cosine of.")] double angle,
		[Summary("angle-type", "If the angle you provide is in degrees or radians.")] AngleType angleType = AngleType.Degrees,
		[ShowEveryone] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		(double Radians, double Degrees) angles = ConvertAngle(angle, angleType);
		double cos = System.Math.Cos(angles.Radians);

		DefaultEmbed embed = new DefaultEmbed("Math Cosine", "🧮", Context)
		{
			Description = $"**Cos({angle}{AngleSymbols[angleType]}) =** {string.Format("{0:N12}", cos)}"
		};
		embed.AddField("📐 Degrees", $"{string.Format("{0:N}", angles.Degrees)}º", true);
		embed.AddField("📐 Radians", $"{string.Format("{0:N}", angles.Radians)}rad", true);

		await embed.SendAsync();
	}
}
