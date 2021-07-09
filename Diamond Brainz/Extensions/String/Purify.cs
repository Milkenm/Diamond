namespace Diamond.Brainz
{
	public static partial class Extensions
	{
		public static string Purify(this string text)
		{
			return text.Replace("*", @"\*").Replace("_", @"\_").Replace("~", @"\~").Replace("`", @"\`");
		}
	}
}
