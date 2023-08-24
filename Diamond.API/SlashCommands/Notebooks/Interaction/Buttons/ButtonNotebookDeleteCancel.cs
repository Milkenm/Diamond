using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.API.SlashCommands.Notebooks.Interaction.Embeds;
using Diamond.Data;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Buttons
{
	public class ButtonNotebookDeleteCancelAttribute : DefaultComponentInteractionAttribute
	{
		public override string ComponentLabel => "Cancel";
		public override ButtonStyle? ButtonComponentStyle => ButtonStyle.Secondary;

		public const string COMPONENT_ID = "button_notebook_delete_cancel:*";

		public ButtonNotebookDeleteCancelAttribute()
			: base(COMPONENT_ID)
		{ }

		public ButtonNotebookDeleteCancelAttribute(long notebookId)
			: base(COMPONENT_ID, notebookId)
		{ }
	}

	public class ButtonNotebookDeleteCancelHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonNotebookDeleteCancel]
		public async Task OnButtonNotebookDeleteCancelClickHandlerAsync(long notebookId)
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();

			_ = await new PageListEmbed(this.Context, notebookId, false, db).SendAsync();
		}
	}
}
