using System.Threading.Tasks;

using Diamond.API.Schems.OpenMeteoGeocoding;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.World;
public partial class World
{
	[SlashCommand("location", "View some information about a country/city.")]
	public async Task PopulationCommandAsync(
	[Summary("location", "The country or city to get information of.")] string location,
	[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		DefaultEmbed embed = new DefaultEmbed("World Location", "🏙️", Context.Interaction);

		Geocoding geocoding = _openMeteoGeocoding.GeocodeSearch(location);
		if (geocoding == null)
		{
			embed.WithDescription($"**Location '**{location}**' was not found.**");

			await embed.SendAsync();
			return;
		}

		string country = "";
		string zoom = "5.5";
		if (geocoding.Results[0].Name != geocoding.Results[0].Country)
		{
			country = $" ({geocoding.Results[0].Country})";
			zoom = "10";
		}
		embed.WithThumbnailUrl($"https://flagsapi.com/{geocoding.Results[0].CountryCode.ToUpper()}/shiny/64.png");
		embed.WithTitle($"{geocoding.Results[0].Name}{country}");
		embed.AddField("🏳️ **Country Code**", geocoding.Results[0].CountryCode);
		embed.AddField("👥 **Population**", string.Format("{0:N0}", geocoding.Results[0].Population));
		embed.AddField("↔️ **Latitude**", geocoding.Results[0].Latitude, true);
		embed.AddField("↕️ **Longitue**", geocoding.Results[0].Longitude, true);

		ComponentBuilder component = new ComponentBuilder()
			.WithButton("View on Google Maps", style: ButtonStyle.Link, emote: Emoji.Parse("🗺️"), url: $"https://www.google.com/maps/@{geocoding.Results[0].Latitude.ToString().Replace(",", ".")},{geocoding.Results[0].Longitude.ToString().Replace(",", ".")},{zoom}z");

		await embed.SendAsync(component.Build());
	}
}
