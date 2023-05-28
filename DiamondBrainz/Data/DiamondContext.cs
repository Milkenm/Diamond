﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ScriptsLibV2.Extensions;

using SUtils = ScriptsLibV2.Util.Utils;

namespace Diamond.API.Data
{
	public partial class DiamondContext : DbContext
	{
		public string DatabasePath => Path.Join(SUtils.GetInstallationFolder(), @"\Data\DiamondDB.db");

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			_ = options.UseSqlite($"Data Source={this.DatabasePath}");
			/*options.LogTo((msg) => Debug.WriteLine(msg));*/
		}

		public enum ConfigSetting
		{
			// Settings
			Token,
			OpenAI_API_Key,
			NightAPI_API_Key,
			RiotAPI_Key,
			DebugGuildID,
			DebugChannelsID,
			IgnoreDebugChannels,
			// Random Stuff
			CsgoItemsLoadUnix,
		}

		private static readonly Dictionary<ConfigSetting, string> _settingsList = new Dictionary<ConfigSetting, string>()
	{
		{ ConfigSetting.Token, "Token" },
		{ ConfigSetting.OpenAI_API_Key, "OpenaiApiKey" },
		{ ConfigSetting.NightAPI_API_Key, "NightapiApiKey" },
		{ ConfigSetting.RiotAPI_Key, "RiotApiKey" },
		{ ConfigSetting.DebugGuildID, "DebugGuildId" },
		{ ConfigSetting.DebugChannelsID, "DebugChannelId" },
		{ ConfigSetting.IgnoreDebugChannels, "IgnoreDebugChannels" },
		{ ConfigSetting.CsgoItemsLoadUnix, "CsgoItemsLoadUnix" },
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
#if DEBUG
				if (setting is ConfigSetting.IgnoreDebugChannels or ConfigSetting.CsgoItemsLoadUnix) continue;
#endif

				if (this.GetSetting(setting).IsEmpty())
				{
					return false;
				}
			}
			return true;
		}

		public string? GetSetting(ConfigSetting setting, bool exceptionIfNull = false)
		{
			Setting dbSetting = this.Settings.Where(s => s.Name == GetSettingString(setting)).FirstOrDefault();
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

			string stringValue = value != null ? value.ToString() : string.Empty;
			if (stringValue.IsEmpty())
			{
				return;
			}

			Setting dbSetting = this.Settings.Where(s => s.Name == settingName).FirstOrDefault();
			if (dbSetting != null)
			{
				dbSetting.Value = stringValue;
			}
			else
			{
				_ = this.Settings.Add(new Setting()
				{
					Name = settingName,
					Value = stringValue,
				});
			}
			_ = await this.SaveChangesAsync();
		}

		public void ClearTable(string tableName)
		{
			_ = this.Database.ExecuteSqlRaw($"DELETE FROM [{tableName}]");
		}
	}
}