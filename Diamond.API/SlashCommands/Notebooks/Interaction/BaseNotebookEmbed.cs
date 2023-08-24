﻿using System.Collections.Generic;

using Diamond.API.Helpers;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
    public abstract class BaseNotebookEmbed : DefaultEmbed
    {
        public BaseNotebookEmbed(string title, IInteractionContext context)
            : base(title, "📔", context)
        { }

        public BaseNotebookEmbed(IInteractionContext context)
            : base(context)
        { }
    }

    public abstract class MultipageNotebookEmbed<T> : MultipageEmbed<T>
    {
        public MultipageNotebookEmbed(string title, IInteractionContext context, List<T> itemsList, int startingIndex, bool showEveryone)
            : base(title, "📔", context, itemsList, startingIndex, 5, showEveryone)
        { }

        public MultipageNotebookEmbed(IInteractionContext context, bool showEveryone)
            : base(context, showEveryone)
        { }
    }
}
