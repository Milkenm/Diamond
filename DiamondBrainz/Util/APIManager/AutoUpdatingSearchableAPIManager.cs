using Diamond.API.Data;

using ScriptsLibV2;

namespace Diamond.API.Util.APIManager
{
	public abstract class AutoUpdatingSearchableAPIManager<T> : SearchableAPIManager<T>
	{
		public event APIManagerStateChanged OnCheckingForUpdate;

		private readonly long _autoupdateDelaySeconds;

		private readonly Timer _timer;

		public AutoUpdatingSearchableAPIManager(ConfigSetting dbUnixConfigSetting, ulong keepResultsForSeconds, string[] tablesName, long autoupdateDelaySeconds)
			: base(dbUnixConfigSetting, keepResultsForSeconds, tablesName)
		{
			this._autoupdateDelaySeconds = autoupdateDelaySeconds;

			this._timer = new Timer(autoupdateDelaySeconds);
			this._timer.OnTick += this.BeginAutoUpdate;
		}

		private async void BeginAutoUpdate(long currentTick)
		{
			if (this.IsLoadingItems) return;

			OnCheckingForUpdate?.Invoke();

			await this.LoadItemsAsync(true);
		}
	}
}
