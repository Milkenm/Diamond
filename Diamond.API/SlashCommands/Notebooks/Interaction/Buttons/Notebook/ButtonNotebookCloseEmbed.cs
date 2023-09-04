using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ButtonNotebookCloseEmbedAttribute : DefaultComponentInteractionAttribute
	{
		public override string ComponentLabel => "Close";
		public override ButtonStyle? ButtonComponentStyle => ButtonStyle.Secondary;

		public const string COMPONENT_ID = "button_notebook_closeembed";

		public ButtonNotebookCloseEmbedAttribute()
			: base(COMPONENT_ID)
		{ }
	}

	public class ButtonNotebookCloseEmbedHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonNotebookCloseEmbed]
		public async Task OnButtonNotebookCloseEmbedClickHandlerAsync()
		{
			await this.DeferAsync();

			await Utils.DeleteResponseAsync(this.Context);
		}
	}
}
