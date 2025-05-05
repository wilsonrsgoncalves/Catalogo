using APICatalogo.Context;
using APICatalogo.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace APICatalogo.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configurar controllers e opções de serialização JSON
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ApiExceptionFilter));
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        // Configurar Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Validar e configurar o DbContext
        string? sqlConnection = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(sqlConnection))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' não foi definida no appsettings.json.");
        }

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(sqlConnection));

        // Configurar filtros
        services.AddScoped<ApiLoggingFilter>();

        return services;
    }
}