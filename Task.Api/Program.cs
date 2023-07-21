using Microsoft.EntityFrameworkCore;
using Task.Api.Data;

var builder = WebApplication.CreateBuilder(args);
//Postgres-Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionPostgres") ?? string.Empty));

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

#pragma warning disable ASP0014
app.UseEndpoints(endpoints => { endpoints?.MapControllers(); });
#pragma warning restore ASP0014

app.Run();