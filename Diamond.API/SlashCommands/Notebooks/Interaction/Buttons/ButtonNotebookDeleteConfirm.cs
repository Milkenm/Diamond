using System;
using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.API.SlashCommands.Notebooks.Interaction.Embeds;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Buttons
{
	public class ButtonNotebookDeleteConfirmAttribute : DefaultComponentInteractionAttribute
	{
		public override string ComponentLabel => "Delete Permanently";
		public override IEmote ButtonComponentEmote => Emoji.Parse("🗑️");
		public override ButtonStyle? ButtonComponentStyle => ButtonStyle.Danger;

		public const string COMPONENT_ID = "button_notebook_delete_confirm:*";

		public ButtonNotebookDeleteConfirmAttribute()
			: base(COMPONENT_ID)
		{ }

		public ButtonNotebookDeleteConfirmAttribute(long notebookId)
			: base(COMPONENT_ID, notebookId)
		{ }
	}

	public class ButtonNotebookDeleteConfirmHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonNotebookDeleteConfirm]
		public async Task OnButtonNotebookDeleteConfirmClickHandlerAsync(long notebookId)
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();
			Notebook notebook = Notebook.GetNotebook(notebookId, db);

			try
			{
				await Notebook.DeleteNotebookAsync(notebook, db);
			}
			catch (Exception ex)
			{
				_ = await new NotebookDeletedEmbed(this.Context, ex).SendAsync();
				return;
			}

			_ = await new NotebookDeletedEmbed(this.Context, notebook.Name).SendAsync();
		}
	}
}
