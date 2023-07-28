using Diamond.API.Helpers;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Modals
{
	public class NotebookPageEditorModal : DefaultModal
	{
		public NotebookPageEditorModal() { }

		public NotebookPageEditorModal(IInteractionContext context)
			: base("Create notebook page", "modal_notebook_createpage", context)
		{ }

		[RequiredInput]
		[InputLabel("Title")]
		[ModalTextInput("field_title", TextInputStyle.Short, "My new page...", maxLength: 250)]
		public string PageTitle { get; set; }

		[RequiredInput]
		[InputLabel("Content")]
		[ModalTextInput("field_content", TextInputStyle.Paragraph, "My page content...")]
		public string PageContent { get; set; }

		[RequiredInput(false)]
		[InputLabel("Notebook")]
		[ModalTextInput("field_notebook", TextInputStyle.Short, "My notebook...")]
		public string? Notebook { get; set; }
	}
}
