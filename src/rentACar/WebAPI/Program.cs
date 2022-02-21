using Application;
using Core.Application.Extensions;
using Core.Application.Pipelines.Caching;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Core.Security.Encryption;
using TokenOptions = Core.Security.Jwt.TokenOptions;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder => builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod() );
});

builder.Services.AddControllers();

//IoC Container Extension Metotları:
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

//For Identity:
//builder.Services.AddIdentity<User, OperationClaim>
//    .AddEntityFrameworkStores<>()
//    .AddDefaultTokenProviders();

//appsettings.json dosyasından TokenOptions kısmını okuyup TokenOptions objeme map et ve değişkene ata.
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //Token geldiğinde hangi kontroller yapılsın ayarları:
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,          //Issuer'ı biz www.engin.com yapmıştık, o şekilde gelsin. Gelmezse hata verir.
                        ValidateAudience = true,        //Audience'ı biz www.engin.com yapmıştık, o şekilde gelsin. Gelmezse hata verir.
                        ValidateLifetime = true,        //sistemde hep login olabilsin istiyorsak burayı false yap.
                        ValidIssuer = tokenOptions.Issuer,      //appsetting.json'daki Issuer'ı verdik. Yani www.engin.com
                        ValidAudience = tokenOptions.Audience,  //appsetting.json'daki Audience'ı verdik. Yani www.engin.com
                        ValidateIssuerSigningKey = true,        // signing keyi de validate edecek.
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)    //appsettings.jsondan alıyor yine.
                    };
                });

builder.Services.AddAuthorization();

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


//Database'den gelen verinin birbirini çağırma döngüsüne girmesini engelliyor.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
