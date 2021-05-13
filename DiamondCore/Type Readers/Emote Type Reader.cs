using Discord;
using Discord.Commands;

using System;
using System.Threading.Tasks;

namespace Diamond.Core.TypeReaders
{
    public class EmoteTypeReader : TypeReader
    {
        public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            return Emote.TryParse(input, out Emote result)
                ? Task.FromResult(TypeReaderResult.FromSuccess(result))
                : Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed, "Input could not be parsed as 'Emote'."));
        }
    }
}
