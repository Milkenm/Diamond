using Diamond.Data.Enums;
using Diamond.Data.Models.Settings;

using Microsoft.EntityFrameworkCore;

using ScriptsLibV2.Extensions;

namespace Diamond.Data
{
	public partial class DiamondContext : DbContext
	{
		private const string CONNECTION_STRING = "Server=localhost; User ID=Diamond; Password=diamond; Database=diamond";

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			_ = optionsBuilder.UseMySql(CONNECTION_STRING, ServerVersion.AutoDetect(CONNECTION_STRING));
		}

		private static readonly Dictionary<ConfigSetting, string> _settingsList = new Dictionary<ConfigSetting, string>()
		{
			{ ConfigSetting.Token, "Token" },
			{ ConfigSetting.DevToken, "DevToken" },
			{ ConfigSetting.OpenAI_API_Key, "OpenaiApiKey" },
			{ ConfigSetting.NightAPI_API_Key, "NightapiApiKey" },
			{ ConfigSetting.RiotAPI_Key, "RiotApiKey" },
			{ ConfigSetting.DebugGuildID, "DebugGuildId" },
			{ ConfigSetting.DebugChannelsID, "DebugChannelId" },
			{ ConfigSetting.IgnoreDebugChannels, "IgnoreDebugChannels" },
			{ ConfigSetting.CsgoItemsLoadUnix, "CsgoItemsLoadUnix" },
			{ ConfigSetting.PokemonsListLoadUnix, "PokemonsListLoadUnix" },
			{ ConfigSetting.TotalUptime, "TotalUptime" },
			{ ConfigSetting.ActivityType, "ActivityType" },
			{ ConfigSetting.ActivityText, "ActivityText" },
			{ ConfigSetting.ActivityStreamURL, "ActivityStreamURL" },
		};
		/// <summary>
		/// Settings that are ignored when validating if all settings are set (for example: when starting the program).
		/// </summary>
		private static readonly List<ConfigSetting> _settingsToIgnoreInValidation = new List<ConfigSetting>()
		{
			// Random stuff
#if DEBUG
			ConfigSetting.Token,
			ConfigSetting.IgnoreDebugChannels,
#elif RELEASE
			ConfigSetting.DevToken,
#endif
			ConfigSetting.CsgoItemsLoadUnix,
			ConfigSetting.PokemonsListLoadUnix,
			ConfigSetting.TotalUptime,
			// Activity
			ConfigSetting.ActivityType,
			ConfigSetting.ActivityText,
			ConfigSetting.ActivityStreamURL,
		};

		private bool _isSaving = false;

		public async Task SaveAsync()
		{
			_ = await Task.Run(async () =>
			{
				if (this._isSaving)
				{
					await Task.Delay(1000);
					return this.SaveAsync();
				}
				this._isSaving = true;
				_ = await base.SaveChangesAsync();
				this._isSaving = false;
				return Task.CompletedTask;
			});
		}

		public static string GetSettingString(ConfigSetting setting) => _settingsList[setting];

		public bool AreSettingsValid()
		{
			foreach (ConfigSetting setting in _settingsList.Keys)
			{
				if (_settingsToIgnoreInValidation.Contains(setting)) continue;
				if (this.GetSetting(setting).IsEmpty())
				{
					return false;
				}
			}
			return true;
		}

		public string? GetSetting(ConfigSetting setting, bool exceptionIfNull = false)
		{
			DbSetting dbSetting = this.Settings.Where(s => s.Name == GetSettingString(setting)).FirstOrDefault();
			// Setting not set
			if (dbSetting == null)
			{
				return exceptionIfNull ? throw new Exception($"Setting '{dbSetting}' is not set.") : null;
			}
			// Valid
			return dbSetting.Value;
		}

		public string GetSetting(ConfigSetting setting, string defaultValue)
		{
			return this.GetSetting(setting) ?? defaultValue;
		}

		public async Task SetSettingAsync(ConfigSetting setting, object value)
		{
			string settingName = GetSettingString(setting);
			string? stringValue = value != null ? value.ToString() : string.Empty;
			if (stringValue.IsEmpty())
			{
				return;
			}
			DbSetting dbSetting = this.Settings.Where(s => s.Name == settingName).FirstOrDefault();
			if (dbSetting != null)
			{
				dbSetting.Value = stringValue;
			}
			else
			{
				_ = this.Settings.Add(new DbSetting()
				{
					Name = settingName,
					Value = stringValue,
				});
			}
			_ = await this.SaveChangesAsync();
		}

		public void ClearTable(string tableName)
		{
			_ = this.Database.ExecuteSqlRaw($"SET FOREIGN_KEY_CHECKS = 0; TRUNCATE TABLE {tableName}; SET FOREIGN_KEY_CHECKS = 1;");
		}
	}
}
