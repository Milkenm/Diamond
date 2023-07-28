using System;

using Diamond.API.SlashCommands.Notebooks.Components;
using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Embeds
{
	public class CreatedNotebookEmbed : BaseNotebookEmbed
	{
		public CreatedNotebookEmbed(IInteractionContext context, string title, string description, long? notebookId)
			: base("Create Notebook", context)
		{
			this.Title = title;
			this.Description = description;
			this.Component = new ComponentBuilder()
				.WithButton("Go Back", new ButtonNotebookGotoListAttribute(notebookId).ButtonIdWithData, ButtonStyle.Primary)
				.Build();
		}

		public CreatedNotebookEmbed(IInteractionContext context, Notebook notebook)
			: this(context, "Notebook created!", $"You created a notebook called '**{notebook.Name}**'.", notebook.Id)
		{ }

		public CreatedNotebookEmbed(IInteractionContext context, Exception exception)
			: this(context, "Error", exception.Message, null)
		{ }
	}
}
