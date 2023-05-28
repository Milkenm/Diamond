using System.Collections.Generic;

using Newtonsoft.Json;

using ScriptsLibV2.Util;

namespace Diamond.API.APIs
{
	public class MemeAPI
	{
		private const string MEME_API_URL = "https://meme-api.com/gimme";

		public static MemeResponse GetRandomMeme()
		{
			MemeResponse response = RequestUtils.Get<MemeResponse>(MEME_API_URL);
			return response.IsNsfw || response.IsSpoiler ? GetRandomMeme() : response;
		}

		public class MemeResponse
		{
			[JsonProperty("postLink")] public string PostLink { get; set; }
			[JsonProperty("subreddit")] public string Subreddit { get; set; }
			[JsonProperty("title")] public string Title { get; set; }
			[JsonProperty("url")] public string Url { get; set; }
			[JsonProperty("nsfw")] public bool IsNsfw { get; set; }
			[JsonProperty("spoiler")] public bool IsSpoiler { get; set; }
			[JsonProperty("author")] public string Author { get; set; }
			[JsonProperty("ups")] public int Upvotes { get; set; }
			[JsonProperty("preview")] public List<string> PreviewList { get; set; }
		}
	}
}