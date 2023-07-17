using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Helpers;
using Diamond.Data;
using Diamond.Data.Exceptions.NotebookExceptions;
using Diamond.Data.Models.Notebooks;

using Discord;
using Discord.Interactions;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.SCNotebook
{
	public partial class SCNotebook
	{
		[DSlashCommand("create-page", "Creates a new page on your notebook.")]
		public async Task CreatePageCommandAsync(
			[Summary("title", "Sets the page title.")] string? title = null,
			[Summary("content", "Sets the content of the page.")] string? content = null,
			[Summary("notebook", "Adds the page to the selected notebook.")] string? notebookName = null
		)
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();

			DefaultEmbed embed = new DefaultEmbed("Notebook", "📔", this.Context);

			if (title.IsEmpty() || content.IsEmpty())
			{
				// Show the editor if the title of content is empty
				await this.Context.Interaction.RespondWithModalAsync($"modal_notebooks_page_editor", new NotebookPageEditorModal()
				{
					PageTitle = title,
					PageContent = content,
					Notebook = notebookName,
				});
				return;
			}
			else
			{
				await this.SavePageAsync(title, content, this.Context.User.Id, notebookName, db, embed);
				return;
			}
		}

		private async Task SavePageAsync(string title, string content, ulong userId, string? notebookName, DiamondContext db, DefaultEmbed embed)
		{
			try
			{
				await NotebookPage.CreateNotebookPageAsync(title, content, this.Context.User.Id, notebookName, db);

				embed.Title = "Notebook page created";
				embed.Description = $"Created the page '{title}'{(!notebookName.IsEmpty() ? $" and added it to the '{notebookName}' notebook" : "")}.";
				_ = await embed.SendAsync();
				return;
			}
			catch (NotebookException ex)
			{
				embed.Title = "Error";
				embed.Description = ex.Message;
				_ = await embed.SendAsync();
				return;
			}
		}

		public class NotebookPageEditorModal : IModal
		{
			public string Title => "Create notebook page";

			[RequiredInput]
			[InputLabel("Title")]
			[ModalTextInput("field_title", TextInputStyle.Short, "My new page...", maxLength: 250)]
			public string PageTitle { get; set; }

			[RequiredInput]
			[InputLabel("Content")]
			[ModalTextInput("field_content", TextInputStyle.Paragraph, "My page content...")]
			public string PageContent { get; set; }

			[RequiredInput(false)]
			[InputLabel("Notebook")]
			[ModalTextInput("field_notebook", TextInputStyle.Short, "My notebook...")]
			public string? Notebook { get; set; }
		}

		[ModalInteraction("modal_notebooks_page_editor")]
		public async Task PollAddOptionModalHandler(ulong messageId, NotebookPageEditorModal modal)
		{
			await this.DeferAsync();
			using DiamondContext db = new DiamondContext();

			DefaultEmbed embed = new DefaultEmbed("Notebook", "📔", this.Context);

			await this.SavePageAsync(modal.PageTitle, modal.PageContent, this.Context.User.Id, modal.Notebook, db, embed);
		}
	}
}
