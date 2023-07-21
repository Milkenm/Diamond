using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
		public static async Task CreateNotebookAsync(string name, string? description, ulong userId, DiamondContext db)
		{
			Dictionary<string, Notebook> userNotebooks = GetNotebooksMap(userId, db);
			if (userNotebooks.Count == 100)
			{
				throw new NotebookLimitReachedException(100);
			}

			if (userNotebooks.ContainsKey(name))
			{
				throw new NotebookAlreadyExistsException(name);
			}

			if (name.IsEmpty())
			{
				throw new NotebookNameIsNullException();
			}

			try
			{
				_ = db.Notebooks.Add(new Notebook()
				{
					Name = name,
					Description = description,
					DiscordUserId = userId,
					CreatedAt = DateTimeOffset.Now.ToUnixTimeSeconds(),
					UpdatedAt = null,
				});
				_ = await db.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new NotebookCreateException(ex);
			}
		}

		public static Dictionary<string, Notebook> GetNotebooksMap(ulong userId, DiamondContext db)
		{
			IQueryable<Notebook>? userNotebooks = db.Notebooks.Where(n => n.DiscordUserId == userId);

			if (!userNotebooks.Any())
			{
				return new Dictionary<string, Notebook>();
			}

			Dictionary<string, Notebook> notebooksMap = new Dictionary<string, Notebook>();
			foreach (Notebook notebook in userNotebooks)
			{
				notebooksMap.Add(notebook.Name, notebook);
			}
			return notebooksMap;
		}

		/// <exception cref="NotebookNameIsNullException"></exception>
		/// <exception cref="NotebookNotChangedException"></exception>
		/// <exception cref="NotebookUpdateException"></exception>
		public static async Task UpdateNotebookAsync(Notebook notebook, string name, string? description, DiamondContext db)
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
		}

		/// <exception cref="NotebookNotFoundException"></exception>
		/// <exception cref="NotebookNotChangedException"></exception>
		/// <exception cref="NotebookUpdateException"></exception>
		public static async Task UpdateNotebookAsync(long notebookId, string name, string? description, DiamondContext db)
		{
			IQueryable<Notebook> foundNotebooks = db.Notebooks.Where(n => n.Id == notebookId);
			if (!foundNotebooks.Any())
			{
				throw new NotebookNotFoundException(notebookId);
			}

			await UpdateNotebookAsync(foundNotebooks.First(), name, description, db);
		}

		/// <exception cref="NotebookDeleteException"></exception>
		public static async Task DeleteNotebookAsync(Notebook notebook, DiamondContext db)
		{
			try
			{
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

		[Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
		public required string Name { get; set; }
		public string? Description { get; set; }
		public required ulong DiscordUserId { get; set; }
		public required long CreatedAt { get; set; }
		public long? UpdatedAt { get; set; }
	}
}
