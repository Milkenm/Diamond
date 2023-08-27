using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.Data;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ButtonNotebookOpenAttribute : DefaultComponentInteractionAttribute
	{
		public override string ComponentLabel => "Open Notebook";
		public override ButtonStyle? ButtonComponentStyle => ButtonStyle.Primary;

		public const string COMPONENT_ID = "button_notebook_open:*";

		public ButtonNotebookOpenAttribute()
			: base(COMPONENT_ID)
		{ }

		public ButtonNotebookOpenAttribute(long notebookId)
			: base(COMPONENT_ID, notebookId)
		{ }
	}

	public class ButtonNotebookOpenHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonNotebookOpen]
		public async Task OnButtonNotebookOpenClickHandlerAsync(long notebookId)
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();

			_ = await new PageListEmbed(this.Context, notebookId, 0, false, db).SendAsync();
		}
	}
}
