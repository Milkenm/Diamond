using System;
using System.Collections.Immutable;

using Diamond.API.SlashCommands.Notebooks.Interaction.Buttons;
using Diamond.Data.Models.Notebooks;

using Discord;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Embeds
{
	public class NotebookUpdatedEmbed : BaseNotebookEmbed
	{
		private NotebookUpdatedEmbed(IInteractionContext context, long? notebookId)
			: base("Update Notebook", context)
		{
			if (notebookId != null)
			{
				_ = this.AddButton(new ButtonNotebookOpenAttribute((long)notebookId).GetButtonBuilder());
			}
			else
			{
				_ = this.AddButton(new ButtonNotebookGotoListAttribute().GetButtonBuilder());
			}
		}

		public NotebookUpdatedEmbed(IInteractionContext context, Notebook notebook, string oldName, string oldDescription)
			: this(context, notebook.Id)
		{
			this.Title = "Notebook updated!";

			if (notebook.Name != oldName && notebook.Description == oldDescription)
			{
				this.Description = $"You changed the name of the notebook from '**{oldName}**' to '**{notebook.Name}**'.";
			}
			else if (notebook.Description != oldDescription && notebook.Name == oldName)
			{
				if (!oldDescription.IsEmpty())
				{
					this.Description = $"You changed the description of '**{notebook.Name}**' from\n`{oldDescription}`\nto:\n`{notebook.Description}`";
				}
				else
				{
					this.Description = $"The description of '**{notebook.Name}**' was set to `{notebook.Description}`";
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
				this.Description = $"You changed the name of the notebook from '**{oldName}**' to '**{notebook.Name}**'.\n{description}";
			}
			else if (notebook.Name == oldName && notebook.Description == oldDescription)
			{
				this.Description = $"Nah just kidding, nothing was changed.";
			}
		}

		public NotebookUpdatedEmbed(IInteractionContext context, Exception exception)
			: this(context, (long?)null)
		{
			this.Title = "Error";
			this.Description = exception.Message;
		}
	}
}
