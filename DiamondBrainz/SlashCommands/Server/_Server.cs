using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Server
{
	[EnabledInDm(false)]
	[Group("guild", "Info about the guild or guild members.")]
	public partial class Server : InteractionModuleBase<SocketInteractionContext> { }
}
