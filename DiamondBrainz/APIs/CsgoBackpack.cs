using System.Collections.Generic;

using Diamond.API.Schems;

namespace Diamond.API.Stuff
{
	public class CsgoBackpack
	{
		private readonly Dictionary<Currency, CsgoItemsList> ItemsMap = new Dictionary<Currency, CsgoItemsList>();
		public CsgoBackpack()
		{

		}

		public enum Currency
		{
			USD,
			EUR,
			BRL,
			JPY,
		}
	}
}
