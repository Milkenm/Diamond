
using Discord;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using static Diamond.Brainz.Structures.ReactionRoles.ReactionRoles;

namespace Diamond.Brainz.Structures.ReactionRoles
{
	public class RoleLinesList
	{
		public RoleLinesList() { }

		public RoleLinesList(IRole role, EmoteType emoteType, dynamic emote, string description)
		{
			Roles.Add(role);
			EmoteTypes.Add(emoteType);
			Emotes.Add(emote);
			Descriptions.Add(description);
		}

		public List<IRole> Roles;
		public List<EmoteType> EmoteTypes;
		public List<dynamic> Emotes;
		public List<string> Descriptions;

		public bool Empty { get { return Roles.Count > 0; } }

		public void AddRecord(RoleLineRecord record)
		{
			AddRecord(record.Role, record.EmoteType, record.Emote, record.Description);
		}

		public void AddRecord(IRole role, EmoteType emoteType, dynamic emote, string description)
		{
			if (!ContainsRole(role) && !ContainsEmote(emote))
			{
				Roles.Add(role);
				EmoteTypes.Add(emoteType);
				Emotes.Add(emote);
				Descriptions.Add(description);
			}
		}

		public void RemoveRecord(dynamic emote)
		{
			int index = Emotes.IndexOf(emote);
			RemoveRecord(index);
		}

		public void RemoveRecord(IRole role)
		{
			int index = Roles.IndexOf(role);
			RemoveRecord(index);
		}

		private void RemoveRecord(int index)
		{
			Roles.RemoveAt(index);
			EmoteTypes.RemoveAt(index);
			Emotes.RemoveAt(index);
			Descriptions.RemoveAt(index);
		}

		private bool ContainsRole(IRole role)
		{
			return Roles.Contains(role);
		}

		private bool ContainsEmote(dynamic emote)
		{
			return Emotes.Contains(emote);
		}

		public IEnumerator GetEnumerator()
		{
			List<RoleLineRecord> roleLines = new List<RoleLineRecord>();

			for (int i = 0; i < Roles.Count; i++)
			{
				roleLines.Add(new RoleLineRecord(Roles[i], EmoteTypes[i], Emotes[i], Descriptions[i]));
			}

			return roleLines.AsEnumerable().GetEnumerator();
		}
	}
}
