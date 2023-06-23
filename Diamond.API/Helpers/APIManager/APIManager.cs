using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Diamond.Data;
using Diamond.Data.Enums;

namespace Diamond.API.Helpers.APIManager
{
	public delegate void APIManagerStateChanged();

	public abstract class APIManager<T>
	{
		public event APIManagerStateChanged OnUpdateInitializing;
		public event APIManagerStateChanged OnUpdateCancelled;
		public event APIManagerStateChanged OnUpdateStart;
		public event APIManagerStateChanged OnUpdateEnd;
		public event APIManagerStateChanged OnUpdateError;

		protected readonly Dictionary<string, T> _itemsMap = new Dictionary<string, T>();

		private readonly ConfigSetting _dbUnixConfigSetting;

		public bool AreItemsLoaded { get; set; } = false;
		public bool IsLoadingItems { get; set; } = false;
		public T ResponseObject { get; private set; }

		public ulong KeepResultsForSeconds { get; private set; }
		public string[] TablesName { get; private set; }

		public APIManager(ConfigSetting dbUnixConfigSetting, ulong keepResultsForSeconds, string[] tablesName)
		{
			this._dbUnixConfigSetting = dbUnixConfigSetting;

			this.KeepResultsForSeconds = keepResultsForSeconds;
			this.TablesName = tablesName;
		}

		public abstract Task<bool> LoadItemsLogicAsync(bool forceUpdate);

		public Task LoadItemsAsync(bool forceUpdate = false)
		{
			return Task.Run(async () =>
			{
				if (this.IsLoadingItems) return;
				this.IsLoadingItems = true;

				OnUpdateInitializing?.Invoke();
				using DiamondContext db = new DiamondContext();

				this.AreItemsLoaded = false;
				ulong currentUnix = (ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

				if (!forceUpdate)
				{
					Debug.WriteLine("Checking last refresh unix...");
					// Check if items need to be updated
					ulong lastUpdate = Convert.ToUInt64(db.GetSetting(_dbUnixConfigSetting));
					if (lastUpdate + this.KeepResultsForSeconds >= currentUnix)
					{
						this.IsLoadingItems = false;
						this.AreItemsLoaded = true;
						OnUpdateCancelled?.Invoke();
						return;
					}
				}
				else
				{
					Debug.WriteLine("Forcing items update...");
				}

				OnUpdateStart?.Invoke();
				Debug.WriteLine("Clearing database...");
				// Clear database and items map
				foreach (string tableName in this.TablesName)
				{
					db.ClearTable(tableName);
				}
				this._itemsMap?.Clear();

				bool success = await this.LoadItemsLogicAsync(forceUpdate);
				if (!success)
				{
					OnUpdateError?.Invoke();
					Debug.WriteLine("Error updating API.");

					this.IsLoadingItems = false;
					this.AreItemsLoaded = false;

					return;
				}

				await db.SetSettingAsync(this._dbUnixConfigSetting, currentUnix);
				Debug.WriteLine("Done!");

				this.IsLoadingItems = false;
				this.AreItemsLoaded = true;
				OnUpdateEnd?.Invoke();
			});
		}

		public abstract void LoadItemsMap(Dictionary<string, T> itemsMap);
	}
}
