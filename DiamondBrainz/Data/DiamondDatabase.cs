using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using ScriptsLibV2.Extensions;

using SUtils = ScriptsLibV2.Util.Utils;

namespace Diamond.API.Data;
public partial class DiamondDatabase : IDatabaseContext
{
	public DiamondDatabase() : base(Path.Join(SUtils.GetInstallationFolder(), @"\Data\DiamondDB.db")) { }

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

	public static string GetSettingString(ConfigSetting setting) => _settingsList[setting];

	public bool AreSettingsValid()
	{
		foreach (ConfigSetting setting in _settingsList.Keys)
		{
			if (SUtils.IsDebugEnabled() && setting == ConfigSetting.IgnoreDebugChannels) continue;

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
			if (exceptionIfNull)
			{
				throw new Exception($"Setting '{dbSetting}' is not set.");
			}
			return null;
		}
		// Valid
		return dbSetting.Value;
	}

	public string GetSetting(ConfigSetting setting, string defaultValue)
	{
		return this.GetSetting(setting) ?? defaultValue;
	}

	public void SetSetting(ConfigSetting setting, object value)
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
			this.Settings.Add(new Setting()
			{
				Name = settingName,
				Value = stringValue,
			});
		}
		this.SaveChanges();
	}

	public void ClearTable(string tableName)
	{
		this.Database.ExecuteSqlRaw($"DELETE FROM [{tableName}]");
	}
}
