using System.Threading.Tasks;

using Discord.Commands;

namespace Diamond.Brainz.Commands
{
	public partial class OwnerModule : ModuleBase<SocketCommandContext>
	{
		[Name("SQL Query"), Command("sqlquery"), Alias("sql"), Summary("Executes a SQL Query."), RequireOwner]
		public async Task SQLQuery(string query, string exportName = null, bool sendToPrivate = true)
		{
			//string xmlPath = GlobalData.Folders[EFolder.Temp].CreateFolder() + "SQL_Query.xml";

			//bool hasErrors = false;

			//EmbedBuilder embed = new EmbedBuilder();
			//embed.WithAuthor("SQL Query", Twemoji.GetEmojiUrlFromEmoji("📜"));

			//try
			//{
			//    DataTable data = GlobalData.DB.ExecuteSQL(query);
			//    data.TableName = exportName;
			//    data.WriteXml(xmlPath);

			//    int affectedRowsCount = data.Rows.Count;
			//    int errorsCount = data.GetErrors().Length;
			//    query = Text.Purify(query);

			//    embed.AddField("**Query**", query);
			//    embed.AddField("**Affected Rows Count**", affectedRowsCount);
			//    embed.AddField("**Errors Count**", errorsCount);
			//}
			//catch (Exception ex)
			//{
			//    hasErrors = true;
			//    embed.WithDescription("**❌ Error:**" + ex.Message);
			//}

			//Embed e = Embeds.FinishEmbed(embed, this.Context);
			//if (!hasErrors && !string.IsNullOrEmpty(exportName))
			//{
			//    if (sendToPrivate)
			//    {
			//        await this.Context.User.SendFileAsync(xmlPath, embed: e).ConfigureAwait(false);
			//    }
			//    else
			//    {
			//        await this.Context.Channel.SendFileAsync(xmlPath, embed: e).ConfigureAwait(false);
			//    }
			//}
			//await ReplyAsync(embed: e).ConfigureAwait(false);

			//File.Delete(xmlPath);
		}
	}
}