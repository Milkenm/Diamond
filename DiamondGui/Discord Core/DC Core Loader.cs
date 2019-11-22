#region Usings
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Discord.Commands;

using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class DiscordCore
	{
		internal static async Task DiscordCoreLoader([Optional][DefaultValue(null)] bool? reload)
		{
			// Unload
			if (reload == true)
			{
				foreach (ModuleInfo cmd in Command.Modules)
				{
					await Command.RemoveModuleAsync(cmd);
				}

				return;
			}


			// Load
			await DiscordCoreMain();
		}
	}
}
