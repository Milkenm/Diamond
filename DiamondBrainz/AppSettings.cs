using System;
using System.Data;

using ScriptsLibV2.Databases;
using ScriptsLibV2.Extensions;

namespace Diamond.API
{
    public class AppSettings : DatabaseObject
    {
        public AppSettings(Database db) : base(db, "Settings")
        {
            DataTable select = SQLiteDB.GetInstance().Select("Settings", "ID", $"Setting LIKE 'AppSettings'");

            if (select.Rows.Count > 0)
            {
                LoadFromDatabase((long)select.Rows[0]["ID"]);
            }
        }

        public Settings Settings { get; private set; } = new Settings();

        [Getter("Setting")]
        public string GetSettingName() => "AppSettings";

        [Getter("Value")]
        public byte[] GetValue() => Settings.ToByteArray();

        [Setter("Value", typeof(byte[]))]
        public void SetValue(byte[] value)
        {
            if (value != null && value.Length > 0)
            {
                Settings = value.ToObject<Settings>();
            }
        }
    }

    [Serializable]
    public class Settings
    {
        public string Token { get; set; } = string.Empty;
        public string CacheFolderPath { get; set; } = string.Empty;
        public string OpenaiApiKey { get; set; } = string.Empty;
        public string NightapiApiKey { get; set; } = string.Empty;
    }
}
