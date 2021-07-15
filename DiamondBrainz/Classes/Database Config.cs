﻿using Newtonsoft.Json;

namespace Diamond.Brainz.Classes
{
	public class DatabaseConfig
	{
		[JsonProperty("server")] public string Server;
		[JsonProperty("user")] public string User;
		[JsonProperty("password")] public string Password;
		[JsonProperty("database")] public string Database;
	}
}
