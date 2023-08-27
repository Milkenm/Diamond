using System.Threading.Tasks;

using Diamond.API.Helpers;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
	public class ButtonPageDeleteAttribute : DefaultComponentInteractionAttribute
	{
		public override string ComponentLabel => "Delete Page";
		public override IEmote ButtonComponentEmote => Emoji.Parse("🗑️");
		public override ButtonStyle? ButtonComponentStyle => ButtonStyle.Danger;

		public const string COMPONENT_ID = "button_notebookpage_delete:*";

		public ButtonPageDeleteAttribute()
			: base(COMPONENT_ID)
		{ }

		public ButtonPageDeleteAttribute(long pageId)
			: base(COMPONENT_ID, pageId)
		{ }
	}

	public class ButtonPageDeleteHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonPageDelete]
		public async Task OnButtonPageDeleteClickHandlerAsync(long pageId)
		{
			await this.DeferAsync(true);

			_ = await new PageDeleteConfirmEmbed(this.Context, pageId).SendAsync();
		}
	}
}
