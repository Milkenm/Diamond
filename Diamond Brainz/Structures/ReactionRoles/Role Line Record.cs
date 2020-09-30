using Discord;

using System;

namespace Diamond.Brainz.Structures.ReactionRoles
{
    public partial class ReactionRoles
    {
        public struct RoleLineRecord
        {
            public RoleLineRecord(IRole role, IEmote emote, string description)
            {
                Role = role;
                Emote = emote;
                Description = description;
            }

            public IRole Role;
            public IEmote Emote;
            public string Description;
        }
    }
}
