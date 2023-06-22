using System;
using System.Threading.Tasks;

using Diamond.API.SlashCommands.VotePoll.Editor;
using Diamond.Data;
using Diamond.Data.Models.Polls;

using Discord;
using Discord.Interactions;
namespace Diamond.API.SlashCommands.VotePoll
{
	public partial class VotePoll
	{
		/// <summary>
		/// Shows a <see cref="PollEditorModal"/> to the user.
		/// <para>(Called when a user clicks the "Change title or description" button on the <see cref="EditorEmbed"/>)</para>
		/// </summary>
		/// <param name="messageId"></param>
		/// <returns></returns>
		[ComponentInteraction("button_edit:*", true)]
		public async Task ButtonEditHandlerAsync(ulong messageId)
		{
			using DiamondContext db = new DiamondContext();

			DbPoll poll = VoteUtils.GetPollByMessageId(db, messageId);
			PollEditorModal editorModal = new PollEditorModal()
			{
				PollName = poll.Title,
				PollDescription = poll.Description,
				PollImageUrl = poll.ImageUrl,
				PollThumbnailUrl = poll.ThumbnailUrl,
			};
			await this.Context.Interaction.RespondWithModalAsync($"modal_poll_edit:{messageId}", editorModal);
		}

		/// <summary>
		/// Updates the <see cref="Poll"/> settings and refreshes the <see cref="EditorEmbed"/>.
		/// <para>(Called when a user submits the <see cref="PollEditorModal"/>)</para>
		/// </summary>
		/// <param name="messageId">The ID of the message containing the button that called the modal.</param>
		/// <param name="modal">The submitted modal.</param>
		/// <returns></returns>
		[ModalInteraction("modal_poll_edit:*", true)]
		public async Task PollEditorModalHandlerAsync(ulong messageId, PollEditorModal modal)
		{
			await this.DeferAsync();
			using DiamondContext db = new DiamondContext();

			DbPoll poll = VoteUtils.GetPollByMessageId(db, messageId);

			// Get the values of components.
			poll.Title = modal.PollName;
			poll.Description = modal.PollDescription;
			poll.ImageUrl = modal.PollImageUrl;
			poll.ThumbnailUrl = modal.PollThumbnailUrl;
			poll.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			await db.SaveAsync();

			await VoteUtils.UpdateEditorEmbed(this.Context, poll, messageId);
		}

		/// <summary>
		/// The modal used to edit the poll's name, description, image and thumbnail.
		/// </summary>
		public class PollEditorModal : IModal
		{
			public string Title => "Edit Poll";

			/// <summary>
			/// The name of the poll.
			/// </summary>
			[RequiredInput]
			[InputLabel("Name")]
			[ModalTextInput("field_name", TextInputStyle.Short, "New option name...", maxLength: 250)]
			public string PollName { get; set; }

			/// <summary>
			/// The description of the poll.
			/// </summary>
			[RequiredInput]
			[InputLabel("Description")]
			[ModalTextInput("field_description", TextInputStyle.Paragraph, "New option description...")]
			public string PollDescription { get; set; }

			/// <summary>
			/// The image of the poll's embed. This is the large image shown at the bottom of the embed.
			/// </summary>
			[RequiredInput(false)]
			[InputLabel("Image URL")]
			[ModalTextInput("field_imageurl", TextInputStyle.Short, "http://www.example.com/sussy.png")]
			public string PollImageUrl { get; set; }

			/// <summary>
			/// The thumbnail of the poll's embed. This is the small image shown at the top right corner of the embed.
			/// </summary>
			[RequiredInput(false)]
			[InputLabel("Thumbnail URL")]
			[ModalTextInput("field_thumbnailurl", TextInputStyle.Short, "http://www.example.com/small_sussy.png")]
			public string PollThumbnailUrl { get; set; }
		}
	}
}
