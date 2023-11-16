using AutoMapper;
using Draft.Product.WebAPI.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Product.Entity;
using Product.Interfaces;
using Product.Repositories;
using Product.Services;
using StackExchange.Redis;

namespace Draft.Product.WebAPI.Extensions;

public static class Configure
{
    public static void ConfigurePostgreSqlContext( this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }
    public static void ConfigureService(this IServiceCollection services)
    {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IRepositoryManager,RepositoryManager>();
            services.AddAutoMapper(typeof(AutoMapperProfiles));  
            services.AddScoped<ICacheService, CacheService>();
            
    }
    public static void ConfigureRedis( this IServiceCollection services, IConfiguration configuration)
    {
        
        string redisConnectionString = configuration.GetConnectionString("Redis");
        
        ConnectionMultiplexer redisConnection = ConnectionMultiplexer.Connect(redisConnectionString);

        services.AddSingleton<IConnectionMultiplexer>(redisConnection);
    }
}