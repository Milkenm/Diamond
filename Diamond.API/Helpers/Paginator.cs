using System;
using System.Collections.Generic;
using System.Linq;

namespace Diamond.API.Helpers
{
	public class Paginator<T>
	{
		public IReadOnlyCollection<T> Items { get; private set; }
		public int StartingIndex { get; private set; }
		public int ItemsPerPage { get; private set; }

		public int MaxPages { get; private set; }
		public int CurrentPage { get; private set; }
		public int PrettyMaxPages => this.MaxPages + 1;
		public int PrettyCurrentPage => this.CurrentPage + 1;

		public bool HasPreviousPage => this.CurrentPage > 0;
		public bool HasNextPage => this.CurrentPage < this.MaxPages;

		public Paginator(IReadOnlyCollection<T> items, int startingIndex, int itemsPerPage)
		{
			this.Items = items;
			this.StartingIndex = startingIndex;
			this.ItemsPerPage = itemsPerPage;

			this.MaxPages = this.CalculateMaxPages();

			if (this.StartingIndex == -1)
			{
				this.StartingIndex = MaxPages * ItemsPerPage; /*(int)Math.Floor((double)this.Items.Count / this.ItemsPerPage);*/
			}

			this.CurrentPage = this.GetPageOfIndex(this.StartingIndex);

		}

		public int CalculateMaxPages()
		{
			int maxPages = (int)Math.Ceiling((double)(this.Items.Count / this.ItemsPerPage));
			if (this.Items.Count > 0 && this.Items.Count % this.ItemsPerPage == 0)
			{
				maxPages--;
			}
			return maxPages;
		}

		public IEnumerable<T> GetItemsFromPage(int page)
		{
			if (page < 0 || page > this.PrettyMaxPages)
			{
				throw new ArgumentOutOfRangeException($"'{nameof(page)}' must range from 0 to {this.PrettyMaxPages}, but was {page}.");
			}

			this.CurrentPage = page;

			return this.Items.Skip(page * this.ItemsPerPage).Take(this.ItemsPerPage).ToList();
		}

		public int GetPageOfIndex(int index)
		{
			return (int)Math.Floor((double)(index / this.ItemsPerPage));
		}
	}
}
