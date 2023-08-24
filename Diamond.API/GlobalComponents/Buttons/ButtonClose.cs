using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;

namespace Diamond.API.GlobalComponents.Buttons
{
	public class ButtonCloseAttribute : DefaultComponentInteractionAttribute
	{
		public override string ComponentLabel => "Close";
		public override ButtonStyle? ButtonComponentStyle => ButtonStyle.Secondary;

		public const string COMPONENT_ID = "button_global_close";

		public ButtonCloseAttribute()
			: base(COMPONENT_ID)
		{ }
	}

	public class ButtonCloseHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonClose]
		public async Task OnButtonCloseClickHandlerAsync()
		{
			await this.DeferAsync();

			await Utils.DeleteResponseAsync(this.Context);
		}
	}
}
