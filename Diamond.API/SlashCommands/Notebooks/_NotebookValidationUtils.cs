using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.SlashCommands.Notebooks.Embeds;
using Diamond.Data.Models.Notebooks;

using Discord;

namespace Diamond.API.SlashCommands.Notebooks
{
	/*public static class NotebookValidationUtils
	{
		public static async Task<bool> HasNotebooks(IInteractionContext context, Dictionary<string, Notebook> userNotebooksMap)
		{
			if (userNotebooksMap.Any())
			{
				return true;
			}

			_ = await new NoNotebooksEmbed(context).SendAsync();

			return false;
		}
	}*/
}
