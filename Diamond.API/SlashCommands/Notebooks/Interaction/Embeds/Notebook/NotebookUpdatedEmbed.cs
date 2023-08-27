using System;

using Diamond.Data.Models.Notebooks;

using Discord;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
    public class NotebookUpdatedEmbed : BaseNotebookEmbed
    {
        private NotebookUpdatedEmbed(IInteractionContext context, long? notebookId)
            : base("Update Notebook", context)
        {
            if (notebookId != null)
            {
                _ = AddButton(new ButtonNotebookOpenAttribute((long)notebookId).GetButtonBuilder());
            }
            else
            {
                _ = AddButton(new ButtonNotebookGotoListAttribute().GetButtonBuilder());
            }
        }

        public NotebookUpdatedEmbed(IInteractionContext context, Notebook notebook, string oldName, string oldDescription)
            : this(context, notebook.Id)
        {
            Title = "Notebook updated!";

            if (notebook.Name != oldName && notebook.Description == oldDescription)
            {
                Description = $"You changed the name of the notebook from '**{oldName}**' to '**{notebook.Name}**'.";
            }
            else if (notebook.Description != oldDescription && notebook.Name == oldName)
            {
                if (!oldDescription.IsEmpty())
                {
                    Description = $"You changed the description of '**{notebook.Name}**' from\n`{oldDescription}`\nto:\n`{notebook.Description}`";
                }
                else
                {
                    Description = $"The description of '**{notebook.Name}**' was set to `{notebook.Description}`";
                }
            }
            else if (notebook.Name != oldName && notebook.Description != oldDescription)
            {
                string description;
                if (!oldDescription.IsEmpty())
                {
                    description = $"The description was also changed from\n`{oldDescription}`\nto:\n`{notebook.Description}`";
                }
                else
                {
                    description = $"The description was also set to `{notebook.Description}`";
                }
                Description = $"You changed the name of the notebook from '**{oldName}**' to '**{notebook.Name}**'.\n{description}";
            }
            else if (notebook.Name == oldName && notebook.Description == oldDescription)
            {
                Description = $"Nah just kidding, nothing was changed.";
            }
        }

        public NotebookUpdatedEmbed(IInteractionContext context, Exception exception)
            : this(context, (long?)null)
        {
            Title = "Error";
            Description = exception.Message;
        }
    }
}
