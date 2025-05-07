using APICatalogo.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace APICatalogo.Extensions;

public static class LoggingExtensions
{
    public static IServiceCollection ConfigureLogging(this IServiceCollection services, IConfiguration configuration)
    {
        // Configurar as opções do logger a partir do appsettings.json
        services.Configure<CustomLoggerOptions>(
            configuration.GetSection("Logging:CustomLogger"));

        // Adicionar o provedor de logging personalizado
        services.AddLogging(logging =>
        {
            logging.ClearProviders(); // Opcional: remover provedores padrão
            logging.AddProvider(new CustomLoggerProvider(
                services.BuildServiceProvider().GetRequiredService<IOptions<CustomLoggerOptions>>()));
        });

        return services;
    }
}