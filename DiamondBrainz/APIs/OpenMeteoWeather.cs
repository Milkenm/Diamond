using System;
using System.Diagnostics;

using Diamond.API.Schemes.OpenMeteoGeocoding;
using Diamond.API.Schemes.OpenMeteoWeather;

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

	// {0}: latitude (ex: 1.1);
	// {1}: longitude (ex: 1.1);
	// {2}: forecast days (ex: 16);
	// {3} timezone (ex: auto)
	private const string DAILY_WEATHER_API_URL = "https://api.open-meteo.com/v1/forecast?latitude={0}&longitude={1}&daily=temperature_2m_max,temperature_2m_min,apparent_temperature_max,apparent_temperature_min,precipitation_hours,precipitation_probability_mean,weathercode,sunrise,sunset,windspeed_10m_max,windgusts_10m_max,winddirection_10m_dominant,uv_index_max,uv_index_clear_sky_max&timeformat=unixtime&forecast_days={2}&timezone={3}";
	private const string HOURLY_WEATHER_API_URL = "https://api.open-meteo.com/v1/forecast?latitude={0}&longitude={1}&hourly=temperature_2m,relativehumidity_2m,precipitation,weathercode,cloudcover,windspeed_10m,uv_index&timeformat=unixtime&forecast_days={2}&timezone={3}";
	private const int CACHE_KEEP_SECONDS = 60 * 60;

	private Cache<DailyWeather> _dailyWeatherCache = new Cache<DailyWeather>();
	private Cache<HourlyWeather> _hourlyWeatherCache = new Cache<HourlyWeather>();

	public DailyWeather? GetDailyForecast(string location)
	{
		// Get location
		Geocoding geocoding = _openMeteoGeocoding.GeocodeSearch(location);
		if (geocoding == null)
		{
			return null;
		}
		Geolocation geolocation = geocoding.Geolocations[0];

		return GetDailyForecast(geolocation);
	}

	public DailyWeather GetDailyForecast(Geolocation geolocation)
	{
		DailyWeather weatherRecord = _dailyWeatherCache.GetCachedValue(geolocation.Id.ToString());
		if (weatherRecord == null)
		{
			string requestUrl = string.Format(DAILY_WEATHER_API_URL, geolocation.Latitude.ToString().Replace(",", "."), geolocation.Longitude.ToString().Replace(",", "."), 6, "auto");
			Debug.WriteLine(requestUrl);
			string apiResponse = RequestUtils.Get(requestUrl);

			weatherRecord = JsonConvert.DeserializeObject<DailyWeather>(apiResponse);
			_dailyWeatherCache.CacheValue(geolocation.Id.ToString(), weatherRecord, CACHE_KEEP_SECONDS);
		}

		return weatherRecord;
	}

	public CurrentForecast? GetCurentForecast(string location)
	{
		// Get location
		Geocoding geocoding = _openMeteoGeocoding.GeocodeSearch(location);
		if (geocoding == null)
		{
			return null;
		}
		Geolocation geolocation = geocoding.Geolocations[0];

		return GetCurrentForecast(geolocation);
	}

	public CurrentForecast GetCurrentForecast(Geolocation geolocation)
	{
		HourlyWeather weatherRecord = _hourlyWeatherCache.GetCachedValue(geolocation.Id.ToString());
		if (weatherRecord == null)
		{
			string requestUrl = string.Format(HOURLY_WEATHER_API_URL, geolocation.Latitude.ToString().Replace(",", "."), geolocation.Longitude.ToString().Replace(",", "."), 1, "auto");
			Debug.WriteLine(requestUrl);
			string apiResponse = RequestUtils.Get(requestUrl);

			weatherRecord = JsonConvert.DeserializeObject<HourlyWeather>(apiResponse);
			_hourlyWeatherCache.CacheValue(geolocation.Id.ToString(), weatherRecord, CACHE_KEEP_SECONDS);
		}

		long currentUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

		int? closestIndex = null;
		long? closestTimestampDiff = null;
		for (int i = 0; i < weatherRecord.Forecast.Time.Count - 1; i++)
		{
			long unixDiff = ToPositive(currentUnix - weatherRecord.Forecast.Time[i]);
			if (closestTimestampDiff == null || unixDiff < closestTimestampDiff)
			{
				closestIndex = i;
				closestTimestampDiff = unixDiff;
			}
		}

		CurrentForecast now = new CurrentForecast()
		{
			ForecastUnits = weatherRecord.ForecastUnits,
			Temperature = (int)Math.Round(weatherRecord.Forecast.Temperature[(int)closestIndex], 0),
			Humidity = weatherRecord.Forecast.RelativeHumidity[(int)closestIndex],
			Precipitation = weatherRecord.Forecast.Precipitation[(int)closestIndex],
			WeatherCode = weatherRecord.Forecast.WeatherCode[(int)closestIndex],
			CloudCover = weatherRecord.Forecast.CloudCover[(int)closestIndex],
			WindSpeed = weatherRecord.Forecast.WindSpeed[(int)closestIndex],
			UvIndex = weatherRecord.Forecast.UvIndex[(int)closestIndex],
		};
		return now;
	}

	private static long ToPositive(long number)
	{
		if (number < 0)
		{
			number *= -1;
		}
		return number;
	}

	public class CurrentForecast
	{
		public HourlyUnits ForecastUnits { get; set; }
		public int Temperature { get; set; }
		public int Humidity { get; set; }
		public double Precipitation { get; set; }
		public int WeatherCode { get; set; }
		public int CloudCover { get; set; }
		public double WindSpeed { get; set; }
		public double UvIndex { get; set; }
	}
}
