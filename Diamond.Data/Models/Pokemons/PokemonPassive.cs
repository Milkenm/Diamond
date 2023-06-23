﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Pokemons
{
	[Table("PokemonPassives")]
	public class DbPokemonPassive
	{
		[Key] public long Id { get; set; }
		public required string Name { get; set; }
		public required string Description { get; set; }
		public required bool IsNonstandard { get; set; }
		public required string GenerationsList { get; set; }
	}
}