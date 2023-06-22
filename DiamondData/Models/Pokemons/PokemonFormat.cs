﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{
	[Table("PokemonFormats")]
	public class DbPokemonFormat
	{
		public long Id { get; set; }
		public required string Name { get; set; }
		public required string Abbreviation { get; set; }
		public required string GenerationsList { get; set; }
	}
}