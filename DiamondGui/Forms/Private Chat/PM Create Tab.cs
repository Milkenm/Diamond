#region Usings
using System;
using System.Drawing;
using System.Windows.Forms;
using Discord.WebSocket;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class PrivateChat
	{
		internal static void CreateTab(SocketUser user)
		{
			// TAB PAGE & ADD IT
			TabPage tp = new TabPage(user.Username);
			tp.Name = "tp_" + user.Username;
			PrivateChatForm.tabs_chats.TabPages.Add(tp);

			// LIST BOX
			ListBox lb = new ListBox();
			lb.Size = new Size(tp.Width, tp.Height - 24);
			lb.Location = new Point(0, 0);
			lb.Name = "lb_" + user.Username;

			// TEXT BOX
			TextBox tb = new TextBox();
			tb.Size = new Size(tp.Width - 50, 23);
			tb.Location = new Point(0, lb.Height + 3);
			tb.Name = "tb_" + user.Username;

			// BUTTON
			Button b = new Button();
			b.Size = new Size(51, 22);
			b.Location = new Point(tb.Location.X + tb.Size.Width, lb.Height + 3 - 1);
			b.Name = "b_" + user.Username;
			b.Text = "Send";

			// ADD THE CREATED CONTROLS TO THE CREATED TAB
			tp.Controls.Add(lb);
			tp.Controls.Add(tb);
			tp.Controls.Add(b);

			// EVENTS
			b.Click += new EventHandler((_Sender, _Event) => SendMsg(user, tb));
			tb.KeyDown += new KeyEventHandler((_Sender, _Event) => TextBoxEnter(_Event, user, tb));
		}
	}
}
