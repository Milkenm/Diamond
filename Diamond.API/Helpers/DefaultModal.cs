using System.Threading.Tasks;

using Diamond.API.SlashCommands;

using Discord;
using Discord.Interactions;

namespace Diamond.API.Helpers
{
	public class DefaultModal : IModal
	{
		public string Title { get; private set; }

		private readonly IInteractionContext _context;

		public DefaultModal() { }

		public DefaultModal(string title, IInteractionContext context)
		{
			this._context = context;

			this.Title = title;
		}

		public async Task SendAsync()
		{
			await this._context.Interaction.RespondWithModalAsync(NotebookComponentIds.MODAL_NOTEBOOK_EDIT_PAGE, this);
		}
	}
}
