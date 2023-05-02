using System.Collections.Generic;

using Newtonsoft.Json;

namespace Diamond.API.Schems.OpenMeteoGeocoding
{
	public class Geocoding
	{
		[JsonProperty("results")] public List<Geolocation> Geolocations { get; set; }
		[JsonProperty("generationtime_ms")] public double GenerationTimeMs { get; set; }
	}

	public class Geolocation
	{
		[JsonProperty("id")] public int Id { get; set; }
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("latitude")] public double Latitude { get; set; }
		[JsonProperty("longitude")] public double Longitude { get; set; }
		[JsonProperty("elevation")] public double Elevation { get; set; }
		[JsonProperty("feature_code")] public string FeatureCode { get; set; }
		[JsonProperty("country_code")] public string CountryCode { get; set; }
		[JsonProperty("admin1_id")] public int Admin1Id { get; set; }
		[JsonProperty("admin2_id")] public int Admin2Id { get; set; }
		[JsonProperty("timezone")] public string Timezone { get; set; }
		[JsonProperty("population")] public int Population { get; set; }
		[JsonProperty("country_id")] public int CountryId { get; set; }
		[JsonProperty("country")] public string Country { get; set; }
		[JsonProperty("admin1")] public string Admin1 { get; set; }
		[JsonProperty("admin2")] public string Admin2 { get; set; }
	}

	public class Elevation
	{
		[JsonProperty("value")] public string Value { get; set; }
		[JsonProperty("type")] public string Type { get; set; }
	}
}