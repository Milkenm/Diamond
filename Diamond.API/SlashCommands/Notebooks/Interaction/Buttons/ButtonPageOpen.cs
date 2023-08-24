﻿using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.API.SlashCommands.Notebooks.Interaction.Embeds;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction.Buttons
{
	public class ButtonPageOpenAttribute : DefaultComponentInteractionAttribute
	{
		public override ButtonStyle? ButtonComponentStyle => ButtonStyle.Success;

		public const string COMPONENT_ID = "button_notebookpage_open:*,*";

		public ButtonPageOpenAttribute()
			: base(COMPONENT_ID)
		{ }

		public ButtonPageOpenAttribute(long pageId, long notebookId)
			: base(COMPONENT_ID, pageId, notebookId)
		{ }
	}

	public class ButtonPageOpenHandler : InteractionModuleBase<SocketInteractionContext>
	{
		[ButtonPageOpen]
		public async Task OnButtonPageOpenClickHandler(long pageId, long notebookId)
		{
			await this.DeferAsync(true);

			_ = await new PageViewEmbed(this.Context, pageId, notebookId).SendAsync();
		}
	}
}
