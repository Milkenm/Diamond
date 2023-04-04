using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

namespace Diamond.API.EmbedUtils;
public class DefaultModal : ModalBuilder
{
	private readonly DiscordSocketClient _client;

	public DefaultModal(string title, ulong messageId, DiscordSocketClient client)
	{
		_client = client;

		Title = title;
		CustomId = $"modal_{title.Replace(" ", "").ToLower()}-{messageId}";

		_client.ModalSubmitted += this.Client_ModalSubmitted;
		Debug.WriteLine("finish");
	}

	private async Task Client_ModalSubmitted(SocketModal modal)
	{
		if (modal.Data.CustomId == CustomId)
		{
			ulong messageId = Convert.ToUInt64(modal.Data.CustomId.Split("-")[1]);

			Dictionary<string, string> fieldsMap = new Dictionary<string, string>();
			foreach (var component in modal.Data.Components)
			{
				fieldsMap.Add(component.CustomId, component.Value);
			}

			OnModalSubmit?.Invoke(modal, fieldsMap, messageId);
		}
		_client.ModalSubmitted -= this.Client_ModalSubmitted;
	}

	public delegate void ModalSubmitEvent(SocketModal modal, Dictionary<string, string> fieldsMap, ulong messageId);
	public event ModalSubmitEvent OnModalSubmit;
}
