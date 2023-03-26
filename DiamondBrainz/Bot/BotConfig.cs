using System.Data;

using ScriptsLibV2.Databases;

namespace Diamond.API.Bot
{
	public class BotSetting : DatabaseObject
	{
		public void LoadConfig(string settingName)
		{
			DataTable select = Database.GetInstance().Select("Settings", "ID", $"Setting LIKE '{settingName}'");
			if (select.Rows.Count > 0)
			{
				LoadFromDatabase((long)select.Rows[0]["ID"]);
			}
			else
			{
				SetSettingName(settingName);
			}
		}

		public BotSetting(Database db) : base(db, "Settings") { }

		private string SettingName;
		private byte[] Value;

		[Getter("Setting")]
		public string GetSettingName()
		{
			return SettingName;
		}

		[Setter("Setting", typeof(string))]
		public void SetSettingName(string settingName)
		{
			SettingName = settingName;
		}

		[Getter("Value")]
		public byte[] GetValue()
		{
			return Value;
		}

		[Setter("Value", typeof(byte[]))]
		public void SetValue(byte[] value)
		{
			Value = value;
		}
	}
}
