using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Diamond.Data.Exceptions.NotebookExceptions;
using Diamond.Data.Exceptions.NotebookPageExceptions;

using ScriptsLibV2.Extensions;

namespace Diamond.Data.Models.Notebooks
{
	[Table("NotebookPages")]
	public class NotebookPage
	{
		#region Actions
		/// <exception cref="NotebookPageCreateException"/>
		/// <exception cref="NotebookPageTitleAndContentAreNullException"/>
		/// <exception cref="NotebookPageTitleIsNullException"/>
		/// <exception cref="NotebookPageContentIsNullException"/>
		public static async Task<NotebookPage> CreateNotebookPageAsync(string title, string? content, ulong userId, Notebook? notebook, DiamondContext db)
		{
			if (content != null)
			{
				if (content.IsEmpty())
				{
					content = null;
				}
				else
				{
					content = DiamondContext.FormatDiscordInput(content);
				}
			}
			title = DiamondContext.FormatDiscordInput(title);

			if (GetAllNotebookPages(userId, db).Count == 20 * 100)
			{
				throw new NotebookPageLimitReachedException(20 * 100);
			}

			ValidateNotebookPage(title);

			try
			{
				NotebookPage page = new NotebookPage()
				{
					Title = title,
					Content = content,
					Notebook = notebook,
					DiscordUserId = userId,
					CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
					UpdatedAt = null,
				};

				_ = db.NotebookPages.Add(page);
				await db.SaveAsync();

				return page;
			}
			catch (Exception ex)
			{
				throw new NotebookPageCreateException(ex);
			}
		}

		/// <exception cref="NotebookNotFoundException"/>
		/// <exception cref="NotebookPageCreateException"/>
		/// <exception cref="NotebookPageTitleAndContentAreNullException"/>
		/// <exception cref="NotebookPageTitleIsNullException"/>
		/// <exception cref="NotebookPageContentIsNullException"/>
		public static async Task<NotebookPage> CreateNotebookPageAsync(string title, string? content, ulong userId, long notebookId, DiamondContext db)
		{
			IQueryable<Notebook> foundNotebooks = db.Notebooks.Where(n => n.Id == notebookId && n.DiscordUserId == userId);

			return !foundNotebooks.Any()
				? throw new NotebookNotFoundException(notebookId)
				: await CreateNotebookPageAsync(title, content, userId, foundNotebooks.First(), db);
		}

		/// <exception cref="NotebookNotFoundException"/>
		/// <exception cref="NotebookPageCreateException"/>
		/// <exception cref="NotebookPageTitleAndContentAreNullException"/>
		/// <exception cref="NotebookPageTitleIsNullException"/>
		/// <exception cref="NotebookPageContentIsNullException"/>
		public static async Task<NotebookPage> CreateNotebookPageAsync(string title, string? content, ulong userId, string notebookName, DiamondContext db)
		{
			IQueryable<Notebook> userNotebooks = db.Notebooks.Where(n => n.Name == notebookName && n.DiscordUserId == userId);
			return !userNotebooks.Any()
				? throw new NotebookNotFoundException(notebookName)
				: await CreateNotebookPageAsync(title, content, userId, userNotebooks.First(), db);
		}

		/// <exception cref="NotebookPageCreateException"/>
		/// <exception cref="NotebookPageTitleAndContentAreNullException"/>
		/// <exception cref="NotebookPageTitleIsNullException"/>
		/// <exception cref="NotebookPageContentIsNullException"/>
		public static async Task<NotebookPage> CreateNotebookPageAsync(string title, string? content, ulong userId, DiamondContext db)
		{
			return await CreateNotebookPageAsync(title, content, userId, notebook: null, db);
		}

		public static NotebookPage GetNotebookPage(long pageId, DiamondContext db)
		{
			NotebookPage? foundPage = db.NotebookPages.Where(p => p.Id == pageId).FirstOrDefault();
			return foundPage ?? throw new NotebookPageNotFoundException(pageId);
		}

		public static Dictionary<string, NotebookPage> GetAllNotebookPages(ulong userId, DiamondContext db)
		{
			IQueryable<NotebookPage> foundPages = db.NotebookPages.Where(p => p.DiscordUserId == userId);
			if (!foundPages.Any())
			{
				return new Dictionary<string, NotebookPage>();
			}

			Dictionary<string, NotebookPage> notebookPagesMap = new Dictionary<string, NotebookPage>();
			foreach (NotebookPage page in foundPages)
			{
				notebookPagesMap.Add(page.Title, page);
			}
			return notebookPagesMap;
		}

		/// <exception cref="ArgumentNullException"/>
		public static List<NotebookPage> GetNotebookPages(Notebook? notebook, ulong? userId, DiamondContext db)
		{
			if (notebook == null && userId == null)
			{
				throw new ArgumentNullException($"Both '{nameof(notebook)}' and '{nameof(userId)}' cannot be null.");
			}

			IQueryable<NotebookPage> foundPages = db.NotebookPages.Where(p => (p.Notebook == notebook) || (p.Notebook == null && p.DiscordUserId == userId));
			if (!foundPages.Any())
			{
				return new List<NotebookPage>();
			}

			List<NotebookPage> pagesMap = new List<NotebookPage>();
			foreach (NotebookPage? page in foundPages)
			{
				pagesMap.Add(page);
			}
			return pagesMap;
		}

		public static List<NotebookPage> GetNotebookPages(Notebook notebook, DiamondContext db)
		{
			return GetNotebookPages(notebook, null, db);
		}

		/// <exception cref="NotebookNotFoundException"/>
		public static List<NotebookPage> GetNotebookPages(long? notebookId, ulong userId, DiamondContext db)
		{
			IQueryable<Notebook> foundNotebook = db.Notebooks.Where(n => n.Id == notebookId);
			return !foundNotebook.Any() ? throw new NotebookNotFoundException(notebookId) : GetNotebookPages(foundNotebook.First(), userId, db);
		}

		/// <exception cref="NotebookPageNotChangedException"/>
		/// <exception cref="NotebookPageUpdateException"/>
		/// <exception cref="NotebookPageTitleAndContentAreNullException"/>
		/// <exception cref="NotebookPageTitleIsNullException"/>
		/// <exception cref="NotebookPageContentIsNullException"/>
		public static async Task<NotebookPage> UpdateNotebookPageAsync(NotebookPage page, string title, string content, Notebook? notebook, DiamondContext db)
		{
			try
			{
				ValidateNotebookPage(title);

				bool hasChanged = false;
				if (title != page.Title)
				{
					page.Title = title;
					hasChanged = true;
				}
				if (content != page.Content)
				{
					page.Content = content;
					hasChanged = true;
				}
				if (notebook != page.Notebook)
				{
					page.Notebook = notebook;
					hasChanged = true;
				}

				if (!hasChanged)
				{
					throw new NotebookPageNotChangedException();
				}

				page.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

				await db.SaveAsync();
				return page;
			}
			catch (Exception ex)
			{
				throw new NotebookPageUpdateException(ex);
			}
		}

		/// <exception cref="NotebookPageNotFoundException"/>
		/// <exception cref="NotebookPageNotChangedException"/>
		/// <exception cref="NotebookPageUpdateException"/>
		public static async Task<NotebookPage> UpdateNotebookPageAsync(long pageId, string title, string content, Notebook? notebook, DiamondContext db)
		{
			IQueryable<NotebookPage> foundPages = db.NotebookPages.Where(p => p.Id == pageId);
			return !foundPages.Any()
				? throw new NotebookPageNotFoundException(pageId)
				: await UpdateNotebookPageAsync(foundPages.First(), title, content, notebook, db);
		}

		public static async Task<NotebookPage> UpdateNotebookPageAsync(long pageId, string title, string content, long notebookId, DiamondContext db)
		{
			Notebook? notebook = Notebook.GetNotebook(notebookId, db);
			NotebookPage? page = GetNotebookPage(pageId, db);

			return await UpdateNotebookPageAsync(page, title, content, notebook, db);
		}

		/// <exception cref="NotebookPageDeleteException"/>
		public static async Task DeleteNotebookPageAsync(NotebookPage page, DiamondContext db)
		{
			try
			{
				_ = db.NotebookPages.Remove(page);
				await db.SaveAsync();
			}
			catch (Exception ex)
			{
				throw new NotebookPageDeleteException(ex);
			}
		}

		/// <exception cref="NotebookPageNotFoundException"/>
		/// <exception cref="NotebookPageDeleteException"/>
		public static async Task DeleteNotebookPageAsync(long pageId, DiamondContext db)
		{
			IQueryable<NotebookPage> foundPages = db.NotebookPages.Where(p => p.Id == pageId);
			if (!foundPages.Any())
			{
				throw new NotebookPageNotFoundException(pageId);
			}

			await DeleteNotebookPageAsync(foundPages.First(), db);
		}

		/// <exception cref="NotebookPageTitleAndContentAreNullException"/>
		/// <exception cref="NotebookPageTitleIsNullException"/>
		/// <exception cref="NotebookPageContentIsNullException"/>
		private static void ValidateNotebookPage(string title/*, string content*/)
		{
			bool isTitleEmpty = title.IsEmpty();
			/*bool isContentEmpty = content.IsEmpty();*/
			if (isTitleEmpty /*|| isContentEmpty*/)
			{
				if (isTitleEmpty /*&& isContentEmpty*/)
				{
					throw new NotebookPageTitleAndContentAreNullException();
				}
				else if (isTitleEmpty)
				{
					throw new NotebookPageTitleIsNullException();
				}
				/*else if (isContentEmpty)
				{
					throw new NotebookPageContentIsNullException();
				}*/
			}
		}
		#endregion

		[Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public long Id { get; set; }
		public required string Title { get; set; }
		public string? Content { get; set; }
		public Notebook? Notebook { get; set; }
		public required ulong DiscordUserId { get; set; }
		public required long CreatedAt { get; set; }
		public long? UpdatedAt { get; set; }
	}
}
