using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.Data;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Modals
{
    public class ModalPageEditorAttribute : DefaultComponentInteractionAttribute
    {
        public const string COMPONENT_ID = "modal_notebookpage_editor:*,*";

        /// <summary>
        /// Initializes a new instance of the <see cref="ModalPageEditorAttribute"/> class.
        /// </summary>
        public ModalPageEditorAttribute()
            : base(COMPONENT_ID)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModalPageEditorAttribute"/> class with a specified notebook ID.
        /// </summary>
        /// <param name="notebookId">The ID of the notebook.</param>
        public ModalPageEditorAttribute(long? pageId, long notebookId)
            : base(COMPONENT_ID, pageId ?? 0, notebookId)
        { }
    }

    public class ModalPageEditorHandler : InteractionModuleBase<SocketInteractionContext>
    {
        [ModalInteraction(ModalPageEditorAttribute.COMPONENT_ID)]
        public async Task OnModalPageEditorSubmitHandlerAsync(long pageId, long notebookId, PageEditorModal modal)
        {
            await DeferAsync();

            using DiamondContext db = new DiamondContext();

            await Notebooks.CreateOrUpdatePageAsync(Context, pageId, notebookId, modal.PageTitle, modal.PageContent, db);
        }
    }

    public class PageEditorModal : IModal
    {
        public string Title => "Create notebook page";

        [RequiredInput]
        [InputLabel("Title")]
        [ModalTextInput("field_title", TextInputStyle.Short, "My new page...", maxLength: 50)]
        public string PageTitle { get; set; }

        [RequiredInput]
        [InputLabel("Content")]
        [ModalTextInput("field_content", TextInputStyle.Paragraph, "My page content...", maxLength: 1800)]
        public string PageContent { get; set; }
    }
}
