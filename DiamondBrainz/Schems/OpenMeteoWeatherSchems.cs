using System.Collections.Generic;

using Newtonsoft.Json;

namespace Diamond.API.Schems.OpenMeteoWeather
{
	public class Weather
	{
		[JsonProperty("latitude")] public double Latitude { get; set; }
		[JsonProperty("longitude")] public double Longitude { get; set; }
		[JsonProperty("generationtime_ms")] public double GenerationTime { get; set; }
		[JsonProperty("utc_offset_seconds")] public double UtcSecondsOffset { get; set; }
		[JsonProperty("timezone")] public string Timezone { get; set; }
		[JsonProperty("timezone_abbreviation")] public string TimezoneAbbreviation { get; set; }
		[JsonProperty("hourly_units")] public ForecastUnits ForecastUnits { get; set; }
		[JsonProperty("hourly")] public Forecast Forecast { get; set; }
	}

	public class ForecastUnits
	{
		[JsonProperty("time")] public string Time { get; set; }
		[JsonProperty("temperature_2m")] public string Temperature { get; set; }
		[JsonProperty("weathercode")] public string WeatherCode { get; set; }
		[JsonProperty("percipitation")] public string Percipitation { get; set; }
		[JsonProperty("cloudcover")] public string CloudCover { get; set; }
		[JsonProperty("windspeed_10m")] public string WindSpeed { get; set; }
	}

	public class Forecast
	{
		[JsonProperty("time")] public List<long> Time { get; set; }
		[JsonProperty("temperature_2m")] public List<double> Temperature { get; set; }
		[JsonProperty("weathercode")] public List<int> WeatherCode { get; set; }
		[JsonProperty("percipitation")] public List<double> Percipitation { get; set; }
		[JsonProperty("cloudcover")] public List<int> CloudCover { get; set; }
		[JsonProperty("windspeed_10m")] public List<double> WindSpeed { get; set; }
	}
}