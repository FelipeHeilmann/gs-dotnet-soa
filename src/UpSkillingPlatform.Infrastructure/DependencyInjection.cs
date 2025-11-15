using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UpSkillingPlatform.Application.Services;
using UpSkillingPlatform.Domain.Interfaces;
using UpSkillingPlatform.Infrastructure.Data;
using UpSkillingPlatform.Infrastructure.Repositories;

namespace UpSkillingPlatform.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<ITrilhaRepository, TrilhaRepository>();

        services.AddScoped<UsuarioService>();
        services.AddScoped<TrilhaService>();

        return services;
    }
}
