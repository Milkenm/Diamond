using System.Threading.Tasks;

using Diamond.API.Data;

using Discord;

using ScriptsLibV2;

namespace Diamond.API.Bot
{
	public class DiamondBot : DiscordBot
	{
		private readonly DiamondDatabase _database;

		public DiamondBot(DiamondDatabase database)
		{
			_database = database;

			LogLevel = LogSeverity.Info;

			RefreshSettings().Wait();
			Initialize();
		}

		public async Task RefreshSettings()
		{
			Token = Utils.GetSetting(_database, "Token");

			if (IsRunning)
			{
				await RestartAsync();
			}
		}
	}
}