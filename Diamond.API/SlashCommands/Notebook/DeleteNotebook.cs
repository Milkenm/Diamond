using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Helpers;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.SCNotebook
{
	public partial class SCNotebook
	{
		[DSlashCommand("delete", "Delete a notebook.")]
		public async Task DeleteNotebookCommandAsync()
		{
			await this.DeferAsync(true);

		}

		[ComponentInteraction($"{NotebookComponentIds.BUTTON_NOTEBOOK_DELETE}:*", true)]
		public async Task OnDeleteNotebookButtonClickHandlerAsync(long notebookId)
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();
			Notebook notebook = Notebook.GetNotebook(notebookId, db);

			_ = await new NotebookDeletedEmbed(this.Context)
			{
				Title = "Are you sure?",
				Description = $"Are you sure you want to delete the notebook '{notebook.Name}'?\n**If you delete it, it cannot be recovered and all pages will be lost.**",
				Component = new ComponentBuilder()
					.WithButton("Delete permanently", $"{NotebookComponentIds.BUTTON_NOTEBOOK_DELETE_CONFIRM}:{notebookId}", ButtonStyle.Danger, row: 0)
					.WithButton("Cancel", $"{NotebookComponentIds.BUTTON_NOTEBOOK_DELETE_CANCEL}:{notebookId}", ButtonStyle.Secondary, row: 0)
					.Build(),
			}.SendAsync();
		}

		[ComponentInteraction($"{NotebookComponentIds.BUTTON_NOTEBOOK_DELETE_CONFIRM}:*", true)]
		public async Task OnDeleteNotebookConfirmButtonClickHandlerAsync(long notebookId)
		{
			await this.DeferAsync();

			using DiamondContext db = new DiamondContext();
			Notebook notebook = Notebook.GetNotebook(notebookId, db);

			try
			{
				await Notebook.DeleteNotebookAsync(notebook, db);
			}
			catch (Exception ex)
			{
				_ = await new NotebookDeletedEmbed(this.Context)
				{
					Title = "There was a problem deleting the notebook.",
					Description = ex.Message,
				}.SendAsync();
			}

			_ = await new NotebookDeletedEmbed(this.Context)
			{
				Title = "Notebook deleted",
				Description = $"The notebook '{notebook.Name}' was deleted.",
			}.SendAsync();
		}

		[ComponentInteraction($"{NotebookComponentIds.BUTTON_NOTEBOOK_DELETE_CANCEL}:*", true)]
		public async Task OnDeleteNotebookCancelButtonClickHandlerAsync(long notebookId)
		{
			await this.DeferAsync();

			using DiamondContext db = new DiamondContext();
			Notebook notebook = Notebook.GetNotebook(notebookId, db);
			Dictionary<string, NotebookPage> notebookPages = NotebookPage.GetNotebookPages(notebook, this.Context.User.Id, db);

			await new NotebookPagesListMultipageEmbed(this.Context, notebook, notebookPages.Values.ToList(), 0, 0, false)
				.SendAsync();
		}

		private class NotebookDeletedEmbed : DefaultEmbed
		{
			public NotebookDeletedEmbed(IInteractionContext context)
				: base("Delete Notebook", "📔", context)
			{ }
		}
	}
}
