using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
    public class NoNotebooksEmbed : BaseNotebookEmbed
    {
        public NoNotebooksEmbed(IInteractionContext context)
            : base("No Notebooks", context)
        {
            Title = "You don't have any notebooks";
            Description = "You can create one with `/notebooks create <name> [description]` or by clicking the button below.";
            _ = AddButton(new ButtonNotebookEditorAttribute().GetButtonBuilder());
        }
    }
}
