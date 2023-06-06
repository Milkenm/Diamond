using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diamond.API.Util.APIManager
{
	public delegate void APIManagerStateChanged();

	public abstract class APIManager<T>
	{
		public event APIManagerStateChanged OnUpdateStart;
		public event APIManagerStateChanged OnUpdateEnd;
		public event APIManagerStateChanged OnUpdateError;

		protected readonly Dictionary<string, T> _itemsMap = new Dictionary<string, T>();

		public bool AreItemsLoaded { get; set; } = false;
		public bool IsLoadingItems { get; set; } = false;
		public T ResponseObject { get; private set; }

		public abstract Task LoadItemsAsync();

		public abstract void LoadItemsMap(Dictionary<string, T> itemsMap);
	}
}
