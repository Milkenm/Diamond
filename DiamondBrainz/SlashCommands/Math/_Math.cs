using System.Collections.Generic;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Math
{
	[Group("math", "Math related commands.")]
	public partial class Math : InteractionModuleBase<SocketInteractionContext>
	{
		public (double Radians, double Degrees) ConvertAngle(double angle, AngleType angleType)
		{
			double radians = angle;
			double degrees = angle;
			if (angleType == AngleType.Degrees)
			{
				radians = angle * System.Math.PI / 180D;
			}
			else
			{
				degrees = angle * (System.Math.PI / 180D);
			}
			return (radians, degrees);
		}

		public enum AngleType
		{
			Degrees,
			Radians,
		}

		public Dictionary<AngleType, string> AngleSymbols = new Dictionary<AngleType, string>()
	{
		{ AngleType.Radians, "rad" },
		{ AngleType.Degrees, "º" },
	};
	}
}