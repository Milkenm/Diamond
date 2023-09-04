﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.SlashCommands.Notebooks.Exceptions;
using Diamond.API.SlashCommands.Notebooks.Interaction;
using Diamond.Data;
using Diamond.Data.Exceptions.NotebookExceptions;
using Diamond.Data.Exceptions.NotebookPageExceptions;
using Diamond.Data.Models.Notebooks;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks
{
	[Group("notebooks", "Notebook related commands.")]
	public partial class Notebooks : InteractionModuleBase<SocketInteractionContext>
	{
		#region Settings
		public const int ITEMS_PER_PAGE = 5;
		#endregion

		#region Utils
		public static Dictionary<string, Notebook> GetNotebooksMap(List<Notebook> notebooksList)
		{
			return notebooksList.ToDictionary(n => n.Name, n => n);
		}

		public static async Task SendNotebooksListEmbedAsync(IInteractionContext context, int startingIndex, bool showEveryone)
		{
			using DiamondContext db = new DiamondContext();

			try
			{
				_ = await new NotebookListEmbed(context, startingIndex, showEveryone, db).SendAsync();
			}
			catch (NoNotebooksException)
			{
				_ = await new NoNotebooksEmbed(context).SendAsync();
			}
		}

		public static async Task SendPagesListEmbedAsync(IInteractionContext context, long notebookId, int startingIndex, bool showEveryone)
		{
			using DiamondContext db = new DiamondContext();

			_ = await new PageListEmbed(context, notebookId, startingIndex, showEveryone, db).SendAsync();
		}
		#endregion

		#region Db
		public static async Task CreateOrUpdateNotebookAsync(IInteractionContext context, long? notebookId, string notebookName, string notebookDescription, DiamondContext db)
		{
			if (notebookId == 0)
			{
				notebookId = null;
			}

			try
			{
				if (notebookId == null)
				{
					Notebook notebook = await Notebook.CreateNotebookAsync(notebookName, notebookDescription, context.User.Id, db);
					_ = await new NotebookCreatedEmbed(context, notebook).SendAsync();
				}
				else
				{
					Notebook oldNotebook = Notebook.GetNotebook((long)notebookId, db);
					string oldName = oldNotebook.Name;
					string oldDescription = oldNotebook.Description;

					Notebook updatedNotebook = await Notebook.UpdateNotebookAsync((long)notebookId, notebookName, notebookDescription, db);
					_ = await new NotebookUpdatedEmbed(context, updatedNotebook, oldName, oldDescription).SendAsync();
				}
			}
			catch (Exception ex) when (ex is NotebookCreateException or NotebookAlreadyExistsException)
			{
				_ = await new NotebookCreatedEmbed(context, ex).SendAsync();
			}
			catch (NotebookUpdateException ex)
			{
				_ = await new NotebookUpdatedEmbed(context, ex).SendAsync();
			}
		}

		public static async Task CreateOrUpdatePageAsync(IInteractionContext context, long? pageId, long notebookId, string title, string content, DiamondContext db)
		{
			if (pageId == 0)
			{
				pageId = null;
			}

			try
			{
				if (pageId == null)
				{
					NotebookPage page = await NotebookPage.CreateNotebookPageAsync(title, content, context.User.Id, notebookId, db);

					_ = await new PageCreatedEmbed(context, page, page.Notebook).SendAsync();
				}
				else
				{
					NotebookPage oldPage = NotebookPage.GetNotebookPage((long)pageId, db);
					string oldTitle = oldPage.Title;
					string oldContent = oldPage.Content;

					NotebookPage page = await NotebookPage.UpdateNotebookPageAsync((long)pageId, title, content, notebookId, db);
					_ = await new PageUpdatedEmbed(context, page, oldTitle, oldContent, notebookId).SendAsync();
				}
			}
			catch (NotebookPageCreateException ex)
			{
				_ = await new PageCreatedEmbed(context, ex).SendAsync();
			}
			catch (NotebookPageUpdateException ex)
			{
				_ = await new PageUpdatedEmbed(context, ex, (long)pageId, notebookId).SendAsync();
			}
		}

		public static async Task CreateOrUpdatePageAsync(IInteractionContext context, long? pageId, string notebookName, string title, string content, DiamondContext db)
		{
			Notebook notebook = Notebook.GetUserNotebooks(context.User.Id, db).Where(n => n.Name == notebookName).FirstOrDefault();
			if (notebook == null)
			{
				_ = await new PageCreatedEmbed(context, new NotebookNotFoundException(notebookName)).SendAsync();
				return;
			}
			await CreateOrUpdatePageAsync(context, pageId, notebook.Id, title, content, db);
		}
		#endregion
	}
}