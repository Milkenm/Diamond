using Diamond.Brainz.Events;
using Diamond.Core;

using Discord;

using System.Reflection;

namespace Diamond.Brainz
{
	public class Bot : DiamondCore
	{
		public readonly GlobalData GlobalData;

		public Bot(string botConfigPath = "", string dbConfigPath = "") : base(LogSeverity.Info, Assembly.GetAssembly(typeof(Bot)), null, botConfigPath)
		{
			GlobalData = new GlobalData(dbConfigPath);
			this.SetupEvents(new CommandEvents().CommandExecuted, new ClientEvents().MessageReceived, new ClientEvents().ReactionAdded, new ClientEvents().ReactionRemoved);
		}
	}
}
