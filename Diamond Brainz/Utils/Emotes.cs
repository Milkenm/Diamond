using Discord;

namespace Diamond.Brainz.Utils
{
	public static class Emotes
	{
		public static IEmote ParseEmote(string emote)
		{
			IEmote parsedEmote;

			try
			{
				parsedEmote = Emote.Parse(emote);
			}
			catch
			{
				parsedEmote = new Emoji(emote);
			}

			return parsedEmote;
		}
	}
}
