using System;
using System.Collections.Generic;
using System.Linq;

using Diamond.API.Data;

using ScriptsLibV2.Extensions;
namespace Diamond.API;
public static class Utils
{
	public enum Setting
	{
		Token,
		OpenAI_API_Key,
		NightAPI_API_Key,
		DebugGuildID,
		DebugChannelID,
	}

	private static Dictionary<Setting, string> _settingsList = new Dictionary<Setting, string>()
	{
		{ Setting.Token, "Token" },
		{ Setting.OpenAI_API_Key, "OpenaiApiKey" },
		{ Setting.NightAPI_API_Key, "NightapiApiKey" },
		{ Setting.DebugGuildID, "DebugGuildId" },
		{ Setting.DebugChannelID, "DebugChannelId" },
	};

	public static string GetSettingString(Setting setting)
	{
		return _settingsList[setting];
	}

	public static bool AreSettingsValid(DiamondDatabase database)
	{
		foreach (string setting in _settingsList.Values)
		{
			if (GetSetting(database, setting).IsEmpty())
			{
				return false;
			}
		}
		return true;
	}

	public static string? GetSetting(DiamondDatabase database, string settingName, bool exceptionIfNull = false)
	{
		Data.Setting setting = database.Settings.Where(s => s.Name == settingName).FirstOrDefault();
		if (setting == null)
		{
			if (exceptionIfNull)
			{
				throw new Exception($"Setting '{settingName}' is not set.");
			}
			return string.Empty;
		}
		return setting.Value;
	}

	public static void SetSetting(DiamondDatabase database, string settingName, string value)
	{
		if (value.IsEmpty()) return;

		Data.Setting setting = database.Settings.Where(s => s.Name == settingName).FirstOrDefault();
		if (setting != null)
		{
			setting.Value = value;
		}
		else
		{
			database.Settings.Add(new Data.Setting()
			{
				Name = settingName,
				Value = value,
			});
		}
		database.SaveChanges();
	}
}
