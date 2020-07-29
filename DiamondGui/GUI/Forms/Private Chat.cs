using Discord;
using Discord.WebSocket;

using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using static DiamondGui.Functions;
using static DiamondGui.Static;

namespace DiamondGui.Forms
{
	public partial class PrivateChat : Form
	{
		public PrivateChat()
		{
			InitializeComponent();
		}

		private void CreateNewChatTab(object sender, EventArgs e)
		{
			SocketUser user;

			try
			{
				user = diamondCore.Client.GetUser(Convert.ToUInt64(privateChatForm.textBox_userId.Text));

				foreach (TabPage tab in privateChatForm.tabs_chats.TabPages)
				{
					if (tab.Name.Contains(user.Username))
					{
						MessageBox.Show("That user already has an open chat tab.");
						return;
					}
				}
			}
			catch
			{
				MessageBox.Show("User not found.");
				return;
			}

			// TAB PAGE & ADD IT
			TabPage tp = new TabPage(user.Username);
			tp.Name = "tp_" + user.Username;
			privateChatForm.tabs_chats.TabPages.Add(tp);

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
			b.Click += new EventHandler((_Sender, _Event) => SendMessage(user, tb));
			tb.KeyDown += new KeyEventHandler((_Sender, _Event) => TextBoxEnter(user, tb));
		}

		private static void TextBoxEnter(SocketUser user, TextBox tb)
		{
			SendMessage(user, tb);
		}

		private static void SendMessage(SocketUser user, TextBox tb)
		{
			user.SendMessageAsync(tb.Text);

			foreach (TabPage tp in privateChatForm.tabs_chats.TabPages)
			{
				if (tp.Name.Contains(user.Username))
				{
					ListBox lb = (ListBox)tp.Controls.Find("lb_" + user.Username, true)[0];
					privateChatForm.Invoke(new Action(() => lb.Items.Add("💎 Diamond 💎: " + tb.Text)));

					break;
				}
			}

			tb.Text = string.Empty;
		}

		private void PrivateChat_FormClosing(object sender, FormClosingEventArgs e)
		{
			CloseForm(this, e);
		}

		internal static bool MessageEvent;

		internal static void LoadMessageReceivedEvent()
		{
			if (!MessageEvent)
			{
				diamondCore.Client.MessageReceived += Client_MessageReceived;
				MessageEvent = true;
			}
		}

		private static Task Client_MessageReceived(SocketMessage msg)
		{
			foreach (TabPage tp in privateChatForm.tabs_chats.TabPages)
			{
				if (tp.Name.Contains(msg.Author.Username))
				{
					ListBox lb = (ListBox)tp.Controls.Find("lb_" + msg.Author.Username, true)[0];
					privateChatForm.Invoke(new Action(() => lb.Items.Add(msg.Author.Username + ": " + msg.Content)));

					break;
				}
			}

			return null;
		}
	}
}