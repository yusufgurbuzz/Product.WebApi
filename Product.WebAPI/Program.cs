
using NLog;
using Product.Interfaces;
using Product.Services;
using ProductWebApi;
using ProductWebApi.Extensions;
using ProductWebApi.Mapper;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),"/nlog.config")); //nlog configrasyonu
// Add services to the container.

builder.Services.AddControllers(config => //içerik pazarlığı (xml/csv/json)
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();// veriyi sadece json formatında değil xml formatında da alabiliriz.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigurePostgreSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger); //exception tanımlaması

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction())
{
    app.UseHsts();
}

 static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<IApiMarker>();
            webBuilder.UseUrls("http://127.0.0.1:5001"); // Portu değiştirin
        });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

