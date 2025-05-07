using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace APICatalogo.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilter>();
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            string? sqlConnection = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(sqlConnection))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' não foi definida no appsettings.json.");
            }

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(sqlConnection));

            services.AddScoped<ApiLoggingFilter>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}