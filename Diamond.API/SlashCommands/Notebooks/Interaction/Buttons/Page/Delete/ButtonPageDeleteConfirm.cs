using System;
using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ButtonPageDeleteConfirmAttribute : DefaultComponentInteractionAttribute
	{
		public override string ComponentLabel => "Delete Permanently";
		public override IEmote ButtonComponentEmote => Emoji.Parse("🗑️");
		public override ButtonStyle? ButtonComponentStyle => ButtonStyle.Danger;

		public const string COMPONENT_ID = "button_notebookpage_delete_confirm:*";

		public ButtonPageDeleteConfirmAttribute()
			: base(COMPONENT_ID)
		{ }

		public ButtonPageDeleteConfirmAttribute(long pageId)
			: base(COMPONENT_ID, pageId)
		{ }
	}

	public class ButtonPageDeleteConfirmHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonPageDeleteConfirm]
		public async Task OnButtonPageDeleteConfirmClickHandlerAsync(long pageId)
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();
			NotebookPage page = NotebookPage.GetNotebookPage(pageId, db);
			long notebookId = page.Notebook.Id;

			try
			{
				await NotebookPage.DeleteNotebookPageAsync(page, db);

				_ = await new PageDeletedEmbed(this.Context, page.Title, notebookId).SendAsync();
			}
			catch (Exception ex)
			{
				_ = await new PageDeletedEmbed(this.Context, ex, notebookId).SendAsync();
			}
		}
	}
}
