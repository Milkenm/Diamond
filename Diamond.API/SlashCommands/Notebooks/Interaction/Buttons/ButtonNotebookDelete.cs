using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.API.SlashCommands.Notebooks.Interaction.Embeds;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Buttons
{
	public class ButtonNotebookDeleteAttribute : DefaultComponentInteractionAttribute
	{
		public override string ComponentLabel => "Delete Notebook";
		public override IEmote ButtonComponentEmote => Emoji.Parse("🗑️");
		public override ButtonStyle? ButtonComponentStyle => ButtonStyle.Danger;

		public const string COMPONENT_ID = "button_notebook_delete:*";

		public ButtonNotebookDeleteAttribute()
			: base(COMPONENT_ID)
		{ }

		public ButtonNotebookDeleteAttribute(long notebookId)
			: base(COMPONENT_ID, notebookId)
		{ }
	}

	public class ButtonNotebookDeleteHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonNotebookDelete]
		public async Task OnButtonNotebookDeleteClickHandlerAsync(long notebookId)
		{
			await this.DeferAsync(true);

			_ = await new NotebookDeleteConfirmEmbed(this.Context, notebookId).SendAsync();
		}
	}
}
