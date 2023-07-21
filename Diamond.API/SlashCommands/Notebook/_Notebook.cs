using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Helpers;
using Diamond.API.Util;
using Diamond.Data;
using Diamond.Data.Models.Notebooks;

using Discord;
using Discord.Interactions;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.SCNotebook
{
	[Group("notebooks", "Notebook related commands.")]
	public partial class SCNotebook : InteractionModuleBase<SocketInteractionContext>
	{
		public class NotebookAutocompleter : AutocompleteHandler
		{
			public override async Task<AutocompletionResult> GenerateSuggestionsAsync(IInteractionContext context, IAutocompleteInteraction autocompleteInteraction, IParameterInfo parameter, IServiceProvider services)
			{
				using DiamondContext db = new DiamondContext();
				Dictionary<string, Notebook> userNotebooks = Notebook.GetNotebooksMap(autocompleteInteraction.User.Id, db);

				List<AutocompleteResult> autocompletions = new List<AutocompleteResult>();

				string input = autocompleteInteraction.Data.Current.Value.ToString();
				if (!input.IsEmpty())
				{
					List<SearchMatchInfo<Notebook>> searchResult = Utils.Search(userNotebooks, input);

					foreach (SearchMatchInfo<Notebook> notebook in searchResult)
					{
						autocompletions.Add(new AutocompleteResult(notebook.Item.Name, notebook.Item.Id));
					}
				}
				else
				{
					foreach (Notebook notebook in db.Notebooks.Where(n => n.DiscordUserId == autocompleteInteraction.User.Id).AsEnumerable().OrderBy(p => p.Name))
					{
						autocompletions.Add(new AutocompleteResult(notebook.Name, notebook.Id));
					}
				}

				return AutocompletionResult.FromSuccess(autocompletions.Take(25));
			}
		}

		public class NotebookEmbed : DefaultEmbed
		{
			public NotebookEmbed(IInteractionContext context)
				: base("Notebooks", "📔", context)
			{ }
		}
	}
}
