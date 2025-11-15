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
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var maxRetries = 10;
    var delay = TimeSpan.FromSeconds(3);
    
    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            Console.WriteLine($"🔄 Attempting to connect to database... (Attempt {i + 1}/{maxRetries})");
            dbContext.Database.Migrate();
            Console.WriteLine("✅ Database migrated successfully!");
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Error connecting to database: {ex.Message}");
            
            if (i == maxRetries - 1)
            {
                Console.WriteLine("❌ Failed to connect to database after maximum retries.");
                throw;
            }
            
            Console.WriteLine($"⏳ Waiting {delay.TotalSeconds} seconds before retry...");
            await Task.Delay(delay);
        }
    }
}

app.Run();
