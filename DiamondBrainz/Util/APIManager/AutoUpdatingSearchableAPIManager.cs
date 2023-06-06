using ScriptsLibV2;

namespace Diamond.API.Util.APIManager
{
	public abstract class AutoUpdatingSearchableAPIManager<T> : SearchableAPIManager<T>
	{
		public event APIManagerStateChanged OnAutoUpdating;

		private readonly long _autoupdateDelaySeconds;

		public AutoUpdatingSearchableAPIManager(long autoupdateDelaySeconds, ulong keepSearchCacheForSeconds) : base(keepSearchCacheForSeconds)
		{
			this._autoupdateDelaySeconds = autoupdateDelaySeconds;

			Timer timer = new Timer(autoupdateDelaySeconds);
			timer.OnTick += this.BeginAutoUpdate;
		}

		private async void BeginAutoUpdate(long currentTick)
		{
			OnAutoUpdating?.Invoke();

			await this.LoadItemsAsync();
		}
	}
}
