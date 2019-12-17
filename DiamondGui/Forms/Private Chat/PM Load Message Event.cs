#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DiamondGui.Static;
using System.Threading.Tasks;
using Discord.WebSocket;
#endregion Usings



namespace DiamondGui
{
	internal static partial class PrivateChat
	{
		internal static bool MessageEvent;
		internal static void LoadMessageReceivedEvent()
		{
			if (!MessageEvent)
			{
				Client.MessageReceived += Client_MessageReceived;
				MessageEvent = true;
			}
		}

		private static Task Client_MessageReceived(SocketMessage arg)
		{
			MessageReceived(arg);
			return null;
		}
	}
}