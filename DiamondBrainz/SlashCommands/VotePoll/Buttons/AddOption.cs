using System.Threading.Tasks;

using Diamond.API.Data;
using Diamond.API.SlashCommands.VotePoll.Embeds;

using Discord;
using Discord.Interactions;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.VotePoll
{
	public partial class VotePoll
	{
		/// <summary>
		/// Shows a <see cref="NewOptionModal"/> to the user.
		/// <para>(Called when a user clicks the "Add Option" button on the <see cref="EditorEmbed"/>)</para>
		/// </summary>
		/// <param name="messageId">The ID of the message containing the button.</param>
		[ComponentInteraction("button_add:*", true)]
		public async Task ButtonAddHandlerAsync(ulong messageId)
		{
			using DiamondContext db = new DiamondContext();

			NewOptionModal newOptionModal = new NewOptionModal();
			await this.Context.Interaction.RespondWithModalAsync($"modal_poll_add_option:{messageId}", newOptionModal);
		}

		/// <summary>
		/// Adds a <see cref="PollOption"/> to the <see cref="Poll"/> and refreshes the <see cref="EditorEmbed"/>.
		/// <para>(Called when a user submits the <see cref="NewOptionModal"/>)</para>
		/// </summary>
		/// <param name="messageId">The ID of the message containing the button that called the modal.</param>
		/// <param name="modal">The submitted modal.</param>
		[ModalInteraction("modal_poll_add_option:*")]
		public async Task PollAddOptionModalHandler(ulong messageId, NewOptionModal modal)
		{
			await this.DeferAsync();
			using DiamondContext db = new DiamondContext();

			Poll poll = VoteUtils.GetPollByMessageId(db, messageId);
			PollOption newOption = new PollOption()
			{
				TargetPoll = poll,
				Name = modal.OptionName,
			};
			if (!modal.OptionDescription.IsEmpty())
			{
				newOption.Description = modal.OptionDescription;
			}
			_ = db.Add(newOption);
			_ = await db.SaveChangesAsync();

			await VoteUtils.UpdateEditorEmbed(this.Context, poll, messageId);
		}

		/// <summary>
		/// The modal used to add a new option to the poll.
		/// </summary>
		public class NewOptionModal : IModal
		{
			public string Title => "Add Option";

			/// <summary>
			/// The name of the new <see cref="PollOption"/>.
			/// </summary>
			[RequiredInput]
			[InputLabel("Name")]
			[ModalTextInput("field_name", TextInputStyle.Short, "New option name...", maxLength: 250)]
			public string OptionName { get; set; }

			/// <summary>
			/// The description of the new <see cref="PollOption"/>.
			/// </summary>
			[RequiredInput(false)]
			[InputLabel("Description")]
			[ModalTextInput("field_description", TextInputStyle.Paragraph, "New option description...")]
			public string OptionDescription { get; set; }
		}
	}
}
