using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.API.SlashCommands.Notebooks.Exceptions;
using Diamond.Data;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ButtonNotebookGotoListAttribute : DefaultComponentInteractionAttribute
	{
		public override string ComponentLabel => "Go Back";

		public const string COMPONENT_ID = "button_notebook_goto_list";

		public ButtonNotebookGotoListAttribute()
			: base(COMPONENT_ID)
		{ }
	}

	public class ButtonNotebookGotoListHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonNotebookGotoList]
		public async Task OnButtonNotebookGotoListClickHandlerAsync()
		{
			await this.DeferAsync(true);

			using DiamondContext db = new DiamondContext();

			try
			{
				_ = await new NotebookListEmbed(this.Context, 0, false, db).SendAsync();
			}
			catch (NoNotebooksException)
			{
				_ = await new NoNotebooksEmbed(this.Context).SendAsync();
			}
		}
	}
}
