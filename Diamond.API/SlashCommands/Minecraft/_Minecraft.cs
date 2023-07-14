using Diamond.API.APIs.Minecraft;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Minecraft
{
    [Group("minecraft", "Minecraft related commands.")]
	public partial class Minecraft : InteractionModuleBase<SocketInteractionContext>
	{
		private readonly McsrvstatAPI _mcsrvstatApi;

		public Minecraft(McsrvstatAPI mcsrvstatAPI)
		{
			this._mcsrvstatApi = mcsrvstatAPI;
		}
	}
}
