#region Usings
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Discord
	{
		internal static async Task DiscordLoader([Optional][DefaultValue(null)] bool? reload)
		{
			// Unload
			if (reload == true)
			{

			}


			// Load
			await DiscordMain();
		}
	}
}
