using System.Diagnostics;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.VotePoll
{
	public partial class VotePoll
	{
		[ComponentInteraction("button_editasd", true)]
		public void ButtonEditClick()
		{
			Debug.WriteLine("Clicked button_edit");
		}

		/*
		void a()
		{

			using DiamondContext db = new DiamondContext();

			Poll poll = VoteUtils.GetPollByMessageId(db, messageId);
			PollOption newOption = new PollOption()
			{
				TargetPoll = poll,
				Name = modal.PollTitle,
			};
			if (!modal.PollDescription.IsEmpty())
			{
				newOption.Description = modal.PollDescription;
			}
			db.PollOptions.Add(newOption);

			await VoteUtils.UpdateEditorEmbed(Context.Interaction as SocketMessageComponent, poll, messageId);
		}
		*/
	}
}
