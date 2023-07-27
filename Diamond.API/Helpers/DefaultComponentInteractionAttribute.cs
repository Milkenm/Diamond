﻿using Diamond.API.Util;

using Discord.Interactions;

namespace Diamond.API.Helpers
{
	public class DefaultComponentInteractionAttribute : ComponentInteractionAttribute
	{
		public string ButtonIdWithData { get; private set; }

		public DefaultComponentInteractionAttribute(string customId) : base(customId, true) { }

		public DefaultComponentInteractionAttribute(string customId, params object[] data) : base(customId, true)
		{
			this.ButtonIdWithData = customId;
			foreach (object dataObject in data)
			{
				_ = this.ButtonIdWithData.ReplaceFirst("*", dataObject.ToString());
			}
		}
	}
}
