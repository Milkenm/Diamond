using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Diamond.Data.Exceptions.SavedNotebookPageExceptions;

namespace Diamond.Data.Models.Notebooks
{
	[Table("SavedNotebookPages")]
	public class SavedNotebookPage
	{
		#region Actions 
		/// <exception cref="SavedNotebookPageCreateException"></exception>
		public static async Task CopyPageAsync(NotebookPage page, string? notebookTitle, ulong userId, DiamondContext db)
		{
			try
			{
				_ = db.SavedNotebookPages.Add(new SavedNotebookPage()
				{
					Title = page.Title,
					Content = page.Content,
					NotebookName = notebookTitle,
					OriginalPageCreatedAt = page.CreatedAt,
					OriginalPageUpdatedAt = page.UpdatedAt,
					OriginalPageId = page.Id,
					CreatedByDiscordUserId = page.DiscordUserId,
					SavedByDiscordUserId = userId,
					CopiedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
				});
				await db.SaveAsync();
			}
			catch (Exception ex)
			{
				throw new SavedNotebookPageCreateException(ex);
			}
		}

		public static Dictionary<string, SavedNotebookPage>? GetSavedNotebookPages(ulong userId, DiamondContext db)
		{
			List<SavedNotebookPage> foundPages = db.SavedNotebookPages.Where(s => s.SavedByDiscordUserId == userId).ToList();
			if (!foundPages.Any())
			{
				return null;
			}

			Dictionary<string, SavedNotebookPage> savedPagesMap = new Dictionary<string, SavedNotebookPage>();
			foreach (SavedNotebookPage? savedPage in foundPages)
			{
				savedPagesMap.Add(savedPage.Title, savedPage);
			}
			return savedPagesMap;
		}

		/// <exception cref="SavedNotebookPageDeleteException"></exception>
		public static async Task DeleteSavedPageAsync(SavedNotebookPage savedPage, DiamondContext db)
		{
			try
			{
				_ = db.SavedNotebookPages.Remove(savedPage);
				await db.SaveAsync();
			}
			catch (Exception ex)
			{
				throw new SavedNotebookPageDeleteException(ex);
			}
		}

		/// <exception cref="SavedNotebookPageNotFoundException"></exception>
		/// <exception cref="SavedNotebookPageDeleteException"></exception>
		public static async Task DeleteSavedPageAsync(long savedPageId, DiamondContext db)
		{
			IQueryable<SavedNotebookPage> foundPages = db.SavedNotebookPages.Where(s => s.Id == savedPageId);
			if (!foundPages.Any())
			{
				throw new SavedNotebookPageNotFoundException(savedPageId);
			}

			await DeleteSavedPageAsync(foundPages.First(), db);
		}
		#endregion

		[Key] public int Id { get; set; }
		public required string Title { get; set; }
		public required string Content { get; set; }
		public string? NotebookName { get; set; }
		public long OriginalPageCreatedAt { get; set; }
		public long? OriginalPageUpdatedAt { get; set; }
		public long OriginalPageId { get; set; }
		public ulong CreatedByDiscordUserId { get; set; }
		public ulong SavedByDiscordUserId { get; set; }
		public long CopiedAt { get; set; }
	}
}
