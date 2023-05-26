using System;
using System.Threading.Tasks;

using Diamond.API.Data;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace Diamond.API.SlashCommands.VotePoll
{
	public partial class VotePoll
	{
		[ComponentInteraction("button_edit", true)]
		public async Task ButtonAddClickAsync()
		{
			await this.Context.Interaction.RespondWithModalAsync($"poll_editor_modal:{(this.Context.Interaction as SocketMessageComponent).Message.Id}", new PollEditorModal());
		}

		[ModalInteraction("poll_editor_modal:*")]
		public async Task FooterAutoResponderModal(ulong messageId, PollEditorModal modal)
		{
			using DiamondContext db = new DiamondContext();

			Poll poll = VoteUtils.GetPollByMessageId(db, messageId);

			// Get the values of components.
			poll.Title = modal.PollTitle;
			poll.Description = modal.PollDescription;
			poll.ImageUrl = poll.ImageUrl;
			poll.ThumbnailUrl = poll.ThumbnailUrl;
			poll.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

			await this.ModifyOriginalResponseAsync((msg) =>
			{
				msg.Content = "a";
			});

			await VoteUtils.UpdateEditorEmbed(this.Context.Interaction as SocketMessageComponent, poll, messageId);
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
