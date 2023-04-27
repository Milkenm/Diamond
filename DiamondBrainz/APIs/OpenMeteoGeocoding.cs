using System;
using System.Collections.Generic;

using Diamond.API.Schems.OpenMeteoGeocoding;

using Newtonsoft.Json;

using ScriptsLibV2.Util;

namespace Diamond.API.APIs;
public class OpenMeteoGeocoding
{
	private const string GEOCODING_API_URL = "https://geocoding-api.open-meteo.com/v1/search?name={0}&count=1&language=en&format=json";
	private const int CACHE_KEEP_SECONDS = 60 * 60;

	private Cache<Geocoding> _geocodingCache = new Cache<Geocoding>();

	private class GeocodingCacheRecord
	{
		public GeocodingCacheRecord()
		{
			CachedAtUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		}

		public List<string> Searches { get; set; }
		public Geocoding GeocodingRecord { get; set; }
		public long CachedAtUnix { get; }
	}

	public Geocoding? GeocodeSearch(string location)
	{
		Geocoding geocodingRecord = _geocodingCache.GetCachedValue(location.ToLower());
		bool fromCache = geocodingRecord != null;
		if (!fromCache)
		{
			string requestUrl = string.Format(GEOCODING_API_URL, location);
			string apiResponse = RequestUtils.Get(requestUrl);

			geocodingRecord = JsonConvert.DeserializeObject<Geocoding>(apiResponse);
			_geocodingCache.CacheValue(location.ToLower(), geocodingRecord, CACHE_KEEP_SECONDS);
		}

		if (geocodingRecord.Results == null)
		{
			return null;
		}
		return geocodingRecord;
	}
}
