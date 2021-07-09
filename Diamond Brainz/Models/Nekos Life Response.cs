using Newtonsoft.Json;

namespace Diamond.Brainz
{
	public static partial class Models
	{
		public class NekosLifeResponse
		{
			[JsonProperty("url")] public string URL { get; set; }
		}
	}
}
