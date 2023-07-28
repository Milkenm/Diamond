using System;

using Discord;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.Notebooks.Embeds
{
	/*public class CreatedNotebookPageEmbed : NotebookEmbed
	{
		public const string AUTHOR_TITLE = "Create Notebook Page";

		public CreatedNotebookPageEmbed(IInteractionContext context, string pageTitle, string? notebookName)
			: base(AUTHOR_TITLE, context)
		{
			this.Title = "Notebook page created";
			this.Description = $"Created the page '**{pageTitle}**'{(!notebookName.IsEmpty() ? $" and added it to the '**{notebookName}**' notebook" : "")}.";
		}

		public CreatedNotebookPageEmbed(IInteractionContext context, Exception exception)
			: base(AUTHOR_TITLE, context)
		{
			this.Title = "Error";
			this.Description = exception.Message;
		}
	}*/
}
