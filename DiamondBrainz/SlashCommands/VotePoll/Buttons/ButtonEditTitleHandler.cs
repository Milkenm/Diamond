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
			using DiamondContext db = new DiamondContext();

			ulong messageId = (this.Context.Interaction as SocketMessageComponent).Message.Id;

			Poll poll = VoteUtils.GetPollByMessageId(db, messageId);
			PollEditorModal editorModal = new PollEditorModal()
			{
				PollName = poll.Title,
				PollDescription = poll.Description,
				PollImageUrl = poll.ImageUrl,
				PollThumbnailUrl = poll.ThumbnailUrl,
			};

			await this.Context.Interaction.RespondWithModalAsync($"poll_editor_modal:{messageId}", editorModal);
		}

		[ModalInteraction("poll_editor_modal:*", true)]
		public async Task PollEditorModalHandlerAsync(ulong messageId, PollEditorModal modal)
		{
			await this.DeferAsync();
			using DiamondContext db = new DiamondContext();

			Poll poll = VoteUtils.GetPollByMessageId(db, messageId);

			// Get the values of components.
			poll.Title = modal.PollName;
			poll.Description = modal.PollDescription;
			poll.ImageUrl = poll.ImageUrl;
			poll.ThumbnailUrl = poll.ThumbnailUrl;
			poll.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			await db.SaveAsync();

			await VoteUtils.UpdateEditorEmbed(this.Context.Interaction, poll, messageId);
		}

		public class PollEditorModal : IModal
		{
			public string Title => "Footer";

			[RequiredInput]
			[InputLabel("Name")]
			[ModalTextInput("field_name", TextInputStyle.Short, "New option name...", maxLength: 250)]
			public string PollName { get; set; }

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
