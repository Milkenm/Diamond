using Discord;

using System;

namespace Diamond.Brainz.Structures.ReactionRoles
{
    public partial class ReactionRoles
    {
        public struct RoleLineRecord
        {
            public RoleLineRecord(IRole role, EmoteType emoteType, dynamic emote, string description)
            {
                Role = role;
                EmoteType = emoteType;
                Emote = emote;
                Description = description;
            }

            public IRole Role;
            public EmoteType EmoteType;
            public dynamic Emote;
            public string Description;
        }

        public enum EmoteType
        {
            Emoji,
            Emote,
        }
    }
}
