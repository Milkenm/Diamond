using Discord;
using Discord.Commands;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diamond.Brainz.Utils
{
	public class Permissions
	{
		public static async Task<ValidatePermissionsResult> ValidatePermissions(SocketCommandContext context, GuildPermission[] requiredGuildPermissions)
		{
			return await ValidatePermissions((context.User as IGuildUser).GuildPermissions, requiredGuildPermissions);
		}

		public static async Task<ValidatePermissionsResult> ValidatePermissions(GuildPermissions currentPermissions, GuildPermission[] requiredGuildPermissions)
		{
			return await Task<ValidatePermissionsResult>.Factory.StartNew(() =>
			{
				List<GuildPermission> userPermissions = currentPermissions.ToList();

				foreach (GuildPermission guildPerm in requiredGuildPermissions)
				{
					if (userPermissions.Contains(guildPerm))
					{
						userPermissions.Remove(guildPerm);
					}
				}

				if (userPermissions.Count > 0)
				{
					return new ValidatePermissionsResult(userPermissions);
				}
				return new ValidatePermissionsResult();
			});
		}

		public static async Task<ValidatePermissionsResult> ValidatePermissions(SocketCommandContext context, ChannelPermission[] requiredChannelPermissions)
		{
			return await ValidatePermissions((context.User as IGuildUser).GetPermissions(context.Channel as IGuildChannel), requiredChannelPermissions);
		}

		public static async Task<ValidatePermissionsResult> ValidatePermissions(ChannelPermissions currentPermissions, ChannelPermission[] requiredChannelPermissions)
		{
			return await Task<ValidatePermissionsResult>.Factory.StartNew(() =>
			{
				List<ChannelPermission> userPermissions = currentPermissions.ToList();

				foreach (ChannelPermission channelPerm in requiredChannelPermissions)
				{
					if (userPermissions.Contains(channelPerm))
					{
						userPermissions.Remove(channelPerm);
					}
				}

				if (userPermissions.Count > 0)
				{
					return new ValidatePermissionsResult(userPermissions);
				}
				return new ValidatePermissionsResult();
			});
		}

		public class ValidatePermissionsResult
		{
			public ValidatePermissionsResult()
			{
				Error = false;
			}

			public ValidatePermissionsResult(List<GuildPermission> invalidPermissions)
			{
				Error = true;
				ErrorMessage = "**Missing permissions:** " + string.Join(", ", invalidPermissions.ToArray());
			}

			public ValidatePermissionsResult(List<ChannelPermission> invalidPermissions)
			{
				Error = true;
				ErrorMessage = "**Missing permissions:** " + string.Join(", ", invalidPermissions.ToArray());
			}

			public bool Error;
			public string ErrorMessage;
		}
	}
}
