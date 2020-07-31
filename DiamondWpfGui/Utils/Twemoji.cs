using System.Web;

namespace Diamond.WPF.Utils
{
    public static class Twemoji
    {
        private const string baseUrl = "https://raw.githubusercontent.com/twitter/twemoji/master/assets/72x72/{0}.png";

        public static string GetEmojiUrlFromCode(string emojiCode)
        {
            return string.Format(baseUrl, emojiCode);
        }

        public static string GetEmojiUrlFromEmoji(string emoji)
        {
            return GetEmojiUrlFromCode(GetEmojiCode(emoji));
        }

        public static string GetEmojiCode(string emoji)
        {
            string decimalEncoded = HttpUtility.HtmlEncode(emoji);
            int dec = int.Parse(decimalEncoded.Substring(2, decimalEncoded.Length - 3));
            return string.Format("{0:X}", dec).ToLower();
        }
    }
}
