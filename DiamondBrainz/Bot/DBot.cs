using ScriptsLibV2;

using ScriptsLibV2.Extensions;

namespace Diamond.API.Bot
{
	public class DBot : DiscordBot
	{
		// Load config from database
		private readonly BotSetting TokenSetting;

		public DBot(Database db)
		{
			TokenSetting = new BotSetting(db);
			TokenSetting.LoadConfig("Token");

			// Set bot token
			byte[] tokenBytes = TokenSetting.GetValue();
			if (tokenBytes != null)
			{
				LogLevel = Discord.LogSeverity.Info;
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
	}
}