#region Usings
using System;
using System.Windows.Forms;

using Discord.WebSocket;

using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class PrivateChat
	{
		internal static void MessageReceived(SocketMessage arg)
		{
			foreach (TabPage tp in PrivateChatForm.tabs_chats.TabPages)
			{
				if (tp.Name.Contains(arg.Author.Username))
				{
					ListBox lb = (ListBox)tp.Controls.Find("lb_" + arg.Author.Username, true)[0];

					PrivateChatForm.Invoke(new Action(() =>
					{
						lb.Items.Add(arg.Author.Username + ": " + arg.Content);
					}));

					break;
				}
			}
		}
	}
}