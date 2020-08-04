using Diamond.WPF.Data;
using Diamond.WPF.Structures.Games;
using Diamond.WPF.Utils;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using System.Threading.Tasks;

namespace Diamond.WPF.Commands
{
    public partial class GamesModule : ModuleBase<SocketCommandContext>
    {
        [Name("Tic Tac Toe"), Command("tictactoe"), Alias("ttt"), Summary("Play a game of Tic Tac Toe with a friend on Discord.")]
        public async Task TicTacToe(SocketUser opponent)
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor("Tic Tac Toe", Twemoji.GetEmojiUrlFromEmoji("🕹"));

            string msg = null;
            if (GlobalData.TTTGamesDataTable.ContainsHost(Context.User) || GlobalData.TTTGamesDataTable.ContainsOpponent(opponent))
            {
                msg = "**❌ Error:** You or your opponent are already in-game!";
            }
            else if (opponent.IsBot)
            {
                msg = "**❌ Error:** You cannot play agaist bots!";
            }
            else if (opponent == Context.User)
            {
                msg = "**❌ Error:** You cannot play against yourself!";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                embed.WithDescription(msg);

                await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);
            }
            else
            {
                embed.WithDescription("Creating game...");

                IUserMessage gameMsg = await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);

                TTTGame game = new TTTGame(gameMsg, Context.User, opponent);
            }
        }
    }
}