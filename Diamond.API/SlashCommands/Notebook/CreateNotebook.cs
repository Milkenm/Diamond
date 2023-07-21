using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Helpers;
using Diamond.Data;
using Diamond.Data.Exceptions.NotebookExceptions;
using Diamond.Data.Models.Notebooks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.SCNotebook
{
	public partial class SCNotebook
	{
		[DSlashCommand("create", "Creates a new page on your notebook.")]
		public async Task CreateNotebookCommandAsync(
			[Summary("name", "Sets the name of the notebook.")] string name,
			[Summary("description", "Set a description for the notebook.")] string? description = null
		)
		{
			await this.DeferAsync(true);

			NotebookEmbed embed = new NotebookEmbed(Context);

			using DiamondContext db = new DiamondContext();

			try
			{
				await Notebook.CreateNotebookAsync(name, description, this.Context.User.Id, db);
			}
			catch (NotebookException ex)
			{
				embed.Title = "Error";
				embed.Description = ex.Message;

				_ = await embed.SendAsync();
				return;
			}

			embed.Title = "Notebook created!";
			embed.Description = $"You created a notebook called '**{name}**'.";
			_ = await embed.SendAsync();
		}
	}
}
