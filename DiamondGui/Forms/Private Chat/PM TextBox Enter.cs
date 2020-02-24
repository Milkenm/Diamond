#region Usings

using Discord.WebSocket;

using System.Windows.Forms;

#endregion Usings

namespace DiamondGui
{
	internal static partial class PrivateChat
	{
		private static void TextBoxEnter(KeyEventArgs e, SocketUser user, TextBox tb)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SendMsg(user, tb);
			}
		}
	}
}