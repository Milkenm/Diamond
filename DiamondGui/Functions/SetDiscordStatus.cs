#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using static DiamondGui.Static;
using System.Text;
using static DiamondGui.Functions;
using System.Threading.Tasks;
using Discord;
using System.Windows.Forms;
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
			catch (Exception _Exception) { ShowException(_Exception, "DiamondGui.Functions.SetDiscordStatus()"); }
		}
	}
}