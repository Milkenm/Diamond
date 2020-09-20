using Diamond.WPFCore.Data;

using Discord;

using System.Threading.Tasks;

namespace Diamond.WPFCore.Events
{
    public class MiscEvents
    {
        public async Task Log(LogMessage arg)
        {
            await GlobalData.Main.LogToListBox(arg.Message).ConfigureAwait(false);
        }
    }
}