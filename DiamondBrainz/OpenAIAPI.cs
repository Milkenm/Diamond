using System;

using Microsoft.Extensions.DependencyInjection;

namespace Diamond.API
{
	public class OpenAIAPI : OpenAI_API.OpenAIAPI
	{
		public OpenAIAPI(IServiceProvider serviceProvider) : base(serviceProvider.GetRequiredService<AppSettings>().Settings.OpenaiApiKey) { }
	}
}
