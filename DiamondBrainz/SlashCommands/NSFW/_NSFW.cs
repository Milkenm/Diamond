using Diamond.API.Data;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.NSFW;

[RequireNsfw]
[Group("nsfw", "Sus commands.")]
public partial class NSFW : InteractionModuleBase<SocketInteractionContext>
{
	private readonly DiamondDatabase _database;

	public NSFW(DiamondDatabase database)
	{
		this._database = database;
	}
}
