using System.Collections.Generic;

using Newtonsoft.Json;

namespace Diamond.API.Schems.OpenMeteoWeather
{
	public class DailyWeather
	{
		[JsonProperty("latitude")] public double Latitude { get; set; }
		[JsonProperty("longitude")] public double Longitude { get; set; }
		[JsonProperty("generationtime_ms")] public double GenerationTime { get; set; }
		[JsonProperty("utc_offset_seconds")] public double UtcSecondsOffset { get; set; }
		[JsonProperty("timezone")] public string Timezone { get; set; }
		[JsonProperty("timezone_abbreviation")] public string TimezoneAbbreviation { get; set; }
		[JsonProperty("elevation")] public double Elevation { get; set; }
		[JsonProperty("daily_units")] public DailyUnits ForecastUnits { get; set; }
		[JsonProperty("daily")] public DailyForecast Forecast { get; set; }
	}

	public class DailyUnits
	{
		[JsonProperty("time")] public string Time { get; set; }
		[JsonProperty("temperature_2m_max")] public string MaxTemperature { get; set; }
		[JsonProperty("temperature_2m_min")] public string MinTemperature { get; set; }
		[JsonProperty("apparent_temperature_max")] public string MaxApparentTemperature { get; set; }
		[JsonProperty("apparent_temperature_min")] public string MinApparentTemperature { get; set; }
		[JsonProperty("precipitation_hours")] public string HoursOfPrecipitation { get; set; }
		[JsonProperty("precipitation_probability_mean")] public string MeanPrecipitationProbability { get; set; }
		[JsonProperty("weathercode")] public string WeatherCode { get; set; }
		[JsonProperty("sunrise")] public string SunRise { get; set; }
		[JsonProperty("sunset")] public string SunSet { get; set; }
		[JsonProperty("windspeed_10m_max")] public string MaxWindSpeed { get; set; }
		[JsonProperty("windgusts_10m_max")] public string MaxWindGust { get; set; }
		[JsonProperty("winddirection_10m_dominant")] public string WindDirection { get; set; }
		[JsonProperty("uv_index_max")] public string MaxUvIndex { get; set; }
		[JsonProperty("uv_index_clear_sky_max")] public string MaxUvIndexWithClearSky { get; set; }
	}

	public class DailyForecast
	{
		[JsonProperty("time")] public List<long> Time { get; set; }
		[JsonProperty("temperature_2m_max")] public List<double> MaxTemperature { get; set; }
		[JsonProperty("temperature_2m_min")] public List<double> MinTemperature { get; set; }
		[JsonProperty("apparent_temperature_max")] public List<double> MaxApparentTemperature { get; set; }
		[JsonProperty("apparent_temperature_min")] public List<double> MinApparentTemperature { get; set; }
		[JsonProperty("precipitation_hours")] public List<double> HoursOfPrecipitation { get; set; }
		[JsonProperty("precipitation_probability_mean")] public List<int> MeanPrecipitationProbability { get; set; }
		[JsonProperty("weathercode")] public List<int> WeatherCode { get; set; }
		[JsonProperty("sunrise")] public List<long> SunRise { get; set; }
		[JsonProperty("sunset")] public List<long> SunSet { get; set; }
		[JsonProperty("windspeed_10m_max")] public List<double> MaxWindSpeed { get; set; }
		[JsonProperty("windgusts_10m_max")] public List<double> MaxWindGust { get; set; }
		[JsonProperty("winddirection_10m_dominant")] public List<int> WindDirection { get; set; }
		[JsonProperty("uv_index_max")] public List<double> MaxUvIndex { get; set; }
		[JsonProperty("uv_index_clear_sky_max")] public List<double> MaxUvIndexWithClearSky { get; set; }
	}

	public class HourlyWeather
	{
		[JsonProperty("latitude")] public double Latitude { get; set; }
		[JsonProperty("longitude")] public double Longitude { get; set; }
		[JsonProperty("generationtime_ms")] public double GenerationTime { get; set; }
		[JsonProperty("utc_offset_seconds")] public double UtcSecondsOffset { get; set; }
		[JsonProperty("timezone")] public string Timezone { get; set; }
		[JsonProperty("timezone_abbreviation")] public string TimezoneAbbreviation { get; set; }
		[JsonProperty("elevation")] public double Elevation { get; set; }
		[JsonProperty("hourly_units")] public HourlyUnits ForecastUnits { get; set; }
		[JsonProperty("hourly")] public HourlyForecast Forecast { get; set; }
	}

	public class HourlyUnits
	{
		[JsonProperty("time")] public string Time { get; set; }
		[JsonProperty("temperature_2m")] public string Temperature { get; set; }
		[JsonProperty("relativehumidity_2m")] public string RelativeHumidity { get; set; }
		[JsonProperty("precipitation")] public string Precipitation { get; set; }
		[JsonProperty("weathercode")] public string WeatherCode { get; set; }
		[JsonProperty("cloudcover")] public string CloudCover { get; set; }
		[JsonProperty("windspeed_10m")] public string WindSpeed { get; set; }
		[JsonProperty("uv_index")] public string UvIndex { get; set; }
	}

	public class HourlyForecast
	{
		[JsonProperty("time")] public List<long> Time { get; set; }
		[JsonProperty("temperature_2m")] public List<double> Temperature { get; set; }
		[JsonProperty("relativehumidity_2m")] public List<int> RelativeHumidity { get; set; }
		[JsonProperty("precipitation")] public List<double> Precipitation { get; set; }
		[JsonProperty("weathercode")] public List<int> WeatherCode { get; set; }
		[JsonProperty("cloudcover")] public List<int> CloudCover { get; set; }
		[JsonProperty("windspeed_10m")] public List<double> WindSpeed { get; set; }
		[JsonProperty("uv_index")] public List<double> UvIndex { get; set; }
	}
}