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
		private readonly List<string> _kaomojisList = new List<string>()
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

		[DSlashCommand("someone", "Selects a random user.")]
		public async Task SomeoneCommandAsync(
			[Summary("with-role", "Only selects users that contain a specific role.")] IRole role = null,
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
			IEnumerable<SocketGuildUser> validUsers = this.Context.Guild.Users;
			if (role != null)
			{
				validUsers = validUsers.Where(u => u.Roles.Contains(role));
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
				await embed.SendAsync();
				return;
			}

			// Select a user based on a random index
			int index = RandomGenerator.GetInstance().Random.Next(0, validUsers.Count());
			SocketGuildUser selectedUser = validUsers.ElementAt(index);

			// Select a random kaomoji
			int kaomojiIndex = RandomGenerator.GetInstance().Random.Next(0, this._kaomojisList.Count);
			string kaomoji = this._kaomojisList[kaomojiIndex];

			embed.Title = kaomoji;
			embed.Description = selectedUser.Mention;
			embed.ThumbnailUrl = selectedUser.GetDisplayAvatarUrl();
			_ = await embed.SendAsync();
		}
	}
}
