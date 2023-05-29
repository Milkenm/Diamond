using System;

namespace Diamond.API.Util
{
	public class RandomGenerator
	{
		private static RandomGenerator Instance;

		public static RandomGenerator GetInstance()
		{
			return Instance ?? new RandomGenerator();
		}

		public Random Random { get; private set; }

		public RandomGenerator()
		{
			Instance = this;

			this.Random = new Random();
		}
	}
}
