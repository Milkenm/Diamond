using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

using Diamond.Data.Exceptions.NotebookExceptions;

using ScriptsLibV2.Extensions;

namespace Diamond.Data.Models.Notebooks
{
	[Table("Notebooks")]
	public class Notebook
	{
		#region Actions
		/// <exception cref="NotebookAlreadyExistsException"></exception>
		/// <exception cref="NotebookNameIsNullException"></exception>
		/// <exception cref="NotebookCreateException"></exception>
		public static async Task<Notebook> CreateNotebookAsync(string name, string? description, ulong userId, DiamondContext db)
		{
			if (description != null)
			{
				if (description.IsEmpty())
				{
					description = null;
				}
				else
				{
					description = DiamondContext.FormatDiscordInput(description);
				}
			}
			name = DiamondContext.FormatDiscordInput(name);

			List<Notebook> userNotebooks = GetUserNotebooks(userId, db);
			if (userNotebooks.Count == 100)
			{
				throw new NotebookLimitReachedException(100);
			}

			if (name.IsEmpty())
			{
				throw new NotebookNameIsNullException();
			}

			if (userNotebooks.Find(n => n.Name == name) != null)
			{
				throw new NotebookAlreadyExistsException(name);
			}

			try
			{
				Notebook notebook = new Notebook()
				{
					Name = name,
					Description = description,
					DiscordUserId = userId,
					CreatedAt = DateTimeOffset.Now.ToUnixTimeSeconds(),
					UpdatedAt = null,
				};

				_ = db.Notebooks.Add(notebook);
				_ = await db.SaveChangesAsync();

				return notebook;
			}
			catch (Exception ex)
			{
				throw new NotebookCreateException(ex);
			}
		}

		public static Notebook GetNotebook(long notebookId, DiamondContext db)
		{
			IQueryable<Notebook> foundNotebooks = db.Notebooks.Where(n => n.Id == notebookId);

			return !foundNotebooks.Any() ? throw new NotebookNotFoundException(notebookId) : foundNotebooks.First();
		}

		public static List<Notebook> GetUserNotebooks(ulong userId, DiamondContext db)
		{
			IQueryable<Notebook>? userNotebooks = db.Notebooks.Where(n => n.DiscordUserId == userId);

			if (!userNotebooks.Any())
			{
				return new List<Notebook>();
			}

			List<Notebook> notebooksMap = new List<Notebook>();
			foreach (Notebook notebook in userNotebooks)
			{
				notebooksMap.Add(notebook);
			}
			return notebooksMap;
		}

		/// <exception cref="NotebookNameIsNullException"></exception>
		/// <exception cref="NotebookNotChangedException"></exception>
		/// <exception cref="NotebookUpdateException"></exception>
		public static async Task<Notebook> UpdateNotebookAsync(Notebook notebook, string name, string? description, DiamondContext db)
		{
			if (name.IsEmpty())
			{
				throw new NotebookNameIsNullException();
			}

			bool hasChanged = false;
			if (name != null && name != notebook.Name)
			{
				notebook.Name = name;
				hasChanged = true;
			}
			if (description != null && description != notebook.Description)
			{
				notebook.Description = description.Trim().Length == 0 ? null : description;
				hasChanged = true;
			}

			if (!hasChanged)
			{
				throw new NotebookNotChangedException();
			}

			notebook.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			try
			{
				await db.SaveAsync();
			}
			catch (Exception ex)
			{
				throw new NotebookUpdateException(ex);
			}

			return notebook;
		}

		/// <exception cref="NotebookNotFoundException"></exception>
		/// <exception cref="NotebookNotChangedException"></exception>
		/// <exception cref="NotebookUpdateException"></exception>
		public static async Task<Notebook> UpdateNotebookAsync(long notebookId, string name, string? description, DiamondContext db)
		{
			IQueryable<Notebook> foundNotebooks = db.Notebooks.Where(n => n.Id == notebookId);
			return !foundNotebooks.Any()
				? throw new NotebookNotFoundException(notebookId)
				: await UpdateNotebookAsync(foundNotebooks.First(), name, description, db);
		}

		/// <exception cref="NotebookDeleteException"></exception>
		public static async Task DeleteNotebookAsync(Notebook notebook, DiamondContext db)
		{
			try
			{
				// Delete all pages first
				foreach (NotebookPage page in NotebookPage.GetNotebookPages(notebook, db))
				{
					_ = db.NotebookPages.Remove(page);
				}

				_ = db.Notebooks.Remove(notebook);
				await db.SaveAsync();
			}
			catch (Exception ex)
			{
				throw new NotebookDeleteException(ex);
			}
		}

		/// <exception cref="NotebookNotFoundException"></exception>
		/// <exception cref="NotebookDeleteException"></exception>
		public static async Task DeleteNotebookAsync(long notebookId, DiamondContext db)
		{
			IQueryable<Notebook> foundNotebooks = db.Notebooks.Where(n => n.Id == notebookId);
			if (!foundNotebooks.Any())
			{
				throw new NotebookNotFoundException(notebookId);
			}

			await DeleteNotebookAsync(foundNotebooks.First(), db);
		}
		#endregion

		[Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public long Id { get; set; }
		public required string Name { get; set; }
		public string? Description { get; set; }
		public required ulong DiscordUserId { get; set; }
		public required long CreatedAt { get; set; }
		public long? UpdatedAt { get; set; }
	}
}
