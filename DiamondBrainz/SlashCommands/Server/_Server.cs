using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Server
{
	[Group("server", "Info about the server or server members.")]
	public partial class Server : InteractionModuleBase<SocketInteractionContext> { }
}
