#region Usings
using System;

using Discord;

using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
	internal static partial class Functions
	{
		internal static void SetDiscordStatus()
		{
			try
			{
				if (Client != null)
				{
					if (Client.LoginState == LoginState.LoggedIn)
					{
						Client.SetStatusAsync(GetUserStatus());
					}
				}
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}
	}
}