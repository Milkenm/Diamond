using System;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class PageDeletedEmbed : BaseNotebookEmbed
	{
		public PageDeletedEmbed(IInteractionContext context, string title, string description, long notebookId)
			: base("Delete Page", context)
		{
			this.Title = title;
			this.Description = description;
			_ = this.AddButton(new ButtonNotebookOpenAttribute(notebookId).GetButtonBuilder("Go To Notebook"));
		}

		public PageDeletedEmbed(IInteractionContext context, string notebookName, long notebookId)
			: this(context, "Page deleted", $"The page '**{notebookName}**' was deleted.", notebookId)
		{ }

		public PageDeletedEmbed(IInteractionContext context, Exception exception, long notebookId)
			: this(context, "There was a problem deleting the page.", exception.Message, notebookId)
		{ }
	}
}
