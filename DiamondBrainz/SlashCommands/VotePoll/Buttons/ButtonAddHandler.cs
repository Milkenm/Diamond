using System.Diagnostics;
using System.Threading.Tasks;

using Diamond.API.Data;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.VotePoll
{
	public partial class VotePoll
	{
		[ComponentInteraction("button_add", true)]
		public async Task ButtonAddClickAsync()
		{
			Debug.WriteLine("a");
			await Context.Interaction.RespondWithModalAsync($"poll_editor_modal:{(Context.Interaction as SocketMessageComponent).Message.Id}", new PollEditorModal());
		}

		[ModalInteraction("poll_editor_modal:*")]
		public async Task FooterAutoResponderModal(ulong messageId, PollEditorModal modal)
		{
			Poll poll = VoteUtils.GetPollByMessageId(messageId);
			PollOption newOption = new PollOption()
			{
				TargetPoll = poll,
				Name = modal.PollTitle,
			};
			if (!modal.PollDescription.IsEmpty())
			{
				newOption.Description = modal.PollDescription;
			}

			using (DiamondDatabase db = new DiamondDatabase())
			{
				db.Add(newOption);
				await db.SaveAsync();
			}

			await VoteUtils.UpdateEditorEmbed(modal, poll, messageId);
		}

		public class PollEditorModal : IModal
		{
			public string Title => "Footer";

			[RequiredInput]
			[InputLabel("Name")]
			[ModalTextInput("field_title", TextInputStyle.Short, "New option name...", maxLength: 250)]
			public string PollTitle { get; set; }

			[RequiredInput]
			[InputLabel("Description")]
			[ModalTextInput("field_description", TextInputStyle.Paragraph, "New option description...")]
			public string PollDescription { get; set; }

			[RequiredInput(false)]
			[InputLabel("Image URL")]
			[ModalTextInput("field_imageurl", TextInputStyle.Short, "http://www.example.com/sussy.png")]
			public string PollImageUrl { get; set; }

			[RequiredInput(false)]
			[InputLabel("Thumbnail URL")]
			[ModalTextInput("field_thumbnailurl", TextInputStyle.Short, "http://www.example.com/small_sussy.png")]
			public string PollThumbnailUrl { get; set; }
		}
	}
}
