using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Diamond.API.SlashCommands.Notebooks.Components;
using Diamond.API.SlashCommands.Notebooks.Exceptions;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Embeds
{
	public class ListNotebooksEmbed : MultipageNotebookEmbed<Notebook>
	{
		public ListNotebooksEmbed(IInteractionContext context, List<Notebook> userNotebooks, int startingIndex, bool showEveryone)
			: base("Notebooks List", context, userNotebooks, startingIndex, showEveryone)
		{ }

		public ListNotebooksEmbed(IInteractionContext context, Notebook? notebook, bool showEveryone)
			: this(context, GetNotebooksByUserId(context.User.Id), GetPageIndexOfNotebook(notebook, context.User.Id), showEveryone)
		{ }

		public ListNotebooksEmbed(IInteractionContext context, long? notebookId, bool showEveryone)
			: this(context, GetNotebooksByUserId(context.User.Id), GetPageIndexOfNotebook(notebookId, context.User.Id), showEveryone)
		{ }

		public ListNotebooksEmbed(IInteractionContext context, int startingIndex, bool showEveryone)
			: this(context, GetNotebooksByUserId(context.User.Id), startingIndex, showEveryone)
		{ }

		private static List<Notebook> GetNotebooksByUserId(ulong userId)
		{
			using DiamondContext db = new DiamondContext();

			Dictionary<string, Notebook> userNotebooksMap = Notebook.GetNotebooksMap(userId, db);

			return userNotebooksMap.Values.ToList();
		}

		private static int GetPageIndexOfNotebook(Notebook? notebook, ulong userId)
		{
			if (notebook == null)
			{
				return 0;
			}

			List<Notebook> userNotebooks = GetNotebooksByUserId(userId);

			// TODO: Broken - element not found
			return userNotebooks.IndexOf(notebook);
		}

		private static int GetPageIndexOfNotebook(long? notebookId, ulong userId)
		{
			if (notebookId == null)
			{
				return 0;
			}

			using DiamondContext db = new DiamondContext();

			Notebook notebook = Notebook.GetNotebook((long)notebookId, db);

			return GetPageIndexOfNotebook(notebook, userId);
		}

		protected override void FillItems(IEnumerable<Notebook> notebooksList)
		{
			if (!notebooksList.Any())
			{
				throw new NoNotebooksException();
			}

			foreach (Notebook notebook in notebooksList)
			{
				_ = this.AddButton(new ButtonBuilder($"#{notebook.Id}", new ButtonNotebookOpenAttribute(notebook.Id, this.StartingIndex).ButtonIdWithData, ButtonStyle.Success), 0);
				_ = this.AddField($"**#{notebook.Id}** :heavy_minus_sign: {notebook.Name}", notebook.Description ?? "*No description*");
			}
			this.AddNavigationButtons(1);
		}
	}
}
