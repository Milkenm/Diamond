using System.Collections.Generic;
using System.Threading.Tasks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands;
public class StyledFont : InteractionModuleBase<SocketInteractionContext>
{
	private readonly Dictionary<string, List<string>> _fontMap = new Dictionary<string, List<string>>()
	{
		// Lower-case
		{ "a", new List<string>() { "🇦", "𝐚", "𝘢" } },
		{ "b", new List<string>() { "🇧", "𝐛", "𝘣" } },
		{ "c", new List<string>() { "🇨", "𝐜", "𝘤" } },
		{ "d", new List<string>() { "🇩", "𝐝", "𝘥" } },
		{ "e", new List<string>() { "🇪", "𝐞", "𝘦" } },
		{ "f", new List<string>() { "🇫", "𝐟", "𝘧" } },
		{ "g", new List<string>() { "🇬", "𝐠", "𝘨" } },
		{ "h", new List<string>() { "🇭", "𝐡", "𝘩" } },
		{ "i", new List<string>() { "🇮", "𝐢", "𝘪" } },
		{ "j", new List<string>() { "🇯", "𝐣", "𝘫" } },
		{ "k", new List<string>() { "🇰", "𝐤", "𝘬" } },
		{ "l", new List<string>() { "🇱", "𝐥", "𝘭" } },
		{ "m", new List<string>() { "🇲", "𝐦", "𝘮" } },
		{ "n", new List<string>() { "🇳", "𝐧", "𝘯" } },
		{ "o", new List<string>() { "🇴", "𝐨", "𝘰" } },
		{ "p", new List<string>() { "🇵", "𝐩", "𝘱" } },
		{ "q", new List<string>() { "🇶", "𝐪", "𝘲" } },
		{ "r", new List<string>() { "🇷", "𝐫", "𝘳" } },
		{ "s", new List<string>() { "🇸", "𝐬", "𝘴" } },
		{ "t", new List<string>() { "🇹", "𝐭", "𝘵" } },
		{ "u", new List<string>() { "🇺", "𝐮", "𝘶" } },
		{ "v", new List<string>() { "🇻", "𝐯", "𝘷" } },
		{ "w", new List<string>() { "🇼", "𝐰", "𝘸" } },
		{ "x", new List<string>() { "🇽", "𝐱", "𝘹" } },
		{ "y", new List<string>() { "🇾", "𝐲", "𝘺" } },
		{ "z", new List<string>() { "🇿", "𝐳", "𝘻" } },
		// Upper-case
		{ "A", new List<string>() { "🇦", "𝐀", "𝘈" } },
		{ "B", new List<string>() { "🇧", "𝐁", "𝘉" } },
		{ "C", new List<string>() { "🇨", "𝐂", "𝘊" } },
		{ "D", new List<string>() { "🇩", "𝐃", "𝘋" } },
		{ "E", new List<string>() { "🇪", "𝐄", "𝘌" } },
		{ "F", new List<string>() { "🇫", "𝐅", "𝘍" } },
		{ "G", new List<string>() { "🇬", "𝐆", "𝘎" } },
		{ "H", new List<string>() { "🇭", "𝐇", "𝘏" } },
		{ "I", new List<string>() { "🇮", "𝐈", "𝘐" } },
		{ "J", new List<string>() { "🇯", "𝐉", "𝘑" } },
		{ "K", new List<string>() { "🇰", "𝐊", "𝘒" } },
		{ "L", new List<string>() { "🇱", "𝐋", "𝘓" } },
		{ "M", new List<string>() { "🇲", "𝐌", "𝘔" } },
		{ "N", new List<string>() { "🇳", "𝐍", "𝘕" } },
		{ "O", new List<string>() { "🇴", "𝐎", "𝘖" } },
		{ "P", new List<string>() { "🇵", "𝐏", "𝘗" } },
		{ "Q", new List<string>() { "🇶", "𝐐", "𝘘" } },
		{ "R", new List<string>() { "🇷", "𝐑", "𝘙" } },
		{ "S", new List<string>() { "🇸", "𝐒", "𝘚" } },
		{ "T", new List<string>() { "🇹", "𝐓", "𝘛" } },
		{ "U", new List<string>() { "🇺", "𝐔", "𝘜" } },
		{ "V", new List<string>() { "🇻", "𝐕", "𝘝" } },
		{ "W", new List<string>() { "🇼", "𝐖", "𝘞" } },
		{ "X", new List<string>() { "🇽", "𝐗", "𝘟" } },
		{ "Y", new List<string>() { "🇾", "𝐘", "𝘠" } },
		{ "Z", new List<string>() { "🇿", "𝐙", "𝘡" } },
	};

	public enum FontStyle
	{
		[ChoiceDisplay("🇪🇲🇴🇯🇮")] Emoji,
		[ChoiceDisplay("𝐁𝐨𝐥𝐝")] Bold,
		[ChoiceDisplay("𝘐𝘵𝘢𝘭𝘪𝘤")] Italic,
	}

	[SlashCommand("styled-font", "Convert normal text to styled text.")]
	public async Task IgfontCommandAsync(
		[Summary("text", "The text to style.")] string text,
		[Summary("font-style", "The font style to apply to the text.")] FontStyle fontStyle,
		[Summary("show-everyone", "Show the command output to everyone.")] bool showEveryone = false
	)
	{
		await DeferAsync(!showEveryone);

		foreach (string letter in _fontMap.Keys)
		{
			text = text.Replace(letter, _fontMap[letter][(int)fontStyle]);
		}

		DefaultEmbed embed = new DefaultEmbed("Styled Font", "🔤", Context.Interaction)
		{
			Description = text,
		};

		await embed.SendAsync();
	}
}
