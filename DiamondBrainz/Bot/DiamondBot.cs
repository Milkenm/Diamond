using Discord;

using ScriptsLibV2;

using ScriptsLibV2.Extensions;

namespace Diamond.API.Bot
{
	public class DiamondBot : DiscordBot
	{
		// Load config from database
		private readonly BotSetting TokenSetting;
		private readonly BotSetting CacheSetting;

		private Folder folder;

		public DiamondBot(Database db)
		{
			TokenSetting = new BotSetting(db);
			TokenSetting.LoadConfig("Token");

			CacheSetting = new BotSetting(db);
			CacheSetting.LoadConfig("CacheFolderPath");

			byte[] pathBytes = CacheSetting.GetValue();
			if (pathBytes != null && !pathBytes.ToObject<string>().IsEmpty())
			{
				folder = new Folder(CacheSetting.GetValue().ToString());
			}

			// Set bot token
			byte[] tokenBytes = TokenSetting.GetValue();
			if (tokenBytes != null)
			{
				LogLevel = LogSeverity.Info;
				Token = tokenBytes.ToObject<string>();
			}

			Initialize();
		}

		public void SetToken(string token)
		{
			Token = token;
			TokenSetting.SetValue(token.ToByteArray());
			TokenSetting.SaveToDatabase();
		}

		public Folder GetCacheFolder() => folder;

		public void SetCacheFolder(string path)
		{
			if (!path.IsEmpty())
			{
				folder = new Folder(path);
			}
		}
	}
}