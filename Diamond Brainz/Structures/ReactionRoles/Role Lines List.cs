
using Discord;

using System;
using System.Collections.Generic;

using static Diamond.Brainz.Structures.ReactionRoles.ReactionRoles;

namespace Diamond.Brainz.Structures.ReactionRoles
{
	public class RoleLinesList
	{
		public RoleLinesList() { }

		public RoleLinesList(IRole role, IEmote emote, string description)
		{
			Roles.Add(role);
			Emotes.Add(emote);
			Descriptions.Add(description);
		}

		public List<IRole> Roles = new List<IRole>();
		public List<IEmote> Emotes = new List<IEmote>();
		public List<string> Descriptions = new List<string>();

		public int Count { get { return Roles.Count; } }

		public bool Empty { get { return Roles.Count > 0; } }

		public void AddRecord(RoleLineRecord record)
		{
			AddRecord(record.Role, record.Emote, record.Description);
		}

		public void AddRecord(IRole role, IEmote emote, string description)
		{
			if (!ContainsRole(role) && !ContainsEmote(emote))
			{
				Roles.Add(role);
				Emotes.Add(emote);
				Descriptions.Add(description);
			}
		}

		public RoleLineRecord GetRecord(int index)
		{
			if (Roles.Count >= index - 1)
			{
				return new RoleLineRecord(Roles[index], Emotes[index], Descriptions[index]);
			}
			else
			{
				throw new Exception("Record not found.");
			}
		}

		public void RemoveRecord(IEmote emote)
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
			Emotes.RemoveAt(index);
			Descriptions.RemoveAt(index);
		}

		private bool ContainsRole(IRole role)
		{
			return Roles.Contains(role);
		}

		private bool ContainsEmote(IEmote emote)
		{
			return Emotes.Contains(emote);
		}
	}
}
