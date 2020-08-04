using Diamond.WPF.Data;
using Diamond.WPF.Utils;

using Discord;
using Discord.WebSocket;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Diamond.WPF.Structures.Games
{
    public class TTTGame
    {
        public TTTGame(IUserMessage msg, SocketUser host, SocketUser opponent)
        {
            GameMesage = msg;
            Host = host;
            Opponent = opponent;

            Turn = DateTime.Now.Millisecond % 2 == 0 ? Host : Opponent;

            GlobalData.TTTGamesDataTable.AddGame(host, opponent, GameMesage.Id, GameMesage.Channel.Id, this);

            InitializeGame();
        }

        private readonly SocketUser Host;
        private readonly SocketUser Opponent;
        private readonly IUserMessage GameMesage;
        private SocketUser Turn;
        private readonly List<int> NumberPlays = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly List<IEmote> GameBoard = new List<IEmote>() { new Emoji("1️⃣"), new Emoji("2️⃣"), new Emoji("3️⃣"), new Emoji("4️⃣"), new Emoji("5️⃣"), new Emoji("6️⃣"), new Emoji("7️⃣"), new Emoji("8️⃣"), new Emoji("9️⃣") };
        private readonly List<IEmote> EmotePlays = new List<IEmote>() { new Emoji("1️⃣"), new Emoji("2️⃣"), new Emoji("3️⃣"), new Emoji("4️⃣"), new Emoji("5️⃣"), new Emoji("6️⃣"), new Emoji("7️⃣"), new Emoji("8️⃣"), new Emoji("9️⃣") };
        private readonly IEmote[] EmotesList = { new Emoji("1️⃣"), new Emoji("2️⃣"), new Emoji("3️⃣"), new Emoji("4️⃣"), new Emoji("5️⃣"), new Emoji("6️⃣"), new Emoji("7️⃣"), new Emoji("8️⃣"), new Emoji("9️⃣") };
        private readonly IEmote HostReaction = new Emoji("❎");
        private readonly IEmote OpponentReaction = new Emoji("✅");
        private int PlayCount = 0;

        public ulong MessageId { get { return GameMesage.Id; } }
        public ulong ChannelId { get { return GameMesage.Channel.Id; } }

        public void InitializeGame()
        {
            GameMesage.ModifyAsync(msg =>
            {
                msg.Content = null;
                msg.Embed = GenerateEmbed();
            }).ConfigureAwait(false);
            GameMesage.AddReactionsAsync(EmotePlays.ToArray()).ConfigureAwait(false);
        }

        public async Task HandlePlay(SocketMessage msg)
        {
            int.TryParse(msg.Content, out int play);
            if (IsUserTurn(msg.Author) && NumberPlays.Contains(play))
            {
                Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
                Debug.WriteLine(play);
                NumberPlays.Remove(play);
                await MakePlay(msg.Author, EmotesList[play - 1]).ConfigureAwait(false);
                await msg.DeleteAsync().ConfigureAwait(false);
            }
        }

        public async Task HandlePlay(SocketReaction reaction)
        {
            await MakePlay(reaction.User.Value, reaction.Emote).ConfigureAwait(false);
        }

        public bool IsPlayValid(ulong msgId, IUser user)
        {
            return IsGameMessage(msgId) && IsUserTurn(user);
        }

        public bool IsGameMessage(ulong msgId)
        {
            return msgId == GameMesage.Id;
        }

        public bool IsUserTurn(IUser user)
        {
            return Turn == user;
        }

        public Embed GenerateEmbed()
        {
            EmbedBuilder embed = GenerateBaseEmbed();

            embed.AddField("**Turn**", Turn.Mention);

            embed.AddField("Board", GetBoard());

            return Embeds.FinishEmbed(embed, Host);
        }

        public EmbedBuilder GenerateBaseEmbed()
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor("Tic Tac Toe", Twemoji.GetEmojiUrlFromEmoji("🕹"));

            embed.AddField("**Host**", Host.Mention, true);
            embed.AddField("**Opponent**", Opponent.Mention, true);

            return embed;
        }

        public string GetBoard()
        {
            return $"{GameBoard[0]}{GameBoard[1]}{GameBoard[2]}\n{GameBoard[3]}{GameBoard[4]}{GameBoard[5]}\n{GameBoard[6]}{GameBoard[7]}{GameBoard[8]}";
        }

        public async Task MakePlay(IUser user, IEmote reaction)
        {
            await GameMesage.RemoveReactionAsync(reaction, user).ConfigureAwait(false);
            await GameMesage.RemoveReactionAsync(reaction, GlobalData.DiamondCore.Client.CurrentUser).ConfigureAwait(false);

            if (EmotePlays.Contains(reaction))
            {
                PlayCount++;

                EmotePlays.Remove(reaction);

                int emoteIndex = GameBoard.IndexOf(reaction);
                GameBoard.RemoveAt(emoteIndex);
                GameBoard.Insert(emoteIndex, Turn == Host ? HostReaction : OpponentReaction);

                Turn = user == Host ? Opponent : Host;

                bool win = CheckVictory(user);

                if (!win)
                {
                    await GameMesage.ModifyAsync(msg => msg.Embed = GenerateEmbed()).ConfigureAwait(false);
                }
            }
        }

        public async void EndGame(IUser winner)
        {
            EmbedBuilder embed = GenerateBaseEmbed();

            embed.WithDescription("Game over!");
            embed.AddField("**Winner**", winner != null ? winner.Mention : "Draw!");

            await GameMesage.ModifyAsync(msg => msg.Embed = Embeds.FinishEmbed(embed, winner)).ConfigureAwait(false);
            await GameMesage.RemoveAllReactionsAsync().ConfigureAwait(false);

            GlobalData.TTTGamesDataTable.RemoveGame(this);
        }

        private bool CheckVictory(IUser player)
        {
            IEmote reaction = player == Host ? HostReaction : OpponentReaction;

            foreach (int combo in CombosList)
            {
                if (CheckCombo(combo, reaction))
                {
                    EndGame(player);
                    return true;
                }
            }

            if (PlayCount == 9)
            {
                EndGame(null);
            }

            return false;
        }

        private bool CheckCombo(int combo, IEmote playerReaction)
        {
            char[] comboSplit = combo.ToString().ToCharArray();

            bool check1 = GameBoard[int.Parse(comboSplit[0].ToString()) - 1] == playerReaction;
            bool check2 = GameBoard[int.Parse(comboSplit[1].ToString()) - 1] == playerReaction;
            bool check3 = GameBoard[int.Parse(comboSplit[2].ToString()) - 1] == playerReaction;

            return check1 && check2 && check3;
        }

        private readonly int[] CombosList =
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
    }
}
