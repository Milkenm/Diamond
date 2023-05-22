using System.Text;
using System.Threading.Tasks;

using Diamond.API.Attributes;

using Discord.Interactions;

using ScriptsLibV2.Extensions;
using ScriptsLibV2.Util;

namespace Diamond.API.SlashCommands
{
    public class RandomPassword : InteractionModuleBase<SocketInteractionContext>
    {
        [DSlashCommand("randompassword", "Generates a random password.")]
        public async Task RandomPasswordCommandAsync(
            [Summary("size", "The amount of characters to generate for the password.")][MinValue(1)][MaxValue(32)] int passwordSize = 16,
            [Summary("use-letters", "Include lower and uppercase letters from 'a' to 'z' (ignored if 'allowedcharacters' is set).")] bool useLetters = true,
            [Summary("use-numbers", "Include numbers from 0 to 9 (ignored if 'allowedcharacters' is set).")] bool useNumbers = true,
            [Summary("use-special-characters", "Inlcude special characters like '#', '!', '\"' and others (ignored if 'allowedcharacters' is set).")] bool useSpecialCharacters = true,
            [Summary("allowed-characters", "Indicate which characters to use to generate the password.")] string allowedCharacters = "",
            [ShowEveryone] bool showEveryone = false
        )
        {
            await DeferAsync(!showEveryone);

            StringBuilder passwordCharacters = new StringBuilder(allowedCharacters);
            if (allowedCharacters.IsEmpty())
            {
                if (useLetters)
                {
                    passwordCharacters.Append("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
                }
                if (useNumbers)
                {
                    passwordCharacters.Append("0123456789");
                }
                if (useSpecialCharacters)
                {
                    passwordCharacters.Append("!\"@#£$§%&/{([])}=?'«»´`~^+¨ºª-.:,;€<>");
                }
            }

            string password = ScriptsLibV2.Util.Utils.GeneratePassword(passwordSize, passwordCharacters.ToString());

            DefaultEmbed embed = new DefaultEmbed("Password Generator", "🔐", Context.Interaction);
            embed.AddField("🔡 Allowed Characters", passwordCharacters.ToString());
            if (allowedCharacters.IsEmpty())
            {
                embed.AddField("🔤 Letters", BoolToString(useLetters), true);
                embed.AddField("🔢 Numbers", BoolToString(useNumbers), true);
                embed.AddField("🔣 Special Characters", BoolToString(useSpecialCharacters), true);
            }
            embed.AddField("#️⃣ Password Size", $"{password.Length} characters", true);
            embed.WithDescription($"`Generated password:` ||{password}||\n\nℹ️ **__Tip__:** You can copy the password without releaving it by selecting the black square and hitting CTRL+C.");

            await embed.SendAsync();
        }

        private static string BoolToString(bool value)
        {
            return value ? "✅ Yes" : "❌ No";
        }
    }
}
