using Diamond.API.Util;

using Discord;
using Discord.Interactions;

namespace Diamond.API.Helpers
{
	public abstract class DefaultComponentInteractionAttribute : ComponentInteractionAttribute
	{
		public virtual string ComponentLabel { get; }
		public virtual IEmote? ButtonComponentEmote { get; } = null;
		public virtual ButtonStyle? ButtonComponentStyle { get; } = null;

		public string ComponentIdWithData { get; private set; }

		public DefaultComponentInteractionAttribute(string customId, params object[]? data)
			: base(customId, true)
		{
			this.ComponentIdWithData = customId;

			if (data != null)
			{
				foreach (object dataObject in data)
				{
					this.ComponentIdWithData = this.ComponentIdWithData.ReplaceFirst("*", dataObject?.ToString());
				}
			}
			else
			{
				this.ComponentIdWithData = this.ComponentIdWithData.Replace("*", null);
			}
		}

		public DefaultComponentInteractionAttribute(string customId)
			: this(customId, null)
		{ }

		public ButtonBuilder GetButtonBuilder(string? customLabel = null, ButtonStyle? customButtonStyle = null, bool isDisabled = false)
		{
			return new ButtonBuilder(this.GetLabel(customLabel), this.ComponentIdWithData, this.GetButtonStyle(customButtonStyle), emote: this.ButtonComponentEmote, isDisabled: isDisabled);
		}

		public SelectMenuBuilder GetSelectMenuBuilder(string? customLabel = null, int maxValues = 1, int minValues = 1, bool isDisabled = false)
		{
			return new SelectMenuBuilder(this.ComponentIdWithData, null, this.GetLabel(customLabel), maxValues, minValues, isDisabled);
		}

		private string GetLabel(string customLabel)
		{
			return GetValue(customLabel, this.ComponentLabel, "<???>");
		}

		private ButtonStyle GetButtonStyle(ButtonStyle? customButtonStyle)
		{
			return (ButtonStyle)GetValue(customButtonStyle, this.ButtonComponentStyle, ButtonStyle.Primary);
		}

		private static T GetValue<T>(T? customValue, T? originalValue, T? nullValue)
		{
			return customValue ?? originalValue ?? nullValue;
		}
	}
}
