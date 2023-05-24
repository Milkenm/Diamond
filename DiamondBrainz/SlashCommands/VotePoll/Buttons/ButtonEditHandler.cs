using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.VotePoll
{
    public partial class VotePoll
    {
        [ComponentInteraction("button_edit", true)]
        public async Task ButtonEditClick()
        {
            Debug.WriteLine("Clicked button_edit");
        }
    }
}
