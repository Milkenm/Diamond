using System.Collections.Generic;
using System.Linq;

using Diamond.API.GlobalComponents.Buttons;
using Diamond.API.SlashCommands.Notebooks.Exceptions;
using Diamond.API.SlashCommands.Notebooks.Interaction.Buttons;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Embeds
{
	public class NotebookListEmbed : MultipageNotebookEmbed<Notebook>
	{
		public NotebookListEmbed(IInteractionContext context, List<Notebook> userNotebooks, int startingIndex, bool showEveryone)
			: base("Notebooks List", context, userNotebooks, startingIndex, showEveryone)
		{ }

		public NotebookListEmbed(IInteractionContext context, Notebook notebook, bool showEveryone)
			: this(context, GetNotebooksByUserId(context.User.Id), 0, showEveryone)
		{ }

		public NotebookListEmbed(IInteractionContext context, bool showEveryone)
			: this(context, GetNotebooksByUserId(context.User.Id), 0, showEveryone)
		{ }

		private static List<Notebook> GetNotebooksByUserId(ulong userId)
		{
			using DiamondContext db = new DiamondContext();

			List<Notebook> userNotebooksMap = Notebook.GetUserNotebooks(userId, db);

			return userNotebooksMap;
		}

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
			_ = this.AddButton(new ButtonCloseAttribute().GetButtonBuilder(), 1);
			this.AddNavigationButtons(2);
		}
	}
}
