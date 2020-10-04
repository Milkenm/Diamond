using System;
using System.Collections.Generic;
using System.Text;

namespace Diamond.Brainz.Structures.ReactionRoles
{
	public class RoleEntry
	{
		public RoleEntry() { }

		public RoleEntry(ulong roleId, string emote, string description)
		{
			RoleId = roleId;
			Emote = emote;
			Description = description;
		}

		public ulong RoleId;
		public string Emote;
		public string Description;
	}
}
