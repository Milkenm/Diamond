namespace Diamond.API.SlashCommands
{
	public class ComponentIds
	{
		public const string BUTTON_BASE = "button";
		public const string SELECT_BASE = "select";
		public const string MODAL_BASE = "modal";
	}

	public class AutoPublisherComponentIds
	{
		// Auto Publisher - BUTTON
		private const string BUTTON_AUTOPUBLISHER_BASE = $"{ComponentIds.BUTTON_BASE}_autopublisher";
		public const string BUTTON_AUTOPUBLISHER_PUBLISH_MESSAGES = $"{BUTTON_AUTOPUBLISHER_BASE}_publish_messages";
		public const string BUTTON_AUTOPUBLISHER_PERMISSIONS_SET_RETRY = $"{BUTTON_AUTOPUBLISHER_BASE}_permissions_set_retry";
		public const string BUTTON_AUTOPUBLISHER_PERMISSIONS_SET_CLOSE = $"{BUTTON_AUTOPUBLISHER_BASE}_permissions_set_close";
		public const string BUTTON_AUTOPUBLISHER_PERMISSIONS_REMOVE_RETRY = $"{BUTTON_AUTOPUBLISHER_BASE}_permissions_remove_retry";
		public const string BUTTON_AUTOPUBLISHER_PERMISSIONS_REMOVE_CLOSE = $"{BUTTON_AUTOPUBLISHER_BASE}_permissions_remove_close";
	}

	public class MemeComponentIds
	{
		// Meme - BUTTON
		private const string BUTTON_MEME_BASE = $"{ComponentIds.BUTTON_BASE}_meme";
		public const string BUTTON_MEME_REROLL = $"{BUTTON_MEME_BASE}_reroll";
		public const string BUTTON_MEME_SHARE = $"{BUTTON_MEME_BASE}_share";
	}

	public class PokemonComponentIds
	{
		// Pokémon - BUTTON
		private const string BUTTON_POKEMON_BASE = $"{ComponentIds.BUTTON_BASE}_pokemon";
		public const string BUTTON_POKEMON_VIEW_MOVES = $"{BUTTON_POKEMON_BASE}_moves";
		public const string BUTTON_POKEMON_VIEW_STRATS = $"{BUTTON_POKEMON_BASE}_builds";
		public const string BUTTON_POKEMON_VIEW_INFO = $"{BUTTON_POKEMON_BASE}_strats";
		public const string BUTTON_POKEMON_VIEW_MOVES_FIRST = $"{BUTTON_POKEMON_BASE}_first";
		public const string BUTTON_POKEMON_VIEW_MOVES_BACK = $"{BUTTON_POKEMON_BASE}_back";
		public const string BUTTON_POKEMON_VIEW_MOVES_NEXT = $"{BUTTON_POKEMON_BASE}_next";
		public const string BUTTON_POKEMON_VIEW_MOVES_LAST = $"{BUTTON_POKEMON_BASE}_last";
		public const string BUTTON_POKEMON_LOAD_STRATEGIES = $"{BUTTON_POKEMON_BASE}_strats_load";
		public const string BUTTON_POKEMON_SHARE_INFO = $"{BUTTON_POKEMON_BASE}_share_pokemon";
		public const string BUTTON_POKEMON_SHARE_MOVES = $"{BUTTON_POKEMON_BASE}_share_moves";
		public const string BUTTON_POKEMON_SHARE_STRATEGIES = $"{BUTTON_POKEMON_BASE}_share_strategies";
		// Pokémon - SELECT
		private const string SELECT_POKEMON_BASE = $"{ComponentIds.SELECT_BASE}_pokemon";
		public const string SELECT_POKEMON_INFO_GENERATION = $"{SELECT_POKEMON_BASE}_info_generation";
		public const string SELECT_POKEMON_MOVES_GENERATION = $"{SELECT_POKEMON_BASE}_moves_generation";
		public const string SELECT_POKEMON_STRATEGIES_GENERATION = $"{SELECT_POKEMON_BASE}_strats_generation";
	}

	public class PruneComponentIds
	{
		// Prune - BUTTON
		private const string BUTTON_PRUNE_BASE = $"{ComponentIds.BUTTON_BASE}_prune";
		public const string BUTTON_PRUNE_DELETE = $"{BUTTON_PRUNE_BASE}_delete";
	}

	public class SomeoneComponentIds
	{
		// Someone - BUTTON
		private const string BUTTON_SOMEONE_BASE = $"{ComponentIds.BUTTON_BASE}_someone";
		public const string BUTTON_SOMEONE_REROLL = $"{BUTTON_SOMEONE_BASE}_reroll";
	}

	public class VotePollComponentIds
	{
		// Vote Poll - BUTTON
		private const string BUTTON_VOTEPOLL_BASE = $"{ComponentIds.BUTTON_BASE}_votepoll";
		public const string BUTTON_VOTEPOLL_SUBMIT_VOTE = $"{BUTTON_VOTEPOLL_BASE}_vote_submit";
		public const string BUTTON_VOTEPOLL_CANCEL_VOTE = $"{BUTTON_VOTEPOLL_BASE}_vote_cancel";
		public const string BUTTON_VOTEPOLL_ADD_OPTION = $"{BUTTON_VOTEPOLL_BASE}_option_add";
		public const string BUTTON_VOTEPOLL_EDIT_OPTION = $"{BUTTON_VOTEPOLL_BASE}_option_edit";
		public const string BUTTON_VOTEPOLL_PUBLISH = $"{BUTTON_VOTEPOLL_BASE}_publish";
		public const string BUTTON_VOTEPOLL_VOTE = $"{BUTTON_VOTEPOLL_BASE}_poll_vote";
		// Vote Poll - SELECT
		private const string SELECT_VOTEPOLL_BASE = $"{ComponentIds.SELECT_BASE}_votepoll";
		public const string SELECT_VOTEPOLL_VOTE = $"{SELECT_VOTEPOLL_BASE}_vote";
		public const string SELECT_VOTEPOLL_REMOVE_OPTION = $"{SELECT_VOTEPOLL_BASE}_option_remove";
		// Vote Poll - MODAL
		private const string MODAL_VOTEPOLL_BASE = $"{ComponentIds.MODAL_BASE}_votepoll";
		public const string MODAL_VOTEPOLL_ADD_OPTION = $"{MODAL_VOTEPOLL_BASE}_option_add";
		public const string MODAL_VOTEPOLL_EDIT_OPTION = $"{MODAL_VOTEPOLL_BASE}_option_edit";
	}

	public class XMentionComponentIds
	{
		// XMention - SELECT
		private const string SELECT_XMENTION_BASE = $"{ComponentIds.SELECT_BASE}_xmention";
		public const string SELECT_XMENTION_CHANNEL = $"{SELECT_XMENTION_BASE}_channel";
	}
}
