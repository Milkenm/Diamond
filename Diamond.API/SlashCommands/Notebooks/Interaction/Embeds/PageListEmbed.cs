using System.Collections.Generic;
using System.Linq;

using Diamond.API.SlashCommands.Notebooks.Interaction.Buttons;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Embeds
{
	public class PageListEmbed : MultipageNotebookEmbed<NotebookPage>
	{
		private readonly Notebook _notebook;

		public PageListEmbed(IInteractionContext context, Notebook notebook, bool showEveryone, DiamondContext db)
			: base(context, showEveryone)
		{
			this._notebook = notebook;

			this.Description = notebook.Description ?? "*No description*";

			List<NotebookPage> notebookPages = NotebookPage.GetNotebookPages(this._notebook, context.User.Id, db);
			this.SetItemsList(notebookPages, 0, 5);
		}

		public PageListEmbed(IInteractionContext context, long notebookId, bool showEveryone, DiamondContext db)
			: this(context, Notebook.GetNotebook(notebookId, db), showEveryone, db)
		{ }

		protected override void FillItems(IEnumerable<NotebookPage> notebookPages)
		{
			this.SetAuthorInfoWithEmoji($"Notebook: {this._notebook.Name} ({notebookPages.Count()} {Utils.Plural("page", "", "s", notebookPages.Count())})", "📔");

			bool hasPages = notebookPages.Any();
			if (!hasPages)
			{
				this.Description += "\n\n`This notebook doesn't have any pages.`";
			}
			else
			{
				foreach (NotebookPage page in notebookPages)
				{
					_ = this.AddButton(new ButtonPageOpenAttribute(page.Id, this._notebook.Id).GetButtonBuilder($"#{page.Id}"));

					string content = page.Content;
					if (page.Content.Length > 28)
					{
						content = $"{string.Join("", page.Content.Take(25))}...";
					}
					else
					{
						string[] splitContent = page.Content.Split("\n");
						if (splitContent.Length > 1)
						{
							content = splitContent[0] + "...";
						}
					}
					_ = this.AddField($"**#{page.Id}** :heavy_minus_sign: {page.Title}", content);
				}
			}

			int pageIncrement = hasPages ? 1 : 0;
			_ = this.AddButton(new ButtonPageEditorAttribute(this._notebook.Id).GetButtonBuilder(), 0 + pageIncrement)
				.AddButton(new ButtonNotebookEditorAttribute(this._notebook.Id).GetButtonBuilder("Edit Notebook"), 0 + pageIncrement)
				.AddButton(new ButtonNotebookDeleteAttribute(this._notebook.Id).GetButtonBuilder(), 0 + pageIncrement)
				.AddButton(new ButtonPageGotoListAttribute().GetButtonBuilder(), 0 + pageIncrement);
			this.AddNavigationButtons(1 + pageIncrement);
		}
	}
}
