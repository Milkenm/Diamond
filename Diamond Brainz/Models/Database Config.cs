﻿using Newtonsoft.Json;

namespace Diamond.Brainz
{
	public static partial class Models
	{
		public class DatabaseConfig
		{
			[JsonProperty("server")] public string Server;
			[JsonProperty("user")] public string User;
			[JsonProperty("password")] public string Password;
			[JsonProperty("database")] public string Database;
		}
	}
}
