using System.Collections.Generic;
using System.Linq;

using Diamond.API.Helpers;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class PageListEmbed : BaseMultipageNotebookEmbed<NotebookPage>
	{
		private readonly Notebook _notebook;

		private static readonly MultipageButtons PagesMultipageButtons = new MultipageButtons()
		{
			ButtonFirst = ButtonPageListFirstAttribute.COMPONENT_ID,
			ButtonPrevious = ButtonPageListPreviousAttribute.COMPONENT_ID,
			ButtonPage = ButtonPageListPageAttribute.COMPONENT_ID,
			ButtonNext = ButtonPageListNextAttribute.COMPONENT_ID,
			ButtonLast = ButtonPageListLastAttribute.COMPONENT_ID,
		};

		public PageListEmbed(IInteractionContext context, Notebook notebook, int startingIndex, bool showEveryone, DiamondContext db)
			: base(context, PagesMultipageButtons, showEveryone)
		{
			this._notebook = notebook;
			this.Description = notebook.Description ?? "*No description*";

			List<NotebookPage> notebookPages = NotebookPage.GetNotebookPages(this._notebook, context.User.Id, db);
			this.SetItemsList(notebookPages, startingIndex, 5);

			this.SetAuthorInfoWithEmoji($"Notebook: {this._notebook.Name} ({notebookPages.Count()} {Utils.Plural("page", "", "s", notebookPages.Count())})", "📔");
			this.AddDataToAllButtons(this._notebook.Id);
		}

		public PageListEmbed(IInteractionContext context, long notebookId, int startingIndex, bool showEveryone, DiamondContext db)
			: this(context, Notebook.GetNotebook(notebookId, db), startingIndex, showEveryone, db)
		{ }

		protected override void FillItems(IEnumerable<NotebookPage> notebookPages)
		{

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
				.AddButton(new ButtonNotebookEditorAttribute(this._notebook.Id).GetButtonBuilder("Edit Notebook", ButtonStyle.Secondary), 0 + pageIncrement)
				.AddButton(new ButtonNotebookDeleteAttribute(this._notebook.Id).GetButtonBuilder(), 0 + pageIncrement)
				.AddButton(new ButtonPageGotoListAttribute().GetButtonBuilder(), 0 + pageIncrement);
			this.AddNavigationButtons(1 + pageIncrement);
		}
	}
}
