using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.Moderation
{
	public partial class Moderation
	{
		[RequireBotPermission(GuildPermission.BanMembers)]
		[DefaultMemberPermissions(GuildPermission.BanMembers)]
		[DSlashCommand("bulk-ban", "Bans all users that meet a criteria.")]
		public async Task BulkBanCommandAsync(
			[Summary("criteria-class", "The criteria \"class\" to apply.")] BanCriteria criteria,
			[Summary("search", "The text to apply with the criteia.")] string search,
			[Summary("ban-reason", "The reason for the ban.")] string banReason = null,
			[Summary("prune-messages", "If set, will clear messages from 1 to 7 days.")][MinValue(1)][MaxValue(7)] int pruneDays = 0,
			[Summary("amount", "Only required if criteria class is \"Message Contains\"")] int messageCount = 100
		)
		{
			await this.DeferAsync();

			if (banReason.IsEmpty())
			{
				banReason = $"Bulk banned (by: {(this.Context.User as IGuildUser).DisplayName}): [{criteria}] {search}.";
			}

			int bannedUsers = 0;
			int failedBans = 0;
			switch (criteria)
			{
				case BanCriteria.UsernameContains:
					{
						IEnumerable<SocketGuildUser> matchingUsers = this.Context.Guild.Users.Where(u => u.DisplayName.Contains(search));
						foreach (SocketGuildUser user in matchingUsers)
						{
							if (await this.BanUserAsync(user, pruneDays, banReason))
							{
								bannedUsers++;
							}
							else
							{
								failedBans++;
							}
						}
						break;
					}
				case BanCriteria.MessageContains:
					{
						IEnumerable<IMessage> matchingMessages = await this.Context.Channel.GetMessagesAsync(messageCount).FlattenAsync();
						foreach (IMessage message in matchingMessages)
						{
							if (!message.Content.Contains(search))
							{
								continue;
							}

							SocketGuildUser author = (SocketGuildUser)message.Author;
							if (await this.BanUserAsync(author, pruneDays, banReason))
							{
								bannedUsers++;
							}
							else
							{
								failedBans++;
							}
						}
						break;
					}
			}

			DefaultEmbed embed = new DefaultEmbed("Bulk Ban", "🔨", this.Context)
			{
				Description = (bannedUsers == 0 && failedBans == 0) ? "No users banned." : $"Banned **{bannedUsers}** user(s).{(failedBans > 0 ? $"\nFailed to ban **{failedBans}** user(s)." : "")}",
			};

			await embed.SendAsync();
		}

		public enum BanCriteria
		{
			[ChoiceDisplay("Username contains...")] UsernameContains,
			[ChoiceDisplay("Message contains...")] MessageContains,
		}

		private async Task<bool> BanUserAsync(SocketGuildUser user, int pruneDays, string banReason)
		{
			try
			{
				await user.BanAsync(pruneDays, banReason);
			}
			catch
			{
				return false;
			}
			return this.Context.Guild.GetBanAsync(user) != null;
		}
	}
}