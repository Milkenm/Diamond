using System;

using Diamond.API.SlashCommands.Notebooks.Interaction.Buttons;
using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Embeds
{
	public class NotebookCreatedEmbed : BaseNotebookEmbed
	{
		public NotebookCreatedEmbed(IInteractionContext context, long? notebookId)
			: base("Create Notebook", context)
		{
			if (notebookId != null)
			{
				_ = this.AddButton(new ButtonNotebookOpenAttribute((long)notebookId).GetButtonBuilder());
			}
			_ = this.AddButton(new ButtonNotebookGotoListAttribute().GetButtonBuilder());
		}

		public NotebookCreatedEmbed(IInteractionContext context, Notebook notebook)
			: this(context, notebook.Id)
		{
			this.Title = "Notebook created!";
			this.Description = $"You created a notebook called '**{notebook.Name}**'.";
		}

		public NotebookCreatedEmbed(IInteractionContext context, Exception exception)
			: this(context, (long?)null)
		{
			this.Title = "Error";
			this.Description = exception.Message;
		}
	}
}
