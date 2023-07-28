using System.Threading.Tasks;

using Discord;
using Discord.Interactions;

namespace Diamond.API.Helpers
{
	public class DefaultModal : IModal
	{
		public string Title { get; private set; }
		public string ModalId { get; private set; }

		private readonly IInteractionContext _context;

		public DefaultModal() { }

		public DefaultModal(string title, string modalId, IInteractionContext context)
		{
			this._context = context;

			this.Title = title;
			this.ModalId = modalId;
		}

		public async Task SendAsync()
		{
			await this._context.Interaction.RespondWithModalAsync(this.ModalId, this);
		}
	}
}
