using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Diamond.API.Util;

using Discord;

using ScriptsLibV2.Extensions;
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
		private readonly MultipageButtons _buttons;

		public MultipageEmbed(IInteractionContext context, MultipageButtons buttons, bool showEveryone)
			: base(context)
		{
			this._buttons = buttons;
			this.ShowEveryone = showEveryone;
		}

		public MultipageEmbed(IInteractionContext context, MultipageButtons buttons, string title, string emoji, List<T> itemsList, int startingIndex, int itemsPerPage, bool showEveryone)
			: base(title, emoji, context)
		{
			this._buttons = buttons;
			this.ShowEveryone = showEveryone;

			this.SetItemsList(itemsList, startingIndex, itemsPerPage);
		}

		public void SetItemsList(List<T> itemsList, int startingIndex, int itemsPerPage)
		{
			this._paginator = new Paginator<T>(itemsList, startingIndex, itemsPerPage);
		}

		public void AddNavigationButtons(int row)
		{
			if (this.ShowEveryone)
			{
				return;
			}

			this.AddDataToButton(this.StartingIndex, MultipageButton.Previous, MultipageButton.Next);

			_ = this.AddButton(customId: $"{this._buttons.ButtonFirst}{this.GetButtonData(MultipageButton.First)}", style: ButtonStyle.Secondary, emote: Emoji.Parse("⏪"), isDisabled: !this._paginator.HasPreviousPage, row: row)
				.AddButton(customId: $"{this._buttons.ButtonPrevious}{this.GetButtonData(MultipageButton.Previous)}", style: ButtonStyle.Secondary, emote: Emoji.Parse("◀️"), isDisabled: !this._paginator.HasPreviousPage, row: row)
				.AddButton($"{this._paginator.PrettyCurrentPage} / {this._paginator.PrettyMaxPages}", this._buttons.ButtonPage, ButtonStyle.Secondary, isDisabled: true, row: row)
				.AddButton(customId: $"{this._buttons.ButtonNext}{this.GetButtonData(MultipageButton.Next)}", style: ButtonStyle.Secondary, emote: Emoji.Parse("▶️"), isDisabled: !this._paginator.HasNextPage, row: row)
				.AddButton(customId: $"{this._buttons.ButtonLast}{this.GetButtonData(MultipageButton.Last)}", style: ButtonStyle.Secondary, emote: Emoji.Parse("⏩"), isDisabled: !this._paginator.HasNextPage, row: row);
		}

		private string GetButtonData(MultipageButton button)
		{
			StringBuilder dataSb = new StringBuilder();

			if (!this._buttonsDataMap.ContainsKey(button))
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
			return await base.SendAsync();
		}

		protected abstract void FillItems(IEnumerable<T> itemsList);
	}

	public class MultipageButtons
	{
		public MultipageButtons() { }

		public MultipageButtons(string buttonFirst, string buttonPrevious, string buttonPage, string buttonNext, string buttonLast)
		{
			this.ButtonFirst = buttonFirst;
			this.ButtonPrevious = buttonPrevious;
			this.ButtonPage = buttonPage;
			this.ButtonNext = buttonNext;
			this.ButtonLast = buttonLast;
		}

		private static string GetValue(string value)
		{
			return value.IsEmpty() ? throw new NullReferenceException() : value;
		}

		private static string SetValue(string value)
		{
			if (value.IsEmpty())
			{
				throw new NullReferenceException();
			}
			return value.Split(":")[0];
		}

		private string _buttonFirst;
		public string ButtonFirst
		{
			get => GetValue(this._buttonFirst);
			set => this._buttonFirst = SetValue(value);
		}

		private string _buttonPrevious;
		public string ButtonPrevious
		{
			get => GetValue(this._buttonPrevious);
			set => this._buttonPrevious = SetValue(value);
		}

		private string _buttonPage;
		public string ButtonPage
		{
			get => GetValue(this._buttonPage);
			set => this._buttonPage = SetValue(value);
		}

		private string _buttonNext;
		public string ButtonNext
		{
			get => GetValue(this._buttonNext);
			set => this._buttonNext = SetValue(value);
		}

		private string _buttonLast;
		public string ButtonLast
		{
			get => GetValue(this._buttonLast);
			set => this._buttonLast = SetValue(value);
		}
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
