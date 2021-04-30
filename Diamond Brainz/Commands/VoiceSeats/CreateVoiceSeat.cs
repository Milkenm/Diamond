using Discord;
using Discord.Commands;
using Discord.WebSocket;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diamond.Brainz.Commands
{
	public partial class VoiceSeatsModule : ModuleBase<SocketCommandContext>
	{
		[Name("VoiceSeat Set"), Command("voiceseat set"), Alias("vs set"), Summary("Set the base channel for the voice seats.")]
		public async Task CreateVoiceSeat()
		{
			IVoiceChannel voiceChannel = (this.Context.User as IGuildUser).VoiceChannel;
			if (voiceChannel.CategoryId == null)
			{
				throw new Exception("The channel must be inside a category.");
			}

			OriginalVoiceChannelId = voiceChannel.Id;
			VoiceName = voiceChannel.Name;
			CategoryId = (ulong)voiceChannel.CategoryId;
			Guild = this.Context.Guild;

			await CreateVoiceChannel(this.Context.User);
		}

		public static ulong OriginalVoiceChannelId { get; set; }
		public static string VoiceName { get; set; }
		public static ulong CategoryId { get; set; }
		public static IGuild Guild { get; set; }
		public static List<ulong> voiceSeats { get; set; } = new List<ulong>();
		public static List<int> UsedIds = new List<int>();

		public static async Task CreateVoiceChannel(SocketUser user)
		{
			ICategoryChannel c = (ICategoryChannel)await Guild.GetChannelAsync(CategoryId);
			int id = default;
			for (int i = 1; i <= 50; i++)
			{
				if (UsedIds.Contains(i) == false)
				{
					id = i;
					break;
				}
			}
			IVoiceChannel createdVc = await Guild.CreateVoiceChannelAsync($"{VoiceName} #{id}", prop => prop.CategoryId = CategoryId);
			voiceSeats.Add(createdVc.Id);
			UsedIds.Add(id);
		}

		public static async Task VoiceHandlerAsync(SocketUser user, SocketVoiceState previousChannel, SocketVoiceState newChannel)
		{
			IVoiceChannel newVc = newChannel.VoiceChannel;
			if (newVc.Id == OriginalVoiceChannelId)
			{
				await CreateVoiceChannel(user);
			}
			else if (voiceSeats.Contains(previousChannel.VoiceChannel.Id))
			{
				if (previousChannel.VoiceChannel.Users.Count == 0)
				{
					await previousChannel.VoiceChannel.DeleteAsync();
					voiceSeats.Remove(previousChannel.VoiceChannel.Id);
				}
			}
		}
	}
}
