using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Util;
using Diamond.API.Schemes.OpenMeteoGeocoding;
using Diamond.API.Schemes.OpenMeteoWeather;

using Discord;
using Discord.Interactions;

using static Diamond.API.APIs.OpenMeteoWeather;

namespace Diamond.API.SlashCommands.World;
public partial class World
{
	[RequireBotPermission(GuildPermission.Administrator)]
	[DSlashCommand("weather", "View the weather for a certain region.")]
	public async Task WeatherCommandAsync(
		[Summary("location", "The country or city to get information of.")] string location,
		[ShowEveryone] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		DefaultEmbed embed = new DefaultEmbed("World Weather", "🌞", Context);

		// Get the geolocation
		Geocoding geocoding = _openMeteoGeocoding.GeocodeSearch(location);
		if (geocoding == null)
		{
			// If the location was not found
			embed.WithDescription($"**Location '**{location}**' was not found.**");

			await embed.SendAsync();
			return;
		}
		Geolocation geolocation = geocoding.Geolocations[0];

		// Get the weather for the geolocation
		CurrentForecast currentForecast = _openMeteoWeather.GetCurrentForecast(geolocation);
		DailyWeather dailyWeather = _openMeteoWeather.GetDailyForecast(geolocation);

		// Send the embed with the weather info
		string countryString = geolocation.Name != geolocation.Country ? $" ({geolocation.Country})" : "";
		int uvIndex = (int)System.Math.Round(currentForecast.UvIndex, 0);
		embed.Title = $"{geolocation.Name}{countryString}";
		embed.Description = $"**__Current__**:" +
			$"\n🌡️ **Temperature**: {currentForecast.Temperature}{currentForecast.ForecastUnits.Temperature}" +
			$"\n🌧 **Precipitation**: {currentForecast.Precipitation}{currentForecast.ForecastUnits.Precipitation}" +
			$"\n💦 **Humidity**: {currentForecast.Humidity}{currentForecast.ForecastUnits.RelativeHumidity}" +
			$"\n🪁 **Wind Speed**: {currentForecast.WindSpeed}{currentForecast.ForecastUnits.WindSpeed}" +
			$"\n☁️ **Cloud Cover**: {currentForecast.CloudCover}{currentForecast.ForecastUnits.CloudCover}" +
			$"\n🩻 **UV Index**: {uvIndex}{currentForecast.ForecastUnits.UvIndex} ({GetUvIndex(uvIndex)})";
		embed.AddField("__Today__", GetDailyForecastString(dailyWeather.Forecast, 0, dailyWeather.ForecastUnits), true);
		embed.AddField("__Tomorrow__", GetDailyForecastString(dailyWeather.Forecast, 1, dailyWeather.ForecastUnits), true);
		for (int i = 2; i <= 5; i++)
		{
			string title = i switch
			{
				0 => "Today",
				1 => "Tomorrow",
				_ => GetWeekDay(dailyWeather.Forecast.Time[i])
			};

			embed.AddField($"__{title}__", GetDailyForecastString(dailyWeather.Forecast, i, dailyWeather.ForecastUnits), true);
		}

		await embed.SendAsync();
	}

	private static string GetDailyForecastString(DailyForecast forecast, int dayIndex, DailyUnits units)
	{
		int uvIndex = (int)System.Math.Round(forecast.MaxUvIndex[dayIndex], 0);
		return $"🌡️ {System.Math.Round(forecast.MaxTemperature[dayIndex], 0)}{units.MaxTemperature} / {System.Math.Round(forecast.MinTemperature[dayIndex], 0)}{units.MinTemperature}" +
			$"\n🌧️ {forecast.MeanPrecipitationProbability[dayIndex]}{units.MeanPrecipitationProbability} ({forecast.HoursOfPrecipitation[dayIndex]}{units.HoursOfPrecipitation})" +
			$"\n🪁 {forecast.MaxWindSpeed[dayIndex]}{units.MaxWindSpeed}" +
			$"\n🩻 UV {uvIndex}{units.MaxUvIndex} ({GetUvIndex(uvIndex)})";
	}

	private static string GetWeekDay(long unix)
	{
		DateTime calendarDay = UnixTimeStampToDateTime(unix);
		return calendarDay.DayOfWeek.ToString();
	}

	private static DateTime UnixTimeStampToDateTime(double unix)
	{
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		dateTime = dateTime.AddSeconds(unix).ToLocalTime();
		return dateTime;
	}

	private static string GetUvIndex(int uv)
	{
		return uv switch
		{
			0 or 1 or 2 => "Low",
			3 or 4 or 5 => "Moderate",
			6 or 7 => "High",
			8 or 9 or 10 => "Very High",
			_ => "Extreme"
		};
	}

	private readonly Dictionary<WeatherEmoji, Emoji> _weatherEmojis = new Dictionary<WeatherEmoji, Emoji>()
	{
		{ WeatherEmoji.Sunny, Emoji.Parse("☀️") },
		{ WeatherEmoji.SmallClouds, Emoji.Parse("🌤️") },
		{ WeatherEmoji.LargeClouds, Emoji.Parse("⛅") },
		{ WeatherEmoji.Cloudy, Emoji.Parse("☁️") },
		{ WeatherEmoji.SunnyRain, Emoji.Parse("🌦️") },
		{ WeatherEmoji.Rain, Emoji.Parse("🌧️") },
		{ WeatherEmoji.WetThunder, Emoji.Parse("⛈️") },
		{ WeatherEmoji.DryThunder, Emoji.Parse("🌩️") },
		{ WeatherEmoji.Snow, Emoji.Parse("🌨️") },
	};

	private enum WeatherEmoji
	{
		Sunny,
		SmallClouds,
		LargeClouds,
		Cloudy,
		SunnyRain,
		Rain,
		WetThunder,
		DryThunder,
		Snow,
	}
}
