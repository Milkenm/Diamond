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
			[Summary("print-settings", "Set to true to show additional information about the settings on the embed.")] bool printSettings = false,
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			SomeoneArgs args = new SomeoneArgs(withRole, withoutRole, onlyInThisChannel, includeOffline, includeBots, includeSelf, printSettings);
			DefaultEmbed embed = await GetSomeoneEmbedAsync(this.Context, args, true);

			_ = await embed.SendAsync();
		}

		[ComponentInteraction("button_someone_reroll:*,*,*,*,*,*,*", true)]
		public async Task ButtonRerollHandlerAsync(ulong withRoleId, ulong withoutRoleId, bool onlyInThisChannel, bool includeOffline, bool includeBots, bool includeSelf, bool printSettings)
		{
			await this.DeferAsync();

			IRole withRole = withRoleId != 0L ? this.Context.Guild.GetRole(withRoleId) : null;
			IRole withoutRole = withoutRoleId != 0L ? this.Context.Guild.GetRole(withoutRoleId) : null;
			SomeoneArgs args = new SomeoneArgs(withRole, withoutRole, onlyInThisChannel, includeOffline, includeBots, includeSelf, printSettings);
			DefaultEmbed embed = await GetSomeoneEmbedAsync(this.Context, args, false);

			_ = await this.ModifyOriginalResponseAsync((msg) =>
			{
				msg.Embed = embed.Build();
			});
		}

		private static async Task<DefaultEmbed> GetSomeoneEmbedAsync(SocketInteractionContext context, SomeoneArgs args, bool addComponents)
		{
			DefaultEmbed embed = new DefaultEmbed("Someone", "👤", context);
			if (addComponents)
			{
				embed.Component = new ComponentBuilder()
				.WithButton("Reroll", $"button_someone_reroll:{(args.WithRole != null ? args.WithRole.Id : 0L)},{(args.WithoutRole != null ? args.WithRole.Id : 0L)},{args.OnlyInThisChannel},{args.IncludeOffline},{args.IncludeBots},{args.IncludeSelf},{args.PrintSettings}", style: ButtonStyle.Secondary, emote: Emoji.Parse("🔁"))
				.Build();
			}

			// Add embed settings
			if (args.PrintSettings)
			{
				// First row
				_ = embed.AddField("🏷️ With role", args.WithRole != null ? args.WithRole.Mention : "Unset", true);
				_ = embed.AddField("🏷️ Without role", args.WithoutRole != null ? args.WithoutRole.Mention : "Unset", true);
				_ = embed.AddField("📰 Only in this channel", args.OnlyInThisChannel.ToString(), true);
				// Second row
				_ = embed.AddField("<:discord_offline_icon:1112230664310378537> Include offline", args.IncludeOffline.ToString(), true);
				_ = embed.AddField("🤖 Include bots", args.IncludeBots.ToString(), true);
				_ = embed.AddField("🫵 Inlcude self", args.IncludeSelf.ToString(), true);
			}

			SocketGuildUser? selectedUser = await GetSomeoneAsync(context, args);

			// No matching users found
			if (selectedUser == null)
			{
				embed.Title = "No users found";
				return embed;
			}

			// Select a random kaomoji
			int kaomojiIndex = RandomGenerator.GetInstance().Random.Next(0, _kaomojisList.Count);
			string kaomoji = _kaomojisList[kaomojiIndex];

			// Set embed content
			embed.Title = kaomoji;
			embed.Description = selectedUser.Mention;
			embed.ThumbnailUrl = selectedUser.GetDisplayAvatarUrl();

			return embed;
		}

		private static async Task<SocketGuildUser?> GetSomeoneAsync(SocketInteractionContext context, SomeoneArgs args)
		{
			// Refresh users
			await context.Guild.DownloadUsersAsync();

			// Filter users
			IEnumerable<IUser> validUsers = !args.OnlyInThisChannel ? context.Guild.Users : await context.Channel.GetUsersAsync().FlattenAsync();
			if (args.WithRole != null)
			{
				validUsers = validUsers.Where(u => (u as SocketGuildUser).Roles.Contains(args.WithRole));
			}
			if (args.WithoutRole != null)
			{
				validUsers = validUsers.Where(u => !(u as SocketGuildUser).Roles.Contains(args.WithoutRole));
			}
			if (!args.IncludeOffline)
			{
				validUsers = validUsers.Where(u => u.Status != UserStatus.Offline);
			}
			if (!args.IncludeBots)
			{
				validUsers = validUsers.Where(u => !u.IsBot);
			}
			if (!args.IncludeSelf)
			{
				validUsers = validUsers.Where(u => u != context.User);
			}

			// No matching users found
			if (!validUsers.Any())
			{
				return null;
			}

			// Select a user based on a random index
			int index = RandomGenerator.GetInstance().Random.Next(0, validUsers.Count());
			SocketGuildUser selectedUser = validUsers.ElementAt(index) as SocketGuildUser;

			// Return the selected user
			return selectedUser;
		}

		private class SomeoneArgs
		{
			public SomeoneArgs(IRole withRole, IRole withoutRole, bool onlyInThisChannel, bool includeOffline, bool includeBots, bool includeSelf, bool printSettings)
			{
				this.WithRole = withRole;
				this.WithoutRole = withoutRole;
				this.OnlyInThisChannel = onlyInThisChannel;
				this.IncludeOffline = includeOffline;
				this.IncludeBots = includeBots;
				this.IncludeSelf = includeSelf;
				this.PrintSettings = printSettings;
			}

			public IRole? WithRole { get; set; }
			public IRole? WithoutRole { get; set; }
			public bool OnlyInThisChannel { get; set; }
			public bool IncludeOffline { get; set; }
			public bool IncludeBots { get; set; }
			public bool IncludeSelf { get; set; }
			public bool PrintSettings { get; set; }
		}
	}
}
