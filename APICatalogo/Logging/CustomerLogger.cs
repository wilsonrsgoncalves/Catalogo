using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;

namespace APICatalogo.Logging;

public sealed class CustomerLogger(string name, IOptions<CustomLoggerOptions> options) : ILogger
{
    private readonly string _loggerName = name ?? throw new ArgumentNullException(nameof(name));
    private readonly CustomLoggerOptions _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    private static readonly object _lockObject = new object();

    public static object LockObject => _lockObject;

    // Implementação explícita para alinhar nullability
    IDisposable ILogger.BeginScope<TState>(TState state) => NullScope.Instance;

    private class NullScope : IDisposable
    {
        public static readonly NullScope Instance = new NullScope();

        private NullScope() { }

        public void Dispose() { }
    }

    public bool IsEnabled(LogLevel logLevel) => logLevel >= _options.LogLevel;

    void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel) || formatter == null)
        {
            return;
        }

        string message = $"{logLevel}: {eventId.Id} - {formatter(state, exception)}";
        WriteToFile(message);
    }

    private void WriteToFile(string message)
    {
        try
        {
            lock (LockObject)
            {
                string directory = Path.GetDirectoryName(_options.LogFilePath) ?? throw new DirectoryNotFoundException("Diretório de log inválido.");
                Directory.CreateDirectory(directory);

                using (StreamWriter streamWriter = new StreamWriter(_options.LogFilePath, append: true))
                {
                    streamWriter.WriteLine($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {_loggerName} - {message}");
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Erro de I/O ao escrever no log: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro inesperado ao escrever no log: {ex.Message}");
        }
    }
}