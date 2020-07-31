using Diamond.WPF.GUI;
using Diamond.WPF.Utils;

using Discord;
using Discord.Commands;

using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace Diamond.WPF.Commands
{
    public partial class Owner_Module : ModuleBase<SocketCommandContext>
    {
        [Name("SQL Query"), Command("sqlquery"), Alias("sql"), Summary("Executes a SQL Query."), RequireOwner]
        public async Task SQLQuery(string query, string exportName, bool sendToPrivate = true)
        {
            string xmlPath = Main.folders[Main.EFolder.Temp].CreateFolder() + @"SQL_Query.xml";

            bool hasErrors = false;

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle("📜 SQL Query");

            try
            {
                DataTable data = Main.database.ExecuteSQL(query);
                data.TableName = exportName;
                data.WriteXml(xmlPath);

                int affectedRowsCount = data.Rows.Count;
                int errorsCount = data.GetErrors().Length;
                query = Text.Purify(query);

                embed.AddField("**Query**", query);
                embed.AddField("**Affected Rows Count**", affectedRowsCount);
                embed.AddField("**Errors Count**", errorsCount);
            }
            catch (Exception ex)
            {
                hasErrors = true;
                embed.WithDescription("**❌ Error:**" + ex.Message);
            }

            Embed e = Embeds.FinishEmbed(embed, Context);
            if (!hasErrors)
            {
                if (sendToPrivate)
                {
                    await Context.User.SendFileAsync(xmlPath, embed: e).ConfigureAwait(false);
                }
                else
                {
                    await Context.Channel.SendFileAsync(xmlPath, embed: e).ConfigureAwait(false);
                }
            }
            await ReplyAsync(embed: e).ConfigureAwait(false);

            File.Delete(xmlPath);
        }
    }
}