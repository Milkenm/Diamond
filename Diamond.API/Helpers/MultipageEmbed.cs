using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Diamond.API.SlashCommands;
using Diamond.API.Util;

using Discord;

namespace Diamond.API.Helpers
{
	public abstract class MultipageEmbed<T> : DefaultEmbed
	{
		public bool ShowEveryone { get; private set; }

		public IReadOnlyCollection<T> ItemsList => this._paginator.Items;
		public int StartingIndex => this._paginator.StartingIndex;
		public int ItemsPerPage => this._paginator.ItemsPerPage;

		private readonly ComponentBuilder _components = new ComponentBuilder();
		private readonly Paginator<T> _paginator;
		private readonly Dictionary<MultipageButton, List<object>> _buttonsDataMap = new Dictionary<MultipageButton, List<object>>();

		public MultipageEmbed(string title, string emoji, IInteractionContext context, List<T> itemsList, int startingIndex, int itemsPerPage,  bool showEveryone)
			: base(title, emoji, context)
		{
			this.ShowEveryone = showEveryone;

			this._paginator = new Paginator<T>(itemsList, startingIndex, itemsPerPage);

			this.AddDataToButton(this.StartingIndex, MultipageButton.Previous, MultipageButton.Next);
		}

		public void AddNavigationButtons(int row)
		{
			if (this.ShowEveryone)
			{
				return;
			}

			this.AddDataToAllButtons(this.ShowEveryone);

			_ = this._components
				.WithButton(customId: $"{MultipageEmbedIds.BUTTON_MULTIPAGE_FIRST}{this.GetButtonData(MultipageButton.First)}", style: ButtonStyle.Secondary, emote: Emoji.Parse("⏪"), disabled: !this._paginator.HasPreviousPage, row: row)
				.WithButton(customId: $"{MultipageEmbedIds.BUTTON_MULTIPAGE_PREVIOUS}{this.GetButtonData(MultipageButton.Previous)}", style: ButtonStyle.Secondary, emote: Emoji.Parse("◀️"), disabled: !this._paginator.HasPreviousPage, row: row)
				.WithButton($"{this._paginator.PrettyCurrentPage} / {this._paginator.PrettyMaxPages}", MultipageEmbedIds.BUTTON_MULTIPAGE_PAGE, ButtonStyle.Secondary, disabled: true, row: row)
				.WithButton(customId: $"{MultipageEmbedIds.BUTTON_MULTIPAGE_NEXT}{this.GetButtonData(MultipageButton.Next)}", style: ButtonStyle.Secondary, emote: Emoji.Parse("▶️"), disabled: !this._paginator.HasNextPage, row: row)
				.WithButton(customId: $"{MultipageEmbedIds.BUTTON_MULTIPAGE_LAST}{this.GetButtonData(MultipageButton.Last)}", style: ButtonStyle.Secondary, emote: Emoji.Parse("⏩"), disabled: !this._paginator.HasNextPage, row: row);
		}

		private string GetButtonData(MultipageButton button)
		{
			StringBuilder dataSb = new StringBuilder();

			if (!this._buttonsDataMap.ContainsKey(MultipageButton.First))
			{
				return "";
			}

			foreach (object data in this._buttonsDataMap[button])
			{
				_ = dataSb.Append(data.ToString(), ",");
			}
			return dataSb.Preappend(":").ToString();
		}

		public ComponentBuilder AddButton(ButtonBuilder buttonBuilder, int row)
		{
			return this._components.WithButton(buttonBuilder, row);
		}

		public ComponentBuilder AddSelectMenu(SelectMenuBuilder selectMenuBuilder, int row)
		{
			return this._components.WithSelectMenu(selectMenuBuilder, row);
		}

		public void AddDataToButton(object data, params MultipageButton[] buttons)
		{
			foreach (MultipageButton button in buttons)
			{
				if (!this._buttonsDataMap.ContainsKey(button))
				{
					this._buttonsDataMap.Add(button, new List<object>());
				}

				this._buttonsDataMap[button].Add(data);
			}
		}

		public void AddDataToAllButtons(object data)
		{
			this.AddDataToButton(data, (MultipageButton[])Enum.GetValues(typeof(MultipageButton)));
		}

		public async Task SendAsync()
		{
			this.FillItems(this.ItemsList.Skip(this.StartingIndex).Take(this.ItemsPerPage));

			/*// Fix the component's rows
			ComponentBuilder fixedComponents = new ComponentBuilder();
			foreach (ActionRowBuilder row in this._components.ActionRows)
			{
				_ = fixedComponents.AddRow(new ActionRowBuilder());
				foreach (IMessageComponent component in row.Components)
				{
					_ = fixedComponents.ActionRows[fixedComponents.ActionRows.Count - 1].AddComponent(component);
				}
			}*/
			this.Component = this._components.Build();

			_ = await base.SendAsync(component: this.Component);
		}

		protected abstract void FillItems(IEnumerable<T> itemsList);
	}

	public enum MultipageButton
	{
		All,
		First,
		Previous,
		Next,
		Last,
	}
}
