using System.Collections.Generic;
using System.Linq;

using Discord.Interactions;

namespace Diamond.API.Helpers
{
	/*public class ComponentId : InteractionModuleBase<SocketInteractionContext>
	{
		public string ButtonId { get; }
		public List<string> ButtonIdWithPattern { get; private set; }
		public string ButtonIdWithData { get; private set; }

		public ComponentId(string buttonId, params object[] data)
		{
			if (!data.Any())
			{
				this.ButtonIdWithPattern = this.ButtonId;
				this.ButtonIdWithData = this.ButtonId;
				return;
			}

			string buttonIdPattern = this.ButtonId + ":";
			string buttonIdData = this.ButtonId + ":";
			for (int i = 0; i < data.Length; i++)
			{
				buttonIdPattern += "*";
				buttonIdData += data.ToString();

				if (i < data.Length)
				{
					buttonIdPattern += ",";
					buttonIdData += ",";
				}
			}
			this.ButtonIdWithPattern = buttonIdPattern;
			this.ButtonIdWithData = buttonIdData;
		}
	}*/
}
