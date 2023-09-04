﻿using System.Threading.Tasks;

using Diamond.API.Helpers;

using Discord;
using Discord.Interactions;

namespace Diamond.API.SlashCommands.Notebooks.Interaction
{
    public class ButtonPageGotoListAttribute : DefaultComponentInteractionAttribute
    {
        public override string ComponentLabel => "Go Back";
        public override ButtonStyle? ButtonComponentStyle => ButtonStyle.Secondary;

        public const string COMPONENT_ID = "button_notebookpage_goto_list";

        public ButtonPageGotoListAttribute()
            : base(COMPONENT_ID)
        { }
    }

    public class ButtonPageGotoListHandler : InteractionModuleBase<SocketInteractionContext>
    {
        [ButtonPageGotoList]
        public async Task OnButtonPageGotoListClickHandlerAsync()
        {
            await DeferAsync(true);

            await Notebooks.SendNotebooksListEmbedAsync(Context, 0, false);
        }
    }
}