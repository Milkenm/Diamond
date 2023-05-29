using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace Diamond.API.SlashCommands
{
	public class Someone : InteractionModuleBase<SocketInteractionContext>
	{
		private static readonly List<string> _kaomojisList = new List<string>()
		{
			"(◕‿◕✿)",
			@"¯\_(ツ)_/¯",
			"(ﾉ◕ヮ◕)ﾉ*:･ﾟ✧",
			"(ﾉ>ω<)ﾉ :｡･:*:･ﾟ’★,｡･:*:･ﾟ’☆",
			"༼ つ ◕_◕ ༽つ",
			"(❤ω❤)",
			"＼(٥⁀▽⁀ )／",
			"(￢‿￢ )",
		};

		[EnabledInDm(false)]
		[DSlashCommand("someone", "Selects a random user.")]
		public async Task SomeoneCommandAsync(
			[Summary("with-role", "Only select users that have a specific role.")] IRole withRole = null,
			[Summary("without-role", "Only select users that do not have a specific role.")] IRole withoutRole = null,
			[Summary("only-in-this-channel", "Only selects users that can see this channel.")] bool onlyInThisChannel = false,
			[Summary("include-offline", "Set to true to include offline users.")] bool includeOffline = false,
			[Summary("include-bots", "Set to true to include bots.")] bool includeBots = false,
			[Summary("include-self", "Set to true to include yourself.")] bool includeSelf = false,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);
			DefaultEmbed embed = new DefaultEmbed("Someone", "👤", this.Context);

			// Refresh users
			await this.Context.Guild.DownloadUsersAsync();

			// Filter users
			IEnumerable<IUser> validUsers = !onlyInThisChannel ? this.Context.Guild.Users : await this.Context.Channel.GetUsersAsync().FlattenAsync();
			if (withRole != null)
			{
				validUsers = validUsers.Where(u => (u as SocketGuildUser).Roles.Contains(withRole));
			}
			if (withoutRole != null)
			{
				validUsers = validUsers.Where(u => !(u as SocketGuildUser).Roles.Contains(withoutRole));
			}
			if (!includeOffline)
			{
				validUsers = validUsers.Where(u => u.Status != UserStatus.Offline);
			}
			if (!includeBots)
			{
				validUsers = validUsers.Where(u => !u.IsBot);
			}
			if (!includeSelf)
			{
				validUsers = validUsers.Where(u => u != this.Context.User);
			}

			// No matching users found
			if (!validUsers.Any())
			{
				embed.Title = "No users found";
				_ = await embed.SendAsync();
				return;
			}

			// Select a user based on a random index
			int index = RandomGenerator.GetInstance().Random.Next(0, validUsers.Count());
			SocketGuildUser selectedUser = validUsers.ElementAt(index) as SocketGuildUser;

			// Select a random kaomoji
			int kaomojiIndex = RandomGenerator.GetInstance().Random.Next(0, _kaomojisList.Count);
			string kaomoji = _kaomojisList[kaomojiIndex];

			embed.Title = kaomoji;
			embed.Description = selectedUser.Mention;
			embed.ThumbnailUrl = selectedUser.GetDisplayAvatarUrl();
			_ = await embed.SendAsync();
		}
	}
}
