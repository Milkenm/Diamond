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
		internal static void OpenUserTab()
		{
			SocketUser user;

			try
			{
				user = Client.GetUser(Convert.ToUInt64(PrivateChatForm.textBox_userId.Text));

				foreach (TabPage tp1 in PrivateChatForm.tabs_chats.TabPages)
				{
					if (tp1.Name.Contains(user.Username))
					{
						MessageBox.Show("That user already has a chat window open.");
						return;
					}
				}
			}
			catch
			{
				MessageBox.Show("User not found.");
				return;
			}

			CreateTab(user);
		}
	}
}