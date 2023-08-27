using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ButtonPageEditorAttribute : DefaultComponentInteractionAttribute
	{
		public override string ComponentLabel => "Create Page";
		public override ButtonStyle? ButtonComponentStyle => ButtonStyle.Success;

		public static string COMPONENT_ID => "button_notebookpage_create:*,*";

		public ButtonPageEditorAttribute()
			: base(COMPONENT_ID)
		{ }

		public ButtonPageEditorAttribute(long notebookId)
			: base(COMPONENT_ID, 0, notebookId)
		{ }

		public ButtonPageEditorAttribute(long pageId, long notebookId)
			: base(COMPONENT_ID, pageId, notebookId)
		{ }
	}

	public class ButtonPageEditorHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonPageEditor]
		public async Task OnButtonPageEditorClickHandlerAsync(long pageId, long notebookId)
		{
			PageEditorModal modal = new PageEditorModal();
			if (pageId != 0)
			{
				using DiamondContext db = new DiamondContext();
				NotebookPage page = NotebookPage.GetNotebookPage(pageId, db);

				modal.PageTitle = page.Title;
				modal.PageContent = page.Content;
			}

			await this.Context.Interaction.RespondWithModalAsync(new ModalPageEditorAttribute(pageId, notebookId).ComponentIdWithData, modal);
		}
	}
}
