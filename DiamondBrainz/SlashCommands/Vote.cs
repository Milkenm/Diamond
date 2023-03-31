using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Diamond.API.Bot;

using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace Diamond.API.SlashCommands;

public class Vote : InteractionModuleBase<SocketInteractionContext>
{
	private readonly DiamondBot _bot;

	public Vote(DiamondBot bot)
	{
		_bot = bot;
		_bot.Client.SelectMenuExecuted += ModalHandler;
		_bot.Client.ButtonExecuted += this.ButtonHandler;
	}

	private Task ButtonHandler(SocketMessageComponent arg) => throw new NotImplementedException();

	public async Task ModalHandler(SocketMessageComponent arg)
	{
		var text = string.Join(", ", arg.Data.Values);
		await arg.RespondAsync($"You have selected {text}");
	}

	[SlashCommand("poll", "Create a vote poll.")]
	public async Task VoteCommand(
		[Summary("title", "The title for the poll.")] string title,
		[Summary("description", "The description off your poll.")] string description
	)
	{
		await DeferAsync(false);

		await Task.Delay(1000);
		throw new Exception("a");

		DefaultEmbed embed = new DefaultEmbed($"Poll: {title}", "🗳️", Context);

		//SelectMenuBuilder menuBuilder = new SelectMenuBuilder()
		//  .WithPlaceholder("Select an option...")
		//  .WithCustomId("menu-1")
		//  .WithMinValues(1)
		//  .WithMaxValues(1);

		try
		{
			ComponentBuilder builder = new ComponentBuilder()
		.WithButton(new ButtonBuilder("Publish", "publish", ButtonStyle.Success))
		.WithButton(new ButtonBuilder("Add option", "add"))
		.WithButton(new ButtonBuilder("Change title or description", "change", ButtonStyle.Secondary))
		.WithSelectMenu("sad", new List<SelectMenuOptionBuilder>(){
				new SelectMenuOptionBuilder("a","b") }, "Delete an option");
			await embed.SendAsync(builder.Build());
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}

	}

	public class VoteOption
	{
		public string Name2 { get; }
		public string Description2 { get; }

		[ComplexParameterCtor]
		public VoteOption(string name2, string description2)
		{
			Name2 = name2;
			Description2 = description2;
		}
	}
}