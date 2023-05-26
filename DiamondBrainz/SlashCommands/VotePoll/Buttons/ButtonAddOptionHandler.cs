using System.Threading.Tasks;

using Diamond.API.Data;

using Discord;
using Discord.Interactions;

using ScriptsLibV2.Extensions;

namespace Diamond.API.SlashCommands.VotePoll
{
	public partial class VotePoll
	{
		[ComponentInteraction("button_add", true)]
		public async Task ButtonAddClickAsync()
		{

		}

		[ModalInteraction("modal_new_option:*")]
		public async Task ModalNewOptionHandlerAsync(ulong messageId, NewOptionModal modal)
		{
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
			_ = db.PollOptions.Add(newOption);
			_ = await db.SaveChangesAsync();

			await VoteUtils.UpdateEditorEmbed(this.Context.Interaction, poll, messageId);
		}

		public class NewOptionModal : IModal
		{
			public string Title => "Footer";

			[RequiredInput]
			[InputLabel("Name")]
			[ModalTextInput("field_name", TextInputStyle.Short, "New option name...", maxLength: 250)]
			public string OptionName { get; set; }

			[RequiredInput(false)]
			[InputLabel("Description")]
			[ModalTextInput("field_description", TextInputStyle.Paragraph, "New option description...")]
			public string OptionDescription { get; set; }
		}
	}
}
