﻿using Diamond.Data.Models.Notebooks;

using Microsoft.EntityFrameworkCore;

namespace Diamond.Data
{
	public partial class DiamondContext
	{
		public DbSet<Notebook> Notebooks { get; set; }
		public DbSet<NotebookPage> NotebookPages { get; set; }
		public DbSet<SavedNotebookPage> SavedNotebookPages { get; set; }

		internal static string FormatDiscordInput(string input)
		{
			return input.Replace("*", @"\*").Replace("_", @"\_").Replace("~", @"\~").Replace("`", @"\`");
		}
	}
}
