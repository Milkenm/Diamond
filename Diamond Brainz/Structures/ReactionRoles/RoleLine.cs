using Discord;

using System;

namespace Diamond.Brainz.Structures
{
    public partial class ReactionRoles
    {
        public struct RoleLine
        {
            public RoleLine(IRole role, EmoteType emoteType, dynamic emote, string desc)
            {
                Role = role;
                EmoteType = emoteType;
                Emote = emote;
                Description = desc;
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
