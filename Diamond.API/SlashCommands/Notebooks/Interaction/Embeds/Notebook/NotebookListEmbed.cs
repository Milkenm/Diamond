using System.Collections.Generic;
using System.Linq;

using Diamond.API.Helpers;
using Diamond.API.SlashCommands.Notebooks.Exceptions;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class NotebookListEmbed : BaseMultipageNotebookEmbed<Notebook>
	{
		private static readonly MultipageButtons NotebooksMultipageButtons = new MultipageButtons()
		{
			ButtonFirst = ButtonNotebookListFirstAttribute.COMPONENT_ID,
			ButtonPrevious = ButtonNotebookListPreviousAttribute.COMPONENT_ID,
			ButtonPage = ButtonNotebookListPageAttribute.COMPONENT_ID,
			ButtonNext = ButtonNotebookListNextAttribute.COMPONENT_ID,
			ButtonLast = ButtonNotebookListLastAttribute.COMPONENT_ID,
		};

		public NotebookListEmbed(IInteractionContext context, int startingIndex, bool showEveryone, DiamondContext db)
			: base(context, NotebooksMultipageButtons, showEveryone)
		{
			this.SetAuthorInfoWithEmoji("Notebooks List", "📔");

			List<Notebook> userNotebooks = Notebook.GetUserNotebooks(context.User.Id, db);
			this.SetItemsList(userNotebooks, startingIndex, 5);
		}

		public NotebookListEmbed(IInteractionContext context, bool showEveryone, DiamondContext db)
			: this(context, 0, showEveryone, db)
		{ }

		protected override void FillItems(IEnumerable<Notebook> notebooksList)
		{
			if (!notebooksList.Any())
			{
				throw new NoNotebooksException();
			}

			foreach (Notebook notebook in notebooksList)
			{
				_ = this.AddButton(new ButtonNotebookOpenAttribute(notebook.Id).GetButtonBuilder($"#{notebook.Id}"));
				_ = this.AddField($"**#{notebook.Id}** :heavy_minus_sign: {notebook.Name}", notebook.Description ?? "*No description*");
			}
			_ = this.AddButton(new ButtonNotebookEditorAttribute().GetButtonBuilder(), 1);
			_ = this.AddButton(new ButtonNotebookCloseEmbedAttribute().GetButtonBuilder(), 1);
			this.AddNavigationButtons(2);
		}
	}
}
