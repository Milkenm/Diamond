namespace Diamond.Brainz.Structures.Games
{
	public class TTTGame
	{
		//      public TTTGame(IUserMessage msg, SocketUser host, SocketUser opponent, bool giveTurn = false)
		//      {
		//          GameMesage = msg;
		//          Host = host;
		//          Opponent = opponent;
		//          Channel = GameMesage.Channel;
		//          GiveTurn = giveTurn;

		//          Turn = giveTurn ? opponent : DateTime.Now.Millisecond % 2 == 0 ? Host : Opponent;

		//          GlobalData.TTTGamesDataTable.AddGame(this, host, opponent, GameMesage.Id, GameMesage.Channel.Id);

		//          InitializeGame().GetAwaiter();
		//      }

		//      private readonly SocketUser Host;
		//      private readonly SocketUser Opponent;
		//      private IUserMessage GameMesage;
		//      private readonly IMessageChannel Channel;
		//      private readonly bool GiveTurn;
		//      private SocketUser Turn;
		//      private readonly List<int> NumberPlays = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		//      private readonly List<IEmote> GameBoard = new List<IEmote>() { new Emoji("1️⃣"), new Emoji("2️⃣"), new Emoji("3️⃣"), new Emoji("4️⃣"), new Emoji("5️⃣"), new Emoji("6️⃣"), new Emoji("7️⃣"), new Emoji("8️⃣"), new Emoji("9️⃣") };
		//      private readonly IEmote HostReaction = new Emoji("❎");
		//      private readonly IEmote OpponentReaction = new Emoji("🅾️");
		//      private int PlayCount;
		//      private readonly IEmote SurrenderReaction = new Emoji("❌");
		//      private readonly IEmote CallDownReaction = new Emoji("⬇️");

		//      public ulong MessageId { get { return GameMesage.Id; } }
		//      public ulong ChannelId { get { return GameMesage.Channel.Id; } }

		//      public async Task InitializeGame(bool callDown = false)
		//      {
		//          if (!callDown)
		//          {
		//              await GameMesage.ModifyAsync(msg =>
		//              {
		//                  msg.Content = null;
		//                  msg.Embed = FinishEmbed(GenerateEmbed());
		//              }).ConfigureAwait(false);
		//          }
		//          else
		//          {
		//              await GameMesage.DeleteAsync().ConfigureAwait(false);

		//              GameMesage = await Channel.SendMessageAsync(embed: FinishEmbed(GenerateEmbed())).ConfigureAwait(false);
		//              GlobalData.TTTGamesDataTable.UpdateGame(this, TTTGamesDataTable.Column.MessageId, GameMesage.Id);
		//          }

		//          await GameMesage.AddReactionsAsync(new IEmote[] { SurrenderReaction, CallDownReaction }).ConfigureAwait(false);
		//      }

		//      public async Task HandlePlay(SocketMessage msg)
		//      {
		//          int.TryParse(msg.Content, out int play);
		//          if (IsUserTurn(msg.Author) && NumberPlays.Contains(play))
		//          {
		//              await MakePlay(msg.Author, play).ConfigureAwait(false);
		//              await msg.DeleteAsync().ConfigureAwait(false);
		//          }
		//      }

		//      public async Task HandleReaction(SocketReaction reaction)
		//      {
		//          if (reaction.Emote.Equals(SurrenderReaction))
		//          {
		//              await EndGame(reaction.User.Value == Host ? Ends.HostSurrender : Ends.OpponentSurrender).ConfigureAwait(false);
		//          }
		//          else if (reaction.Emote.Equals(CallDownReaction))
		//          {
		//              await InitializeGame(true).ConfigureAwait(false);
		//          }
		//      }

		//      public bool IsPlayValid(ulong msgId, IUser user)
		//      {
		//          return IsGameMessage(msgId) && IsUserTurn(user);
		//      }

		//      public bool IsGameMessage(ulong msgId)
		//      {
		//          return msgId == GameMesage.Id;
		//      }

		//      public bool IsUserTurn(IUser user)
		//      {
		//          return Turn == user;
		//      }

		//      private EmbedBuilder GenerateEmbed(Ends end = Ends.None)
		//      {
		//          EmbedBuilder embed = new EmbedBuilder();
		//          embed.WithAuthor("Tic Tac Toe", Twemoji.GetEmojiUrlFromEmoji("🕹"));
		//          embed.WithDescription($"*__Type from 1 to 9 in chat to play.__*\nPress {SurrenderReaction} to surrender.\nPress {CallDownReaction} to call the message.");

		//          embed.AddField("**Host**", Host.Mention, true);
		//          embed.AddField("**Opponent**", Opponent.Mention, true);

		//          embed.AddField("Board", GetBoard());

		//          if (end == Ends.None)
		//          {
		//              embed.AddField("**Turn**", Turn.Mention);
		//          }
		//          else
		//          {
		//              embed.WithDescription("Game over!");

		//              string title = "**Winner**";
		//              if (end == Ends.OpponentSurrender || end == Ends.HostSurrender)
		//              {
		//                  title += " (by surrender)";
		//              }

		//              object result = null;
		//              if (end == Ends.HostWins || end == Ends.OpponentSurrender)
		//              {
		//                  result = Host.Mention;
		//              }
		//              else if (end == Ends.OpponentWins || end == Ends.HostSurrender)
		//              {
		//                  result = Opponent.Mention;
		//              }
		//              else
		//              {
		//                  result = "Draw!";
		//              }

		//              embed.AddField(title, result);
		//          }

		//          return embed;
		//      }

		//      public Embed FinishEmbed(EmbedBuilder embed)
		//      {
		//          return Embeds.FinishEmbed(embed, Host);
		//      }

		//      public string GetBoard()
		//      {
		//          return $"{GameBoard[0]}{GameBoard[1]}{GameBoard[2]}\n{GameBoard[3]}{GameBoard[4]}{GameBoard[5]}\n{GameBoard[6]}{GameBoard[7]}{GameBoard[8]}";
		//      }

		//      public async Task MakePlay(IUser user, int play)
		//      {
		//          if (NumberPlays.Contains(play))
		//          {
		//              PlayCount++;

		//              NumberPlays.Remove(play);

		//              int emoteIndex = play - 1;
		//              GameBoard.RemoveAt(emoteIndex);
		//              GameBoard.Insert(emoteIndex, Turn == Host ? HostReaction : OpponentReaction);

		//              Turn = user == Host ? Opponent : Host;

		//              await CheckVictory(user).ConfigureAwait(false);
		//          }
		//      }

		//public async Task SendGameEmbed(Embed embed = null)
		//{
		//	if (embed == null)
		//	{
		//		embed = FinishEmbed(GenerateEmbed());
		//	}
		//	await GameMesage.ModifyAsync(msg => msg.Embed = embed).ConfigureAwait(false);
		//}

		//private async Task EndGame(Ends end)
		//{
		//	GlobalData.TTTGamesDataTable.RemoveGame(this);

		//	EmbedBuilder embed = GenerateEmbed(end);

		//	await SendGameEmbed(FinishEmbed(embed)).ConfigureAwait(false);
		//	await GameMesage.RemoveAllReactionsAsync().ConfigureAwait(false);
		//}

		//private async Task CheckVictory(IUser player)
		//{
		//	IEmote reaction = player == Host ? HostReaction : OpponentReaction;

		//	foreach (int combo in CombosList)
		//	{
		//		if (CheckCombo(combo, reaction))
		//		{
		//			await EndGame(player == Host ? Ends.HostWins : Ends.OpponentWins).ConfigureAwait(false);
		//			return;
		//		}
		//	}

		//	if (PlayCount == 9)
		//	{
		//		await EndGame(Ends.Draw).ConfigureAwait(false);
		//		return;
		//	}

		//	await SendGameEmbed().ConfigureAwait(false);
		//}

		//private bool CheckCombo(int combo, IEmote playerReaction)
		//{
		//	char[] comboSplit = combo.ToString().ToCharArray();

		//	bool check1 = GameBoard[int.Parse(comboSplit[0].ToString()) - 1] == playerReaction;
		//	bool check2 = GameBoard[int.Parse(comboSplit[1].ToString()) - 1] == playerReaction;
		//	bool check3 = GameBoard[int.Parse(comboSplit[2].ToString()) - 1] == playerReaction;

		//	return check1 && check2 && check3;
		//}

		//private readonly int[] CombosList =
		//      {
		//          123, 147, 159, // 1
		//	213, 258, // 2
		//	312, 369, 357, // 3
		//	417, 456, // 4
		//	546, 528, 537, 519, // 5
		//	645, 639, // 6
		//	789, 714, 735, // 7
		//	879, 825, // 8
		//	978, 936, 915 // 9
		//};

		//      private enum Ends
		//      {
		//          None,
		//          HostWins,
		//          OpponentWins,
		//          Draw,
		//          HostSurrender,
		//          OpponentSurrender,
		//      }
	}
}