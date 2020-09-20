namespace Diamond.Brainz.Utils
{
    public static partial class Text
    {
        public static string Purify(string text)
        {
            return text.Replace("*", @"\*").Replace("_", @"\_").Replace("~", @"\~").Replace("`", @"\`");
        }
    }
}