
using APICatalogo.Extensions;
using APICatalogo.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configurar serviços
builder.Services.AddApplicationServices(builder.Configuration);

// Configurar logging
builder.Services.ConfigureLogging(builder.Configuration);

// Configurar middleware e pipeline
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