namespace Diamond.API.Structures.ReactionRoles
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
