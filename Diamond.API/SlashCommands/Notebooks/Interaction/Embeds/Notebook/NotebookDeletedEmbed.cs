using System;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
    public class NotebookDeletedEmbed : BaseNotebookEmbed
    {
        public NotebookDeletedEmbed(IInteractionContext context, string title, string description)
            : base("Delete Notebook", context)
        {
            Title = title;
            Description = description;
            _ = AddButton(new ButtonNotebookGotoListAttribute().GetButtonBuilder());
        }

        public NotebookDeletedEmbed(IInteractionContext context, string notebookName)
            : this(context, "Notebook deleted", $"The notebook '**{notebookName}**' was deleted.")
        { }

        public NotebookDeletedEmbed(IInteractionContext context, Exception exception)
            : this(context, "There was a problem deleting the notebook.", exception.Message)
        { }
    }
}
