using APICatalogo.Context;
using APICatalogo.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
      .AddJsonOptions(options =>
         options.JsonSerializerOptions
            .ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? sqlConnection = builder.Configuration.GetConnectionString("DefaultConnection"); 

if (string.IsNullOrEmpty(sqlConnection))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' is not defined.");
}

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(sqlConnection));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();