using Diamond.Brainz.Utils;

using Discord;
using Discord.Commands;

using System;
using System.Text;

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

		private readonly SocketCommandContext Context;
		private readonly IUserMessage Reply;
		private EmbedBuilder Embed;
		private readonly RoleLinesList RoleLinesList;
		private string BaseDescription;

		public ulong GetReplyId()
		{
			return Context.Message.Id;
		}

		public ulong GetChannelId()
		{
			return Context.Channel.Id;
		}

		public void AddRoleLine(IRole role, string emote, params string[] description)
		{
			IEmote parsedEmote;

			try // EMOJI
			{
				Twemoji.GetEmojiCode(emote);
				parsedEmote = new Emoji(emote);
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

			AddRoleLine(role, parsedEmote, string.Join(' ', description));
		}

		public void AddRoleLine(IRole role, IEmote emote, string description)
		{
			RoleLinesList.AddRecord(role, emote, description);
		}

		public void RemoveRoleLine(IRole role)
		{
			RoleLinesList.RemoveRecord(role);
		}

		public void UpdateEmbed(EmbedBuilder embed)
		{
			Embed = embed;
		}

		public void SetTitle(params string[] title)
		{
			SetTitle(string.Join(' ', title));
		}

		public void SetTitle(string title)
		{
			Embed.WithTitle(title);
		}

		public void SetDescription(params string[] description)
		{
			string desc = string.Join(' ', description);
			SetDescription(desc);
			BaseDescription = desc;
		}

		public void SetDescription(string description)
		{
			Embed.WithDescription(description);
			BaseDescription = description;
		}

		public void AddRolesBlock()
		{
			string rolesBlock = GenerateRolesBlock();

			if (!string.IsNullOrEmpty(Embed.Description))
			{
				rolesBlock = "\n\n" + rolesBlock;
			}

			rolesBlock = BaseDescription + rolesBlock;

			Embed.WithDescription(rolesBlock);
		}

		public string GenerateRolesBlock()
		{
			StringBuilder roles = new StringBuilder();
			if (RoleLinesList.Count > 0)
			{
				bool newLine = false;
				for (int i = 0; i < RoleLinesList.Count; i++)
				{
					if (newLine)
					{
						roles.Append("\n");
					}

					RoleLineRecord record = RoleLinesList.GetRecord(i);
					roles.Append(record.Emote).Append(" ").Append(record.Role.Mention).Append(" : ").Append(record.Description);
					newLine = true;
				}
			}

			return roles.ToString();
		}

		public async void ModifyDiscordEmbedAsync()
		{
			AddRolesBlock();
			await Reply.ModifyAsync(msg => msg.Embed = Embeds.FinishEmbed(Embed, Context)).ConfigureAwait(false);
			AddReactions();
		}

		public async void AddReactions()
		{
			foreach (IEmote emote in RoleLinesList.Emotes)
			{
				if (!Reply.Reactions.ContainsKey(emote))
				{
					await Reply.AddReactionsAsync(RoleLinesList.Emotes.ToArray()).ConfigureAwait(false);
				}
			}
		}

		public async void RemoveReactions(IEmote emote)
		{

		}
	}
}
