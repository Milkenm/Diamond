using System;

using Diamond.API.APIs.Csgo;

namespace Diamond.API.ConsoleCommands.Commands
{
	[CommandInfo("refresh-csgo-items")]
	public class RefreshCsgoItems : IConsoleCommand
	{
		private readonly CsgoBackpack _csgoBackpack;

		public RefreshCsgoItems(CsgoBackpack csgoBackpack)
		{
			this._csgoBackpack = csgoBackpack;
		}

		public Func<string[], string> Function => new Func<string[], string>((str) =>
		{
			this._csgoBackpack.LoadItems(true).Wait();
			return "Refreshing items...";
		});
	}
}
