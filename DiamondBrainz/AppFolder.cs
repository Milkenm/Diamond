using System;

using Microsoft.Extensions.DependencyInjection;

using ScriptsLibV2;

namespace Diamond.API
{
	public class AppFolder : Folder
	{
		private readonly IServiceProvider _serviceProvider;

		public AppFolder(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;

			Path = _serviceProvider.GetRequiredService<AppSettings>().Settings.CacheFolderPath;
		}
	}
}
