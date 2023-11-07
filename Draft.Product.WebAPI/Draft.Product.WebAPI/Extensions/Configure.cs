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
        
            services.AddScoped<IProductMaterialRecordService,ProductMaterialRecordService>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IRepositoryManager,RepositoryManager>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));
            services.AddAutoMapper(typeof(AutoMapperProfiles));
    }
}