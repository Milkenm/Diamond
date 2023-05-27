using System.Collections.Generic;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Util;

using Discord.Interactions;

namespace Diamond.API.SlashCommands;
public class StyledFont : InteractionModuleBase<SocketInteractionContext>
{
	[DSlashCommand("styled-font", "Convert normal text to styled text.")]
	public async Task IgfontCommandAsync(
		[Summary("text", "The text to style.")] string text,
		[Summary("font-style", "The font style to apply to the text.")] FontStyle fontStyle,
		[ShowEveryone] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		foreach (string letter in _fontMap.Keys)
		{
			text = text.Replace(letter, _fontMap[letter][(int)fontStyle]);
		}

		DefaultEmbed embed = new DefaultEmbed("Styled Font", "🔤", Context)
		{
			Description = text,
		};

		await embed.SendAsync();
	}

	private readonly Dictionary<string, string[]> _fontMap = new Dictionary<string, string[]>()
	{
		// Lower-case
		{ "a", new string[] { "🇦", "𝐚", "𝘢" } },
		{ "b", new string[] { "🇧", "𝐛", "𝘣" } },
		{ "c", new string[] { "🇨", "𝐜", "𝘤" } },
		{ "d", new string[] { "🇩", "𝐝", "𝘥" } },
		{ "e", new string[] { "🇪", "𝐞", "𝘦" } },
		{ "f", new string[] { "🇫", "𝐟", "𝘧" } },
		{ "g", new string[] { "🇬", "𝐠", "𝘨" } },
		{ "h", new string[] { "🇭", "𝐡", "𝘩" } },
		{ "i", new string[] { "🇮", "𝐢", "𝘪" } },
		{ "j", new string[] { "🇯", "𝐣", "𝘫" } },
		{ "k", new string[] { "🇰", "𝐤", "𝘬" } },
		{ "l", new string[] { "🇱", "𝐥", "𝘭" } },
		{ "m", new string[] { "🇲", "𝐦", "𝘮" } },
		{ "n", new string[] { "🇳", "𝐧", "𝘯" } },
		{ "o", new string[] { "🇴", "𝐨", "𝘰" } },
		{ "p", new string[] { "🇵", "𝐩", "𝘱" } },
		{ "q", new string[] { "🇶", "𝐪", "𝘲" } },
		{ "r", new string[] { "🇷", "𝐫", "𝘳" } },
		{ "s", new string[] { "🇸", "𝐬", "𝘴" } },
		{ "t", new string[] { "🇹", "𝐭", "𝘵" } },
		{ "u", new string[] { "🇺", "𝐮", "𝘶" } },
		{ "v", new string[] { "🇻", "𝐯", "𝘷" } },
		{ "w", new string[] { "🇼", "𝐰", "𝘸" } },
		{ "x", new string[] { "🇽", "𝐱", "𝘹" } },
		{ "y", new string[] { "🇾", "𝐲", "𝘺" } },
		{ "z", new string[] { "🇿", "𝐳", "𝘻" } },
		// Upper-case
		{ "A", new string[] { "🇦", "𝐀", "𝘈" } },
		{ "B", new string[] { "🇧", "𝐁", "𝘉" } },
		{ "C", new string[] { "🇨", "𝐂", "𝘊" } },
		{ "D", new string[] { "🇩", "𝐃", "𝘋" } },
		{ "E", new string[] { "🇪", "𝐄", "𝘌" } },
		{ "F", new string[] { "🇫", "𝐅", "𝘍" } },
		{ "G", new string[] { "🇬", "𝐆", "𝘎" } },
		{ "H", new string[] { "🇭", "𝐇", "𝘏" } },
		{ "I", new string[] { "🇮", "𝐈", "𝘐" } },
		{ "J", new string[] { "🇯", "𝐉", "𝘑" } },
		{ "K", new string[] { "🇰", "𝐊", "𝘒" } },
		{ "L", new string[] { "🇱", "𝐋", "𝘓" } },
		{ "M", new string[] { "🇲", "𝐌", "𝘔" } },
		{ "N", new string[] { "🇳", "𝐍", "𝘕" } },
		{ "O", new string[] { "🇴", "𝐎", "𝘖" } },
		{ "P", new string[] { "🇵", "𝐏", "𝘗" } },
		{ "Q", new string[] { "🇶", "𝐐", "𝘘" } },
		{ "R", new string[] { "🇷", "𝐑", "𝘙" } },
		{ "S", new string[] { "🇸", "𝐒", "𝘚" } },
		{ "T", new string[] { "🇹", "𝐓", "𝘛" } },
		{ "U", new string[] { "🇺", "𝐔", "𝘜" } },
		{ "V", new string[] { "🇻", "𝐕", "𝘝" } },
		{ "W", new string[] { "🇼", "𝐖", "𝘞" } },
		{ "X", new string[] { "🇽", "𝐗", "𝘟" } },
		{ "Y", new string[] { "🇾", "𝐘", "𝘠" } },
		{ "Z", new string[] { "🇿", "𝐙", "𝘡" } },
	};

	public enum FontStyle
	{
		[ChoiceDisplay("🇪🇲🇴🇯🇮")] Emoji,
		[ChoiceDisplay("𝐁𝐨𝐥𝐝")] Bold,
		[ChoiceDisplay("𝘐𝘵𝘢𝘭𝘪𝘤")] Italic,
	}
}
