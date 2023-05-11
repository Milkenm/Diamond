using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using ScriptsLibV2.Extensions;

namespace Diamond.API.Data;
public partial class DiamondDatabase : IDatabaseContext
{
	public DiamondDatabase() : base(Path.Join(ScriptsLibV2.Util.Utils.GetInstallationFolder(), @"\Data\DiamondDB.db")) { }

	public enum ConfigSetting
	{
		Token,
		OpenAI_API_Key,
		NightAPI_API_Key,
		DebugGuildID,
		DebugChannelID,
	}

	private static readonly Dictionary<ConfigSetting, string> _settingsList = new Dictionary<ConfigSetting, string>()
	{
		{ ConfigSetting.Token, "Token" },
		{ ConfigSetting.OpenAI_API_Key, "OpenaiApiKey" },
		{ ConfigSetting.NightAPI_API_Key, "NightapiApiKey" },
		{ ConfigSetting.DebugGuildID, "DebugGuildId" },
		{ ConfigSetting.DebugChannelID, "DebugChannelId" },
	};

	public static string GetSettingString(ConfigSetting setting) => _settingsList[setting];

	public bool AreSettingsValid()
	{
		foreach (string setting in _settingsList.Values)
		{
			if (this.GetSetting(setting).IsEmpty())
			{
				return false;
			}
		}
		return true;
	}

	public string? GetSetting(string settingName, bool exceptionIfNull = false)
	{
		Setting setting = this.Settings.Where(s => s.Name == settingName).FirstOrDefault();
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

	public string? GetSetting(ConfigSetting setting, bool exceptionIfNull = false) => this.GetSetting(GetSettingString(setting), exceptionIfNull);

	public void SetSetting(string settingName, string value)
	{
		if (value.IsEmpty())
		{
			return;
		}

		Setting setting = this.Settings.Where(s => s.Name == settingName).FirstOrDefault();
		if (setting != null)
		{
			setting.Value = value;
		}
		else
		{
			this.Settings.Add(new Setting()
			{
				Name = settingName,
				Value = value,
			});
		}
		this.SaveChanges();
	}

	public void SetSetting(ConfigSetting setting, string value) => this.SetSetting(GetSettingString(setting), value);
}
