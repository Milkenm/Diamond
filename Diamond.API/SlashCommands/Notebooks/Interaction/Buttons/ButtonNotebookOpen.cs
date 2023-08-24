using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.API.SlashCommands.Notebooks.Interaction.Embeds;
using Diamond.Data;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Buttons
{
	public class ButtonNotebookOpenAttribute : DefaultComponentInteractionAttribute
	{
		public override string ComponentLabel => "Open Notebook";
		public override ButtonStyle? ButtonComponentStyle => ButtonStyle.Success;

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

			_ = await new PageListEmbed(this.Context, notebookId, false, db).SendAsync();
		}
	}
}
