using Application;
using Core.Application.Extensions;
using Core.Application.Pipelines.Caching;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//IoC Container Extension Metotları:
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//Caching için in memory kullanmamızı sağlar.
builder.Services.AddDistributedMemoryCache();

//Caching için Reddis ile çalışmak istersen: 
//builder.Services.AddStackExchangeRedisCache(options => options.Configuration= "localhost:6379");
builder.Services.AddScoped<CacheSettings>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injection Cycle'ı önlemek için.
builder.Services.AddLazyResolution();

builder.Services.AddLogging(config =>
{
   config.AddDebug();
   config.AddConsole();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("http://localhost:3000"));
});

//Database'den gelen verinin birbirini çağırma döngüsüne girmesini engelliyor.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

var app = builder.Build();

//ILogger logger = app.Logger;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.ConfigureCustomExceptionMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();
