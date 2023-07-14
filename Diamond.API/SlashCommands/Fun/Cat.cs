using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Helpers;

using Discord.Interactions;

namespace Diamond.API.SlashCommands.Fun
{
    public partial class Fun
	{
		[DSlashCommand("cat", "Shows a random picture of a cat.")]
		public async Task CatCommandAsync(
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			using (WebClient client = new WebClient())
			using (Stream stream = client.OpenRead("https://cataas.com/cat"))
			{
				long id = RandomGenerator.GetInstance().Random.NextInt64();
				DefaultEmbed embed = new DefaultEmbed("Cat", "🐱", this.Context)
				{
					ImageUrl = $"attachment://cat_{id}.png",
				};
				// Send the embed
				try
				{
					// This throws an error for some reason but still sends the embed
					_ = await base.FollowupWithFileAsync(stream, $"cat_{id}.png", embed: embed.Build());
				}
				catch { }
			}
		}
	}
}
