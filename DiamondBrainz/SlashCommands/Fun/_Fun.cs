﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Fun
{
	[Group("fun", "Commands related to funny stuff.")]
	public partial class Fun : InteractionModuleBase<SocketInteractionContext> { }
}