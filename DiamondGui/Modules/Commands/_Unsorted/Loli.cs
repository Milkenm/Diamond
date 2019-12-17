#region Usings
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using Newtonsoft.Json;

using static DiamondGui.Functions;
using static DiamondGui.Static;
using static ScriptsLib.Network.Requests;
#endregion Usings



namespace DiamondGui
{
	public partial class CommandsModule : ModuleBase<SocketCommandContext>
	{
		[Command("loli"), Summary("Gives you a loli pic.")]
		public async Task CMD_Loli(LoliType type = LoliType.Random)
		{
			try
			{
				Settings.CommandsUsed++;

				ITextChannel channel = Context.Channel as ITextChannel;
				if (channel.IsNsfw == false)
				{
					if (type == LoliType.Futa || type == LoliType.Lewd || type == LoliType.Slave || type == LoliType.Monster)
					{
						await ReplyAsync("You must be on an NSFW channel to use this command.");
						return;
					}
				}

				string request;

				if (type == LoliType.Neko)
				{
					request = GET("https://api.lolis.life/neko");
				}
				else if (type == LoliType.Futa) // NSFW
				{
					request = GET("https://api.lolis.life/futa");
				}
				else if (type == LoliType.Kawaii)
				{
					request = GET("https://api.lolis.life/kawaii");
				}
				else if (type == LoliType.Lewd) // NSFW
				{
					request = GET("https://api.lolis.life/lewd");
				}
				else if (type == LoliType.Slave) // NSFW
				{
					request = GET("https://api.lolis.life/slave");
				}
				else if (type == LoliType.Pat)
				{
					request = GET("https://api.lolis.life/pat");
				}
				else if (type == LoliType.Monster) // NSFW
				{
					request = GET("https://api.lolis.life/monster");
				}
				else
				{
					request = GET("https://api.lolis.life/random");
				}

				LoliSchema loli = JsonConvert.DeserializeObject<LoliSchema>(request);

				EmbedAuthorBuilder author = new EmbedAuthorBuilder();
				author.Name = "Click here to view image in browser";
				author.Url = loli.url;

				EmbedBuilder embed = new EmbedBuilder();
				embed.ImageUrl = loli.url;
				embed.WithAuthor(author);

				await ReplyAsync(embed: FinishEmbed(embed, Context));

				if (channel.IsNsfw == true)
				{
					int rand = new Random().Next(0, 100);
					if (type == LoliType.Futa || type == LoliType.Lewd || type == LoliType.Slave || type == LoliType.Monster)
					{
						if (rand >= 0 && rand <= 10)
						{
							await ReplyAsync("https://tenor.com/Wp1T.gif");
						}
					}
				}
			}
			catch (Exception _Exception) { ShowException(_Exception); }
		}

		public class LoliSchema
		{
			public bool sucess;
			public string url;
			public List<string> categories;
		}

		// https://api.lolis.life/<neko, futa, kawaii, lewd, slave, pat, monster>
		public enum LoliType
		{
			Random,
			Neko,
			Futa,
			Kawaii,
			Lewd,
			Slave,
			Pat,
			Monster,
		}
	}
}