using System;

using Diamond.API.SlashCommands.Notebooks.Interaction.Buttons;
using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Embeds
{
	public class PageCreatedEmbed : BaseNotebookEmbed
	{
		private PageCreatedEmbed(IInteractionContext context, long? notebookId)
			: base("Create Notebook Page", context)
		{
			_ = notebookId == null
				? this.AddButton(new ButtonNotebookGotoListAttribute().GetButtonBuilder())
				: this.AddButton(new ButtonNotebookOpenAttribute((long)notebookId).GetButtonBuilder("Go To Notebook"));
		}

		public PageCreatedEmbed(IInteractionContext context, NotebookPage page, Notebook notebook)
			: this(context, notebook.Id)
		{
			this.Title = "Notebook page created";
			this.Description = $"Created the page '**{page.Title}**'{(notebook != null ? $" and added it to the '**{notebook.Name}**' notebook" : "")}.";
		}

		public PageCreatedEmbed(IInteractionContext context, Exception exception)
			: this(context, (long?)null)
		{
			this.Title = "Error";
			this.Description = exception.Message;
		}
	}
}
