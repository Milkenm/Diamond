using ScriptsLibV2;

namespace Diamond.Brainz
{
	public class Bot : DiscordBot
	{
		public Bot(string token)
		{
			base.Token = token;
			base.Initialize();
		}
	}
}
