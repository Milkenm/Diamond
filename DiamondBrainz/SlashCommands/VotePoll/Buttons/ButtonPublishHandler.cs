using System.Diagnostics;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.VotePoll
{
	public partial class VotePoll
	{
		[ComponentInteraction("button_publish", true)]
		public void ButtonEditClick()
		{
			Debug.WriteLine("Clicked button_edit");
		}
	}
}
