using Diamond.Brainz.Utils;

using Discord;
using Discord.Commands;

using System;
using System.Data;
using System.Threading.Tasks;

using static Diamond.Brainz.Structures.ReactionRoles.ReactionRoles;

namespace Diamond.Brainz.Structures.ReactionRoles
{
	public class RRMessage
	{
		public RRMessage(SocketCommandContext context, IUserMessage reply, EmbedBuilder embed, RoleLinesList roleLinesList)
		{
			Context = context;
			Reply = reply;
			Embed = embed;
			RoleLinesList = roleLinesList ?? new RoleLinesList();
		}

		private SocketCommandContext Context;
		private IUserMessage Reply;
		private EmbedBuilder Embed;
		private RoleLinesList RoleLinesList;

		public ulong GetReplyId()
		{
			return Context.Message.Id;
		}

		public ulong GetChannelId()
		{
			return Context.Channel.Id;
		}

		public async Task AddRoleLine(IRole role, string emote, params string[] description)
		{
			await Task.Run(new Action(async () =>
			{
				EmoteType emoteType = EmoteType.Emote;
				dynamic parsedEmote = emote;

				try // EMOJI
				{
					string emoji = Twemoji.GetEmojiUrlFromEmoji(emote);
					emoteType = EmoteType.Emoji;
				}
				catch // EMOTE
				{
					Emote.TryParse(emote, out Emote e);

					if (e != null)
					{
						 parsedEmote = e;
					}
					else
					{
						throw new Exception("Invalid emoji/emote.");
					}
				}

				await AddRoleLine(role, emoteType, emote, string.Join(' ', description));
			}));
		}

		public async Task AddRoleLine(IRole role, EmoteType emoteType, dynamic emote, string description)
		{
			await Task.Run(new Action(() => RoleLinesList.AddRecord(role, emoteType, emote, description)));
		}

		public async Task RemoveRoleLine(IRole role)
		{
			await Task.Run(new Action(() => RoleLinesList.RemoveRecord(role)));
		}

		public async Task UpdateEmbed(EmbedBuilder embed)
		{
			await Task.Run(new Action(() => Embed = embed));
		}

		public async Task SetTitle(params string[] title)
		{
			await Task.Run(new Action(async () => await SetTitle(string.Join(' ', title))));
		}

		public async Task SetTitle(string title)
		{
			await Task.Run(new Action(() => Embed.WithTitle(title)));
		}

		public async Task SetDescription(params string[] description)
		{
			await Task.Run(new Action(async () => await SetDescription(string.Join(' ', description))));
		}

		public async Task SetDescription(string description)
		{
			await Task.Run(new Action(() => Embed.WithDescription(description)));
		}

		public async Task ModifyDiscordEmbedAsync(EmbedBuilder embed = null)
		{
			if (RoleLinesList.Empty)
			{
				embed.Fields.Clear();

				foreach (var a in RoleLinesList)
				{

				}
			}

			if (embed != null)
			{
				Embed = embed;
			}

			await Reply.ModifyAsync(msg => msg.Embed = Embeds.FinishEmbed(Embed, Context));
		}
	}
}
