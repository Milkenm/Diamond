using Diamond.WPF.Utils;

using Discord;
using Discord.Commands;

using Newtonsoft.Json;

using System.Collections.Generic;
using System.Threading.Tasks;

using static ScriptsLib.Network.Requests;

// # = #
// http://api.obutts.ru/butts/0/1/random
// http://api.oboobs.ru/boobs/0/1/random
//
// http://media.obutts.ru/<preview>
// http://media.oboobs.ru/<preview>
// # = #

namespace Diamond.WPF.Commands
{
    public partial class NSFW_Module : ModuleBase<SocketCommandContext>
    {
        [Name("NSFW"), Command("nsfw"), Summary("Gives you a NSFW image"), RequireNsfw(ErrorMessage = "1You must be on an NSFW channel to use this command.")]
        public async Task NSFW(NsfwType type)
        {
            // CHECK IF IT IS NSFW CHANNEL
            ITextChannel channel = Context.Channel as ITextChannel;
            if (!channel.IsNsfw)
            {
                await ReplyAsync("You must be on an NSFW channel to use this command.").ConfigureAwait(false);
                return;
            }

            // PARSE TYPE
            string typeString = type == NsfwType.Butt ? "butts" : "boobs";

            // MAKE THE REQUEST AND DESERIALIZE IT
            string request = GET($"http://api.o{typeString}.ru/{typeString}/0/1/random");
            List<NsfwSchema> nsfwList = JsonConvert.DeserializeObject<List<NsfwSchema>>(request);
            NsfwSchema nsfw = nsfwList[0];

            // CREATE THE EMBED
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle("🔞 NSFW");
            embed.AddField("**Model**", !string.IsNullOrEmpty(nsfw.model) ? nsfw.model : "Unknown model");
            embed.WithImageUrl($"http://media.o{typeString}.ru/" + nsfw.preview);

            // REPLY
            await ReplyAsync(embed: Embeds.FinishEmbed(embed, Context)).ConfigureAwait(false);
        }

        public class NsfwSchema
        {
            public string model;
            public string preview;
            public int id;
            public int rank;
            public string author;
        }

        public enum NsfwType
        {
            Butt,
            Boobs,
        }
    }
}