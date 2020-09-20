using Diamond.WPFCore.Data;
using Diamond.WPFCore.Structures.Games;
using Diamond.WPFCore.Utils;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using System.Threading.Tasks;

namespace Diamond.WPFCore.Commands
{
    public partial class GamesModule : ModuleBase<SocketCommandContext>
    {
        [Name("Tic Tac Toe"), Command("tictactoe"), Alias("ttt"), Summary("Play a game of Tic Tac Toe with a friend on Discord.")]
        public async Task TicTacToe(SocketUser opponent, bool giveTurn = false)
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithAuthor("Tic Tac Toe", Twemoji.GetEmojiUrlFromEmoji("🕹"));

            string msg = "**❌ Error:** ";
            if (GlobalData.TTTGamesDataTable.ContainsChannelId(Context.Channel.Id))
            {
                msg += "There is already a game running on this channel!";
            }
            else if (GlobalData.TTTGamesDataTable.ContainsHost(Context.User) || GlobalData.TTTGamesDataTable.ContainsOpponent(opponent))
            {
                msg += "You or your opponent are already in-game!";
            }
            else if (opponent.IsBot)
            {
                msg += "You cannot play agaist bots!";
            }
            else if (opponent == Context.User)
            {
                msg += "You cannot play against yourself!";
            }

            if (msg != "**❌ Error:** ")
            {
                embed.WithDescription(msg);

                await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);
            }
            else
            {
                embed.WithDescription("Creating game...");

                IUserMessage gameMsg = await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);

                TTTGame game = new TTTGame(gameMsg, Context.User, opponent, giveTurn);
            }
        }
    }
}