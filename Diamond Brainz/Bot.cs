using Diamond.Brainz.Events;
using Diamond.Core;

using Discord;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace Diamond.Brainz
{
	public class Bot : DiamondCore
	{
		private static readonly ServiceProvider sp = ServicesProvider.GetServiceProvider();
		private static readonly Config cfg = sp.GetService<Config>();

		public Bot() : base(cfg.JsonConfig.BotConfig.Token, LogSeverity.Info, cfg.JsonConfig.BotConfig.Prefix, Assembly.GetAssembly(typeof(Bot)), sp)
		{
			Client.MessageReceived += new ClientEvents().MessageReceived;
			Client.ReactionAdded += new ClientEvents().ReactionAdded;
			Client.ReactionRemoved += new ClientEvents().ReactionRemoved;

			Commands.CommandExecuted += new CommandEvents().CommandExecuted;

			this.AddDebugChannels(cfg.JsonConfig.BotConfig.DebugChannels);
		}
	}
}
