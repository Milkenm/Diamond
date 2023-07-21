using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Diamond.API.Attributes;

namespace Diamond.API.SlashCommands.SCNotebook
{
	public partial class SCNotebook
	{
		[DSlashCommand("delete", "Delete a notebook.")]
		public async Task DeleteNotebookCommandAsync()
		{
			await DeferAsync(true);


		}
	}
}
