using DiamondGui.Properties;

using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiamondGui.Modules.Commands.Joguinhos
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		private static List<Bitmap> board = new List<Bitmap>() { null, null, null, null, null, null, null, null, null, null };
		private static List<string> boardFormats = new List<string>() { ":one:", ":two:", ":three:", ":four:", ":five:", ":six:", ":seven:", ":eight:", ":nine:" };
		private static Bitmap boardImage = new Bitmap(Resources.Board_PNG);
		private static string player;
		private static int playCount;
		private static Bitmap playerX = new Bitmap(Resources.X_PNG);
		private static Bitmap playerO = new Bitmap(Resources.O_PNG);
		private static readonly int[] combosList =
		{
			123, 147, 159, // 1
			213, 258, // 2
			312, 369, 357, // 3
			417, 456, // 4
			546, 528, 537, 519, // 5
			645, 639, // 6
			789, 714, 735, // 7
			879, 825, // 8
			978, 936, 915 // 9
		};





		[Command("tictactoe"), Alias("jogodogalo", "jc"), Summary("Play Tik Tak Toe against your friend.")]
		public async Task TikTakToe(IUser opponent)
		{
			Static.Settings.CommandsUsed++;

			if (opponent.IsBot || opponent == Context.User)
			{
				string message = opponent.IsBot ? "You cannot play against bots!" : "You cannot play against yourself! (are you that lonely?)";
				await ReplyAsync(message);
			}
			else
			{
				string reply = $"Host: {Context.User.Mention}\nOpponent: {opponent.Mention}\n\n\n{board}";
				await Context.Channel.SendFileAsync(SaveBitMap(), "lmao functiona.png");
			}
		}

		public string SaveBitMap()
		{
			Random rand = new Random();
			string filename;

			do
			{
				int r = rand.Next(100000000, 999999999); // 100.000.000 - 999.999.999
				filename = AppContext.BaseDirectory + @"\Temp\JogoDoGalo\BoardPNG_{0}.bmp";
			} while (File.Exists(filename));

			boardImage.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
			return filename;
		}
		
		private void ButtonClick(object sender, EventArgs e)
		{
			Button b = (Button)sender;

			switch (b.Name)
			{
				case "button1":
					UpdateBoard(new Point(0, 0)); break;
				case "button2":
					UpdateBoard(new Point(48, 0)); break;
				case "button3":
					UpdateBoard(new Point(96, 0)); break;
				case "button4":
					UpdateBoard(new Point(0, 48)); break;
				case "button5":
					UpdateBoard(new Point(48, 48)); break;
				case "button6":
					UpdateBoard(new Point(96, 48)); break;
				case "button7":
					UpdateBoard(new Point(0, 96)); break;
				case "button8":
					UpdateBoard(new Point(48, 96)); break;
				case "button9":
					UpdateBoard(new Point(96, 96)); break;
			}

			if (player == "X")
			{
				player = "O";
			}
			else
			{
				player = "X";
			}
		}

		public void UpdateBoard(Point position)
		{
			Graphics bGraphics = Graphics.FromImage(boardImage);
			bGraphics.CompositingMode = CompositingMode.SourceOver;

			if (player == "X")
			{
				bGraphics.DrawImage(playerX, position);
			}
			else
			{
				bGraphics.DrawImage(playerO, position);
			}
		}


		private static bool CheckCombo(int combo, Bitmap player)
		{
			switch (combo)
			{
				case 123: return CheckButtonLine(board[1], board[2], board[3], player); // 1 HORIZONTAL
				case 147: return CheckButtonLine(board[1], board[4], board[7], player); // 1 VERTICAL
				case 159: return CheckButtonLine(board[1], board[5], board[9], player); // 1 DIAGONAL

				case 213: return CheckButtonLine(board[2], board[1], board[3], player); // 2 HORIZONTAL
				case 258: return CheckButtonLine(board[2], board[5], board[8], player); // 2 VERTICAL

				case 312: return CheckButtonLine(board[3], board[1], board[2], player); // 3 HORIZONTAL
				case 369: return CheckButtonLine(board[3], board[6], board[9], player); // 3 VERTICAL
				case 357: return CheckButtonLine(board[3], board[5], board[7], player); // 3 DIAGONAL

				case 417: return CheckButtonLine(board[4], board[1], board[7], player); // 4 HORIZONTAL
				case 456: return CheckButtonLine(board[4], board[5], board[6], player); // 4 VERTICAL

				case 546: return CheckButtonLine(board[5], board[4], board[6], player); // 5 HORIZONTAL
				case 528: return CheckButtonLine(board[5], board[2], board[8], player); // 5 VERTICAL
				case 537: return CheckButtonLine(board[5], board[3], board[7], player); // 5 DIAGONAL [3/5/7]
				case 519: return CheckButtonLine(board[5], board[1], board[9], player); // 5 DIAGONAL [1/5/9]

				case 645: return CheckButtonLine(board[6], board[4], board[5], player); // 6 HORIZONTAL
				case 639: return CheckButtonLine(board[6], board[3], board[9], player); // 6 VERTICAL

				case 789: return CheckButtonLine(board[7], board[8], board[9], player); // 7 HORIZONTAL
				case 714: return CheckButtonLine(board[7], board[1], board[4], player); // 7 VERTICAL
				case 735: return CheckButtonLine(board[7], board[3], board[5], player); // 7 DIAGONAL

				case 879: return CheckButtonLine(board[8], board[7], board[9], player); // 8 HORIZONTAL
				case 825: return CheckButtonLine(board[8], board[2], board[5], player); // 8 VERTICAL

				case 978: return CheckButtonLine(board[9], board[7], board[8], player); // 9 HORIZONTAL
				case 936: return CheckButtonLine(board[9], board[3], board[6], player); // 9 VERTICAL
				case 915: return CheckButtonLine(board[9], board[1], board[5], player); // 9 DIAGONAL

				default:
					return false;
			}
		}

		private static bool CheckButtonLine(Bitmap placeA, Bitmap placeB, Bitmap placeC, Bitmap player)
		{
			return placeA == player && placeB == player && placeC == player;
		}

		private static string CheckVictory(Bitmap player)
		{
			foreach (int combo in combosList)
			{
				if (CheckCombo(combo, player))
				{
					if (player == playerX)
					{
						return "X";
					}
					else if (player == playerO)
					{
						return "O";
					}
					return null;
				}
			}

			if (playCount == 9)
			{
				return "D";
			}

			return null;
		}
	}
}