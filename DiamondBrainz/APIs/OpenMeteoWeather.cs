using System;
using System.Diagnostics;

using Diamond.API.Schems.OpenMeteoGeocoding;
using Diamond.API.Schems.OpenMeteoWeather;

using Newtonsoft.Json;

using ScriptsLibV2.Util;

namespace Diamond.API.APIs;
public class OpenMeteoWeather
{
	private static OpenMeteoGeocoding _openMeteoGeocoding;

	public OpenMeteoWeather(OpenMeteoGeocoding openMeteoGeocoding)
	{
		_openMeteoGeocoding = openMeteoGeocoding;
	}

	// {0}: latitude (ex: 1.1); {1}: longitude (ex: 1.1); {2}: start date (ex: 2023-05-02); {3}: end date (ex: 2023-12-02)
	private const string WEATHER_API_URL = "https://api.open-meteo.com/v1/ecmwf?latitude={0}&longitude={1}&hourly=temperature_2m,weathercode,precipitation,cloudcover,windspeed_10m&timeformat=unixtime&start_date={2}&end_date={3}";
	private const int CACHE_KEEP_SECONDS = 60 * 60;

	private Cache<Weather> _weatherCache = new Cache<Weather>();

	public Weather? WeatherSearch(string location)
	{
		// Get location
		Geocoding geocoding = _openMeteoGeocoding.GeocodeSearch(location);
		if (geocoding == null)
		{
			return null;
		}
		Geolocation geolocation = geocoding.Geolocations[0];

		return WeatherSearch(geolocation);
	}

	public Weather WeatherSearch(Geolocation geolocation)
	{
		Weather weatherRecord = _weatherCache.GetCachedValue(geolocation.Id.ToString());
		if (weatherRecord == null)
		{
			string time = DateTime.Now.ToString("yyyy-MM-dd");
			string request = string.Format(WEATHER_API_URL, geolocation.Latitude.ToString().Replace(",", "."), geolocation.Longitude.ToString().Replace(",", "."), time, time);
			Debug.WriteLine("Request: " + request);
			string requestUrl = request;
			string apiResponse = RequestUtils.Get(requestUrl);

			weatherRecord = JsonConvert.DeserializeObject<Weather>(apiResponse);
			_weatherCache.CacheValue(geolocation.Id.ToString(), weatherRecord, CACHE_KEEP_SECONDS);
		}

		return weatherRecord;
	}
}
