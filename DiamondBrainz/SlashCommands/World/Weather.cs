using System.Text;
using System.Threading.Tasks;

using Diamond.API.Schems.OpenMeteoGeocoding;
using Diamond.API.Schems.OpenMeteoWeather;

using Discord.Interactions;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.World;
public partial class World
{
	[SlashCommand("weather", "View the weather for a certain region.")]
	public async Task WeatherCommandAsync(
		[Summary("location", "The country or city to get information of.")] string location,
		[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		DefaultEmbed embed = new DefaultEmbed("World Weather", "🌞", Context.Interaction);

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
		Weather weather = _openMeteoWeather.WeatherSearch(geolocation);


		// Send the embed with the weather info
		string countryString = !geolocation.Country.IsEmpty() ? $" ({geolocation.Country})" : "";
		embed.Title = $"{geolocation.Name}{countryString}";
		StringBuilder weathers = new StringBuilder();
		foreach (double temperature in weather.Forecast.Temperature)
		{
			weathers.Append(temperature.ToString().Replace(",", ".") + ", ");
		}
		embed.Description = "weather: " + weathers.ToString();

		await embed.SendAsync();
	}

	private static long ToPositive(long number)
	{
		if (number < 0)
		{
			number *= -1;
		}
		return number;
	}
}
