#region Usings

using Discord;
using Discord.Commands;

#endregion Usings

namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static Embed FinishEmbed(EmbedBuilder embed, SocketCommandContext context)
		{
			embed.WithFooter(context.User.Username);
			embed.WithCurrentTimestamp();
			embed.Color = Color.DarkMagenta;

			return embed.Build();
		}
	}
}