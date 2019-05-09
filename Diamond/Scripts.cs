#region Using
using System;
using System.Management;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
#endregion



namespace Diamond
{
	class Scripts
	{
		#region Scripts
		public static DC discord = new DC();
		public static Tools tools = new Tools();
		public static Device device = new Device();
		#endregion

		#region Discord
		public static DiscordSocketClient Client;
		public static CommandService Commands;
		#endregion








		public class DC : Scripts
		{
			public void Load(LogSeverity _logSeverity)
			{
				Client = new DiscordSocketClient(new DiscordSocketConfig
				{
					LogLevel = _logSeverity,
				});

				Commands = new CommandService(new CommandServiceConfig
				{
					CaseSensitiveCommands = false,
					DefaultRunMode = RunMode.Async,
					LogLevel = _logSeverity,
				});
			}
			


			public Color Color(string _color)
			{
				return (Color)System.Drawing.Color.FromName(_color);
			}
		}
		

		public class Tools : Scripts
		{
			public enum UniList
			{
				ZeroWidthJoiner,
			}

			public string Unicode(UniList _unicode)
			{
				if (_unicode == UniList.ZeroWidthJoiner)
				{
					return "‍";
				}
				else
				{
					throw new Exception();
				}
			}
		}



		public class Device : Scripts
		{
			public enum RAMType
			{
				Max,
				Free,
				Used,
			}

			public double RAM(RAMType _type)
			{
				var wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
				var searcher = new ManagementObjectSearcher(wql);
				var results = searcher.Get();

				double ram = 0;

				if (_type == RAMType.Max)
				{
					foreach (var result in results)
					{
						ram = Math.Round(Convert.ToDouble(result["TotalVisibleMemorySize"]) / (1024 * 1024), 2);
					}
				}
				else if (_type == RAMType.Free)
				{
					foreach(var result in results)
					{
						ram = Math.Round(Convert.ToDouble(result["FreePhysicalMemory"]) / (1024 * 1024), 2);
					}
				}
				else if (_type == RAMType.Used)
				{
					foreach (var result in results)
					{
						ram = Math.Round(Convert.ToDouble(result["TotalVisibleMemorySize"]) / (1024 * 1024), 2);
					}
					foreach (var result in results)
					{
						ram = ram - Math.Round(Convert.ToDouble(result["FreePhysicalMemory"]) / (1024 * 1024), 2);
					}
				}
				else
				{
					throw new Exception();
				}

				return ram;
			}
		}
	}
}