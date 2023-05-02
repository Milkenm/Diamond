using Diamond.API.Schems.OpenMeteoGeocoding;

using Newtonsoft.Json;

using ScriptsLibV2.Util;

namespace Diamond.API.APIs;
public class OpenMeteoGeocoding
{
	// 0: location name
	private const string GEOCODING_API_URL = "https://geocoding-api.open-meteo.com/v1/search?name={0}&count=1&language=en&format=json";
	private const int CACHE_KEEP_SECONDS = 60 * 60;

	private Cache<Geocoding> _geocodingCache = new Cache<Geocoding>();

	public Geocoding? GeocodeSearch(string location)
	{
		Geocoding geocodingRecord = _geocodingCache.GetCachedValue(location.ToLower());
		if (geocodingRecord == null)
		{
			string requestUrl = string.Format(GEOCODING_API_URL, location);
			string apiResponse = RequestUtils.Get(requestUrl);

			geocodingRecord = JsonConvert.DeserializeObject<Geocoding>(apiResponse);
			_geocodingCache.CacheValue(location.ToLower(), geocodingRecord, CACHE_KEEP_SECONDS);
		}

		if (geocodingRecord.Geolocations == null)
		{
			return null;
		}
		return geocodingRecord;
	}
}
