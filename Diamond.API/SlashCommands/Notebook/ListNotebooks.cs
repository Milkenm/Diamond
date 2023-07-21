using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

namespace Diamond.API.SlashCommands.SCNotebook
{
	public partial class SCNotebook
	{
		[DSlashCommand("list", "List every notebook you have.")]
		public async Task ListNotebooksCommandAsync()
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();

			Dictionary<string, Notebook> userNotebooks = Notebook.GetNotebooksMap(this.Context.User.Id, db);

			NotebookEmbed embed = new NotebookEmbed(this.Context)
			{
				Title = "Notebooks List",
			};

			if (!userNotebooks.Any())
			{
				embed.Description = "You don't have any notebook.";
				_ = await embed.SendAsync();
				return;
			}

			foreach (KeyValuePair<string, Notebook> notebook in userNotebooks)
			{
				_ = embed.AddField(notebook.Value.Name, notebook.Value.Description ?? "*No description*", true);
			}
			_ = await embed.SendAsync();
		}
	}
}
