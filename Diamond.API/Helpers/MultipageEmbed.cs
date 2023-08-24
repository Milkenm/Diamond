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
		public bool ShowEveryone { get; set; }

		public IReadOnlyCollection<T> ItemsList => this._paginator.Items;
		public int StartingIndex => this._paginator.StartingIndex;
		public int ItemsPerPage => this._paginator.ItemsPerPage;

		private Paginator<T> _paginator;
		private readonly Dictionary<MultipageButton, List<object>> _buttonsDataMap = new Dictionary<MultipageButton, List<object>>();

		public MultipageEmbed(IInteractionContext context, bool showEveryone)
			: base(context)
		{
			this.ShowEveryone = showEveryone;
		}

		public MultipageEmbed(string title, string emoji, IInteractionContext context, List<T> itemsList, int startingIndex, int itemsPerPage, bool showEveryone)
			: base(title, emoji, context)
		{
			this.ShowEveryone = showEveryone;

			this.SetItemsList(itemsList, startingIndex, itemsPerPage);
		}

		public void SetItemsList(List<T> itemsList, int startingIndex, int itemsPerPage)
		{
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

			_ = this.AddButton(customId: $"{MultipageEmbedIds.BUTTON_MULTIPAGE_FIRST}{this.GetButtonData(MultipageButton.First)}", style: ButtonStyle.Secondary, emote: Emoji.Parse("⏪"), isDisabled: !this._paginator.HasPreviousPage, row: row)
				.AddButton(customId: $"{MultipageEmbedIds.BUTTON_MULTIPAGE_PREVIOUS}{this.GetButtonData(MultipageButton.Previous)}", style: ButtonStyle.Secondary, emote: Emoji.Parse("◀️"), isDisabled: !this._paginator.HasPreviousPage, row: row)
				.AddButton($"{this._paginator.PrettyCurrentPage} / {this._paginator.PrettyMaxPages}", MultipageEmbedIds.BUTTON_MULTIPAGE_PAGE, ButtonStyle.Secondary, isDisabled: true, row: row)
				.AddButton(customId: $"{MultipageEmbedIds.BUTTON_MULTIPAGE_NEXT}{this.GetButtonData(MultipageButton.Next)}", style: ButtonStyle.Secondary, emote: Emoji.Parse("▶️"), isDisabled: !this._paginator.HasNextPage, row: row)
				.AddButton(customId: $"{MultipageEmbedIds.BUTTON_MULTIPAGE_LAST}{this.GetButtonData(MultipageButton.Last)}", style: ButtonStyle.Secondary, emote: Emoji.Parse("⏩"), isDisabled: !this._paginator.HasNextPage, row: row);
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

		public async Task<ulong> SendAsync()
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
			//this.Component = this._components.Build();

			return await base.SendAsync(/*component: this.Component*/);
		}

		protected abstract void FillItems(IEnumerable<T> itemsList);
	}

	public class MultipageEmbedIds
	{
		private const string BUTTON_MULTIPAGE_BASE = $"{ComponentIds.BUTTON_BASE}_multipageembed";
		public const string BUTTON_MULTIPAGE_PAGE = $"{BUTTON_MULTIPAGE_BASE}_page";
		public const string BUTTON_MULTIPAGE_FIRST = $"{BUTTON_MULTIPAGE_BASE}_first";
		public const string BUTTON_MULTIPAGE_PREVIOUS = $"{BUTTON_MULTIPAGE_BASE}_previous";
		public const string BUTTON_MULTIPAGE_NEXT = $"{BUTTON_MULTIPAGE_BASE}_next";
		public const string BUTTON_MULTIPAGE_LAST = $"{BUTTON_MULTIPAGE_BASE}_last";
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
