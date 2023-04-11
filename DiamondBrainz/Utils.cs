using System;
using System.Linq;

using Diamond.API.Data;

namespace Diamond.API;
public static class Utils
{
	public static object? GetSetting(DiamondDatabase database, string settingName, bool exceptionIfNull = true)
	{
		Setting setting = database.Settings.Where(s => s.Name == settingName).FirstOrDefault();
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

	public static void SetSetting(DiamondDatabase database, string settingName, object value)
	{
		Setting setting = database.Settings.Where(s => s.Name == settingName).FirstOrDefault();
		if (setting != null)
		{
			setting.Value = value;
		}
		else
		{
			database.Settings.Add(new Setting()
			{
				Name = settingName,
				Value = value,
			});
		}
		database.SaveChanges();
	}
}
