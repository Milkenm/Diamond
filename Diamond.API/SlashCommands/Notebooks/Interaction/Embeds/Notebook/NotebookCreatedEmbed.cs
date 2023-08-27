using System;

using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
    public class NotebookCreatedEmbed : BaseNotebookEmbed
    {
        public NotebookCreatedEmbed(IInteractionContext context, long? notebookId)
            : base("Create Notebook", context)
        {
            if (notebookId != null)
            {
                _ = AddButton(new ButtonNotebookOpenAttribute((long)notebookId).GetButtonBuilder());
            }
            _ = AddButton(new ButtonNotebookGotoListAttribute().GetButtonBuilder(customButtonStyle: ButtonStyle.Secondary));
        }

        public NotebookCreatedEmbed(IInteractionContext context, Notebook notebook)
            : this(context, notebook.Id)
        {
            Title = "Notebook created!";
            Description = $"You created a notebook called '**{notebook.Name}**'.";
        }

        public NotebookCreatedEmbed(IInteractionContext context, Exception exception)
            : this(context, (long?)null)
        {
            Title = "Error";
            Description = exception.Message;
        }
    }
}
