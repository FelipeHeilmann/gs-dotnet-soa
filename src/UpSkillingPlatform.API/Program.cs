using Microsoft.EntityFrameworkCore;
using UpSkillingPlatform.API.Middleware;
using UpSkillingPlatform.Infrastructure;
using UpSkillingPlatform.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "UpSkilling Platform API",
        Version = "v1",
        Description = "API RESTful para plataforma de Upskilling/Reskilling - Futuro do Trabalho 2030+",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "FIAP - 3ESPY",
            Email = "contato@fiap.com.br"
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        dbContext.Database.Migrate();
        Console.WriteLine("✅ Database migrated successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error migrating database: {ex.Message}");
    }
}

app.Run();
