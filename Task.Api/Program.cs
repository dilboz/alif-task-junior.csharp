using System.Reflection;
using Api.Task.Services;
using Microsoft.EntityFrameworkCore;
using Task.Api.Data;

var builder = WebApplication.CreateBuilder(args);

//Postgres-Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionPostgres") ?? string.Empty));

builder.Services.AddControllers();
builder.Services.AddScoped<CategoryService>();

builder.Services.AddSwaggerGen(config =>
{
    var xmlFile = $"{Assembly.GetEntryAssembly()!.GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.RoutePrefix = string.Empty;
    config.SwaggerEndpoint("swagger/v1/swagger.json", "Swagger Task-Junior-CSharp-Alif Api");
});

#pragma warning disable ASP0014
app.UseEndpoints(endpoints => { endpoints?.MapControllers(); });
#pragma warning restore ASP0014


app.Run();