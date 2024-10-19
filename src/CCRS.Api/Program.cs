using CCRS.Api.Configuration;
using CCRS.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityConfiguration(builder.Configuration);

//builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddControllers();

//builder.Services.AddEndpointsApiExplorer();



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.WebApiConfig();

builder.Services.AddSwaggerGen();

builder.Services.AddLoggingConfig(builder.Configuration);


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;

});

builder.Services.AddSwaggerConfig();



builder.Services.ResolveDependencies(builder.Configuration);



var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseApiConfig(app.Environment);

app.UseSwaggerConfig(apiVersionDescriptionProvider);

app.UseLoggingConfiguration();

// nao utilizado:
app.MapControllers();

app.Run();
