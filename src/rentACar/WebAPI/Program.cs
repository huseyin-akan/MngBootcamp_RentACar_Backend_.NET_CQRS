using Application;
using Core.Application.Extensions;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using Persistence;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//IoC Container Extension Metotları:
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

//Mail Servis IoC:
builder.Services.AddSingleton<IMailService, MailKitMailService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injection Cycle'ı önlemek için.
builder.Services.AddLazyResolution();

//Database'den gelen verinin birbirini çağırma döngüsüne girmesini engelliyor.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
