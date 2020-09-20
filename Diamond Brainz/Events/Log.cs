using Diamond.Brainz.Data;

using Discord;

using System;
using System.Threading.Tasks;

namespace Diamond.Brainz.Events
{
    public class MiscEvents
    {
        public async Task Log(LogMessage arg)
        {
            await GlobalData.Brainz.TriggerLogEventAsync(arg.Message).ConfigureAwait(false);
        }
    }
}