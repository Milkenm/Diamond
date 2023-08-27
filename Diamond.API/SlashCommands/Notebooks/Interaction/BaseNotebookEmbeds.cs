using System.Collections.Generic;

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

	public abstract class BaseMultipageNotebookEmbed<T> : MultipageEmbed<T>
	{
		public BaseMultipageNotebookEmbed(IInteractionContext context, MultipageButtons buttons, string title, List<T> itemsList, int startingIndex, bool showEveryone)
			: base(context, buttons, title, "📔", itemsList, startingIndex, 5, showEveryone)
		{ }

		public BaseMultipageNotebookEmbed(IInteractionContext context, MultipageButtons buttons, bool showEveryone)
			: base(context, buttons, showEveryone)
		{ }
	}
}
