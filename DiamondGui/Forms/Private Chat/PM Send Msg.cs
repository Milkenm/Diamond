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
		internal static void SendMsg(SocketUser user, TextBox tb)
		{
			Discord.UserExtensions.SendMessageAsync(user, tb.Text);

			foreach (TabPage tp in PrivateChatForm.tabs_chats.TabPages)
			{
				if (tp.Name.Contains(user.Username))
				{
					ListBox lb = (ListBox)tp.Controls.Find("lb_" + user.Username, true)[0];

					PrivateChatForm.Invoke(new Action(() =>
					{
						lb.Items.Add("💎 Diamond 💎: " + tb.Text);
					}));

					break;
				}
			}

			tb.Text = string.Empty;
		}
	}
}
