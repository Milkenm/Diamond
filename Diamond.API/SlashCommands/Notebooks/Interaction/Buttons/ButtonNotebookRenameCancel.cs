using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.API.SlashCommands.Notebooks.Interaction.Embeds;
using Diamond.Data;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Buttons
{
	public class ButtonNotebookRenameCancelAttribute : DefaultComponentInteractionAttribute
	{
		public override string ComponentLabel => "Cancel";
		public override ButtonStyle? ButtonComponentStyle => ButtonStyle.Secondary;

		public const string COMPONENT_ID = "button_notebook_rename_cancel:*";

		public ButtonNotebookRenameCancelAttribute()
			: base(COMPONENT_ID)
		{ }

		public ButtonNotebookRenameCancelAttribute(long notebookId)
			: base(COMPONENT_ID, notebookId)
		{ }
	}

	public class ButtonNotebookRenameCancelHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonNotebookRenameCancel]
		public async Task OnButtonNotebookRenameCancelClickHandlerAsync(long notebookId)
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();

			_ = await new PageListEmbed(this.Context, notebookId, false, db).SendAsync();
		}
	}
}
