using Discord;

namespace Diamond.API
{
	public static partial class Extensions
	{
		public static IEmote ToEmote(this string emote)
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
