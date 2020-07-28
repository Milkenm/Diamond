#region Usings

using Discord;

#endregion Usings

namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static UserStatus GetUserStatus(string statusString)
		{
			UserStatus _UserStatus = UserStatus.Online;

			if (statusString == "Do Not Disturb")
			{
				_UserStatus = UserStatus.DoNotDisturb;
			}
			else if (statusString == "Idle")
			{
				_UserStatus = UserStatus.Idle;
			}
			else if (statusString == "Invisible")
			{
				_UserStatus = UserStatus.Invisible;
			}

			return _UserStatus;
		}
	}
}