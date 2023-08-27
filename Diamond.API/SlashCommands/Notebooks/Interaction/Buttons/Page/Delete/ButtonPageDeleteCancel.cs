using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.Data;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ButtonPageDeleteCancelAttribute : DefaultComponentInteractionAttribute
	{
		public override string ComponentLabel => "Cancel";
		public override ButtonStyle? ButtonComponentStyle => ButtonStyle.Secondary;

		public const string COMPONENT_ID = "button_notebookpage_delete_cancel:*";

		public ButtonPageDeleteCancelAttribute()
			: base(COMPONENT_ID)
		{ }

		public ButtonPageDeleteCancelAttribute(long pageId)
			: base(COMPONENT_ID, pageId)
		{ }
	}

	public class ButtonPageDeleteCancelHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonPageDeleteCancel]
		public async Task OnButtonPageDeleteCancelClickHandlerAsync(long pageId)
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();

			_ = await new PageListEmbed(this.Context, pageId, 0, false, db).SendAsync();
		}
	}
}
