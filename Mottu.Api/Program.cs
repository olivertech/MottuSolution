//Configurações para uso do Minio
using Microsoft.Extensions.Configuration.Json;
using System.Configuration;

var endpoint = "play.min.io";
var accessKey = "Q3AM3UQ867SPQQA43P2F";
var secretKey = "zuf+tfteSlswRu7BJ86wtrueekitnifILbZam1KYY3TG";

//Referenciando o appsettings
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

// Add services to the container.
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mottu.Api", Version = "v1" });
    })
    .AddAutoMapper(typeof(Program))
    .AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Add Injection Dependencies
builder.Services.AddDependenciesInjection(builder.Configuration);

// MassTransit configuration to use RabbitMQ Broker
builder.Services.AddMassTransitServices();

// Add Minio using the default endpoint
builder.Services.AddMinio(accessKey, secretKey);

// Add Minio using the custom endpoint and configure additional settings for default MinioClient initialization
builder.Services.AddMinio(configureClient => configureClient
    .WithEndpoint(endpoint)
    .WithCredentials(accessKey, secretKey));

// Add Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    //options.InstanceName = "redis";
    options.Configuration = config.GetValue<string>("CacheSettings:ConnectionString");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
