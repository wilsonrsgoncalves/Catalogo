using Microsoft.Extensions.Logging;

namespace APICatalogo.Logging;

public class CustomLoggerOptions
{
    public LogLevel LogLevel { get; set; } = LogLevel.Information;
    public string LogFilePath { get; set; } = @"c:\dados\logs\ApiCatalogo_Log.txt";
}