using Discord;

namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static EmbedAuthorBuilder AuthorBuilder(string name, string iconUrl = null, string url = null)
		{
			EmbedAuthorBuilder author = new EmbedAuthorBuilder();

			author.Name = name;
			author.Url = url;
			author.IconUrl = iconUrl;

			return author;
		}
	}
}