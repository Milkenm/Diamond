using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Threading.Tasks;

using Diamond.API.SlashCommands;

using Discord;
using Discord.Interactions;

namespace Diamond.API.Helpers
{
	public class MultipageEmbed<T> : DefaultEmbed
	{
		public List<T> ItemsList { get; private set; }
		public int StartingIndex { get; private set; }
		public int ItemsPerPage { get; private set; }
		public int NavigationButtonsRow { get; private set; }

		private readonly ComponentBuilder _components = new ComponentBuilder();
		private readonly Paginator<T> _paginator;

		public MultipageEmbed(string title, string emoji, IInteractionContext context, List<T> itemsList, int staringIndex, int itemsPerPage, int navigationButtonsRow)
			: base(title, emoji, context)
		{
			this.ItemsList = itemsList;
			this.StartingIndex = staringIndex;
			this.ItemsPerPage = itemsPerPage;
			this.NavigationButtonsRow = navigationButtonsRow;

			this._paginator = new Paginator<T>(this.ItemsList, itemsPerPage);

			_ = this._components
				.WithButton(customId: MultipageEmbedIds.BUTTON_MULTIPAGE_FIRST, style: ButtonStyle.Primary, emote: Emote.Parse("⏪"), row: this.NavigationButtonsRow)
				.WithButton(customId: MultipageEmbedIds.BUTTON_MULTIPAGE_PREVIOUS, style: ButtonStyle.Primary, emote: Emote.Parse("◀"), row: this.NavigationButtonsRow)
				.WithButton($"{this._paginator.CurrentPage}/{this._paginator.MaxPages}", customId: MultipageEmbedIds.BUTTON_MULTIPAGE_PAGE, style: ButtonStyle.Primary, disabled: true, row: this.NavigationButtonsRow)
				.WithButton(customId: MultipageEmbedIds.BUTTON_MULTIPAGE_NEXT, style: ButtonStyle.Primary, emote: Emote.Parse("▶"), row: this.NavigationButtonsRow)
				.WithButton(customId: MultipageEmbedIds.BUTTON_MULTIPAGE_LAST, style: ButtonStyle.Primary, emote: Emote.Parse("⏩"), row: this.NavigationButtonsRow);
		}

		public void AddButton(ButtonBuilder buttonBuilder, int row)
		{
			_ = this._components.WithButton(buttonBuilder, row);
		}

		public void AddSelectMenu(SelectMenuBuilder selectMenuBuilder, int row)
		{
			_ = this._components.WithSelectMenu(selectMenuBuilder, row);
		}

		public async Task SendAsync()
		{
			base.Component = this._components.Build();

			_ = await base.SendAsync(component: this.Component);
		}
	}
}
