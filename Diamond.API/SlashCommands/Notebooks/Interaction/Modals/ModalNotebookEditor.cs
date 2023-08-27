using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.Data;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ModalNotebookEditorAttribute : DefaultComponentInteractionAttribute
	{
		public const string COMPONENT_ID = "modal_notebook_editor:*";

		/// <summary>
		/// Initializes a new instance of the <see cref="ModalNotebookEditorAttribute"/> class.
		/// </summary>
		public ModalNotebookEditorAttribute()
			: base(COMPONENT_ID, 0)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="ModalNotebookEditorAttribute"/> class with a specified notebook ID.
		/// </summary>
		/// <param name="notebookId">The ID of the notebook.</param>
		public ModalNotebookEditorAttribute(long notebookId)
			: base(COMPONENT_ID, notebookId)
		{ }
	}

	public class ModalNotebookEditorHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ModalInteraction(ModalNotebookEditorAttribute.COMPONENT_ID)]
		public async Task OnModalNotebookEditorSubmitHandlerAsync(long notebookId, NotebookEditorModal modal)
		{
			await this.DeferAsync();

			using DiamondContext db = new DiamondContext();

			await Notebooks.CreateOrUpdateNotebookAsync(this.Context, notebookId, modal.NotebookName, modal.NotebookDescription, db);
		}
	}

	public class NotebookEditorModal : IModal
	{
		public string Title => "Create new notebook";

		[RequiredInput]
		[InputLabel("Name")]
		[ModalTextInput("field_name", TextInputStyle.Short, "My new notebook...", maxLength: 50)]
		public string NotebookName { get; set; }

		[RequiredInput(false)]
		[InputLabel("Description")]
		[ModalTextInput("field_description", TextInputStyle.Short, "My notebook description...", maxLength: 100)]
		public string NotebookDescription { get; set; }
	}
}
