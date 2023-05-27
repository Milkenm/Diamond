using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Util;
using Diamond.API.Schemes.OpenMeteoGeocoding;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.World;
public partial class World
{
	[DSlashCommand("location", "View some information about a country/city.")]
	public async Task PopulationCommandAsync(
		[Summary("location", "The country or city to get information of.")] string location,
		[ShowEveryone] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		DefaultEmbed embed = new DefaultEmbed("World Location", "🏙️", Context);

		Geocoding geocoding = _openMeteoGeocoding.GeocodeSearch(location);
		if (geocoding == null)
		{
			embed.WithDescription($"**Location '**{location}**' was not found.**");
			await embed.SendAsync();
			return;
		}

		string country = "";
		string zoom = "5.5";
		if (geocoding.Geolocations[0].Name != geocoding.Geolocations[0].Country)
		{
			// It it's not a country
			country = $" ({geocoding.Geolocations[0].Country})";
			zoom = "10";
		}

		embed.WithThumbnailUrl($"https://flagsapi.com/{geocoding.Geolocations[0].CountryCode.ToUpper()}/shiny/64.png");
		embed.WithTitle($"{geocoding.Geolocations[0].Name}{country}");
		embed.AddField("🏳️ **Country Code**", geocoding.Geolocations[0].CountryCode, true);
		embed.AddField("🕒 **Timezone**", geocoding.Geolocations[0].Timezone, true);
		embed.AddField("👥 **Population**", string.Format("{0:N0}", geocoding.Geolocations[0].Population));
		embed.AddField("↔️ **Latitude**", System.Math.Round(geocoding.Geolocations[0].Latitude, 2), true);
		embed.AddField("↕️ **Longitue**", System.Math.Round(geocoding.Geolocations[0].Longitude, 2), true);
		embed.AddField("⬆️ **Elevation**", geocoding.Geolocations[0].Elevation == 9999 ? "Unknown" : $"{geocoding.Geolocations[0].Elevation}m", true);
		ComponentBuilder component = new ComponentBuilder()
			.WithButton("View on Google Maps", style: ButtonStyle.Link, emote: Emoji.Parse("🗺️"), url: $"https://www.google.com/maps/@{geocoding.Geolocations[0].Latitude.ToString().Replace(",", ".")},{geocoding.Geolocations[0].Longitude.ToString().Replace(",", ".")},{zoom}z");

		await embed.SendAsync(component.Build());
	}
}
