using System;

namespace Diamond.Brainz.Utils
{
	public static class TimeToMilliseconds
	{
		public static int ConvertToMilliseconds(TimeUnits timeUnit, int value)
		{
			switch (timeUnit)
			{
				case TimeUnits.Seconds:
					return value * 1000;
				case TimeUnits.Minutes:
					return value * 60000;
				case TimeUnits.Hours:
					return value * 3600000;
				case TimeUnits.Days:
					return value * 86400000;
				default:
					throw new Exception("Something went wrong.");
			}
		}

		public enum TimeUnits
		{
			Seconds,
			Minutes,
			Hours,
			Days,
		}
	}
}
