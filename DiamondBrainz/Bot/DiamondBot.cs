using System.Threading.Tasks;

using Discord;

using ScriptsLibV2;
using ScriptsLibV2.Extensions;

namespace Diamond.API.Bot
{
	public class DiamondBot : DiscordBot
	{
		private readonly AppSettings _appSettings;
		private readonly AppFolder _appFolder;

		public DiamondBot(AppSettings appSettings, AppFolder appFolder)
		{
			_appSettings = appSettings;
			_appFolder = appFolder;

			LogLevel = LogSeverity.Info;

			RefreshSettings(false).Wait();
			Initialize();
		}

		public async Task RefreshSettings(bool saveToDatabase = true)
		{
			Token = _appSettings.Settings.Token;
			if (base.IsRunning)
			{
				await base.RestartAsync();
			}

			if (!_appSettings.Settings.CacheFolderPath.IsEmpty())
			{
				_appFolder.Path = _appSettings.Settings.CacheFolderPath;
				_appFolder.CreateFolder();
			}

			if (saveToDatabase)
			{
				_appSettings.SaveToDatabase();
			}
		}
	}
}