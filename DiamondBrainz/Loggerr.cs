using System;

using Microsoft.Extensions.Logging;

using Victoria.Node;

namespace Diamond.API;
public class Loggerr : ILogger<LavaNode>
{
	public IDisposable BeginScope<TState>(TState state) where TState : notnull
	{
		return null;
	}

	public bool IsEnabled(LogLevel logLevel)
	{
		return true;
	}

	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
	{

	}
}
