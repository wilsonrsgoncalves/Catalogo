using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace APICatalogo.Logging;

public class CustomLoggerProvider(IOptions<CustomLoggerOptions> options) : ILoggerProvider
{
    private readonly CustomLoggerOptions _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    private readonly Dictionary<string, CustomerLogger> _loggers = new();

    public ILogger CreateLogger(string categoryName)
    {
        lock (_loggers)
        {
            if (!_loggers.TryGetValue(categoryName, out var logger))
            {
                logger = new CustomerLogger(categoryName, Options.Create(_options));
                _loggers[categoryName] = logger;
            }
            return logger;
        }
    }

    public void Dispose()
    {
        _loggers.Clear();
    }
}