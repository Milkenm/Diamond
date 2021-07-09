using Diamond.Brainz.Events;
using Diamond.Core;

using Discord;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace Diamond.Brainz
{
	public class Main
	{
		public Main()
		{
			ServiceProvider sp = ServicesProvider.GetServiceProvider();

			this.DCore = new DiamondCore(sp.GetService<Config>().JsonConfig.Token, LogSeverity.Warning, "!", Assembly.GetAssembly(typeof(Main)), new MiscEvents().Log, sp);

			this.DCore.Client.MessageReceived += new ClientEvents().MessageReceived;
			this.DCore.Client.ReactionAdded += new ClientEvents().ReactionAdded;
			this.DCore.Client.ReactionRemoved += new ClientEvents().ReactionRemoved;
			this.DCore.Commands.CommandExecuted += new CommandEvents().CommandExecuted;
			this.DCore.AddDebugChannels(657392886966517782, 622150096720756736, 681532995374415895, 738383172084957254);
		}

		public DiamondCore DCore { get; private set; }
	}
}
