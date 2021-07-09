using Newtonsoft.Json;

using System.IO;

using static Diamond.Brainz.Classes;

namespace Diamond.Brainz
{
	public class Config
	{
		public string ConfigPath { get; private set; }

		public Config(string path)
		{
			this.ConfigPath = path;
			this.ReloadConfig();
		}

		public JsonConfig ReloadConfig()
		{
			string json = File.ReadAllText(this.ConfigPath);
			this.JsonConfig = JsonConvert.DeserializeObject<JsonConfig>(json);

			return this.JsonConfig;
		}

		public JsonConfig JsonConfig { get; private set; }
	}
}
