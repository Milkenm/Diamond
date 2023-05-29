using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

using Diamond.API.Attributes;
using Diamond.API.Util;

using Discord;
using Discord.Interactions;
using Discord.Rest;

using SMath = System.Math;
namespace Diamond.API.SlashCommands
{
	public class Info : InteractionModuleBase<SocketInteractionContext>
	{
		private readonly DiamondClient _client;

		public Info(DiamondClient client)
		{
			this._client = client;
		}

		[DSlashCommand("info", "Shows info about the bot.")]
		public async Task InfoCommandAsync(
			[ShowEveryone] bool showEveryone = false
		)
		{
			await this.DeferAsync(!showEveryone);

			// Get some bot info
			string uptime = TimeSpan.FromSeconds(this._client.Uptime).ToString(@"hh\:mm\:ss");
			RestApplication appInfo = await this._client.GetApplicationInfoAsync();
			// Get system info
			float cpuUsage = this.GetCpuUsage();
			(_, float usedRam, float totalRam) = this.GetRamUsage();
			(_, long usedSpace, long totalSpace) = this.GetDiskUsage();

			DefaultEmbed embed = new DefaultEmbed("Info", "🤖", this.Context)
			{
				ThumbnailUrl = this.Context.Client.CurrentUser.GetAvatarUrl().Replace("?size=128", "?size=" + 512),
			};
			// First row
			_ = embed.AddField("💻 Developer", appInfo.Owner.Mention, true);
			_ = embed.AddField("🏷 Version", $"v{Utils.GetAssemblyVersion()}", true);
			_ = embed.AddField("⏰ Latency", $"{this._client.Latency}ms", true);
			// Second row
			_ = embed.AddField("🪺 Created at", Utils.FormatTime(appInfo.CreatedAt), true);
			_ = embed.AddField("🛏 Online since", $"{Utils.FormatTime(this._client.LastLogin)} UTC", true);
			_ = embed.AddField("🕒 Uptime", uptime, true);
			// Third row
			_ = embed.AddField("🧠 Server CPU Usage", $"**{SMath.Round(cpuUsage, 0)}%**/100%", true);
			_ = embed.AddField("💾 Server RAM Usage", $"**{SMath.Round(usedRam / 1024, 1)} GB**/{totalRam} GB", true);
			_ = embed.AddField("💽 Server Disk Usage", $"**{SMath.Round(usedSpace / 1024D / 1024D / 1024D / 1024D, 1)} TB**/{SMath.Round(totalSpace / 1024D / 1024D / 1024D / 1024D, 1)} TB", true);
			// Buttons
			ComponentBuilder components = new ComponentBuilder()
				.WithButton("Invite", style: ButtonStyle.Link, emote: Emoji.Parse("🤖"), url: "https://discord.com/api/oauth2/authorize?client_id=456022260063404033&permissions=8&scope=applications.commands%20bot")
				.WithButton("Suppor server", style: ButtonStyle.Link, emote: Emoji.Parse("❓"), url: "https://discord.gg/hPhdWpaFWq")
				.WithButton("GitHub", style: ButtonStyle.Link, emote: Emoji.Parse("🐙"), url: "https://github.com/Milkenm/Diamond");

			_ = await embed.SendAsync(component: components.Build());
		}

		private float GetCpuUsage()
		{
			ulong totalProcessorTime = 0;
			int processorCount = 0;
			ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PerfFormattedData_PerfOS_Processor");
			foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
			{
				totalProcessorTime += (ulong)obj["PercentProcessorTime"];
				processorCount++;
			}
			return (float)totalProcessorTime / processorCount;
		}

		private (float freeRam, float usedRam, float totalRam) GetRamUsage()
		{
			_ = PInvoke.GetPhysicallyInstalledSystemMemory(out long totalRam);
			totalRam = totalRam / 1024 / 1024;
			PerformanceCounter performance = new PerformanceCounter("Memória", "MBytes disponíveis");
			float usedRam = performance.NextValue();

			return (totalRam - usedRam, usedRam, totalRam);
		}

		private (long freeSpace, long usedSpace, long totalSpace) GetDiskUsage()
		{
			long totalSpace = 0L;
			long freeSpace = 0L;
			foreach (DriveInfo drive in DriveInfo.GetDrives())
			{
				if (drive.DriveType != DriveType.Fixed) continue;

				totalSpace += drive.TotalSize;
				freeSpace += drive.TotalFreeSpace;

				Debug.WriteLine("drive type: " + drive.DriveType);
				Debug.WriteLine("total size: " + drive.TotalSize);
				Debug.WriteLine("free space: " + drive.TotalFreeSpace);
			}
			return (freeSpace, totalSpace - freeSpace, totalSpace);
		}
	}
}