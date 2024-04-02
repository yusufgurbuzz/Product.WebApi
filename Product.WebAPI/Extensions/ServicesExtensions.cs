using Microsoft.EntityFrameworkCore;
using Product.Entity.LogModel;
using Product.Interfaces;
using Product.Repositories;
using Product.Services;
using ProductWebApi.ActionFilters;
using StackExchange.Redis;

namespace ProductWebApi.Extensions;

public static class ServicesExtensions
{
    public static void ConfigurePostgreSqlContext( this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager,RepositoryManager>();
    }
    public static void ConfigureServiceManager(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager,ServiceManager>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));
    }

    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerService, LoggerManager>();
    }
    public static void ConfigureActionFilters(this IServiceCollection services)
    {
        services.AddScoped<ValidationFilterAttribute>();
        services.AddSingleton<LogFilterAttribute>();
    }


}